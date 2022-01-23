using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using System.Timers;
using System.IO;
using Windows.Devices.Bluetooth;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SensorScanner
{
    struct BleDeviceData
    {
        public int index;
        public string address;
        public string serial_id;
    }


    public partial class FrmScanner : Form
    {
        private const string WEAR_DEV = "WearDev";
        private const string HOME_DEVICE = "HomeDevice";

        private string strMessage;

        Dictionary<char, Dictionary<char, List<BleDeviceData>>> fileData;        

        private List<string> addressList; // read from file. without semicolon.
        private List<string> tempAddressList; //
        private BindingList<string> logList;
        private Action<string, int, bool> logAddAct;
        private Action enableScanAct;
        private Action<string> setTextAct;
        private Action<string> newDeviceAct;

        private Action<ulong> sendLedAct;
        private Action<ulong> shutdownAct;

        private BluetoothLEAdvertisementWatcher watcher;
        private bool isScaning = false;
        private int scanDeviceType = 0;        
        private int _row;        
        DateTime startTime;

        private System.Timers.Timer aTimer;

        private FileStream fs;        

        public FrmScanner()
        {
            InitializeComponent();

            logList = new BindingList<string>();
            lbxLog.DataSource = logList;
            logAddAct += logAdd;
            newDeviceAct += addNewDevice;
            enableScanAct += enableScan;
            setTextAct += setTextBtn;
            sendLedAct += sendLed;
            shutdownAct += shutdown;
            _row = 1;

            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += OnTimedEvent;
            watcher = new BluetoothLEAdvertisementWatcher();
            watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(100);
            watcher.ScanningMode = BluetoothLEScanningMode.Active;
            watcher.Received += Watcher_Received;

            this.yearCombo.SelectedIndex = 0;
            this.monthCombo.SelectedIndex = 0;

            this.tableLayout.Controls.Add(new Label() { Text = "BDアドレス", TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(150, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 0, 0);
            this.tableLayout.Controls.Add(new Label() { Text = "シリアルID", TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(150, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 1, 0);            
        }

        private bool fileOpen(int scanDeviceType)
        {
            string file_name = scanDeviceType<2 ? WEAR_DEV+".csv" : HOME_DEVICE+".csv";
            
            char file_type = scanDeviceType < 2 ? 'c' : 'h';
            try
            {
                if (File.Exists(file_name))
                {
                    var reader = new StreamReader(file_name);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var items = line.Split(',');
                        if (items.Length < 2) continue;
                        
                        var id_str = items[0];
                        var device_address = items[1];

                        if (id_str[0] != file_type ) continue;
                        char year = id_str[1];
                        char month = id_str[2];

                        if (!fileData.ContainsKey(year))
                        {
                            fileData[year] = new Dictionary<char, List<BleDeviceData>>();
                        }
                        if (!fileData[year].ContainsKey(month))
                        {
                            fileData[year][month] = new List<BleDeviceData>();
                        }
                        BleDeviceData item = new BleDeviceData();
                        item.index = getIndex(items[0]);
                        item.address = items[1]; //with semicolon
                        item.serial_id = items[2]; //serial id
                        
                        fileData[year][month].Add(item);
                        addressList.Add(device_address.Replace(":", "")); //without semicolon
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                MyMessageBox msgBox = new MyMessageBox("「 " + file_name + " 」ファイルを読めません。\nファイルを閉じてください。");
                msgBox.Text = "エラー";
                msgBox.ShowDialog();
                return false;
            }            
            return true;
        }

        private void showSavedMsg(string addr)
        {
            string file_name = scanDeviceType<2 ? WEAR_DEV+".csv" : HOME_DEVICE+".csv";
            logAddEvent("Saved to "+file_name, 1);
            MyMessageBox msgBox = new MyMessageBox("「" + addressLast6(addr) + "」の通し番号は「" + strMessage + "」です");
            msgBox.ShowDialog();
            
        }

        private void initTable()
        {
            this.tableLayout.Controls.Clear();

            _row = 1;
            this.tableLayout.RowCount = 1;
            this.tableLayout.ColumnCount = 4;

            this.tableLayout.ResumeLayout();
            this.tableLayout.AutoScroll = false;
            this.tableLayout.AutoScroll = true;

            this.tableLayout.Controls.Add(new Label() { Text = "BDアドレス", TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(300, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 0, 0);
            this.tableLayout.Controls.Add(new Label() { Text = "シリアルID", TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(300, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 1, 0);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (startTime == null)
            {
                startTime = e.SignalTime;
            }
            setTextEvent("スキャン中... " + (10 - (e.SignalTime - startTime).Seconds).ToString() + "秒");
        }

        private void logAddEvent(string item, int fileKind, bool savefile = false)
        {
            this.Invoke(logAddAct, item, fileKind, savefile);
        }

        private void findNewDeviceEvent(string addr)
        {
            this.Invoke(newDeviceAct, addr);
        }

        private void EnableScanEvent()
        {
            this.Invoke(enableScanAct);
        }
        private void setTextEvent(string text)
        {
            this.Invoke(setTextAct, text);
            
        }
        private void setTextBtn(string text)
        {
            btnScan.Text = text;
        }
        private void enableScan()
        {
            btnScan.Enabled = true;
            btnScan.Text = "スキャン";
        }
        private void disableScan()
        {
            btnScan.Enabled = false;
            btnScan.Text = "スキャン中... 10秒";
        }
        //item:  without semi-colon.
        private void logAdd(string item, int fileKind, bool savefile = false)
        {
            if (savefile)
            {
                string strYear = this.yearCombo.Text;
                string strMonth = this.monthCombo.Text;
                char year = strYear[0];
                char month = strMonth[0];
                if (strMonth == "10")
                    month = 'A';
                if (strMonth == "11")
                    month = 'B';
                if (strMonth == "12")
                    month = 'C';

                string file_name = fileKind == 1 ? WEAR_DEV : HOME_DEVICE;                

                fs = new FileStream(file_name + ".csv", FileMode.Create, FileAccess.Write);                

                var header = Encoding.GetEncoding("shift-jis").GetBytes("通し番号" + "," + "BDアドレス" + "," + "シリアルID" + "\n");
                fs.Write(header, 0, header.Length);

                if ( !fileData.ContainsKey(year) ){
                    fileData[year] = new Dictionary<char, List<BleDeviceData>>();
                }
                if (!fileData[year].ContainsKey(month))
                {
                    fileData[year][month] = new List<BleDeviceData>();
                }
                
                BleDeviceData deviceData = new BleDeviceData();
                deviceData.index = 0;
                deviceData.address = addressWithColon(item);
                deviceData.serial_id = addressLast6(item);

                if (fileData[year][month].Count == 0)
                {
                    deviceData.index = 1;
                }
                else
                {
                    BleDeviceData last = fileData[year][month].Last<BleDeviceData>();
                    deviceData.index = last.index + 1;
                }
                strMessage = generateId(deviceData.index, fileKind == 1, year, month);

                fileData[year][month].Add(deviceData);
                
                foreach ( KeyValuePair<char,Dictionary<char, List<BleDeviceData>>> yearDataList  in fileData)
                {
                    char year_index = yearDataList.Key;
                    foreach(KeyValuePair<char, List<BleDeviceData>> monthDataList in yearDataList.Value)
                    {
                        char month_index = monthDataList.Key;
                        List<BleDeviceData> deviceDataList = monthDataList.Value;                        
                        for ( int i = 0; i< deviceDataList.Count; i++)
                        {                            
                            var data = Encoding.GetEncoding("shift-jis").GetBytes(generateId(deviceDataList[i].index, fileKind == 1, year_index, month_index) 
                                + "," + deviceDataList[i].address + "," 
                                + deviceDataList[i].serial_id + "\n");
                            fs.Write(data, 0, data.Length);                                
                        }
                    }
                }                
                fs.Close();                
            }
            else
            {
                logList.Add(item);
            }
            lbxLog.TopIndex = lbxLog.Items.Count - 1; //scroll down
        }

        private void addNewDevice(string address)
        {
           this.tableLayout.RowCount += 1;

            this.tableLayout.Controls.Add(new Label() { Text = addressWithColon(address), TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(300, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 0, _row);
            this.tableLayout.Controls.Add(new Label() { Text = addressLast6(address), TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(200, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 1, _row);
            
            if (scanDeviceType < 2) // 0, 1
            {
                Button ledBtn = new Button();
                ledBtn.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ledBtn.Size = new System.Drawing.Size(105, 35);
                ledBtn.Text = "LED点滅";
                ledBtn.TextAlign = ContentAlignment.MiddleCenter;
                ledBtn.Name = "LedBtn_" + _row;
                ledBtn.Click += new EventHandler(LedBtn_Click);
                this.tableLayout.Controls.Add(ledBtn, 2, _row);
            }
            
            if (scanDeviceType % 2 == 0) // 0 or 2
            {
                Button saveBtn = new Button();
                saveBtn.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                saveBtn.Size = new System.Drawing.Size(105, 35);
                saveBtn.Text = "記録";
                saveBtn.TextAlign = ContentAlignment.MiddleCenter;
                saveBtn.Name = "SaveBtn_" + _row;
                saveBtn.Click += new EventHandler(SaveBtn_Click);
                this.tableLayout.Controls.Add(saveBtn, 3, _row);
            }
            if (scanDeviceType == 1)
            {
                Button shoutdownBtn = new Button();
                shoutdownBtn.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                shoutdownBtn.Size = new System.Drawing.Size(150, 35);
                shoutdownBtn.Text = "シャットダウン";
                shoutdownBtn.TextAlign = ContentAlignment.MiddleCenter;
                shoutdownBtn.Name = "ShutdownBtn_" + _row;
                shoutdownBtn.Click += new EventHandler(ShutdownBtn_Click);
                this.tableLayout.Controls.Add(shoutdownBtn, 2, _row);
            }
            _row++;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (this.radioCore.Checked)            
                scanDeviceType = 0;  //WearDev
            else if (this.radioCore1.Checked)
                scanDeviceType = 1;  //WearDev completed
            else if (this.radioHome.Checked)
                scanDeviceType = 2;  //HomeDevice
            else if (this.radioHome1.Checked)
                scanDeviceType = 3;  //HomeDevice completed
            else
                return;
            
            initTable();

            if (!isScaning)
            {
                if (fs != null)
                    fs.Close();                

                fileData = new Dictionary<char, Dictionary<char, List<BleDeviceData>>>();                

                addressList = new List<string>();
                tempAddressList = new List<string>();

                if (!fileOpen(scanDeviceType))
                {
                    return;
                }
                isScaning = true;
                startTime = DateTime.Now;
                aTimer.Start();
                disableScan();
                var task = Task.Run(new Action(watcherRun));
            }
        }

        private void watcherRun()
        {
            logAddEvent("Scan Start", 1);
            watcher.Start();
            Thread.Sleep(10000);  // 10s
            watcher.Stop();
            logAddEvent("Scan Ended", 1);
            aTimer.Stop();
            isScaning = false;
            EnableScanEvent();
        }


        public void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            var addr = args.BluetoothAddress;
            string sAddr = addr.ToString("X").ToUpper();
            string file_name = scanDeviceType<2 ? WEAR_DEV : HOME_DEVICE;
            
            if (args.Advertisement.LocalName != file_name) return;
            
            if (scanDeviceType % 2 == 0)
            {
                if (addressList.Contains(sAddr))
                {
                    if (!tempAddressList.Contains(sAddr))
                    {
                        logAddEvent("Searched Device: " + file_name + ":" + sAddr + " (already saved)", 1);
                        tempAddressList.Add(sAddr);
                    }
                }
                else
                {
                    findNewDeviceEvent(sAddr);
                    addressList.Add(sAddr);
                    tempAddressList.Add(sAddr);
                    logAddEvent(sAddr, 1);
                }
            }
            else
            {
                if (!tempAddressList.Contains(sAddr))
                {
                    findNewDeviceEvent(sAddr);
                    tempAddressList.Add(sAddr);
                }
            }
            
        }
        private void LedBtn_Click(object sender, EventArgs e)
        {
            Button ledBtn = sender as Button;
            var name = ledBtn.Name;
            var list = name.Split('_');
            var rowIndex = Int32.Parse(list[1]);
            var addrControl = this.tableLayout.GetControlFromPosition(0, rowIndex);
            string bluetoothAddr = addrControl.Text.Replace(":", "");
            ulong device_addr = Convert.ToUInt64(bluetoothAddr, 16);

            this.Invoke(sendLedAct, device_addr);

        }
        private async void sendLed(ulong addr)
        {
            BluetoothLEDevice device = await BluetoothLEDevice.FromBluetoothAddressAsync(addr);
            Guid service_LED = new Guid("1074c00d-8a96-fe1e-c5a5-a27d11f5c777");
            Guid characteristic_uuid_LED_DATA = new Guid("1074ac01-8a96-fe1e-c5a5-a27d11f5c777");

            var sRet = await device.GetGattServicesAsync();
            ReadOnlyCollection<GattDeviceService> serviceList = sRet.Services.ToList().AsReadOnly();
            if (serviceList.Count == 0) return;
            var service = serviceList.First(s => s.Uuid == service_LED);
            if (service == null) return;

            var cRet = await service.GetCharacteristicsAsync();
            ReadOnlyCollection<GattCharacteristic> characteristicList = cRet.Characteristics.ToList().AsReadOnly();
            if (characteristicList.Count == 0) return;
            var characteristic = characteristicList.First(c => c.Uuid == characteristic_uuid_LED_DATA);
            byte[] tx = { 0x00, 0x00 };
            await characteristic.WriteValueAsync(tx.AsBuffer(), GattWriteOption.WriteWithoutResponse);
            byte[] tx1 = { 0x10, 0x00 };
            await characteristic.WriteValueAsync(tx1.AsBuffer(), GattWriteOption.WriteWithoutResponse);
            byte[] tx2 = { 0x20, 0x00 };
            await characteristic.WriteValueAsync(tx2.AsBuffer(), GattWriteOption.WriteWithoutResponse);
            //await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.None);
            characteristic.Service.Dispose();
        }
        private void ShutdownBtn_Click(object sender, EventArgs e)
        {
            Button shutdownBtn = sender as Button;
            var name = shutdownBtn.Name;
            var list = name.Split('_');
            var rowIndex = Int32.Parse(list[1]);
            var addrControl = this.tableLayout.GetControlFromPosition(0, rowIndex);
            string bluetoothAddr = addrControl.Text.Replace(":", "");
            ulong device_addr = Convert.ToUInt64(bluetoothAddr, 16);
            this.Invoke(shutdownAct, device_addr);
        }
        private async void shutdown(ulong addr)
        {
            BluetoothLEDevice device = await BluetoothLEDevice.FromBluetoothAddressAsync(addr);
            Guid service_LED = new Guid("1074c00d-8a96-fe1e-c5a5-a27d11f5c777");
            Guid characteristic_uuid_LED_DATA = new Guid("1074ac01-8a96-fe1e-c5a5-a27d11f5c777");

            var sRet = await device.GetGattServicesAsync();
            ReadOnlyCollection<GattDeviceService> serviceList = sRet.Services.ToList().AsReadOnly();
            if (serviceList.Count == 0) return;
            var service = serviceList.First(s => s.Uuid == service_LED);
            if (service == null) return;
            var cRet = await service.GetCharacteristicsAsync();
            ReadOnlyCollection<GattCharacteristic> characteristicList = cRet.Characteristics.ToList().AsReadOnly();
            if (characteristicList.Count == 0) return;
            var characteristic = characteristicList.First(c => c.Uuid == characteristic_uuid_LED_DATA);
            byte[] tx = { 0x00, 0x00 };
            await characteristic.WriteValueAsync(tx.AsBuffer(), GattWriteOption.WriteWithoutResponse);
            byte[] tx1 = { 0xF0, 0x00 };
            await characteristic.WriteValueAsync(tx1.AsBuffer(), GattWriteOption.WriteWithoutResponse);            
            //await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.None);
            characteristic.Service.Dispose();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Button saveBtn = sender as Button;
            var name = saveBtn.Name;

            var list = name.Split('_');
            var rowIndex = Int32.Parse(list[1]);

            var addrControl = this.tableLayout.GetControlFromPosition(0, rowIndex);

            string bluetoothAddr = addrControl.Text.Replace(":", "");
            
            logAddEvent(bluetoothAddr, scanDeviceType == 0?1:2, true);

            showSavedMsg(bluetoothAddr);

            for (int i = 0; i < this.tableLayout.ColumnCount; i++)
            {
                var control = this.tableLayout.GetControlFromPosition(i, rowIndex);
                this.tableLayout.Controls.Remove(control);
            }

            this.tableLayout.RowCount -= 1;

            this.tableLayout.ResumeLayout();
            this.tableLayout.AutoScroll = false;
            this.tableLayout.AutoScroll = true;
        }

        private string addressWithColon(string addr)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < addr.Length; i++)
            {
                if (i > 0 && i % 2 == 0)
                {
                    stringBuilder.Append(':');
                }
                stringBuilder.Append(addr[i]);
            }
            return stringBuilder.ToString().ToUpper();
        }
        private string addressLast6(string addr)
        {
            if (addr.Length > 6)
            {
                return addr.Substring(addr.Length - 6).ToUpper();
            }
            return "";
        }

        private string addZero(int num)
        {
            string str = num.ToString();
            while (str.Length < 4)
            {
                str = str.Insert(0, "0");
            }
            return str;
        }

        private string generateId(int index, bool is_wear, char year, char month)
        {
            string ret = "";
            ret += is_wear ? 'c' : 'h';
            ret += year;
            ret += month;
            ret += addZero(index);
            return ret;
        }
        private int getIndex(string serial_id)
        {
            if (serial_id.Length != 7) return 0;
            return Int32.Parse(serial_id.Substring(3));
        }
    }
}

