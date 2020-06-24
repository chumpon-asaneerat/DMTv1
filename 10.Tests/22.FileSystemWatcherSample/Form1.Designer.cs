namespace FileSystemWatcherSample
{
    partial class Form1
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
            this.lstMsgs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstMsgs
            // 
            this.lstMsgs.FormattingEnabled = true;
            this.lstMsgs.IntegralHeight = false;
            this.lstMsgs.ItemHeight = 16;
            this.lstMsgs.Location = new System.Drawing.Point(12, 12);
            this.lstMsgs.Name = "lstMsgs";
            this.lstMsgs.Size = new System.Drawing.Size(627, 408);
            this.lstMsgs.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 424);
            this.Controls.Add(this.lstMsgs);
            this.Name = "Form1";
            this.Text = "Watch File(s)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstMsgs;
    }
}

