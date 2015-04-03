namespace ChatClient
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.logout = new System.Windows.Forms.Button();
            this.addFriendName = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.friendList = new System.Windows.Forms.FlowLayoutPanel();
            this.messageList = new System.Windows.Forms.FlowLayoutPanel();
            this.remove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logout
            // 
            this.logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.Location = new System.Drawing.Point(13, 13);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(210, 40);
            this.logout.TabIndex = 0;
            this.logout.Text = "shalazar(logout)";
            this.logout.UseVisualStyleBackColor = true;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // addFriendName
            // 
            this.addFriendName.BackColor = System.Drawing.Color.LightCyan;
            this.addFriendName.Font = new System.Drawing.Font("Buxton Sketch", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addFriendName.Location = new System.Drawing.Point(13, 60);
            this.addFriendName.Name = "addFriendName";
            this.addFriendName.Size = new System.Drawing.Size(210, 31);
            this.addFriendName.TabIndex = 1;
            // 
            // add
            // 
            this.add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add.Font = new System.Drawing.Font("Buxton Sketch", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add.Location = new System.Drawing.Point(13, 97);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(100, 31);
            this.add.TabIndex = 2;
            this.add.Text = "add";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.addFriend_Click);
            // 
            // messageBox
            // 
            this.messageBox.BackColor = System.Drawing.Color.LightCyan;
            this.messageBox.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageBox.Location = new System.Drawing.Point(249, 526);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(422, 37);
            this.messageBox.TabIndex = 5;
            // 
            // send
            // 
            this.send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.send.Font = new System.Drawing.Font("Arimo", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send.Location = new System.Drawing.Point(677, 526);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(37, 37);
            this.send.TabIndex = 6;
            this.send.Text = ">";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // friendList
            // 
            this.friendList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.friendList.AutoScroll = true;
            this.friendList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.friendList.Location = new System.Drawing.Point(13, 135);
            this.friendList.Name = "friendList";
            this.friendList.Size = new System.Drawing.Size(230, 428);
            this.friendList.TabIndex = 7;
            this.friendList.WrapContents = false;
            // 
            // messageList
            // 
            this.messageList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.messageList.AutoScroll = true;
            this.messageList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.messageList.Location = new System.Drawing.Point(250, 13);
            this.messageList.Name = "messageList";
            this.messageList.Size = new System.Drawing.Size(464, 507);
            this.messageList.TabIndex = 8;
            this.messageList.WrapContents = false;
            // 
            // remove
            // 
            this.remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remove.Font = new System.Drawing.Font("Buxton Sketch", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remove.Location = new System.Drawing.Point(123, 98);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(100, 31);
            this.remove.TabIndex = 9;
            this.remove.Text = "remove";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(727, 575);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.messageList);
            this.Controls.Add(this.friendList);
            this.Controls.Add(this.send);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.add);
            this.Controls.Add(this.addFriendName);
            this.Controls.Add(this.logout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Chatter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.TextBox addFriendName;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.FlowLayoutPanel friendList;
        private System.Windows.Forms.FlowLayoutPanel messageList;
        private System.Windows.Forms.Button remove;
    }
}