using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aorus.X570.Pro.RgbController
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool startMinimized = false;

            if (args != null && args.Length > 0 && args[0].Equals("-winstartup"))
                startMinimized = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Motherboard.Instance.SetLedColor(Color.Red);
            Motherboard.Instance.SetMemoryLedColor(Color.Red);

            FormMain form = new FormMain();

            if (startMinimized)
            {
                form.ShowSystemTray();
                Application.Run();
            }
            else
            {
                Application.Run(form);
            }
        }
    }
}
