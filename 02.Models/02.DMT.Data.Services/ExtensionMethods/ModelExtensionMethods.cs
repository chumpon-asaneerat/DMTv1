﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Models
{
    #region Data Model to/from Object Model Extension Methods

    /// <summary>
    /// The Data Model to/from Object Model Extension Methods.
    /// </summary>
    public static class ObjectExtensionMethods
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
    /*
    /// <summary>
    /// The Data Model to/from Report Model Extension Methods.
    /// </summary>
    public static class ReportExtensionMethods
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
    */
    #endregion
}
