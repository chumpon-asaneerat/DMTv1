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
            lstTSB.DataSource = LocalDbServer.Instance.Db.GetAllWithChildren<TSB>();
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

            /*
            var tsb = (from p in LocalDbServer.Instance.Db.Table<TSB>()
                         where p.TSBId == inst.TSBId
                         select p).FirstOrDefault();

            if (null == tsb)
            {
                LocalDbServer.Instance.Db.Insert(inst);
            }
            else
            {
                LocalDbServer.Instance.Db.Update(inst);
            }

            if (null != inst.Plazas)
            {
                foreach (var plaza in inst.Plazas)
                {
                    var pz = (from p in LocalDbServer.Instance.Db.Table<Plaza>()
                                 where p.PlazaId == plaza.PlazaId
                              select p).FirstOrDefault();
                    if (null == pz)
                    {
                        LocalDbServer.Instance.Db.Insert(plaza);
                    }
                    else
                    {
                        LocalDbServer.Instance.Db.Update(plaza);
                    }
                }
            }

            LocalDbServer.Instance.Db.UpdateWithChildren(inst);
            */

            // reload
            lstTSB.DataSource = LocalDbServer.Instance.Db.GetAllWithChildren<TSB>();
        }
    }
}
