using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
// SQLite
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using NLib.IO;

namespace DMT.Services
{
    #region LobalDbServer Implements

    /// <summary>
    /// Local Database Server.
    /// </summary>
    public class LocalDbServer
    {
        #region Singelton

        private static LocalDbServer _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static LocalDbServer Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(LocalDbServer))
                    {
                        _instance = new LocalDbServer();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private LocalDbServer() : base()
        {
            this.FileName = "TODxTA.db";
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~LocalDbServer()
        {

        }

        #endregion

        #region Private Methods

        private void InitTables()
        {
            if (null == Db) return;
            Db.CreateTable<Models.Domains.TSB>();
            Db.CreateTable<Models.Domains.Plaza>();
            //Db.CreateTable<Models.Domains.Lane>();
            //Db.CreateTable<Models.Domains.User>();
            //Db.CreateTable<Models.Domains.RevenueSlip>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start.
        /// </summary>
        public void Start()
        {
            if (null == Db)
            {
                lock (typeof(LocalDbServer))
                {
                    string path = Path.Combine("./", FileName);
                    Db = new SQLiteConnection(path);
                    InitTables();
                }
            }
        }
        /// <summary>
        /// Shutdown.
        /// </summary>
        public void Shutdown()
        {
            if (null != Db)
            {
                Db.Dispose();
            }
            Db = null;
        }

        #endregion

        #region Public Methods

        public T[] Table<T>()
            where T : class, new()
        {
            var result = this.Db.Table<T>().ToArray();
            return result;
        }

        public void Add<T>(T value)
            where T : class
        {
            if (null == this.Db || null == value) return;
            this.Db.Insert(value, typeof(T));
            //this.Db.Insert(value);
        }

        public void Update<T>(T value)
            where T : class
        {
            if (null == this.Db || null == value) return;
            this.Db.Update(value, typeof(T));
            //this.Db.Update(value);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets database file name.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Gets SQLite Connection.
        /// </summary>
        public SQLiteConnection Db { get; private set; }

        #endregion
    }

    #endregion
}
