namespace WebSocketClientSample
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.lvMessages = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(67, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(155, 22);
            this.txtName.TabIndex = 1;
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(229, 7);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(105, 30);
            this.cmdConnect.TabIndex = 2;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.Location = new System.Drawing.Point(341, 7);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Size = new System.Drawing.Size(105, 30);
            this.cmdDisconnect.TabIndex = 3;
            this.cmdDisconnect.Text = "Disconnect";
            this.cmdDisconnect.UseVisualStyleBackColor = true;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // lvMessages
            // 
            this.lvMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colMessage});
            this.lvMessages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMessages.HideSelection = false;
            this.lvMessages.Location = new System.Drawing.Point(15, 87);
            this.lvMessages.Name = "lvMessages";
            this.lvMessages.Size = new System.Drawing.Size(773, 351);
            this.lvMessages.TabIndex = 4;
            this.lvMessages.UseCompatibleStateImageBehavior = false;
            this.lvMessages.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 150;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 250;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(67, 46);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(267, 22);
            this.txtMessage.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name:";
            // 
            // cmdSend
            // 
            this.cmdSend.Location = new System.Drawing.Point(341, 42);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(105, 30);
            this.cmdSend.TabIndex = 7;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmdSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvMessages);
            this.Controls.Add(this.cmdDisconnect);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Web Socket Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.ListView lvMessages;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdSend;
    }
}

