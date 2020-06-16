using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Models.Domains
{
    #region Plaza

    /// <summary>
    /// The Plaza Data Model class.
    /// </summary>
    public class Plaza
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Plaza() : base() { }

        #endregion
    }

    #endregion

    #region Revenue Slip class

    /// <summary>
    /// The RevenueSlip Data Model class.
    /// </summary>
    public class RevenueSlip
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueSlip() : base() { }

        #endregion
    }

    #endregion
}

namespace DMT.Models.Domains
{
    #region Data Model to/from Object Model Extension Methods

    /// <summary>
    /// The Data Model to/from Object Model Extension Methods.
    /// </summary>
    public static class DataModelToObjectExtensionMethods
    {
        public static Objects.Plaza ToObject(this Domains.Plaza value)
        {
            if (null == value) return null;
            return null;
        }

        public static void Test()
        {
            Domains.Plaza item = null;
            var result = item.ToObject();
        }
    }

    #endregion

    #region Data Model to/from Report Model Extension Methods

    /// <summary>
    /// The Data Model to/from Report Model Extension Methods.
    /// </summary>
    public static class DataModelToReportExtensionMethods
    {
        public static Reports.RevenueSlip ToReport(this Domains.RevenueSlip value)
        {
            if (null == value) return null;
            return null;
        }

        public static void Test()
        {
            Domains.RevenueSlip item = null;
            var result = item.ToReport();
        }
    }

    #endregion
}
