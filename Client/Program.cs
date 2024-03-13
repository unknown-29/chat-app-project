using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists(MySession.SessionFilePath))
            {
                string line = File.ReadAllLines(MySession.SessionFilePath)[0];
                MySession.Username = line.Split('=')[1];
                MySession.UsersService.SetUserOnline(MySession.Username);
                Application.Run(new UserDashboard());
            }
            else Application.Run(new Login());
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
