namespace ChatClient
{
    partial class FriendControl
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
            this.select = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // select
            // 
            this.select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select.Font = new System.Drawing.Font("Buxton Sketch", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.select.Location = new System.Drawing.Point(0, 0);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(205, 40);
            this.select.TabIndex = 3;
            this.select.Text = "a name asds";
            this.select.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.select.UseVisualStyleBackColor = true;
            // 
            // FriendControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.select);
            this.Name = "FriendControl";
            this.Size = new System.Drawing.Size(206, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button select;
    }
}
