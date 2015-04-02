using System;
using System.Timers;
using System.Windows.Forms;
using WcfChatService;

using Timer = System.Timers.Timer;

namespace ChatClient
{
    public partial class LoginWindow : Form
    {
        ClientModel service;

        const string serverUp = "server is up";
        const string serverDown= "server is down";
        const string loginOk = "logged in";
        const string loginFailed = "invalid credentials";
        const string registerOk = "account created";
        const string registerFailed = "username taken";

        public LoginWindow(ClientModel serviceRef)
        {
            service = serviceRef;

            InitializeComponent();

            login.Enabled = false;
            register.Enabled = false;

            password.TextChanged += CredentialsChanged;
            username.TextChanged += CredentialsChanged;

            CheckConnection();
        }

        void CheckConnection()
        {
            switch(service.PingServer())
            {
                case Response.Succes:

                    status.Text = serverUp;

                    break;
                case Response.Failed:

                    status.Text = serverDown;

                    username.Enabled = false;
                    password.Enabled = false;

                    break;
            }
        }

        public void CredentialsChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(username.Text) || String.IsNullOrWhiteSpace(password.Text))
                login.Enabled = register.Enabled = false;
            else
                login.Enabled = register.Enabled = true;

        }

        private void login_Click(object sender, EventArgs e)
        {
            switch(service.Login(username.Text, password.Text))
            {
                case Response.Succes:

                    status.Text = loginOk;

                    login.Enabled = false;
                    register.Enabled = false;
                    username.Enabled = false;
                    password.Enabled = false;

                    QueueAction(1500, new ElapsedEventHandler((x, y) =>
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new MethodInvoker(() =>
                            {
                                MainWindow mainWindow = new MainWindow(service);
                                mainWindow.Show();
                                this.Hide();
                                (x as Timer).Enabled = false;
                            }));
                        }
                    }));

                    break;
                case Response.Failed:

                    status.Text = loginFailed;

                    break;
            }
        }

        private void register_Click(object sender, EventArgs e)
        {
            switch (service.Register(username.Text, password.Text))
            {
                case Response.Succes:

                    status.Text = registerOk;

                    break;
                case Response.Failed:

                    status.Text = registerFailed;

                    break;
            }
        }

        static void QueueAction(int ms, ElapsedEventHandler action)
        {
            Timer timer = new Timer(ms);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(action);
            timer.Enabled = true;
        }
    }
}
