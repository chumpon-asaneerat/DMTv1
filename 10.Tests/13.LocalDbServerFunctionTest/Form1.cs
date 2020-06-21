using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DMT.Models.Domains;
using DMT.Services;
using NLib.Controls.Utils;
using SQLiteNetExtensions.Extensions;

namespace LocalDbServerFunctionTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LocalDbServer.Instance.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalDbServer.Instance.Shutdown();
        }

        private void lstTSB_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgTSB.SelectedObject = lstTSB.SelectedItem;
        }

        private void cmdTSBRefresh_Click(object sender, EventArgs e)
        {
            //lstTSB.DataSource = LocalDbServer.Instance.GetTSBs(true);
            lstTSB.DataSource = TSB.Gets(true);
        }

        private void cmdNewTSB_Click(object sender, EventArgs e)
        {
            TSB inst = TSB.Create();
            pgTSB.SelectedObject = inst;
        }

        private void cmdSaveTSB_Click(object sender, EventArgs e)
        {
            TSB inst = pgTSB.SelectedObject as TSB;

            //LocalDbServer.Instance.Save(inst);
            TSB.Save(inst);

            // reload
            //lstTSB.DataSource = LocalDbServer.Instance.Db.GetAllWithChildren<TSB>(recursive: true);
            //lstTSB.DataSource = LocalDbServer.Instance.GetTSBs(true);
            lstTSB.DataSource = TSB.Gets(true);
        }

        private void cmdUserSearch_Click(object sender, EventArgs e)
        {
            //lstUsers.DataSource = LocalDbServer.Instance.GetUsers(true);
            lstUsers.DataSource = User.Gets(true);
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgUser.SelectedObject = lstUsers.SelectedItem;
        }

        private void cmdNewUser_Click(object sender, EventArgs e)
        {
            User inst = User.Create();
            pgUser.SelectedObject = inst;
        }

        private void cmdSaveUser_Click(object sender, EventArgs e)
        {
            User inst = pgUser.SelectedObject as User;

            //LocalDbServer.Instance.Save(inst);
            User.Save(inst);

            // reload
            //lstTSB.DataSource = LocalDbServer.Instance.GetUsers(true);
            lstUsers.DataSource = User.Gets(true);
        }

        private void cmdAdd300_Click(object sender, EventArgs e)
        {
            dgStressTest.DataSource = null;

            Task.Run(() =>
            {
                StressTest inst;
                for (int i = 0; i < 300; ++i)
                {
                    inst = new StressTest();
                    inst.RowId = Guid.NewGuid().ToString();
                    inst.Updated = DateTime.Now;
                    inst.Name = "Item " + i.ToString("D6");

                    //LocalDbServer.Instance.Save(inst);
                    StressTest.Save(inst);
                }

                Invoke((MethodInvoker)delegate
                {
                    //var items = LocalDbServer.Instance.GetStressTests(true);
                    var items = StressTest.Gets(true);
                    lbStressTestCount.Text = "Count: " + ((null != items) ? items.Count.ToString("n0") : "0");
                    dgStressTest.DataSource = items;
                });
            });
        }

        private void cmdRefreshStressTest_Click(object sender, EventArgs e)
        {
            //var items = LocalDbServer.Instance.GetStressTests(true);
            var items = StressTest.Gets(true);
            lbStressTestCount.Text = "Count: " + ((null != items) ? items.Count.ToString("n0") : "0");
            dgStressTest.DataSource = items;
        }

        private void cmdClearStressTests_Click(object sender, EventArgs e)
        {
            //LocalDbServer.Instance.Db.DeleteAll<StressTest>();
            StressTest.DeleteAll();
            //var items = LocalDbServer.Instance.GetStressTests(true);
            var items = StressTest.Gets(true);
            lbStressTestCount.Text = "Count: " + ((null != items) ? items.Count.ToString("n0") : "0");
            dgStressTest.DataSource = items;
        }
    }
}
