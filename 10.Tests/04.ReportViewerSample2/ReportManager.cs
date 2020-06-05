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
    /// <summary>
    /// Windows Forms Report Viewer Control Extension Methods for Export Excel.
    /// </summary>
    public static class ExcelReportExtensionMethods
    {
        /// <summary>
        /// Load Report.
        /// </summary>
        /// <param name="rptViewer">The instance of Report Viewer Control.</param>
        /// <param name="reportSource">The instance of RdlcReportModel.</param>
        public static void LoadReport(this ReportViewer rptViewer,
            RdlcReportModel reportSource)
        {
            rptViewer.ProcessingMode = ProcessingMode.Local;
            LocalReport lr = rptViewer.LocalReport;

            if (null != reportSource && null != reportSource.Definition &&
                null != reportSource.Definition.RdlcInstance &&
                null != reportSource.DataSources)
            {
                // Load Rdlc file
                lr.LoadReportDefinition(reportSource.Definition.RdlcInstance);

                // Clear all datasource before assign new one.
                lr.DataSources.Clear();

                if (reportSource.DataSources.Count > 0)
                {
                    // Set all data source.
                    foreach (var dataSource in reportSource.DataSources)
                    {
                        if (null == dataSource.Items)
                            continue;
                        lr.DataSources.Add(new ReportDataSource(
                            dataSource.Name, dataSource.Items));
                    }
                }

                if (lr.DataSources.Count > 0 &&
                    null != reportSource.Parameters && reportSource.Parameters.Count > 0)
                {
                    foreach (RdlcReportParameter para in reportSource.Parameters)
                    {
                        if (null == para || string.IsNullOrWhiteSpace(para.Name))
                            continue;
                        try
                        {
                            lr.SetValue(para.Name, para.Value);
                        }
                        catch (Exception) { }
                    }
                }

                if (lr.DataSources.Count > 0)
                {
                    // Read Setting and set to report viewer
                    ReportPageSettings rdlcPageSettings = lr.GetDefaultPageSettings();
                    System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();

                    pageSettings.PaperSize = rdlcPageSettings.PaperSize;
                    pageSettings.Landscape = rdlcPageSettings.IsLandscape;
                    pageSettings.Margins = rdlcPageSettings.Margins;

                    rptViewer.SetPageSettings(pageSettings);
                    // refresh
                    rptViewer.LocalReport.Refresh();
                }
            }

            rptViewer.RefreshReport();

            if (null != reportSource && null != reportSource.Definition &&
                null != reportSource.Definition.RdlcInstance)
            {
                try
                {
                    reportSource.Definition.Dispose();
                }
                catch { }
            }
        }
        /// <summary>
        /// Show Excel Save Dialog.
        /// </summary>
        /// <param name="rptViewer">The instance of ReportViewer.</param>
        /// <param name="defaultFileName">The default file name.</param>
        /// <returns>Returns file name that user enter.</returns>
        public static string ShowSaveExcelDialog(this ReportViewer rptViewer,
            string defaultFileName)
        {
            string fileName = string.Empty;
            try
            {
                System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                sd.Title = "Export To Excel";
                sd.FileName = defaultFileName;
                sd.Filter = "Excel Files (*.xls)|*.xls";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = sd.FileName;
                }
                sd.Dispose();
                sd = null;
            }
            catch (Exception)
            {
                fileName = string.Empty;
            }
            return fileName;
        }
        /// <summary>
        /// Export To Excel.
        /// </summary>
        /// <param name="rptViewer">The instance of ReportViewer.</param>
        /// <param name="fileName">The target file name.</param>
        /// <returns>Returns true if export success.</returns>
        public static bool ExportToExcel(this ReportViewer rptViewer, string fileName)
        {
            bool success = false;

            #region Checks

            if (null == rptViewer)
                return success;
            LocalReport lr = rptViewer.LocalReport;
            if (null == lr)
                return success;

            #endregion

            byte[] bytes = null;
            System.IO.FileStream fs = null;

            try
            {
                #region Render to EXCEL

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                bytes = lr.Render("EXCEL", null, out mimeType,
                    out encoding, out extension, out streamids, out warnings);

                #endregion
            }
            catch (Exception)
            {
                bytes = null;
                success = false;
            }
            if (null != bytes && bytes.Length > 0)
            {
                #region Create File Stream

                try
                {
                    fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                }
                catch (Exception)
                {
                    success = false;
                    try { fs.Dispose(); }
                    catch { }
                    finally { fs = null; }
                }

                #endregion

                #region Write output byte to Stream

                if (null != fs)
                {
                    try
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Flush();
                        success = true;
                    }
                    catch (Exception)
                    {
                        success = false;
                    }
                }

                #endregion

                #region Free File Stream

                if (null != fs)
                {
                    try { fs.Dispose(); }
                    catch { }
                }
                fs = null;

                #endregion
            }

            return success;
        }
    }


    #region SlipRegularItem

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
