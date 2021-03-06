﻿#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.ComponentModel;
using DMT.Services;
// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;

#endregion

namespace DMT.Models.Domains
{
    #region TSB

    /// <summary>
    /// The TSB Data Model class.
    /// </summary>
    //[Table("TSB")]
    public class TSB : DMTModelBase
    {
        #region Intenral Variables

        private string _TSBId = string.Empty;
        private string _NetworkId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSB() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId 
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }
        /// <summary>
        /// Gets or sets NetworkId.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NetworkId")]
        public string NetworkId
        {
            get
            {
                return _NetworkId;
            }
            set
            {
                if (_NetworkId != value)
                {
                    _NetworkId = value;
                    this.RaiseChanged("NetworkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameEN.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameEN")]
        public string TSBNameEN
        {
            get
            {
                return _TSBNameEN;
            }
            set
            {
                if (_TSBNameEN != value)
                {
                    _TSBNameEN = value;
                    this.RaiseChanged("TSBNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameTH.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameTH")]
        public string TSBNameTH
        {
            get
            {
                return _TSBNameTH;
            }
            set
            {
                if (_TSBNameTH != value)
                {
                    _TSBNameTH = value;
                    this.RaiseChanged("TSBNameTH");
                }
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Plaza> Plazas { get; set; }

        #endregion

        #region Static Methods

        private static object sync = new object();

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
        internal static bool Exists(SQLiteConnection db, TSB value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<TSB>()
                            where p.TSBId == value.TSBId
                            select p).FirstOrDefault();
                return (null != item);
            }

        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, TSB value)
        {
            lock (sync)
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
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<TSB> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<TSB>();
                return db.GetAllWithChildren<TSB>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="TSBId">The TSBId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static TSB Get(SQLiteConnection db, string TSBId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<TSB>(
                    p => p.TSBId == TSBId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<TSB>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(TSB value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(TSB value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<TSB> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="TSBId">The TSBId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static TSB Get(string TSBId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, TSBId, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region Plaza

    /// <summary>
    /// The Plaza Data Model class.
    /// </summary>
    //[Table("Plaza")]
    public class Plaza : DMTModelBase
    {
        #region Intenral Variables

        private string _PlazaId = string.Empty;
        private string _TSBId = string.Empty;
        private string _PlazaNameEN = string.Empty;
        private string _PlazaNameTH = string.Empty;
        private string _Direction = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Plaza() : base() { }

        #endregion

        #region Public Proprties
        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBId
        /// </summary>
        [ForeignKey(typeof(TSB)), MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly=true)] 
        public TSB TSB { get; set; }

        /// <summary>
        /// Gets or sets PlazaNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameEN")]
        public string PlazaNameEN
        {
            get
            {
                return _PlazaNameEN;
            }
            set
            {
                if (_PlazaNameEN != value)
                {
                    _PlazaNameEN = value;
                    this.RaiseChanged("PlazaNameEN");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaNameTH
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameTH")]
        public string PlazaNameTH
        {
            get
            {
                return _PlazaNameTH;
            }
            set
            {
                if (_PlazaNameTH != value)
                {
                    _PlazaNameTH = value;
                    this.RaiseChanged("PlazaNameTH");
                }
            }
        }

        /// <summary>
        /// Gets or sets Direction
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("Direction")]
        public string Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if (_Direction != value)
                {
                    _Direction = value;
                    this.RaiseChanged("Direction");
                }
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Lane> Lanes { get; set; }

        #endregion

        #region Static Methods

        private static object sync = new object();

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
        internal static bool Exists(SQLiteConnection db, Plaza value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<Plaza>()
                            where p.PlazaId == value.PlazaId
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, Plaza value)
        {
            lock (sync)
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
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<Plaza> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<Plaza>();
                return db.GetAllWithChildren<Plaza>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Plaza Get(SQLiteConnection db, string PlazaId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Plaza>(
                    p => p.PlazaId == PlazaId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<Plaza>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(Plaza value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(Plaza value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<Plaza> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Plaza Get(string PlazaId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, PlazaId, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region Shift

    /// <summary>
    /// The Shift Data Model class.
    /// </summary>
    //[Table("Shift")]
    public class Shift : DMTModelBase
    {
        #region Intenral Variables

        private int _ShiftId = 0;
        private string _NameTH = string.Empty;
        private string _NameEN = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Shift() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets ShiftId.
        /// </summary>
        [PrimaryKey]
        [PeropertyMapName("ShiftId")]
        public int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                if (_ShiftId != value)
                {
                    _ShiftId = value;
                    this.RaiseChanged("ShiftId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Name TH.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NameTH")]
        public string NameTH
        {
            get
            {
                return _NameTH;
            }
            set
            {
                if (_NameTH != value)
                {
                    _NameTH = value;
                    this.RaiseChanged("NameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Name EN.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NameEN")]
        public string NameEN
        {
            get
            {
                return _NameEN;
            }
            set
            {
                if (_NameEN != value)
                {
                    _NameEN = value;
                    this.RaiseChanged("NameEN");
                }
            }
        }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static Shift Create()
        {
            return new Shift() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, Shift value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<Shift>()
                            where p.ShiftId == value.ShiftId
                            select p).FirstOrDefault();
                return (null != item);
            }

        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, Shift value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
                // udpate
                db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<Shift> Gets(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return new List<Shift>();
                return db.Table<Shift>().ToList();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="shiftId">The TSBId.</param>
        /// <returns>Returns found record.</returns>
        internal static Shift Get(SQLiteConnection db, int shiftId)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Shift>(
                    p => p.ShiftId == shiftId).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<Shift>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(Shift value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(Shift value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <returns>Returns List of all records</returns>
        public static List<Shift> Gets()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="shiftId">The shiftId.</param>
        /// <returns>Returns found record.</returns>
        public static Shift Get(int shiftId)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, shiftId);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region Lane

    /// <summary>
    /// The Lane Data Model class.
    /// </summary>
    //[Table("Lane")]
    public class Lane : DMTModelBase
    {
        #region Intenral Variables

        private int _LanePkId = 0;
        private int _LaneId = 0;
        private string _LaneType = string.Empty;
        private string _LaneAbbr = string.Empty;
        private string _PlazaId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Lane() : base() { }

        #endregion

        #region Public Proprties
        /// <summary>
        /// Gets or sets LanePkId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("LanePkId")]
        public int LanePkId
        {
            get
            {
                return _LanePkId;
            }
            set
            {
                if (_LanePkId != value)
                {
                    _LanePkId = value;
                    this.RaiseChanged("LanePkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneId")]
        public int LaneId
        {
            get
            {
                return _LaneId;
            }
            set
            {
                if (_LaneId != value)
                {
                    _LaneId = value;
                    this.RaiseChanged("LaneId");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneType
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneType")]
        public string LaneType
        {
            get
            {
                return _LaneType;
            }
            set
            {
                if (_LaneType != value)
                {
                    _LaneType = value;
                    this.RaiseChanged("LaneType");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneAbbr
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneAbbr")]
        public string LaneAbbr
        {
            get
            {
                return _LaneAbbr;
            }
            set
            {
                if (_LaneAbbr != value)
                {
                    _LaneAbbr = value;
                    this.RaiseChanged("LaneAbbr");
                }
            }
        }
        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [ForeignKey(typeof(TSB)), MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Plaza Plaza { get; set; }

        #endregion

        #region Static Methods

        private static object sync = new object();

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
        internal static bool Exists(SQLiteConnection db, Lane value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<Lane>()
                            where p.PlazaId == value.PlazaId && p.LaneId == value.LaneId
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, Lane value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<Lane> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<Lane>();
                return db.GetAllWithChildren<Lane>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="LaneId">The LaneId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Lane Get(SQLiteConnection db, string PlazaId, int LaneId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Lane>(
                    p => p.PlazaId == PlazaId &&
                    p.LaneId == LaneId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<Lane>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(Lane value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(Lane value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<Lane> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="LaneId">The LaneId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Lane Get(string PlazaId, int LaneId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, PlazaId, LaneId, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region Role

    /// <summary>
    /// The Role Data Model Class.
    /// </summary>
    //[Table("Role")]
    public class Role : DMTModelBase
    {
        #region Intenral Variables

        private string _RoleId = string.Empty;
        private string _RoleName = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Role() : base() { }

        #endregion

        #region Public Proprties
        /// <summary>
        /// Gets or sets RoleId
        /// </summary>
        [PrimaryKey, MaxLength(20)]
        [PeropertyMapName("RoleId")]
        public string RoleId
        {
            get
            {
                return _RoleId;
            }
            set
            {
                if (_RoleId != value)
                {
                    _RoleId = value;
                    this.RaiseChanged("RoleId");
                }
            }
        }
        /// <summary>
        /// Gets or sets RoleName
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("RoleName")]
        public string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                if (_RoleName != value)
                {
                    _RoleName = value;
                    this.RaiseChanged("RoleName");
                }
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<User> Users { get; set; }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static Role Create()
        {
            return new Role() { Users = new List<User>() };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, Role value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<Role>()
                            where p.RoleId == value.RoleId
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, Role value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
                // save children.
                if (null != value.Users)
                {
                    foreach (var user in value.Users)
                    {
                        User.Save(db, user);
                    }
                }
                // udpate all children item
                db.UpdateWithChildren(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<Role> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<Role>();
                return db.GetAllWithChildren<Role>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="RoleId">The RoleId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Role Get(SQLiteConnection db, string RoleId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Role>(
                    p => p.RoleId == RoleId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<Role>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(Role value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(Role value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<Role> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="RoleId">The RoleId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Role Get(string RoleId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, RoleId, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region User

    /// <summary>
    /// The User Data Model Class.
    /// </summary>
    //[Table("User")]
    public class User : DMTModelBase
    {
        #region Intenral Variables

        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;
        private string _UserName = string.Empty;
        private string _Password = string.Empty;
        private string _CardId = string.Empty;
        private string _RoleId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public User() : base() { }

        #endregion

        #region Public Proprties
        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("UserId")]
        public string UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if (_UserId != value)
                {
                    _UserId = value;
                    this.RaiseChanged("UserId");
                }
            }
        }

        /// <summary>
        /// Gets or sets FullNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("FullNameEN")]
        public string FullNameEN
        {
            get
            {
                return _FullNameEN;
            }
            set
            {
                if (_FullNameEN != value)
                {
                    _FullNameEN = value;
                    this.RaiseChanged("FullNameEN");
                }
            }
        }

        /// <summary>
        /// Gets or sets FullNameTH
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("FullNameTH")]
        public string FullNameTH
        {
            get
            {
                return _FullNameTH;
            }
            set
            {
                if (_FullNameTH != value)
                {
                    _FullNameTH = value;
                    this.RaiseChanged("FullNameTH");
                }
            }
        }


        /// <summary>
        /// Gets or sets UserName
        /// </summary>
        [MaxLength(20)]
        [PeropertyMapName("UserName")]
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    this.RaiseChanged("UserName");
                }
            }
        }

        /// <summary>
        /// Gets or sets Password
        /// </summary>
        [MaxLength(20)]
        [PeropertyMapName("Password")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    this.RaiseChanged("Password");
                }
            }
        }

        /// <summary>
        /// Gets or sets CardId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("CardId")]
        public string CardId
        {
            get
            {
                return _CardId;
            }
            set
            {
                if (_CardId != value)
                {
                    _CardId = value;
                    this.RaiseChanged("CardId");
                }
            }
        }

        /// <summary>
        /// Gets or sets RoleId
        /// </summary>
        [ForeignKey(typeof(Role)), MaxLength(10)]
        [PeropertyMapName("RoleId")]
        public string RoleId
        {
            get
            {
                return _RoleId;
            }
            set
            {
                if (_RoleId != value)
                {
                    _RoleId = value;
                    this.RaiseChanged("RoleId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Role Role { get; set; }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static User Create()
        {
            return new User() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, User value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<User>()
                            where p.UserId == value.UserId
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, User value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<User> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<User>();
                return db.GetAllWithChildren<User>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id without password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="UserId">The UserId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User Get(SQLiteConnection db, string UserId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.UserId == UserId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by UserId and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="UserId">The UserId.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User GetByUserId(SQLiteConnection db, string UserId, string password, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.UserId == UserId && p.Password == password,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by UserName and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="userName">The userName.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User GetByUserName(SQLiteConnection db, string userName, string password, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.UserName == userName && p.Password == password,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by CardId
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="cardId">The cardId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User GetByCardId(SQLiteConnection db, string cardId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.CardId == cardId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<User>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(User value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(User value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<User> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="UserId">The UserId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User Get(string UserId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, UserId, recursive);
        }
        /// <summary>
        /// Gets by UserId and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="UserId">The UserId.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByUserId(string UserId, string password, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return GetByUserId(db, UserId, password, recursive);
        }
        /// <summary>
        /// Gets by UserName and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="userName">The userName.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByUserName(string userName, string password, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return GetByUserName(db, userName, password, recursive);
        }
        /// <summary>
        /// Gets by CardId
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="cardId">The cardId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByCardId(string cardId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return GetByCardId(db, cardId, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region Config

    /// <summary>
    /// The Config Data Model Class.
    /// </summary>
    [Table("Config")]
    public class Config : DMTModelBase
    {
        #region Intenral Variables

        private string _Key = string.Empty;
        private string _Value = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Config() : base() { }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets Key
        /// </summary>
        [PrimaryKey, MaxLength(20)]
        [PeropertyMapName("Key")]
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                if (_Key != value)
                {
                    _Key = value;
                    this.RaiseChanged("Key");
                }
            }
        }

        /// <summary>
        /// Gets or sets Value
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("Value")]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    this.RaiseChanged("Value");
                }
            }
        }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static Config Create()
        {
            return new Config() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, Config value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<Config>()
                            where p.Key == value.Key
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, Config value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<Config> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<Config>();
                return db.GetAllWithChildren<Config>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Key.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="key">The key.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Config Get(SQLiteConnection db, string key, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Config>(
                    p => p.Key == key,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<Config>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(Config value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(Config value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<Config> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Config Get(string key, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, key, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region SupervisorShift

    /// <summary>
    /// The SupervisorShift Data Model Class.
    /// </summary>
    //[Table("SupervisorShift")]
    public class SupervisorShift : DMTModelBase
    {
        #region Intenral Variables

        private int _SupervisorShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _SupervisorId = string.Empty;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SupervisorShift() : base() { }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets SupervisorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("SupervisorShiftId")]
        public int SupervisorShiftId
        {
            get
            {
                return _SupervisorShiftId;
            }
            set
            {
                if (_SupervisorShiftId != value)
                {
                    _SupervisorShiftId = value;
                    this.RaiseChanged("SupervisorShiftId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }

        /// <summary>
        /// Gets or sets SupervisorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("SupervisorId")]
        public string SupervisorId
        {
            get
            {
                return _SupervisorId;
            }
            set
            {
                if (_SupervisorId != value)
                {
                    _SupervisorId = value;
                    this.RaiseChanged("SupervisorId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "SupervisorId", CascadeOperations = CascadeOperation.All)]        
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
        [PeropertyMapName("Begin")]
        public DateTime Begin
        {
            get { return _Begin; }
            set
            {
                if (_Begin != value)
                {
                    _Begin = value;
                    // Raise event.
                    RaiseChanged("Begin");
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
        [PeropertyMapName("End")]
        public DateTime End
        {
            get { return _End; }
            set
            {
                if (_End != value)
                {
                    _End = value;
                    // Raise event.
                    RaiseChanged("End");
                }
            }
        }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static SupervisorShift Create()
        {
            return new SupervisorShift() { };
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<SupervisorShift> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<SupervisorShift>();
                return db.GetAllWithChildren<SupervisorShift>(recursive: recursive);
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<SupervisorShift>();
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<SupervisorShift> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region CollectorJob

    /// <summary>
    /// The CollectorJob Data Model Class.
    /// </summary>
    //[Table("CollectorJob")]
    public class CollectorJob : DMTModelBase
    {
        #region Intenral Variables

        private int _JobId = 0;
        private int _ShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorJob() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets CollectorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("JobId")]
        public int JobId
        {
            get
            {
                return _JobId;
            }
            set
            {
                if (_JobId != value)
                {
                    _JobId = value;
                    this.RaiseChanged("JobId");
                }
            }
        }
        /// <summary>
        /// Gets or sets ShiftId
        /// </summary>
        [PeropertyMapName("ShiftId")]
        public int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                if (_ShiftId != value)
                {
                    _ShiftId = value;
                    this.RaiseChanged("ShiftId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }

        /// <summary>
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
        [PeropertyMapName("Begin")]
        public DateTime Begin
        {
            get { return _Begin; }
            set
            {
                if (_Begin != value)
                {
                    _Begin = value;
                    // Raise event.
                    RaiseChanged("Begin");
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
        [PeropertyMapName("End")]
        public DateTime End
        {
            get { return _End; }
            set
            {
                if (_End != value)
                {
                    _End = value;
                    // Raise event.
                    RaiseChanged("End");
                }
            }
        }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static CollectorJob Create()
        {
            return new CollectorJob() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, CollectorJob value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<CollectorJob>()
                            where p.JobId == value.JobId
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, CollectorJob value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<CollectorJob> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<CollectorJob>();
                return db.GetAllWithChildren<CollectorJob>(recursive: recursive);
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<CollectorJob>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(CollectorJob value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(CollectorJob value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<CollectorJob> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region CollectorShift

    /// <summary>
    /// The CollectorShift Data Model Class.
    /// </summary>
    //[Table("CollectorShift")]
    public class CollectorShift : DMTModelBase
    {
        #region Intenral Variables

        private int _CollectorShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorShift() : base() { }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets CollectorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("CollectorShiftId")]
        public int CollectorShiftId
        {
            get
            {
                return _CollectorShiftId;
            }
            set
            {
                if (_CollectorShiftId != value)
                {
                    _CollectorShiftId = value;
                    this.RaiseChanged("CollectorShiftId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }

        /// <summary>
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
        [PeropertyMapName("Begin")]
        public DateTime Begin
        {
            get { return _Begin; }
            set
            {
                if (_Begin != value)
                {
                    _Begin = value;
                    // Raise event.
                    RaiseChanged("Begin");
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
        [PeropertyMapName("End")]
        public DateTime End
        {
            get { return _End; }
            set
            {
                if (_End != value)
                {
                    _End = value;
                    // Raise event.
                    RaiseChanged("End");
                }
            }
        }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static CollectorShift Create()
        {
            return new CollectorShift() { };
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<CollectorShift> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<CollectorShift>();
                return db.GetAllWithChildren<CollectorShift>(recursive: recursive);
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<CollectorShift>();
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<CollectorShift> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region CollectorLane

    /// <summary>
    /// The CollectorLane Data Model Class.
    /// </summary>
    public class CollectorLane : DMTModelBase
    {
        #region Intenral Variables

        private int _CollectorShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;
        private int _LaneId = 0;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorLane() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets CollectorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("CollectorShiftId")]
        public int CollectorShiftId
        {
            get
            {
                return _CollectorShiftId;
            }
            set
            {
                if (_CollectorShiftId != value)
                {
                    _CollectorShiftId = value;
                    this.RaiseChanged("CollectorShiftId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }
        /// <summary>
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        [PeropertyMapName("LaneId")]
        public int LaneId
        {
            get
            {
                return _LaneId;
            }
            set
            {
                if (_LaneId != value)
                {
                    _LaneId = value;
                    this.RaiseChanged("LaneId");
                }
            }
        }

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
        [PeropertyMapName("Begin")]
        public DateTime Begin
        {
            get { return _Begin; }
            set
            {
                if (_Begin != value)
                {
                    _Begin = value;
                    // Raise event.
                    RaiseChanged("Begin");
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
        [PeropertyMapName("End")]
        public DateTime End
        {
            get { return _End; }
            set
            {
                if (_End != value)
                {
                    _End = value;
                    // Raise event.
                    RaiseChanged("End");
                }
            }
        }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static CollectorLane Create()
        {
            return new CollectorLane() { };
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<CollectorLane> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<CollectorLane>();
                return db.GetAllWithChildren<CollectorLane>(recursive: recursive);
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<CollectorLane>();
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<CollectorLane> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region Revenue Entry

    /// <summary>
    /// The RevenueEntry class.
    /// </summary>
    //[Table("RevenueEntry")]
    public class RevenueEntry : DMTModelBase
    {
        #region Intenral Variables

        // Common
        private int _EntryId = 0;

        private DateTime _EntryDate = DateTime.MinValue;
        private DateTime _RevDate = DateTime.MinValue;
        private int _ShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;

        private string _bagNo = string.Empty;
        private string _bagNo2 = string.Empty;
        // Traffic
        private int _TrafficBHT1 = 0;
        private int _TrafficBHT2 = 0;
        private int _TrafficBHT5 = 0;
        private int _TrafficBHT10 = 0;
        private int _TrafficBHT20 = 0;
        private int _TrafficBHT50 = 0;
        private int _TrafficBHT100 = 0;
        private int _TrafficBHT500 = 0;
        private int _TrafficBHT1000 = 0;
        private decimal _TrafficBHTTotal = decimal.Zero;
        private string _TrafficRemark = "";
        // Other
        private decimal _OtherBHTTotal = decimal.Zero;
        private string _OtherRemark = "";
        // Coupon Usage
        private int _CouponUsageBHT30 = 0;
        private int _CouponUsageBHT35 = 0;
        private int _CouponUsageBHT75 = 0;
        private int _CouponUsageBHT80 = 0;
        private int _CouponUsageFreePassA = 0;
        private int _CouponUsageFreePassOther = 0;
        // Coupon Sold
        private int _CouponSoldBHT35 = 0;
        private int _CouponSoldBHT80 = 0;
        private int _CouponSoldBHT35Total = 0;
        private int _CouponSoldBHT80Total = 0;
        private int _CouponSoldBHTTotal = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntry() : base() { }

        #endregion

        #region Private Methods

        private void CalcTrafficTotal()
        {
            decimal total = 0;
            total += _TrafficBHT1 * 1;
            total += _TrafficBHT2 * 2;
            total += _TrafficBHT5 * 5;
            total += _TrafficBHT10 * 10;
            total += _TrafficBHT20 * 20;
            total += _TrafficBHT50 * 50;
            total += _TrafficBHT100 * 100;
            total += _TrafficBHT500 * 500;
            total += _TrafficBHT1000 * 1000;

            _TrafficBHTTotal = total;
            // Raise event.
            this.RaiseChanged("TrafficBHTTotal");
        }

        private void CalcCouponSoldTotal()
        {
            // Raise event.
            RaiseChanged("CntTotal");

            int total = 0;
            total += _CouponSoldBHT35Total;
            total += _CouponSoldBHT80Total;

            _CouponSoldBHTTotal = total;
            // Raise event.
            this.RaiseChanged("CouponSoldBHTTotal");
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets EntryId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("EntryId")]
        public int EntryId
        {
            get
            {
                return _EntryId;
            }
            set
            {
                if (_EntryId != value)
                {
                    _EntryId = value;
                    this.RaiseChanged("EntryId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Entry Date.
        /// </summary>
        [PeropertyMapName("EntryDate")]
        public DateTime EntryDate
        {
            get { return _EntryDate; }
            set
            {
                if (_EntryDate != value)
                {
                    _EntryDate = value;
                    // Raise event.
                    this.RaiseChanged("EntryDate");
                }
            }
        }
        /// <summary>
        /// Gets or sets Revenue Date.
        /// </summary>
        [PeropertyMapName("RevDate")]
        public DateTime RevDate
        {
            get { return _RevDate; }
            set
            {
                if (_RevDate != value)
                {
                    _RevDate = value;
                    // Raise event.
                    this.RaiseChanged("RevDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets ShiftId
        /// </summary>
        [ForeignKey(typeof(Shift), Name = "ShiftId")]
        [PeropertyMapName("ShiftId")]
        public int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                if (_ShiftId != value)
                {
                    _ShiftId = value;
                    this.RaiseChanged("ShiftId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "ShiftId", CascadeOperations = CascadeOperation.All)]
        public Shift Shift { get; set; }

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }
        /// <summary>
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Bag Number.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("BagNo")]
        public string BagNo
        {
            get { return _bagNo; }
            set
            {
                if (_bagNo != value)
                {
                    _bagNo = value;
                    // Raise event.
                    this.RaiseChanged("BagNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets Bag Number (2).
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("BagNo2")]
        public string BagNo2
        {
            get { return _bagNo2; }
            set
            {
                if (_bagNo2 != value)
                {
                    _bagNo2 = value;
                    // Raise event.
                    this.RaiseChanged("BagNo2");
                }
            }
        }

        #endregion

        #region Traffic

        /// <summary>
        /// Gets or sets number of 1 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT1")]
        public int TrafficBHT1
        {
            get { return _TrafficBHT1; }
            set
            {
                if (_TrafficBHT1 != value)
                {
                    _TrafficBHT1 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT1BHT1");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 2 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT2")]
        public int TrafficBHT2
        {
            get { return _TrafficBHT2; }
            set
            {
                if (_TrafficBHT2 != value)
                {
                    _TrafficBHT2 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT2");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 5 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT5")]
        public int TrafficBHT5
        {
            get { return _TrafficBHT5; }
            set
            {
                if (_TrafficBHT5 != value)
                {
                    _TrafficBHT5 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT5");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 10 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT10")]
        public int TrafficBHT10
        {
            get { return _TrafficBHT10; }
            set
            {
                if (_TrafficBHT10 != value)
                {
                    _TrafficBHT10 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT10");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 20 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT20")]
        public int TrafficBHT20
        {
            get { return _TrafficBHT20; }
            set
            {
                if (_TrafficBHT20 != value)
                {
                    _TrafficBHT20 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT20");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 50 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT50")]
        public int TrafficBHT50
        {
            get { return _TrafficBHT50; }
            set
            {
                if (_TrafficBHT50 != value)
                {
                    _TrafficBHT50 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT50");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 100 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT100")]
        public int TrafficBHT100
        {
            get { return _TrafficBHT100; }
            set
            {
                if (_TrafficBHT100 != value)
                {
                    _TrafficBHT100 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT100");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 500 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT500")]
        public int TrafficBHT500
        {
            get { return _TrafficBHT500; }
            set
            {
                if (_TrafficBHT500 != value)
                {
                    _TrafficBHT500 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT500");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 1000 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT1000")]
        public int TrafficBHT1000
        {
            get { return _TrafficBHT1000; }
            set
            {
                if (_TrafficBHT1000 != value)
                {
                    _TrafficBHT1000 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT1000");
                }
            }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [PeropertyMapName("TrafficBHTTotal")]
        public decimal TrafficBHTTotal
        {
            get { return _TrafficBHTTotal; }
            set { }
        }

        /// <summary>
        /// Gets or sets Traffic Remark.
        /// </summary>
        [MaxLength(255)]
        [PeropertyMapName("TrafficRemark")]
        public string TrafficRemark
        {
            get { return _TrafficRemark; }
            set
            {
                if (_TrafficRemark != value)
                {
                    _TrafficRemark = value;
                    // Raise event.
                    //this.RaiseChanged("Remark");
                }
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// Gets or sets total value in baht (Other).
        /// </summary>
        [PeropertyMapName("OtherBHTTotal")]
        public decimal OtherBHTTotal
        {
            get { return _OtherBHTTotal; }
            set
            {
                if (_OtherBHTTotal != value)
                {
                    _OtherBHTTotal = value;
                    // Raise event.
                    this.RaiseChanged("OtherBHTTotal");
                }
            }
        }
        /// <summary>
        /// Gets or sets Other Remark.
        /// </summary>
        [MaxLength(255)]
        [PeropertyMapName("OtherRemark")]
        public string OtherRemark
        {
            get { return _OtherRemark; }
            set
            {
                if (_OtherRemark != value)
                {
                    _OtherRemark = value;
                    // Raise event.
                    //this.RaiseChanged("Remark");
                }
            }
        }

        #endregion

        #region Coupon Usage

        /// <summary>
        /// Gets or sets number of 30 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT30")]
        public int CouponUsageBHT30
        {
            get { return _CouponUsageBHT30; }
            set
            {
                if (_CouponUsageBHT30 != value)
                {
                    _CouponUsageBHT30 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT30");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 35 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT35")]
        public int CouponUsageBHT35
        {
            get { return _CouponUsageBHT35; }
            set
            {
                if (_CouponUsageBHT35 != value)
                {
                    _CouponUsageBHT35 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT35");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 75 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT75")]
        public int CouponUsageBHT75
        {
            get { return _CouponUsageBHT75; }
            set
            {
                if (_CouponUsageBHT75 != value)
                {
                    _CouponUsageBHT75 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT75");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT80")]
        public int CouponUsageBHT80
        {
            get { return _CouponUsageBHT80; }
            set
            {
                if (_CouponUsageBHT80 != value)
                {
                    _CouponUsageBHT80 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT80");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of FreePass Class A (4 wheel).
        /// </summary>
        [PeropertyMapName("CouponUsageFreePassA")]
        public int CouponUsageFreePassA
        {
            get { return _CouponUsageFreePassA; }
            set
            {
                if (_CouponUsageFreePassA != value)
                {
                    _CouponUsageFreePassA = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageFreePassA");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of FreePass Other (> 4 wheel).
        /// </summary>
        [PeropertyMapName("CouponUsageFreePassOther")]
        public int CouponUsageFreePassOther
        {
            get { return _CouponUsageFreePassOther; }
            set
            {
                if (_CouponUsageFreePassOther != value)
                {
                    _CouponUsageFreePassOther = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageFreePassOther");
                }
            }
        }

        #endregion

        #region Coupon Sold

        /// <summary>
        /// Gets or sets number of 35 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponSoldBHT35")]
        public int CouponSoldBHT35
        {
            get { return _CouponSoldBHT35; }
            set
            {
                if (_CouponSoldBHT35 != value)
                {
                    _CouponSoldBHT35 = value;
                    _CouponSoldBHT35Total = _CouponSoldBHT35 * 665;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT35");
                    this.RaiseChanged("CouponSoldBHT35Total");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponSoldBHT80")]
        public int CouponSoldBHT80
        {
            get { return _CouponSoldBHT80; }
            set
            {
                if (_CouponSoldBHT80 != value)
                {
                    _CouponSoldBHT80 = value;
                    _CouponSoldBHT80Total = _CouponSoldBHT80 * 1520;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT80");
                    this.RaiseChanged("CouponSoldBHT80Total");
                }
            }
        }
        [PeropertyMapName("CouponSoldBHT35Total")]
        public int CouponSoldBHT35Total
        {
            get { return _CouponSoldBHT35Total; }
            set { }
        }
        [PeropertyMapName("CouponSoldBHT80Total")]
        public int CouponSoldBHT80Total
        {
            get { return _CouponSoldBHT80Total; }
            set { }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [PeropertyMapName("CouponSoldBHT80Total")]
        public decimal CouponSoldBHTTotal
        {
            get { return _CouponSoldBHTTotal; }
            set { }
        }

        #endregion

        #endregion
    }

    #endregion

    public class StressTest
    {
        [PrimaryKey, MaxLength(50)]
        public string RowId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Amount1 { get; set; }
        public int Amount2 { get; set; }
        public DateTime Updated { get; set; }

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static StressTest Create()
        {
            return new StressTest() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, StressTest value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<StressTest>()
                            where p.RowId == value.RowId
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, StressTest value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<StressTest> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<StressTest>();
                return db.GetAllWithChildren<StressTest>(recursive: recursive);
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<StressTest>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(StressTest value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(StressTest value)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<StressTest> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return DeleteAll(db);
        }

        public class SumAmount
        {
            public int Sum1 { get; set; }
            public int Sum2 { get; set; }
        }

        internal static int Sum(SQLiteConnection db)
        {
            int ret = 0;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += @"SELECT SUM(Amount) AS TotalCnt ";
                cmd += @"  FROM StressTest ";
                cmd += @" WHERE Amount > ? ";
                ret = db.ExecuteScalar<int>(cmd, 0);
            }
            return ret;
        }

        internal static SumAmount Sum2(SQLiteConnection db)
        {
            SumAmount ret = new SumAmount();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += @"SELECT SUM(Amount1) AS Sum1, ";
                cmd += @"       SUM(Amount2) AS Sum2 ";
                cmd += @"  FROM StressTest ";
                ret = db.Query<SumAmount>(cmd).FirstOrDefault();
            }
            return ret;
        }
        public static int Sum()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Sum(db);
        }
        public static SumAmount Sum2()
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Sum2(db);
        }

        #endregion
    }
}

namespace DMT.Models.Domains
{
    // for LocalServer2 only

    #region TSB2

    /// <summary>
    /// The TSB2 Data Model class.
    /// </summary>
    //[Table("TSB2")]
    public class TSB2 : DMTModelBase
    {
        #region Intenral Variables

        private string _TSBId = string.Empty;
        private string _NetworkId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSB2() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }
        /// <summary>
        /// Gets or sets NetworkId.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NetworkId")]
        public string NetworkId
        {
            get
            {
                return _NetworkId;
            }
            set
            {
                if (_NetworkId != value)
                {
                    _NetworkId = value;
                    this.RaiseChanged("NetworkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameEN.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameEN")]
        public string TSBNameEN
        {
            get
            {
                return _TSBNameEN;
            }
            set
            {
                if (_TSBNameEN != value)
                {
                    _TSBNameEN = value;
                    this.RaiseChanged("TSBNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameTH.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameTH")]
        public string TSBNameTH
        {
            get
            {
                return _TSBNameTH;
            }
            set
            {
                if (_TSBNameTH != value)
                {
                    _TSBNameTH = value;
                    this.RaiseChanged("TSBNameTH");
                }
            }
        }

        #endregion

        #region Static Methods

        private static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static TSB2 Create()
        {
            return new TSB2() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, TSB2 value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<TSB2>()
                            where p.TSBId == value.TSBId
                            select p).FirstOrDefault();
                return (null != item);
            }

        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, TSB2 value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<TSB2> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<TSB2>();
                return db.GetAllWithChildren<TSB2>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="TSBId">The TSBId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static TSB2 Get(SQLiteConnection db, string TSBId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<TSB2>(
                    p => p.TSBId == TSBId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<TSB2>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(TSB2 value)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(TSB2 value)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<TSB2> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="TSBId">The TSBId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static TSB2 Get(string TSBId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Get(db, TSBId, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    #endregion

    #region Plaza2

    /// <summary>
    /// The Plaza2 Data Model class.
    /// </summary>
    //[Table("Plaza2")]
    public class Plaza2 : DMTModelBase
    {
        #region Intenral Variables

        private string _PlazaId = string.Empty;
        private string _TSBId = string.Empty;
        private string _PlazaNameEN = string.Empty;
        private string _PlazaNameTH = string.Empty;
        private string _Direction = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Plaza2() : base() { }

        #endregion

        #region Public Proprties
        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBId
        /// </summary>
        //[ForeignKey(typeof(TSB)), MaxLength(10)]
        [MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }
        
        /// <summary>
        /// Gets or sets PlazaNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameEN")]
        public string PlazaNameEN
        {
            get
            {
                return _PlazaNameEN;
            }
            set
            {
                if (_PlazaNameEN != value)
                {
                    _PlazaNameEN = value;
                    this.RaiseChanged("PlazaNameEN");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaNameTH
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameTH")]
        public string PlazaNameTH
        {
            get
            {
                return _PlazaNameTH;
            }
            set
            {
                if (_PlazaNameTH != value)
                {
                    _PlazaNameTH = value;
                    this.RaiseChanged("PlazaNameTH");
                }
            }
        }

        /// <summary>
        /// Gets or sets Direction
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("Direction")]
        public string Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if (_Direction != value)
                {
                    _Direction = value;
                    this.RaiseChanged("Direction");
                }
            }
        }

        #endregion

        #region Static Methods

        protected static object sync = new object();

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance</returns>
        public static Plaza2 Create()
        {
            return new Plaza2() { };
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        internal static bool Exists(SQLiteConnection db, Plaza2 value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                var item = (from p in db.Table<Plaza2>()
                            where p.PlazaId == value.PlazaId
                            select p).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        internal static void Save(SQLiteConnection db, Plaza2 value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else db.Update(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        internal static List<Plaza2> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<Plaza2>();
                return db.GetAllWithChildren<Plaza2>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Plaza2 Get(SQLiteConnection db, string PlazaId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Plaza2>(
                    p => p.PlazaId == PlazaId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        internal static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<Plaza2>();
            }
        }
        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(Plaza2 value)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Exists(db, value);
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(Plaza2 value)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            Save(db, value);
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<Plaza2> Gets(bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Plaza2 Get(string PlazaId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Get(db, PlazaId, recursive);
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return DeleteAll(db);
        }

        #endregion
    }

    // Note: Do not create this table used for query only.
    public class PlazaWithTSB2 : Plaza2
    {
        public string NetworkId { get; set; }
        public string TSBNameTH { get; set; }
        public string TSBNameEN { get; set; }

        internal static List<PlazaWithTSB2> Gets(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return new List<PlazaWithTSB2>();
                //return db.GetAllWithChildren<Plaza2>(recursive: recursive);
                string cmd = string.Empty;
                cmd += "SELECT Plaza2.* ";
                cmd += "      ,TSB2.NetworkId ";
                cmd += "      ,TSB2.TSBNameEN ";
                cmd += "      ,TSB2.TSBNameTH ";
                cmd += "  FROM Plaza2, TSB2";
                cmd += " WHERE Plaza2.TSBId = TSB2.TSBId";
                return db.Query<PlazaWithTSB2>(cmd);
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static PlazaWithTSB2 Get(SQLiteConnection db, string PlazaId)
        {
            lock (sync)
            {
                if (null == db) return null;
                string cmd = string.Empty;
                cmd += "SELECT Plaza2.* ";
                cmd += "      ,TSB2.NetworkId ";
                cmd += "      ,TSB2.TSBNameEN ";
                cmd += "      ,TSB2.TSBNameTH ";
                cmd += "  FROM Plaza2, TSB2";
                cmd += " WHERE Plaza2.TSBId = TSB2.TSBId ";
                cmd += "   AND Plaza2.PlazaId = ?";
                return db.Query<PlazaWithTSB2>(cmd, PlazaId).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <returns>Returns List of all records</returns>
        public static List<PlazaWithTSB2> Gets()
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Gets(db);
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <returns>Returns found record.</returns>
        public static PlazaWithTSB2 Get(string PlazaId)
        {
            SQLiteConnection db = LocalDbServer2.Instance.Db;
            return Get(db, PlazaId);
        }
    }

    #endregion
}

namespace DMT.Models.Domains
{
    // Note:
    // - The Default connection should seperate by Domain class but can initialize with
    //   value assigned in NTable class.
    // - Query static methods (in NTable<T> class) required for custom search/filter.


    #region NTable

    /// <summary>
    /// The NTable abstract class.
    /// </summary>
    public abstract class NTable : DMTModelBase
    {
        #region Static Variables and Properties

        /// <summary>
        /// sync object used for lock concurrent access.
        /// </summary>
        protected static object sync = new object();
        /// <summary>
        /// Gets default Connection.
        /// </summary>
        public static SQLiteConnection Default { get; set; }

        #endregion
    }

    #endregion

    #region NTable<T>

    /// <summary>
    /// The NTable (Generic) abstract class.
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
    public abstract class NTable<T> : NTable
        where T : NTable, new()
    {
        #region Static Methods

        #region Create

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance.</returns>
        public static T Create()
        { 
            return new T();
        }

        #endregion

        #region Used Custom Connection

        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(SQLiteConnection db, T value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                // read mapping information.
                var map = db.GetMapping<T>(CreateFlags.None);
                if (null == map) return false;

                string tableName = map.TableName;
                string columnName = map.PK.Name;
                string propertyName = map.PK.PropertyName;
                // get pk id.
                object Id = PropertyAccess.GetValue(value, propertyName);
                // init query string.
                string cmd = string.Empty;
                cmd += string.Format("SELECT * FROM {0} WHERE {1} = ?", tableName, columnName);
                // execute query.
                var item = db.Query<T>(cmd, Id).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        public static void Save(SQLiteConnection db, T value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                if (!Exists(db, value))
                {
                    db.Insert(value);
                }
                else
                {
                    db.Update(value);
                }
            }
        }
        /// <summary>
        /// Update relationship with children that assigned with relationship attribute.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to load children.</param>
        public static void UpdateWithChildren(SQLiteConnection db, T value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                db.UpdateWithChildren(value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<T> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<T>();
                return db.GetAllWithChildren<T>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static T Get(SQLiteConnection db, object Id, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db || null == Id) return null;
                // read mapping information.
                var map = db.GetMapping<T>(CreateFlags.None);
                if (null == map) return null;

                string tableName = map.TableName;
                string columnName = map.PK.Name;
                string propertyName = map.PK.PropertyName;
                // init query string.
                string cmd = string.Empty;
                cmd += string.Format("SELECT * FROM {0} WHERE {1} = ?", tableName, columnName);
                // execute query.
                T item = db.Query<T>(cmd, Id).FirstOrDefault();
                if (null != item)
                {
                    // read children.
                    db.GetChildren(item, recursive);
                }
                return item;
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<T>();
            }
        }
        /// <summary>
        /// Delete by Id.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        public static void Delete(SQLiteConnection db, object Id, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db || null == Id) return;
                T inst = Get(db, Id, recursive);
                db.Delete(inst, recursive);
            }
        }

        #endregion

        #region Used Default Connection

        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(T value)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Exists(db, value);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(T value)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                Save(db, value);
            }
        }
        /// <summary>
        /// Update relationship with children that assigned with relationship attribute.
        /// </summary>
        /// <param name="value">The item to load children.</param>
        public static void UpdateWithChildren(T value)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                UpdateWithChildren(db, value);
            }
        }
        /// <summary>
        /// Gets All.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<T> Gets(bool recursive = false)
        {
            SQLiteConnection db = Default;
            return Gets(db, recursive);
        }
        /// <summary>
        /// Gets by Id.
        /// </summary>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static T Get(object Id, bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Get(Id, recursive);
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return DeleteAll(db);
            }
        }
        /// <summary>
        /// Delete by Id.
        /// </summary>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        public static void Delete(object Id, bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                Delete(db, recursive);
            }
        }

        #endregion

        #endregion
    }

    #endregion

    #region NQuery

    /// <summary>
    /// The NQuery class.
    /// </summary>
    public class NQuery : DMTModelBase
    {
        #region Static Variables and Properties

        /// <summary>
        /// sync object used for lock concurrent access.
        /// </summary>
        protected static object sync = new object();
        /// <summary>
        /// Gets default Connection.
        /// </summary>
        public static SQLiteConnection Default { get; set; }
        /// <summary>
        /// Query.
        /// </summary>
        /// <typeparam name="T">The Target Class.</typeparam>
        /// <param name="db">The connection.</param>
        /// <param name="query">The query string.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>Returns query result in List.</returns>
        public static List<T> Query<T>(SQLiteConnection db, string query, params object[] args)
            where T : new()
        {
            lock (sync)
            {
                List<T> results = null;
                if (null == db || string.IsNullOrEmpty(query)) return results;
                // execute query.
                results = db.Query<T>(query, args).ToList();
                return results;
            }
        }
        /// <summary>
        /// Query.
        /// </summary>
        /// <typeparam name="T">The Target Class.</typeparam>
        /// <param name="query">The query string.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>Returns query result in List.</returns>
        public static List<T> Query<T>(string query, params object[] args)
            where T : new()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Query<T>(db, query, args);
            }
        }
        /// <summary>
        /// Execute Non Query.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="query">The query string.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>Returns effected row(s) count.</returns>
        public static int Execute(SQLiteConnection db, string query, params object[] args)
        {
            lock (sync)
            {
                int result = 0;
                if (null == db || string.IsNullOrEmpty(query)) return result;
                // execute query.
                result = db.Execute(query, args);
                return result;
            }
        }
        /// <summary>
        /// Execute Non Query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>Returns effected row(s) count.</returns>
        public static int Execute(string query, params object[] args)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Execute(db, query, args);
            }
        }

        #endregion
    }

    #endregion

    [Table("TSB3")]
    public class TSB3 : NTable<TSB3>
    {
        #region Intenral Variables

        private string _TSBId = string.Empty;
        private string _NetworkId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSB3() : base()
        {
            Plazas = new List<Plaza3>();
        }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }
        /// <summary>
        /// Gets or sets NetworkId.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NetworkId")]
        public string NetworkId
        {
            get
            {
                return _NetworkId;
            }
            set
            {
                if (_NetworkId != value)
                {
                    _NetworkId = value;
                    this.RaiseChanged("NetworkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameEN.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameEN")]
        public string TSBNameEN
        {
            get
            {
                return _TSBNameEN;
            }
            set
            {
                if (_TSBNameEN != value)
                {
                    _TSBNameEN = value;
                    this.RaiseChanged("TSBNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameTH.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameTH")]
        public string TSBNameTH
        {
            get
            {
                return _TSBNameTH;
            }
            set
            {
                if (_TSBNameTH != value)
                {
                    _TSBNameTH = value;
                    this.RaiseChanged("TSBNameTH");
                }
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Plaza3> Plazas { get; set; }

        #endregion
    }

    [Table("Plaza3")]
    public class Plaza3 : NTable<Plaza3>
    {
        #region Intenral Variables

        private string _PlazaId = string.Empty;
        private string _TSBId = string.Empty;
        private string _PlazaNameEN = string.Empty;
        private string _PlazaNameTH = string.Empty;
        private string _Direction = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Plaza3() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBId
        /// </summary>
        [ForeignKey(typeof(TSB3)), MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public TSB3 TSB { get; set; }

        /// <summary>
        /// Gets or sets PlazaNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameEN")]
        public string PlazaNameEN
        {
            get
            {
                return _PlazaNameEN;
            }
            set
            {
                if (_PlazaNameEN != value)
                {
                    _PlazaNameEN = value;
                    this.RaiseChanged("PlazaNameEN");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaNameTH
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameTH")]
        public string PlazaNameTH
        {
            get
            {
                return _PlazaNameTH;
            }
            set
            {
                if (_PlazaNameTH != value)
                {
                    _PlazaNameTH = value;
                    this.RaiseChanged("PlazaNameTH");
                }
            }
        }

        /// <summary>
        /// Gets or sets Direction
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("Direction")]
        public string Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if (_Direction != value)
                {
                    _Direction = value;
                    this.RaiseChanged("Direction");
                }
            }
        }

        #endregion
    }
}