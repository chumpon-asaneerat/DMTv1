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
            //lstTSB.DataSource = LocalDbServer.Instance.Db.Table<TSB>().ToList();
            lstTSB.DataSource = LocalDbServer.Instance.GetTSBs(true);
        }

        private void cmdNewTSB_Click(object sender, EventArgs e)
        {
            TSB inst = TSB.Create();
            pgTSB.SelectedObject = inst;
        }

        private void cmdSaveTSB_Click(object sender, EventArgs e)
        {
            TSB inst = pgTSB.SelectedObject as TSB;

            LocalDbServer.Instance.Save(inst);
            // reload
            //lstTSB.DataSource = LocalDbServer.Instance.Db.GetAllWithChildren<TSB>(recursive: true);
            lstTSB.DataSource = LocalDbServer.Instance.GetTSBs(true);
        }

        private void cmdUserSearch_Click(object sender, EventArgs e)
        {

        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdNewUser_Click(object sender, EventArgs e)
        {

        }

        private void cmdSaveUser_Click(object sender, EventArgs e)
        {

        }
    }
}
