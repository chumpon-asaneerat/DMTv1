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
using System.ComponentModel;

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
        [TypeConverter(typeof(ExpandableObjectConverter))]
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
        public static bool Exists(SQLiteConnection db, Plaza value)
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
        public static void Save(SQLiteConnection db, Plaza value)
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
        public static List<Plaza> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
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
        public static Plaza Get(SQLiteConnection db, string PlazaId, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<Plaza>(
                p => p.PlazaId == PlazaId,
                recursive: recursive).FirstOrDefault();
            }
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
        public static bool Exists(SQLiteConnection db, Lane value)
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
        public static void Save(SQLiteConnection db, Lane value)
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
        public static List<Lane> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<Lane>(recursive: recursive);
            }
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
            lock (sync)
            {
                return db.GetAllWithChildren<Lane>(
                p => p.PlazaId == PlazaId &&
                p.LaneId == LaneId,
                recursive: recursive).FirstOrDefault();
            }
        }

        #endregion
    }

    #endregion

    #region Role

    /// <summary>
    /// The Role Data Model Class.
    /// </summary>
    //[Table("Role")]
    public class Role
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Role() : base() { }

        #endregion

        #region Public Proprties

        [PrimaryKey, MaxLength(20)]
        public string RoleId { get; set; }
        [MaxLength(50)]
        public string RoleName { get; set; }

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
        public static bool Exists(SQLiteConnection db, Role value)
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
        public static void Save(SQLiteConnection db, Role value)
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
        public static List<Role> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
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
        public static Role Get(SQLiteConnection db, string RoleId, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<Role>(
                p => p.RoleId == RoleId,
                recursive: recursive).FirstOrDefault();
            }
        }

        #endregion
    }

    #endregion

    #region User

    /// <summary>
    /// The User Data Model Class.
    /// </summary>
    //[Table("User")]
    public class User
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public User() : base() { }

        #endregion

        #region Public Proprties

        [PrimaryKey, MaxLength(10)]
        public string UserId { get; set; }
        [MaxLength(100)]
        public string FullNameEN { get; set; }
        [MaxLength(100)]
        public string FullNameTH { get; set; }

        [MaxLength(20)]
        public string UserName { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(10)]
        public string CardId { get; set; }

        [ForeignKey(typeof(Role)), MaxLength(10)]
        public string RoleId { get; set; }
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
        public static bool Exists(SQLiteConnection db, User value)
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
        public static void Save(SQLiteConnection db, User value)
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
        public static List<User> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
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
        public static User Get(SQLiteConnection db, string UserId, bool recursive = false)
        {
            lock (sync)
            {
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
        public static User GetByUserId(SQLiteConnection db, string UserId, string password, bool recursive = false)
        {
            lock (sync)
            {
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
        public static User GetByUserName(SQLiteConnection db, string userName, string password, bool recursive = false)
        {
            lock (sync)
            {
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
        public static User GetByCardId(SQLiteConnection db, string cardId, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<User>(
                p => p.CardId == cardId,
                recursive: recursive).FirstOrDefault();
            }
        }

        #endregion
    }

    #endregion

    #region Config

    /// <summary>
    /// The Config Data Model Class.
    /// </summary>
    [Table("Config")]
    public class Config
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Config() : base() { }

        #endregion

        #region Public Properties

        [PrimaryKey, MaxLength(20)]
        public string Key { get; set; }
        [MaxLength(100)]
        public string Value { get; set; }

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
        public static bool Exists(SQLiteConnection db, Config value)
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
        public static void Save(SQLiteConnection db, Config value)
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
        public static List<Config> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
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
        public static Config Get(SQLiteConnection db, string key, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<Config>(
                p => p.Key == key,
                recursive: recursive).FirstOrDefault();
            }
        }

        #endregion
    }

    #endregion

    #region SupervisorShift

    /// <summary>
    /// The SupervisorShift Data Model Class.
    /// </summary>
    //[Table("SupervisorShift")]
    public class SupervisorShift
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SupervisorShift() : base() { }

        #endregion

        #region Public Properties

        [PrimaryKey, AutoIncrement]
        public int SupervisorShiftId { get; set; }
        [MaxLength(10)]
        public string PlazaId { get; set; }

        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        public string SupervisorId { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "SupervisorId", CascadeOperations = CascadeOperation.All)]        
        public User User { get; set; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

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
        public static List<SupervisorShift> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<SupervisorShift>(recursive: recursive);
            }
        }

        #endregion
    }

    #endregion

    #region CollectorShift

    /// <summary>
    /// The CollectorShift Data Model Class.
    /// </summary>
    //[Table("CollectorShift")]
    public class CollectorShift
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorShift() : base() { }

        #endregion

        #region Public Properties

        [PrimaryKey, AutoIncrement]
        public int CollectorShiftId { get; set; }
        [MaxLength(10)]
        public string PlazaId { get; set; }
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        public string CollectorId { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

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
        public static List<CollectorShift> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<CollectorShift>(recursive: recursive);
            }
        }

        #endregion
    }

    #endregion

    #region CollectorLane

    /// <summary>
    /// The CollectorLane Data Model Class.
    /// </summary>
    public class CollectorLane
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorLane() : base() { }

        #endregion

        #region Public Properties

        [PrimaryKey, AutoIncrement]
        public int CollectorLaneId { get; set; }
        [MaxLength(10)]
        public string PlazaId { get; set; }
        
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        public string CollectorId { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        public int LaneId { get; set; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

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
        public static List<CollectorLane> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<CollectorLane>(recursive: recursive);
            }
        }

        #endregion
    }

    #endregion


    public class StressTest
    {
        [PrimaryKey, MaxLength(50)]
        public string RowId { get; set; }
        public string Name { get; set; }
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
        public static bool Exists(SQLiteConnection db, StressTest value)
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
        public static void Save(SQLiteConnection db, StressTest value)
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
        public static List<StressTest> Gets(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                return db.GetAllWithChildren<StressTest>(recursive: recursive);
            }
        }

        #endregion
    }

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
