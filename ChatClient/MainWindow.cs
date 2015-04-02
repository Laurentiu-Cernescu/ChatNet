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

        }

        private void addFriend_Click(object sender, EventArgs e)
        {

        }

        private void send_Click(object sender, EventArgs e)
        {
            var to = sender as FriendControl;

            AddMessage(service.SendMessage(selectedFriend, messageBox.Text));
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
    }
}
