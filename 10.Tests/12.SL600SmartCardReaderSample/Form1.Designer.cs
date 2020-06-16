namespace SL600SmartCardReaderSample
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
            this.cmdGetDevices = new System.Windows.Forms.Button();
            this.lstDevices = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cmdGetDevices
            // 
            this.cmdGetDevices.Location = new System.Drawing.Point(12, 12);
            this.cmdGetDevices.Name = "cmdGetDevices";
            this.cmdGetDevices.Size = new System.Drawing.Size(112, 32);
            this.cmdGetDevices.TabIndex = 0;
            this.cmdGetDevices.Text = "Get Devices";
            this.cmdGetDevices.UseVisualStyleBackColor = true;
            this.cmdGetDevices.Click += new System.EventHandler(this.cmdGetDevices_Click);
            // 
            // lstDevices
            // 
            this.lstDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDevices.FormattingEnabled = true;
            this.lstDevices.IntegralHeight = false;
            this.lstDevices.ItemHeight = 16;
            this.lstDevices.Location = new System.Drawing.Point(12, 50);
            this.lstDevices.Name = "lstDevices";
            this.lstDevices.Size = new System.Drawing.Size(607, 155);
            this.lstDevices.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 374);
            this.Controls.Add(this.lstDevices);
            this.Controls.Add(this.cmdGetDevices);
            this.Name = "Form1";
            this.Text = "SL600 Smart Card Reader Sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdGetDevices;
        private System.Windows.Forms.ListBox lstDevices;
    }
}

