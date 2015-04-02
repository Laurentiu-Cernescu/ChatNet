using System.Windows.Forms;
using System;

using Message = WcfChatService.Message;

namespace ChatClient
{
    public partial class MessageControlReceived : UserControl
    {
        public MessageControlReceived()
        {
            InitializeComponent();
        }

        public void ChangeData(Message message)
        {
            message.MessageText = message.MessageText.TrimEnd(' ');

            date.Text = message.Date.ToString("mm:hh dd:MM:yyyy") + "   " + message.Sender.Username.Trim() + ":";
            messageLabel.Text = message.MessageText;

            int ySize = Convert.ToInt32(Math.Ceiling((message.MessageText.Length / 50.0)));

            messageLabel.Size = new System.Drawing.Size(360, 21 * ySize);

            Size = new System.Drawing.Size(440, 44 + 21 * ySize);
        }
    }
}
