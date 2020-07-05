namespace DMTSmartCardSample
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbCardExist = new System.Windows.Forms.Label();
            this.lstDevices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lbBlock0 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbBlock1 = new System.Windows.Forms.Label();
            this.lbBlock2 = new System.Windows.Forms.Label();
            this.lbBlock3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Card Exist : ";
            // 
            // lbCardExist
            // 
            this.lbCardExist.AutoSize = true;
            this.lbCardExist.Location = new System.Drawing.Point(101, 9);
            this.lbCardExist.Name = "lbCardExist";
            this.lbCardExist.Size = new System.Drawing.Size(62, 17);
            this.lbCardExist.TabIndex = 1;
            this.lbCardExist.Text = "No card.";
            // 
            // lstDevices
            // 
            this.lstDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstDevices.FormattingEnabled = true;
            this.lstDevices.IntegralHeight = false;
            this.lstDevices.ItemHeight = 16;
            this.lstDevices.Location = new System.Drawing.Point(12, 182);
            this.lstDevices.Name = "lstDevices";
            this.lstDevices.Size = new System.Drawing.Size(789, 214);
            this.lstDevices.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Devices:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // lbBlock0
            // 
            this.lbBlock0.AutoSize = true;
            this.lbBlock0.Location = new System.Drawing.Point(101, 41);
            this.lbBlock0.Name = "lbBlock0";
            this.lbBlock0.Size = new System.Drawing.Size(62, 17);
            this.lbBlock0.TabIndex = 5;
            this.lbBlock0.Text = "Block 0 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Card Date : ";
            // 
            // lbBlock1
            // 
            this.lbBlock1.AutoSize = true;
            this.lbBlock1.Location = new System.Drawing.Point(101, 68);
            this.lbBlock1.Name = "lbBlock1";
            this.lbBlock1.Size = new System.Drawing.Size(62, 17);
            this.lbBlock1.TabIndex = 6;
            this.lbBlock1.Text = "Block 0 :";
            // 
            // lbBlock2
            // 
            this.lbBlock2.AutoSize = true;
            this.lbBlock2.Location = new System.Drawing.Point(101, 95);
            this.lbBlock2.Name = "lbBlock2";
            this.lbBlock2.Size = new System.Drawing.Size(62, 17);
            this.lbBlock2.TabIndex = 7;
            this.lbBlock2.Text = "Block 0 :";
            // 
            // lbBlock3
            // 
            this.lbBlock3.AutoSize = true;
            this.lbBlock3.Location = new System.Drawing.Point(101, 125);
            this.lbBlock3.Name = "lbBlock3";
            this.lbBlock3.Size = new System.Drawing.Size(62, 17);
            this.lbBlock3.TabIndex = 8;
            this.lbBlock3.Text = "Block 0 :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 408);
            this.Controls.Add(this.lbBlock3);
            this.Controls.Add(this.lbBlock2);
            this.Controls.Add(this.lbBlock1);
            this.Controls.Add(this.lbBlock0);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstDevices);
            this.Controls.Add(this.lbCardExist);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Smartcard Reader Sample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCardExist;
        private System.Windows.Forms.ListBox lstDevices;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lbBlock0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbBlock1;
        private System.Windows.Forms.Label lbBlock2;
        private System.Windows.Forms.Label lbBlock3;
    }
}

