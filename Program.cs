using System;
using System.Windows.Forms;

namespace PhoneBook
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        [STAThread]
        static void Main()
        {
            string connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("SQL server connection string environment variable 'SQL_SERVER_CONNECTION_STRING' is not set!");
            }
            else
            {
                if (Environment.OSVersion.Version.Major >= 6)
                    SetProcessDPIAware();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(connectionString));
            }
        }
    }
}
