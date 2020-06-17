namespace LocalDbServerFunctionTest
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdSaveTSB = new System.Windows.Forms.Button();
            this.cmdNewTSB = new System.Windows.Forms.Button();
            this.pgTSB = new System.Windows.Forms.PropertyGrid();
            this.cmdTSBRefresh = new System.Windows.Forms.Button();
            this.lstTSB = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmdSaveUser = new System.Windows.Forms.Button();
            this.cmdNewUser = new System.Windows.Forms.Button();
            this.pgUser = new System.Windows.Forms.PropertyGrid();
            this.cmdUserSearch = new System.Windows.Forms.Button();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(934, 605);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdSaveTSB);
            this.tabPage1.Controls.Add(this.cmdNewTSB);
            this.tabPage1.Controls.Add(this.pgTSB);
            this.tabPage1.Controls.Add(this.cmdTSBRefresh);
            this.tabPage1.Controls.Add(this.lstTSB);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(926, 576);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TSB/Plaza/Lane";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdSaveTSB
            // 
            this.cmdSaveTSB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSaveTSB.Location = new System.Drawing.Point(796, 6);
            this.cmdSaveTSB.Name = "cmdSaveTSB";
            this.cmdSaveTSB.Size = new System.Drawing.Size(122, 37);
            this.cmdSaveTSB.TabIndex = 4;
            this.cmdSaveTSB.Text = "Save";
            this.cmdSaveTSB.UseVisualStyleBackColor = true;
            this.cmdSaveTSB.Click += new System.EventHandler(this.cmdSaveTSB_Click);
            // 
            // cmdNewTSB
            // 
            this.cmdNewTSB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNewTSB.Location = new System.Drawing.Point(452, 6);
            this.cmdNewTSB.Name = "cmdNewTSB";
            this.cmdNewTSB.Size = new System.Drawing.Size(122, 37);
            this.cmdNewTSB.TabIndex = 3;
            this.cmdNewTSB.Text = "New";
            this.cmdNewTSB.UseVisualStyleBackColor = true;
            this.cmdNewTSB.Click += new System.EventHandler(this.cmdNewTSB_Click);
            // 
            // pgTSB
            // 
            this.pgTSB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTSB.Location = new System.Drawing.Point(452, 49);
            this.pgTSB.Name = "pgTSB";
            this.pgTSB.Size = new System.Drawing.Size(466, 519);
            this.pgTSB.TabIndex = 2;
            // 
            // cmdTSBRefresh
            // 
            this.cmdTSBRefresh.Location = new System.Drawing.Point(8, 6);
            this.cmdTSBRefresh.Name = "cmdTSBRefresh";
            this.cmdTSBRefresh.Size = new System.Drawing.Size(122, 37);
            this.cmdTSBRefresh.TabIndex = 1;
            this.cmdTSBRefresh.Text = "Refresh";
            this.cmdTSBRefresh.UseVisualStyleBackColor = true;
            this.cmdTSBRefresh.Click += new System.EventHandler(this.cmdTSBRefresh_Click);
            // 
            // lstTSB
            // 
            this.lstTSB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTSB.FormattingEnabled = true;
            this.lstTSB.IntegralHeight = false;
            this.lstTSB.ItemHeight = 16;
            this.lstTSB.Location = new System.Drawing.Point(8, 49);
            this.lstTSB.Name = "lstTSB";
            this.lstTSB.Size = new System.Drawing.Size(438, 519);
            this.lstTSB.TabIndex = 0;
            this.lstTSB.SelectedIndexChanged += new System.EventHandler(this.lstTSB_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cmdSaveUser);
            this.tabPage2.Controls.Add(this.cmdNewUser);
            this.tabPage2.Controls.Add(this.pgUser);
            this.tabPage2.Controls.Add(this.cmdUserSearch);
            this.tabPage2.Controls.Add(this.lstUsers);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(926, 576);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmdSaveUser
            // 
            this.cmdSaveUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSaveUser.Location = new System.Drawing.Point(796, 7);
            this.cmdSaveUser.Name = "cmdSaveUser";
            this.cmdSaveUser.Size = new System.Drawing.Size(122, 37);
            this.cmdSaveUser.TabIndex = 9;
            this.cmdSaveUser.Text = "Save";
            this.cmdSaveUser.UseVisualStyleBackColor = true;
            this.cmdSaveUser.Click += new System.EventHandler(this.cmdSaveUser_Click);
            // 
            // cmdNewUser
            // 
            this.cmdNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNewUser.Location = new System.Drawing.Point(452, 7);
            this.cmdNewUser.Name = "cmdNewUser";
            this.cmdNewUser.Size = new System.Drawing.Size(122, 37);
            this.cmdNewUser.TabIndex = 8;
            this.cmdNewUser.Text = "New";
            this.cmdNewUser.UseVisualStyleBackColor = true;
            this.cmdNewUser.Click += new System.EventHandler(this.cmdNewUser_Click);
            // 
            // pgUser
            // 
            this.pgUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgUser.Location = new System.Drawing.Point(452, 50);
            this.pgUser.Name = "pgUser";
            this.pgUser.Size = new System.Drawing.Size(466, 519);
            this.pgUser.TabIndex = 7;
            // 
            // cmdUserSearch
            // 
            this.cmdUserSearch.Location = new System.Drawing.Point(8, 7);
            this.cmdUserSearch.Name = "cmdUserSearch";
            this.cmdUserSearch.Size = new System.Drawing.Size(122, 37);
            this.cmdUserSearch.TabIndex = 6;
            this.cmdUserSearch.Text = "Refresh";
            this.cmdUserSearch.UseVisualStyleBackColor = true;
            this.cmdUserSearch.Click += new System.EventHandler(this.cmdUserSearch_Click);
            // 
            // lstUsers
            // 
            this.lstUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.IntegralHeight = false;
            this.lstUsers.ItemHeight = 16;
            this.lstUsers.Location = new System.Drawing.Point(8, 50);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(438, 519);
            this.lstUsers.TabIndex = 5;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 605);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Local Database Server Function Tests";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdSaveTSB;
        private System.Windows.Forms.Button cmdNewTSB;
        private System.Windows.Forms.PropertyGrid pgTSB;
        private System.Windows.Forms.Button cmdTSBRefresh;
        private System.Windows.Forms.ListBox lstTSB;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdSaveUser;
        private System.Windows.Forms.Button cmdNewUser;
        private System.Windows.Forms.PropertyGrid pgUser;
        private System.Windows.Forms.Button cmdUserSearch;
        private System.Windows.Forms.ListBox lstUsers;
    }
}

