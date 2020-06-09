using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Reporting.WinForms;
using NLib.Controls;
using NLib.Reports.Rdlc;

namespace ReportViewerSample2
{
    #region SlipRegularItemCriteria

    public class SlipRegularItemCriteria : RdlcReportCriteria
    {
        public SlipRegularItemCriteria() : base()
        {
            this.count = 10;
        }

        #region Override Methods

        /// <summary>
        /// Verify.
        /// </summary>
        /// <returns>Retruns true if criteria is valid.</returns>
        public override bool Verify()
        {
            return true;
        }

        #endregion

        #region Public Properties

        public int count { get; set; }

        #endregion
    }

    #endregion

    #region SlipRegularItem

    /// <summary>
    /// Slip Regular Item for report.
    /// </summary>
    public class SlipRegularItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Box.
        /// </summary>
        public string Box { get; set; }
        /// <summary>
        /// Gets or sets Case.
        /// </summary>
        public string Case { get; set; }
        /// <summary>
        /// Gets or sets Cause.
        /// </summary>
        public string Cause { get; set; }

        #endregion
    }

    #endregion

    public class TestReportManager : RdlcReportManager
    {
        #region Singelton

        private static TestReportManager _instance = null;
        /// <summary>
        /// Singelton Access instance.
        /// </summary>
        public static TestReportManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(TestReportManager))
                    {
                        _instance = new TestReportManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private TestReportManager() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TestReportManager()
        {

        }

        #endregion

        public RdlcReportModel GetSlipReportModel(RdlcReportCriteria criteria, bool noPageBreak)
        {
            RdlcReportModel inst = new RdlcReportModel();

            // set report definition and load report stream
            if (noPageBreak)
            {
                inst.Definition.EmbededReportName = "ReportViewerSample2.slip.rdlc";
            }
            else
            {
                inst.Definition.EmbededReportName = "ReportViewerSample2.slip.rdlc";
            }
            inst.Definition.RdlcInstance =
                this.GetEmbededReport(inst.Definition.EmbededReportName);

            // set criteria
            SlipRegularItemCriteria rptCriteria = null;
            if (criteria is SlipRegularItemCriteria)
            {
                rptCriteria = criteria as SlipRegularItemCriteria;

            }
            else rptCriteria = new SlipRegularItemCriteria();

            inst.Criteria = rptCriteria;
            inst.DataSources.Clear();

            List<SlipRegularItem> items = new List<SlipRegularItem>();
            for (int i = 0; i < rptCriteria.count; i++)
            {
                SlipRegularItem item = new SlipRegularItem();
                item.Box = i.ToString("n0");
                item.Case = "case type: " + i.ToString("n0");
                item.Cause = "";
                items.Add(item);
            }
            /*
            List<SlipRegularItem> items = Operations.Instance.SearchForRegularSlip(
                rptCriteria.PackingMonth,
                rptCriteria.RemovalNo, rptCriteria.boxId,
                rptCriteria.FZNo, rptCriteria.PartNo,
                rptCriteria.Model, rptCriteria.RefNo);
            */

            // assign new data source
            RdlcReportDataSource mainDS = new RdlcReportDataSource();
            mainDS.Name = "main"; // the datasource name in the rdlc report.
            mainDS.Items = items; // setup data source
            // Add to datasources
            inst.DataSources.Add(mainDS);


            // Add parameters
            /*
            DateTime today = DateTime.Now;

            if (null != items && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.PrintSlipDate.HasValue)
                    {
                        today = item.PrintSlipDate.Value; // today match first found item.
                        break;
                    }
                }
            }

            string printDate = today.ToString("dddd, MMMM dd, yyyy HH:mm:ss",
                System.Globalization.DateTimeFormatInfo.InvariantInfo);
            inst.Parameters.Add(RdlcReportParameter.Create("PrintDate", printDate));
            */

            return inst;
        }
    }
}
