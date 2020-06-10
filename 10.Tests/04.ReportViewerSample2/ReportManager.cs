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

    #region SlipRegularItem

    /// <summary>
    /// Slip Regular Item for report.
    /// </summary>
    public class TotalIncomeSlip
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets CheckPointName.
        /// </summary>
        public string CheckPointName
        { get; set; }
        /// <summary>
        /// Gets or sets CheckPointCode.
        /// </summary>
        public string CheckPointCode { get; set; }
        /// <summary>
        /// Gets or sets EmpCode.
        /// </summary>
        public string EmpCode { get; set; }
        public string EmpName { get; set; }

        public string Lane { get; set; }

        public string Shift { get; set; }

        public string StartShift { get; set; }

        public string EndShift { get; set; }

        public string MoneyBagNo { get; set; }

        public decimal? Baht1Qty { get; set; }

        public decimal? Baht2Qty { get; set; }

        public decimal? Baht5Qty { get; set; }

        public decimal? Baht10Qty { get; set; }

        public decimal? Baht20Qty { get; set; }

        public decimal? Baht50Qty { get; set; }

        public decimal? Baht100Qty { get; set; }

        public decimal? Baht500Qty { get; set; }

        public decimal? Baht1000Qty { get; set; }

        public decimal? Baht1Total { get; set; }

        public decimal? Baht2Total { get; set; }

        public decimal? Baht5Total { get; set; }

        public decimal? Baht10Total { get; set; }

        public decimal? Baht20Total { get; set; }

        public decimal? Baht50Total { get; set; }

        public decimal? Baht100Total { get; set; }

        public decimal? Baht500Total { get; set; }

        public decimal? Baht1000Total { get; set; }

        public decimal? BahtTotal { get; set; }

        public decimal? Coupon35Sell { get; set; }

        public decimal? Coupon80Sell { get; set; }

        public decimal? Coupon35SellTotal { get; set; }

        public decimal? Coupon80SellTotal { get; set; }

        public decimal? CouponSellTotal { get; set; }
        public decimal? Coupon30Use { get; set; }
        public decimal? Coupon35Use { get; set; }

        public decimal? Coupon75Use { get; set; }

        public decimal? Coupon80Use { get; set; }

        public decimal? CouponFreeUse { get; set; }

        public decimal? CouponUseTotal { get; set; }

        public decimal? OtherIncome { get; set; }

        public string UserName1 { get; set; }

        public string Position1 { get; set; }

        public string UserName2 { get; set; }

        public string Position2 { get; set; }

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
                inst.Definition.EmbededReportName = "ReportViewerSample2.Report1.rdlc";
            }
            else
            {
                inst.Definition.EmbededReportName = "ReportViewerSample2.Report1.rdlc";
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
            mainDS.Name = "DataSet1"; // the datasource name in the rdlc report.
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
