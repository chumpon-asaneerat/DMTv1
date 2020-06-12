#region Using

using System;
using System.Collections.Generic;

using NLib.Reports.Rdlc;

#endregion

namespace DMT.TOD.Reports.Slip
{
    /// <summary>
    /// Revenue Slip Criteria class.
    /// </summary>
    public class SlipCriteria : RdlcReportCriteria
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SlipCriteria() : base()
        {
        }

        #endregion

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
    }
    /// <summary>
    /// Revenue Slip Item class.
    /// </summary>
    public class SlipItem 
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SlipItem() : base()
        {
        }

        #endregion
    }
}
