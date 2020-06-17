using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.Runtime.InteropServices;

namespace DMT.Models.Domains
{
    #region Objects

    #region TSB

    /// <summary>
    /// The TSB Data Model class.
    /// </summary>
    //[Table("TSB")]
    public class TSB
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSB() : base() { }

        #endregion

        #region Public Proprties

        [PrimaryKey, MaxLength(10)]
        public string TSBId { get; set; }
        [MaxLength(10)]
        public string NetworkId { get; set; }

        [MaxLength(100)]
        public string TSBNameEN { get; set; }
        [MaxLength(100)]
        public string TSBNameTH { get; set; }

        [OneToMany]
        public List<Plaza> Plazas { get; set; }

        #endregion
    }

    #endregion

    #region Plaza

    /// <summary>
    /// The Plaza Data Model class.
    /// </summary>
    //[Table("Plaza")]
    public class Plaza
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Plaza() : base() { }

        #endregion

        #region Public Proprties

        [PrimaryKey, MaxLength(10)]
        public string PlazaId { get; set; }

        [ForeignKey(typeof(TSB)), MaxLength(10)]
        public string TSBId { get; set; }
        [ManyToOne] 
        public TSB TSB { get; set; }

        [MaxLength(100)]
        public string PlazaNameEN { get; set; }
        [MaxLength(100)]
        public string PlazaNameTH { get; set; }

        [MaxLength(10)]
        public string Direction { get; set; }

        #endregion
    }

    #endregion

    #endregion

    #region Reports

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

        #region Public Proprties

        #endregion
    }

    #endregion

    #endregion
}
