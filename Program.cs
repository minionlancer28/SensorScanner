using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SensorScanner
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine($"Application Start");
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new FrmBLE());
                Application.Run(new FrmScanner());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("予期せぬ例外が発生しました。\nアプリケーションを終了します。",
                                "Application Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            Console.WriteLine($"Application End");
        }
    }
}
