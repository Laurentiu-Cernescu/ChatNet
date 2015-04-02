namespace ChatClient
{
    partial class MessageControlReceived
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.date = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // date
            // 
            this.date.Font = new System.Drawing.Font("Buxton Sketch", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date.Location = new System.Drawing.Point(0, 0);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(367, 30);
            this.date.TabIndex = 0;
            this.date.Text = "12:35 19-Apr-2015   alahu achbar:";
            this.date.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.messageLabel.Font = new System.Drawing.Font("Buxton Sketch", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(7, 30);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(360, 84);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            // 
            // MessageControlReceived
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.date);
            this.Name = "MessageControlReceived";
            this.Size = new System.Drawing.Size(445, 125);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label date;
        private System.Windows.Forms.Label messageLabel;
    }
}
