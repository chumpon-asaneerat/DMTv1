using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using NLib.Reports.Rdlc;
using Ctrl = NLib.Controls;


namespace ReportViewerSample2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SlipRegularItemCriteria criteria = new SlipRegularItemCriteria();
            criteria.count = 3;
            RdlcReportModel rptSource = 
                TestReportManager.Instance.GetSlipReportModel(criteria, false);
            this.rptViewer.LoadReport(rptSource);
        }
    }
}
