using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    static class MainEntry
    {
        static ClientModel service;

        public static LoginWindow LoginForm;
        public static MainWindow MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            service = new ClientModel();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm = new LoginWindow(service);
            MainForm = new MainWindow(service);

            Application.Run(LoginForm);
        }
    }
}
