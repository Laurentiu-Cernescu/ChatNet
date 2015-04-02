using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfChatService;
using ChatClient.Properties;

namespace ChatClient
{
    public partial class FriendControl : UserControl
    {
        Label newMessageIcon;
        Label statusIcon;

        Action<User> callback;

        User mData;

        public FriendControl()
        {
            InitializeComponent();

            newMessageIcon = new Label()
            {
                Text = null,
                Location = new Point(146, 10),
                Size = new Size(32, 20),
                Image = Resources.newMessage
            };

            statusIcon = new Label()
            {
                Text = null,
                Location = new Point(180, 12),
                Size = new Size(16, 16),
                Image = Resources.offline
            };

            select.Controls.Add(newMessageIcon);
            select.Controls.Add(statusIcon);

            select.Click += OnClick;
        }

        void OnClick(Object sender, EventArgs args)
        {
            if(mData != null && callback != null)
            {
                callback(mData);
            }
        }

        public FriendControl ChangeData(User friend, Action<User> onClick)
        {
            mData = friend;
            callback = onClick;

            select.Text = friend.Username;
            newMessageIcon.Visible = friend.HasUnread;
            statusIcon.Image = friend.Status == Status.Online ? Resources.online : Resources.offline;
            
            return this;
        }
    }
}
