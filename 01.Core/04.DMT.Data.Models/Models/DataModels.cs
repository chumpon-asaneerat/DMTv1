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

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Plaza> Plazas { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static TSB Create()
        {
            return new TSB() { Plazas = new List<Plaza>() };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(SQLiteConnection db, TSB value)
        {
            if (null == db || null == value) return false;
            var item = (from p in db.Table<TSB>()
                        where p.TSBId == value.TSBId
                        select p).FirstOrDefault();
            return (null != item);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        public static void Save(SQLiteConnection db, TSB value)
        {
            if (null == db || null == value) return;
            if (!Exists(db, value))
            {
                db.Insert(value);
            }
            else db.Update(value);
            // save children.
            if (null != value.Plazas)
            {
                foreach (var plaza in value.Plazas)
                {
                    Plaza.Save(db, plaza);
                }
            }
            // udpate all children item
            db.UpdateWithChildren(value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<TSB> Gets(SQLiteConnection db, bool recursive = false)
        {
            return db.GetAllWithChildren<TSB>(recursive: recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="TSBId">The TSBId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static TSB Get(SQLiteConnection db, string TSBId, bool recursive = false)
        {
            return db.GetAllWithChildren<TSB>(
                p => p.TSBId == TSBId, 
                recursive: recursive).FirstOrDefault();
        }

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
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly=true)] 
        public TSB TSB { get; set; }

        [MaxLength(100)]
        public string PlazaNameEN { get; set; }
        [MaxLength(100)]
        public string PlazaNameTH { get; set; }

        [MaxLength(10)]
        public string Direction { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Lane> Lanes { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static Plaza Create()
        {
            return new Plaza() { Lanes = new List<Lane>() };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(SQLiteConnection db, Plaza value)
        {
            if (null == db || null == value) return false;
            var item = (from p in db.Table<Plaza>()
                        where p.PlazaId == value.PlazaId
                        select p).FirstOrDefault();
            return (null != item);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        public static void Save(SQLiteConnection db, Plaza value)
        {
            if (null == db || null == value) return;
            if (!Exists(db, value))
            {
                db.Insert(value);
            }
            else db.Update(value);
            // save children.
            if (null != value.Lanes)
            {
                foreach (var lane in value.Lanes)
                {
                    Lane.Save(db, lane);
                }
            }
            // udpate all children item
            db.UpdateWithChildren(value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<Plaza> Gets(SQLiteConnection db, bool recursive = false)
        {
            return db.GetAllWithChildren<Plaza>(recursive: recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Plaza Get(SQLiteConnection db, string PlazaId, bool recursive = false)
        {
            return db.GetAllWithChildren<Plaza>(
                p => p.PlazaId == PlazaId, 
                recursive: recursive).FirstOrDefault();
        }

        #endregion
    }

    #endregion

    #region Lane

    /// <summary>
    /// The Lane Data Model class.
    /// </summary>
    //[Table("Lane")]
    public class Lane
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Lane() : base() { }

        #endregion

        #region Public Proprties

        [PrimaryKey, AutoIncrement]
        public int LanePkId { get; set; }

        [MaxLength(10)]
        public int LaneId { get; set; }
        [MaxLength(10)]
        public string LaneType { get; set; }
        [MaxLength(10)]
        public string LaneAbbr { get; set; }

        [ForeignKey(typeof(TSB)), MaxLength(10)]
        public string PlazaId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Plaza Plaza { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static Lane Create()
        {
            return new Lane() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(SQLiteConnection db, Lane value)
        {
            if (null == db || null == value) return false;
            var item = (from p in db.Table<Lane>()
                        where p.PlazaId == value.PlazaId && p.LaneId == value.LaneId
                        select p).FirstOrDefault();
            return (null != item);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        public static void Save(SQLiteConnection db, Lane value)
        {
            if (null == db || null == value) return;
            if (!Exists(db, value))
            {
                db.Insert(value);
            }
            else db.Update(value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<Lane> Gets(SQLiteConnection db, bool recursive = false)
        {
            return db.GetAllWithChildren<Lane>(recursive: recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Lane Get(SQLiteConnection db, string PlazaId, int LaneId, bool recursive = false)
        {
            return db.GetAllWithChildren<Lane>(
                p => p.PlazaId == PlazaId && 
                p.LaneId == LaneId, 
                recursive: recursive).FirstOrDefault();
        }

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
