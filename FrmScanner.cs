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
using System.Timers;
using System.IO;

namespace SensorScanner
{
    public partial class FrmScanner : Form
    {
		private string deviceName_WearDev = "WearDev";
        private string deviceName_HomeDevice = "HomeDevice";

        private List<string> wearDevList;
        private List<string> homeDeviceList;

        private List<string> wearDevList1;
        private List<string> homeDeviceList1;

        private BindingList<string> logList;
        private Action<string, int, bool> logAddAct;
        private Action enableScanAct;
        private Action<string> setTextAct;
        private Action<string> newDeviceAct;

        private BluetoothLEAdvertisementWatcher watcher;
        private bool isScaning = false;
        private bool isCheckedCore = true;
        private int _row;
        private int _wearDevNum, _homeDevNum;
        DateTime startTime;

        private System.Timers.Timer aTimer;

        private FileStream fs_wearDev;
		private FileStream fs_homeDevice;


		public FrmScanner()
        {
            InitializeComponent();

            logList = new BindingList<string>();
            lbxLog.DataSource = logList;
            logAddAct += logAdd;
            newDeviceAct += addNewDevice;
            enableScanAct += enableScan;
            setTextAct += setTextBtn;
            _row = 1;
            _wearDevNum = 0;
            _homeDevNum = 0;

            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += OnTimedEvent;
            watcher = new BluetoothLEAdvertisementWatcher();
            watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(100);
            watcher.ScanningMode = BluetoothLEScanningMode.Active;
            watcher.Received += Watcher_Received;

            this.tableLayout.Controls.Add(new Label() { Text = "BDアドレス", TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(300, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 0, 0);
            this.tableLayout.Controls.Add(new Label() { Text = "シリアルID", TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(300, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 1, 0);
		}
		
		private bool fileOpen()
        {
            try
            {
                if (File.Exists("WearDev.csv"))
                {
                    var reader = new StreamReader("WearDev.csv");
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line != "")
                        {
                            var items = line.Split(',');
                            if (items.Length > 0)
                            {
                                var item1 = items[1];
                                wearDevList.Add(item1.Replace(":", ""));
                            }
                        }
                    }
                    reader.Close();
                }
                if (wearDevList.Count() > 0)
                    _wearDevNum = wearDevList.Count() - 1;
                else
                    _wearDevNum = wearDevList.Count();

            }
            catch (Exception e)
            {
                MyMessageBox msgBox = new MyMessageBox("「 WearDev.csv」ファイルを生成できません。\nファイルを閉じてください。");
                msgBox.Text = "エラー";
                msgBox.ShowDialog();
                return false;
            }
            try
            {
                if (File.Exists("HomeDevice.csv"))
                {
                    var reader = new StreamReader("HomeDevice.csv");
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line != "")
                        {
                            var items = line.Split(',');
                            if (items.Length > 0)
                            {
                                var item1 = items[1];
                                homeDeviceList.Add(item1.Replace(":", ""));
                            }
                        }
                    }
                    reader.Close();
                }
                if (homeDeviceList.Count() > 0)
                    _homeDevNum = homeDeviceList.Count() - 1;
                else
                    _homeDevNum = homeDeviceList.Count();

            }
            catch (Exception e)
            {
                fs_wearDev.Close();
                MyMessageBox msgBox = new MyMessageBox("「 HomeDevice.csv」ファイルを生成できません。\nファイルを閉じてください。");
                msgBox.Text = "エラー";
                msgBox.ShowDialog();
                return false;
            }
            return true;
        }

        private void saveAddr(string addr)
        {
            if(isCheckedCore)
            {
                logAddEvent("Saved to WearDev.csv", 1);

                MyMessageBox msgBox = new MyMessageBox("「" + addressLast6(addr) + "」の通し番号は「" + _wearDevNum + "」です");
                msgBox.ShowDialog();
            }
            else
            {
                logAddEvent("Saved to HomeDevice.csv", 1);

                MyMessageBox msgBox = new MyMessageBox("「" + addressLast6(addr) + "」の通し番号は「" + _homeDevNum + "」です");
                msgBox.ShowDialog();
            }
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
            if(startTime == null)
            {
                startTime = e.SignalTime;
            }
            setTextEvent("スキャン中... " + (10 - (e.SignalTime - startTime).Seconds).ToString()+ "秒");
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
            try { 
                this.Invoke(setTextAct, text);
            }catch(Exception e)
            {
            }
        }
        private async void setTextBtn(string text)
        {
            btnScan.Text = text;
        }
        private async void enableScan()
        {
            btnScan.Enabled = true;
            btnScan.Text = "スキャン";
        }
        private void disableScan()
        {
            btnScan.Enabled = false;
            btnScan.Text = "スキャン中... 10秒";
        }
        
        private async void logAdd(string item, int fileKind, bool savefile = false)
        {
            if (savefile)
            {
                if (fileKind == 1)
                {
                    fs_wearDev = new FileStream("WearDev.csv", FileMode.Append, FileAccess.Write);

                    if (_wearDevNum == 0)
                    {
                        var header = Encoding.GetEncoding("shift_jis").GetBytes("通し番号" + "," + "BDアドレス" + "," + "シリアルID" + "\n");
                        fs_wearDev.Write(header, 0, header.Length);
                    }
                    _wearDevNum++;
                    var data = Encoding.GetEncoding("shift_jis").GetBytes(_wearDevNum + "," + addressWithColon(item) + "," + addressLast6(item) + "\n");

                    fs_wearDev.Write(data, 0, data.Length);
                    logList.Add("Searched Device: WearDevice " + ":" + item);

                    fs_wearDev.Close();
                }
                else if (fileKind == 2)
                {
                    fs_homeDevice = new FileStream("HomeDevice.csv", FileMode.Append, FileAccess.Write);
                    if (_homeDevNum == 0)
                    {
                        var header = Encoding.GetEncoding("shift_jis").GetBytes("通し番号" + "," + "BDアドレス" + "," + "シリアルID" + "\n");
                        fs_homeDevice.Write(header, 0, header.Length);
                    }
                    _homeDevNum++;
                    var data = Encoding.GetEncoding("shift_jis").GetBytes(_homeDevNum + "," + addressWithColon(item) + "," + addressLast6(item) + "\n");

                    fs_homeDevice.Write(data, 0, data.Length);
                    logList.Add("Searched Device: HomeDevice " + ":" + item);

                    fs_homeDevice.Close();
                }
            }
            else
            {
                logList.Add(item);
            }
            lbxLog.TopIndex = lbxLog.Items.Count - 1;
        }

        private async void addNewDevice(string address)
        {
            Button ledBtn = new Button();
            ledBtn.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ledBtn.Size = new System.Drawing.Size(105, 35);
            ledBtn.Text = "LED点滅";
            ledBtn.TextAlign = ContentAlignment.MiddleCenter;

            Button saveBtn = new Button();
            saveBtn.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            saveBtn.Size = new System.Drawing.Size(105, 35);
            saveBtn.Text = "記録";
            saveBtn.TextAlign = ContentAlignment.MiddleCenter;

            this.tableLayout.RowCount += 1;

            this.tableLayout.Controls.Add(new Label() { Text = addressWithColon(address), TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(300, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 0, _row);
            this.tableLayout.Controls.Add(new Label() { Text = addressLast6(address), TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), Size = new System.Drawing.Size(200, 30), Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) }, 1, _row);
            this.tableLayout.Controls.Add(ledBtn, 2, _row);
            this.tableLayout.Controls.Add(saveBtn, 3, _row);
            
            saveBtn.Name = "SaveBtn_" + _row;
            saveBtn.Click += new EventHandler(SaveBtn_Click);

            _row++;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (this.radioCore.Checked)
                isCheckedCore = true;
            else
                isCheckedCore = false;

            initTable();
            
            if (!isScaning)
            {
                if (fs_wearDev != null)
                    fs_wearDev.Close();
                if (fs_homeDevice != null)
                    fs_homeDevice.Close();
                wearDevList = new List<string>();
                homeDeviceList = new List<string>();
                wearDevList1 = new List<string>();
                homeDeviceList1 = new List<string>();

                if ( !fileOpen() )
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

		
		public async void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            var addr = args.BluetoothAddress;
            string sAddr = addr.ToString("X").ToUpper();
            
            if (args.Advertisement.LocalName == deviceName_WearDev && isCheckedCore)
            {
                if (wearDevList.Contains(sAddr))
                {
                    if (!wearDevList1.Contains(sAddr))
                    {
                        logAddEvent("Searched Device: WearDevice " + ":" + sAddr + " (already saved)", 1);
                        wearDevList1.Add(sAddr);
                    }
                }
                else
                {
                    findNewDeviceEvent(sAddr);

                    wearDevList.Add(sAddr);
                    logAddEvent(sAddr, 1);
                }
            }else if (args.Advertisement.LocalName == deviceName_HomeDevice && !isCheckedCore)
            {
                if (homeDeviceList.Contains(sAddr))
                {
                    if (!homeDeviceList1.Contains(sAddr))
                    {
                        logAddEvent("Searched Device: HomeDevice " + ":" + sAddr + " (already saved)", 1);
                        homeDeviceList1.Add(sAddr);
                    }
                }
                else
                {
                    findNewDeviceEvent(sAddr);

                    homeDeviceList.Add(sAddr);
                    logAddEvent(sAddr, 2);
                }
            }
        }
		

		private void SaveBtn_Click(object sender, EventArgs e)
        {
            Button saveBtn = sender as Button;
            var name = saveBtn.Name;

            var list = name.Split('_');
            var rowIndex = Int32.Parse(list[1]);

            var addrControl = this.tableLayout.GetControlFromPosition(0, rowIndex);

            string bluetoothAddr = addrControl.Text.Replace(":", "");
            if (isCheckedCore)
                logAddEvent(bluetoothAddr, 1, true);
            else
                logAddEvent(bluetoothAddr, 2, true);

            saveAddr(bluetoothAddr);
            
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

            for (int i=0; i<addr.Length; i++)
            {
                if (i> 0 && i % 2 == 0)
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
    }
}
