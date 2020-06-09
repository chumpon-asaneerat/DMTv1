#region Using

using System;
using System.Collections.Generic;

using Microsoft.Reporting.WinForms;
using NLib.Controls;
using NLib.Reports.Rdlc;

#endregion

namespace DMT.TOD.Reports
{
    /// <summary>
    /// Report Manager (TOD).
    /// </summary>
    public class ReportManager : RdlcReportManager
    {
        #region Singelton

        private static ReportManager _instance = null;
        /// <summary>
        /// Singelton Access instance.
        /// </summary>
        public static ReportManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ReportManager))
                    {
                        _instance = new ReportManager();
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
        private ReportManager() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~ReportManager()
        {

        }

        #endregion

        #region Public Methods

        #endregion
    }
}
