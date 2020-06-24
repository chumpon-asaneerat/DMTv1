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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbStressTestSum = new System.Windows.Forms.Label();
            this.cmdClearStressTests = new System.Windows.Forms.Button();
            this.lbStressTestCount = new System.Windows.Forms.Label();
            this.dgStressTest = new System.Windows.Forms.DataGridView();
            this.cmdRefreshStressTest = new System.Windows.Forms.Button();
            this.cmdAdd300 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cmdPlaza2Refresh = new System.Windows.Forms.Button();
            this.cmdTSB2Save = new System.Windows.Forms.Button();
            this.cmdTSB2New = new System.Windows.Forms.Button();
            this.pgTSB2 = new System.Windows.Forms.PropertyGrid();
            this.cmdTSB2Refresh = new System.Windows.Forms.Button();
            this.lstTSB2 = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cmdRefreshPlaza3 = new System.Windows.Forms.Button();
            this.pgServer3 = new System.Windows.Forms.PropertyGrid();
            this.cmdRefreshTSB3 = new System.Windows.Forms.Button();
            this.lstServer3 = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStressTest)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
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
            this.cmdTSBRefresh.Size = new System.Drawing.Size(129, 37);
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lbStressTestSum);
            this.tabPage3.Controls.Add(this.cmdClearStressTests);
            this.tabPage3.Controls.Add(this.lbStressTestCount);
            this.tabPage3.Controls.Add(this.dgStressTest);
            this.tabPage3.Controls.Add(this.cmdRefreshStressTest);
            this.tabPage3.Controls.Add(this.cmdAdd300);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(926, 576);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Stress Tests";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbStressTestSum
            // 
            this.lbStressTestSum.AutoSize = true;
            this.lbStressTestSum.Location = new System.Drawing.Point(611, 23);
            this.lbStressTestSum.Name = "lbStressTestSum";
            this.lbStressTestSum.Size = new System.Drawing.Size(40, 17);
            this.lbStressTestSum.TabIndex = 7;
            this.lbStressTestSum.Text = "Sum:";
            // 
            // cmdClearStressTests
            // 
            this.cmdClearStressTests.Location = new System.Drawing.Point(264, 13);
            this.cmdClearStressTests.Name = "cmdClearStressTests";
            this.cmdClearStressTests.Size = new System.Drawing.Size(122, 37);
            this.cmdClearStressTests.TabIndex = 6;
            this.cmdClearStressTests.Text = "Clear";
            this.cmdClearStressTests.UseVisualStyleBackColor = true;
            this.cmdClearStressTests.Click += new System.EventHandler(this.cmdClearStressTests_Click);
            // 
            // lbStressTestCount
            // 
            this.lbStressTestCount.AutoSize = true;
            this.lbStressTestCount.Location = new System.Drawing.Point(469, 23);
            this.lbStressTestCount.Name = "lbStressTestCount";
            this.lbStressTestCount.Size = new System.Drawing.Size(49, 17);
            this.lbStressTestCount.TabIndex = 5;
            this.lbStressTestCount.Text = "Count:";
            // 
            // dgStressTest
            // 
            this.dgStressTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgStressTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStressTest.Location = new System.Drawing.Point(8, 56);
            this.dgStressTest.Name = "dgStressTest";
            this.dgStressTest.RowHeadersWidth = 51;
            this.dgStressTest.RowTemplate.Height = 24;
            this.dgStressTest.Size = new System.Drawing.Size(910, 512);
            this.dgStressTest.TabIndex = 4;
            // 
            // cmdRefreshStressTest
            // 
            this.cmdRefreshStressTest.Location = new System.Drawing.Point(8, 13);
            this.cmdRefreshStressTest.Name = "cmdRefreshStressTest";
            this.cmdRefreshStressTest.Size = new System.Drawing.Size(122, 37);
            this.cmdRefreshStressTest.TabIndex = 3;
            this.cmdRefreshStressTest.Text = "Refresh";
            this.cmdRefreshStressTest.UseVisualStyleBackColor = true;
            this.cmdRefreshStressTest.Click += new System.EventHandler(this.cmdRefreshStressTest_Click);
            // 
            // cmdAdd300
            // 
            this.cmdAdd300.Location = new System.Drawing.Point(136, 13);
            this.cmdAdd300.Name = "cmdAdd300";
            this.cmdAdd300.Size = new System.Drawing.Size(122, 37);
            this.cmdAdd300.TabIndex = 2;
            this.cmdAdd300.Text = "Add 300 rows";
            this.cmdAdd300.UseVisualStyleBackColor = true;
            this.cmdAdd300.Click += new System.EventHandler(this.cmdAdd300_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cmdPlaza2Refresh);
            this.tabPage4.Controls.Add(this.cmdTSB2Save);
            this.tabPage4.Controls.Add(this.cmdTSB2New);
            this.tabPage4.Controls.Add(this.pgTSB2);
            this.tabPage4.Controls.Add(this.cmdTSB2Refresh);
            this.tabPage4.Controls.Add(this.lstTSB2);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(926, 576);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "TSB2 / Plaza2";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cmdPlaza2Refresh
            // 
            this.cmdPlaza2Refresh.Location = new System.Drawing.Point(153, 7);
            this.cmdPlaza2Refresh.Name = "cmdPlaza2Refresh";
            this.cmdPlaza2Refresh.Size = new System.Drawing.Size(145, 37);
            this.cmdPlaza2Refresh.TabIndex = 10;
            this.cmdPlaza2Refresh.Text = "Refresh (Plaza2)";
            this.cmdPlaza2Refresh.UseVisualStyleBackColor = true;
            this.cmdPlaza2Refresh.Click += new System.EventHandler(this.cmdPlaza2Refresh_Click);
            // 
            // cmdTSB2Save
            // 
            this.cmdTSB2Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTSB2Save.Location = new System.Drawing.Point(796, 7);
            this.cmdTSB2Save.Name = "cmdTSB2Save";
            this.cmdTSB2Save.Size = new System.Drawing.Size(122, 37);
            this.cmdTSB2Save.TabIndex = 9;
            this.cmdTSB2Save.Text = "Save";
            this.cmdTSB2Save.UseVisualStyleBackColor = true;
            this.cmdTSB2Save.Click += new System.EventHandler(this.cmdTSB2Save_Click);
            // 
            // cmdTSB2New
            // 
            this.cmdTSB2New.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTSB2New.Location = new System.Drawing.Point(452, 7);
            this.cmdTSB2New.Name = "cmdTSB2New";
            this.cmdTSB2New.Size = new System.Drawing.Size(122, 37);
            this.cmdTSB2New.TabIndex = 8;
            this.cmdTSB2New.Text = "New";
            this.cmdTSB2New.UseVisualStyleBackColor = true;
            this.cmdTSB2New.Click += new System.EventHandler(this.cmdTSB2New_Click);
            // 
            // pgTSB2
            // 
            this.pgTSB2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTSB2.Location = new System.Drawing.Point(452, 50);
            this.pgTSB2.Name = "pgTSB2";
            this.pgTSB2.Size = new System.Drawing.Size(466, 519);
            this.pgTSB2.TabIndex = 7;
            // 
            // cmdTSB2Refresh
            // 
            this.cmdTSB2Refresh.Location = new System.Drawing.Point(8, 7);
            this.cmdTSB2Refresh.Name = "cmdTSB2Refresh";
            this.cmdTSB2Refresh.Size = new System.Drawing.Size(139, 37);
            this.cmdTSB2Refresh.TabIndex = 6;
            this.cmdTSB2Refresh.Text = "Refresh (TSB2)";
            this.cmdTSB2Refresh.UseVisualStyleBackColor = true;
            this.cmdTSB2Refresh.Click += new System.EventHandler(this.cmdTSB2Refresh_Click);
            // 
            // lstTSB2
            // 
            this.lstTSB2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTSB2.FormattingEnabled = true;
            this.lstTSB2.IntegralHeight = false;
            this.lstTSB2.ItemHeight = 16;
            this.lstTSB2.Location = new System.Drawing.Point(8, 50);
            this.lstTSB2.Name = "lstTSB2";
            this.lstTSB2.Size = new System.Drawing.Size(438, 519);
            this.lstTSB2.TabIndex = 5;
            this.lstTSB2.SelectedIndexChanged += new System.EventHandler(this.lstTSB2_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.cmdRefreshPlaza3);
            this.tabPage5.Controls.Add(this.pgServer3);
            this.tabPage5.Controls.Add(this.cmdRefreshTSB3);
            this.tabPage5.Controls.Add(this.lstServer3);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(926, 576);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "TSB3 / Plaza3";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // cmdRefreshPlaza3
            // 
            this.cmdRefreshPlaza3.Location = new System.Drawing.Point(153, 7);
            this.cmdRefreshPlaza3.Name = "cmdRefreshPlaza3";
            this.cmdRefreshPlaza3.Size = new System.Drawing.Size(145, 37);
            this.cmdRefreshPlaza3.TabIndex = 14;
            this.cmdRefreshPlaza3.Text = "Refresh (Plaza3)";
            this.cmdRefreshPlaza3.UseVisualStyleBackColor = true;
            this.cmdRefreshPlaza3.Click += new System.EventHandler(this.cmdRefreshPlaza3_Click);
            // 
            // pgServer3
            // 
            this.pgServer3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgServer3.Location = new System.Drawing.Point(452, 50);
            this.pgServer3.Name = "pgServer3";
            this.pgServer3.Size = new System.Drawing.Size(466, 519);
            this.pgServer3.TabIndex = 13;
            // 
            // cmdRefreshTSB3
            // 
            this.cmdRefreshTSB3.Location = new System.Drawing.Point(8, 7);
            this.cmdRefreshTSB3.Name = "cmdRefreshTSB3";
            this.cmdRefreshTSB3.Size = new System.Drawing.Size(139, 37);
            this.cmdRefreshTSB3.TabIndex = 12;
            this.cmdRefreshTSB3.Text = "Refresh (TSB3)";
            this.cmdRefreshTSB3.UseVisualStyleBackColor = true;
            this.cmdRefreshTSB3.Click += new System.EventHandler(this.cmdRefreshTSB3_Click);
            // 
            // lstServer3
            // 
            this.lstServer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstServer3.FormattingEnabled = true;
            this.lstServer3.IntegralHeight = false;
            this.lstServer3.ItemHeight = 16;
            this.lstServer3.Location = new System.Drawing.Point(8, 50);
            this.lstServer3.Name = "lstServer3";
            this.lstServer3.Size = new System.Drawing.Size(438, 519);
            this.lstServer3.TabIndex = 11;
            this.lstServer3.SelectedIndexChanged += new System.EventHandler(this.lstServer3_SelectedIndexChanged);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStressTest)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button cmdAdd300;
        private System.Windows.Forms.DataGridView dgStressTest;
        private System.Windows.Forms.Button cmdRefreshStressTest;
        private System.Windows.Forms.Label lbStressTestCount;
        private System.Windows.Forms.Button cmdClearStressTests;
        private System.Windows.Forms.Label lbStressTestSum;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button cmdTSB2Save;
        private System.Windows.Forms.Button cmdTSB2New;
        private System.Windows.Forms.PropertyGrid pgTSB2;
        private System.Windows.Forms.Button cmdTSB2Refresh;
        private System.Windows.Forms.ListBox lstTSB2;
        private System.Windows.Forms.Button cmdPlaza2Refresh;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button cmdRefreshPlaza3;
        private System.Windows.Forms.PropertyGrid pgServer3;
        private System.Windows.Forms.Button cmdRefreshTSB3;
        private System.Windows.Forms.ListBox lstServer3;
    }
}

