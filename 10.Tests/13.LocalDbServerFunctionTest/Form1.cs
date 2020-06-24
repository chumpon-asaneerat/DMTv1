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
            LocalDbServer2.Instance.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalDbServer.Instance.Shutdown();
            LocalDbServer2.Instance.Shutdown();
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

            if (null == inst) return;
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
                    inst.Amount = 1;
                    inst.Amount1 = 2;
                    inst.Amount2 = 3;
                    inst.Name = "Item " + i.ToString("D6");

                    //LocalDbServer.Instance.Save(inst);
                    StressTest.Save(inst);
                }

                Invoke((MethodInvoker)delegate
                {
                    //var items = LocalDbServer.Instance.GetStressTests(true);
                    var items = StressTest.Gets(true);
                    lbStressTestCount.Text = "Count: " + ((null != items) ? items.Count.ToString("n0") : "0");

                    //lbStressTestSum.Text = "Sum:" + StressTest.Sum().ToString("n0");
                    var sum2 = StressTest.Sum2();
                    lbStressTestSum.Text = string.Format("Sum: Amount1 = {0:n0}, Amount2 = {1:n0}", sum2.Sum1, sum2.Sum2);

                    dgStressTest.DataSource = items;
                });
            });
        }

        private void cmdRefreshStressTest_Click(object sender, EventArgs e)
        {
            //var items = LocalDbServer.Instance.GetStressTests(true);
            var items = StressTest.Gets(true);
            lbStressTestCount.Text = "Count: " + ((null != items) ? items.Count.ToString("n0") : "0");

            //lbStressTestSum.Text = "Sum:" + StressTest.Sum().ToString("n0");
            var sum2 = StressTest.Sum2();
            lbStressTestSum.Text = string.Format("Sum: Amount1 = {0:n0}, Amount2 = {1:n0}", sum2.Sum1, sum2.Sum2);

            dgStressTest.DataSource = items;
        }

        private void cmdClearStressTests_Click(object sender, EventArgs e)
        {
            //LocalDbServer.Instance.Db.DeleteAll<StressTest>();
            StressTest.DeleteAll();
            //var items = LocalDbServer.Instance.GetStressTests(true);
            var items = StressTest.Gets(true);
            lbStressTestCount.Text = "Count: " + ((null != items) ? items.Count.ToString("n0") : "0");

            //lbStressTestSum.Text = "Sum:" + StressTest.Sum().ToString("n0");
            var sum2 = StressTest.Sum2();
            lbStressTestSum.Text = string.Format("Sum: Amount1 = {0:n0}, Amount2 = {1:n0}", sum2.Sum1, sum2.Sum2);

            dgStressTest.DataSource = items;
        }

        private void lstTSB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgTSB2.SelectedObject = lstTSB2.SelectedItem;
        }

        private void cmdTSB2Refresh_Click(object sender, EventArgs e)
        {
            lstTSB2.DataSource = TSB2.Gets(true);
        }

        private void cmdPlaza2Refresh_Click(object sender, EventArgs e)
        {
            lstTSB2.DataSource = PlazaWithTSB2.Gets();
        }

        private void cmdTSB2New_Click(object sender, EventArgs e)
        {
            TSB2 inst = TSB2.Create();
            pgTSB2.SelectedObject = inst;
        }

        private void cmdTSB2Save_Click(object sender, EventArgs e)
        {
            TSB2 inst = pgTSB2.SelectedObject as TSB2;
            TSB2.Save(inst);
            // reload
            lstTSB2.DataSource = TSB2.Gets(true);
        }
    }
}
