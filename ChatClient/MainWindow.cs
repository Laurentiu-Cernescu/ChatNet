using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfChatService;

using Message = WcfChatService.Message;

namespace ChatClient
{
    public partial class MainWindow : Form
    {
        ClientModel service;

        User selectedFriend;

        const string logoutText = "(logout)";

        public MainWindow(ClientModel serviceRef)
        {
            service = serviceRef;

            InitializeComponent();
        }

        public void Config()
        {
            service.NewMessageAction = ReceiveNewMessage;
            service.NotifyStatusChange = ChangeStatus;

            logout.Text = service.CurrentUser.Username + logoutText;

            send.Enabled = false;
            messageBox.Enabled = false;

            RefreshFriendList();
        }

        public void ChangeConversation(User toThis)
        {
            send.Enabled = true;
            messageBox.Enabled = true;

            selectedFriend = toThis;

            messageList.Controls.Clear();

            var messages = service.GetMessages(selectedFriend);

            foreach(var msg in messages)
            {
                AddMessage(msg);

                if(!msg.Seen)
                {
                    service.MarkAsRead(msg);
                }
            }
        }

        private void RefreshFriendList()
        {
            friendList.Controls.Clear();

            var friends = service.GetFriends();

            foreach(User friend in friends)
            {
                var control = new FriendControl();

                control.ChangeData(friend, ChangeConversation);

                friendList.Controls.Add(control);
            }
        }

        private void logout_Click(object sender, EventArgs e)
        {
            service.Logout();

            MainEntry.LoginForm.Config();
            MainEntry.LoginForm.Show();
            this.Hide();
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            if(service.AddFriend(addFriendName.Text) == Response.Succes)
            {
                RefreshFriendList();
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            var to = sender as FriendControl;

            AddMessage(service.SendMessage(selectedFriend, messageBox.Text));

            messageBox.Text = null;
        }

        public void ReceiveNewMessage(Message message)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                    {
                        if (selectedFriend != null && message.Sender.Username.Trim().Equals(selectedFriend.Username.Trim()))
                        {
                            AddMessage(message);
                        }
                        else
                        {
                            foreach (var control in friendList.Controls)
                            {
                                if (control is FriendControl)
                                {
                                    FriendControl display = control as FriendControl;

                                    if (display.DisplayedUser.Username.Trim().Equals(message.Sender.Username.Trim()))
                                    {
                                        display.ChangeNewMessageStatus(true);
                                    }
                                }
                            }
                        }
                    }));
            }
            else
            {
                if (selectedFriend != null && message.Sender.Username.Trim().Equals(selectedFriend.Username.Trim()))
                {
                    AddMessage(message);
                }
                else
                {
                    foreach (var control in friendList.Controls)
                    {
                        if (control is FriendControl)
                        {
                            FriendControl display = control as FriendControl;

                            if (display.DisplayedUser.Username.Trim().Equals(message.Sender.Username.Trim()))
                            {
                                display.ChangeNewMessageStatus(true);
                            }
                        }
                    }
                }
            }
        }

        public void ChangeStatus(User user, Status status)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                    {
                        foreach (var control in friendList.Controls)
                        {
                            if (control is FriendControl)
                            {
                                FriendControl display = control as FriendControl;

                                if (display.DisplayedUser.Username.Trim().Equals(user.Username.Trim()))
                                {
                                    display.ChangeStatus(status);
                                }
                            }
                        }
                    }));
            }
            else
            {
                foreach (var control in friendList.Controls)
                {
                    if (control is FriendControl)
                    {
                        FriendControl display = control as FriendControl;

                        if (display.DisplayedUser.Username.Trim().Equals(user.Username.Trim()))
                        {
                            display.ChangeStatus(status);
                        }
                    }
                }
            }
        }

        private void AddMessage(Message message)
        {
            if (message.Sender.Username.Trim().Equals(service.CurrentUser.Username.Trim()))
            {
                var msgControl = new MessageControlSent();

                msgControl.ChangeData(message);

                messageList.Controls.Add(msgControl);
            }
            else
            {
                if (message.Sender.Username.Trim().Equals(selectedFriend.Username.Trim()))
                {
                    var msgControl = new MessageControlReceived();

                    msgControl.ChangeData(message);

                    messageList.Controls.Add(msgControl);
                }
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (service.RemoveFriend(addFriendName.Text) == Response.Succes)
            {
                RefreshFriendList();
            }
        }
    }
}
