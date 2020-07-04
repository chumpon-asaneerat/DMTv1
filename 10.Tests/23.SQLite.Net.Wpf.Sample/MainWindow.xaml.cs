using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using SQLite;
using System.ComponentModel;

using NLib;
using NLib.Reflection;
using SQLiteNetExtensions.Extensions;

using DMT.Models;

namespace SQLite.Net.Wpf.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private SQLiteConnection db;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            dt.CultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            Console.WriteLine(System.DateTimeOffset.Now.Offset);
            */

            dt.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            dt.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
            dt.TimeFormat = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            dt.TimeFormatString = "HH:mm:ss.fff";

            dt.DefaultValue = DateTime.Now;
            dt.Value = DateTime.Now;

            InitDatabase();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (null != db)
            {
                db.Close();
                db.Dispose();
            }
            db = null;
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void cmdUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            LoadData();
        }

        private void InitDatabase()
        {
            string databasePath = "./wpf-db-sample.db";
            db = new SQLiteConnection(databasePath, true);

            NTable.Default = db;
            NQuery.Default = db;

            db.CreateTable<Stock>();
            db.CreateTable<TSB>();

            if (db.Table<TSB>().Count() == 0)
            {
                TSB item;
                item = new TSB();
                item.NetworkId = "31";
                item.TSBId = "311";
                item.TSBNameEN = "DIN DAENG";
                item.TSBNameTH = "ดินแดง";
                item.Active = true;
                if (!TSB.Exists(item)) TSB.Save(item);

                item = new TSB();
                item.NetworkId = "31";
                item.TSBId = "312";
                item.TSBNameEN = "SUTHISARN";
                item.TSBNameTH = "สุทธิสาร";
                item.Active = false;
                if (!TSB.Exists(item)) TSB.Save(item);
            }
            /*
            if (db.Table<TSB>().Count() == 0) ;
            {

            }
            */

            LoadData();
        }

        private void LoadData()
        {
            if (null == db) return;
            var datasource = db.Table<Stock>().ToArray();
            grid.ItemsSource = datasource;
        }

        private void ClearData()
        {
            if (null == db) return;
            db.DeleteAll<Stock>();
        }

        private void Add()
        {
            if (null == db) return;

            if (string.IsNullOrWhiteSpace(txtName.Text.Trim())) return;
            if (!dt.Value.HasValue) return;
            var name = txtName.Text.Trim();
            var dtValue = dt.Value.Value;
            //var dtOffset = new System.DateTimeOffset(dtValue);

            var stock = new Stock()
            {
                Name = name,
                Time = dtValue
                //LocalTime = dtOffset
            };
            db.Insert(stock);

            // clear value.
            txtName.Text = string.Empty;
            dt.DefaultValue = DateTime.Now;

            LoadData();
        }

        private void Update()
        {
            if (null == db) return;
            var item = grid.SelectedItem as Stock;
            if (null != item)
            {
                item.Time2 = DateTime.Now;
                db.Update(item);
                // Raise events
                item.Raise("Time");
                item.Raise("STime");
                //item.Raise("LocalTime");
                item.Raise("Time2");
                item.Raise("STime2");
            }
        }
    }
}

namespace DMT.Models
{
    #region DMTModelBase (abstract)

    /// <summary>
    /// The DMTModelBase abstract class.
    /// Provide basic implementation of INotifyPropertyChanged interface.
    /// </summary>
    public abstract class DMTModelBase : INotifyPropertyChanged
    {
        #region Internal Variables

        private bool _lock = false;

        #endregion

        #region Private Methods

        /// <summary>
        /// Raise Property Changed Event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void RaiseChanged(string propertyName)
        {
            if (!_lock)
            {
                PropertyChanged.Call(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Enable Notify Change Event.
        /// </summary>
        public void EnableNotify()
        {
            _lock = true;
        }
        /// <summary>
        /// Disable Notify Change Event.
        /// </summary>
        public void DisableNotify()
        {
            _lock = false;
        }
        /// <summary>
        /// Checks is notifiy enabled or disable.
        /// </summary>
        /// <returns>Returns true if enabled.</returns>
        public bool Notified() { return _lock; }

        #endregion

        #region Public Events

        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    #endregion

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
        /// Get All with children.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<T> GetAllWithChildren(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<T>();
                return db.GetAllWithChildren<T>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id with children.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static T GetWithChildren(SQLiteConnection db, object Id, bool recursive = false)
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
        /// Delete by Id with children.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        public static void DeleteWithChildren(SQLiteConnection db, object Id, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db || null == Id) return;
                T inst = GetWithChildren(db, Id, recursive);
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
        /// Gets All with children.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<T> GetAllWithChildren(bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return GetAllWithChildren(db, recursive);
            }
        }
        /// <summary>
        /// Gets by Id with children.
        /// </summary>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static T GetWithChildren(object Id, bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return GetWithChildren(db, Id, recursive);
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
        /// Delete by Id with children.
        /// </summary>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        public static void DeleteWithChildren(object Id, bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                DeleteWithChildren(db, recursive);
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

    #region Stock

    public class Stock : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        // Update Time.
        public DateTime Time { get; set; }

        public void Raise(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Show time in string.
        [Ignore]
        public string STime
        {
            get
            {
                return (this.Time != DateTime.MinValue) ?
                    this.Time.ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty;
            }
            set { }
        }

        public DateTime Time2 { get; set; }
        [Ignore]
        public string STime2
        {
            get
            {
                return (this.Time2 != DateTime.MinValue) ?
                    this.Time2.ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty;
            }
            set { }
        }
        /*
        //[DateTimeOffsetSerialize]
        [DateTimeOffsetSerialize("yyyy-MM-dd HH:mm:ss.fff zzzz")]
        //[DateTimeOffsetSerialize("yyyy-MM-dd HH:mm:ss.fff zzzz", true)]
        public System.DateTimeOffset LocalTime { get; set; }

        [Ignore]
        public string SLocalTime
        {
            get
            {
                return (this.LocalTime != System.DateTimeOffset.MinValue) ?
                    this.LocalTime.ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty;
            }
            set { }
        }
        */

        public event PropertyChangedEventHandler PropertyChanged;
    }

    #endregion

    #region TSB

    /// <summary>
    /// The TSB Data Model class.
    /// </summary>
    //[Table("TSB")]
    public class TSB : NTable<TSB>
    {
        #region Intenral Variables

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;
        private string _NetworkId = string.Empty;
        private bool _Active = false;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

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
        /// <summary>
        /// Gets or sets is active TSB.
        /// </summary>
        [PeropertyMapName("Active")]
        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    this.RaiseChanged("Active");
                }
            }
        }
        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
        [PeropertyMapName("Status")]
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    this.RaiseChanged("Status");
                }
            }
        }
        /// <summary>
        /// Gets or sets LastUpdated (Sync to DC).
        /// </summary>
        [PeropertyMapName("LastUpdate")]
        public DateTime LastUpdate
        {
            get { return _LastUpdate; }
            set
            {
                if (_LastUpdate != value)
                {
                    _LastUpdate = value;
                    this.RaiseChanged("LastUpdate");
                }
            }
        }

        #endregion

        #region Static Methods

        public static void SetActive(string tsbId)
        {
            lock (sync)
            {
                // inactive all TSBs
                string cmd = string.Empty;
                cmd += "UPDATE TSB ";
                cmd += "   SET Active = 0";
                NQuery.Execute(cmd);
                // Set active TSB
                cmd = string.Empty;
                cmd += "UPDATE TSB ";
                cmd += "   SET Active = 1 ";
                cmd += " WHERE TSBId = ? ";
                NQuery.Execute(cmd, tsbId);
            }
        }

        public static TSB GetCurrent()
        {
            lock (sync)
            {
                // inactive all TSBs
                string cmd = string.Empty;
                cmd += "SELECT * FROM TSB ";
                cmd += " WHERE Active = 1 ";
                var results = NQuery.Query<TSB>(cmd, true);
                return (null != results) ? results.FirstOrDefault() : null;
            }
        }

        #endregion
    }

    #endregion

    #region TSBBase<T>

    /// <summary>
    /// The TSB Base Data Model abstract class.
    /// </summary>
    /// <typeparam name="T">The target class type.</typeparam>
    public abstract class TSBBase<T> : NTable<T>
        where T : NTable, new()
    {
        #region Intenral Variables

        private string _TSBId = string.Empty;
        //private string _TSBNameEN = string.Empty;
        //private string _TSBNameTH = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBBase() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
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
        /*
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
        */

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}