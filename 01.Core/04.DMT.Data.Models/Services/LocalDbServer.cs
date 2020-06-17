﻿#region Using

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
using System.Runtime.CompilerServices;
using DMT.Models.Domains;

#endregion

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
            Db.CreateTable<Models.Domains.Lane>();
            Db.CreateTable<Models.Domains.Role>();
            Db.CreateTable<Models.Domains.User>();
            //Db.CreateTable<Models.Domains.RevenueSlip>();

            InitDefaults();
        }

        private void InitDefaults()
        {
            InitTSBAndPlazaAndLanes();
            InitRoleAndUsers();
        }

        private void InitTSBAndPlazaAndLanes()
        {
            if (null == Db) return;
            TSB item;
            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "311";
            item.TSBNameEN = "DIN DAENG";
            item.TSBNameTH = "ดินแดง";
            item.Plazas = new List<Plaza>() 
            { 
                new Plaza() { 
                    PlazaId = "3101", 
                    PlazaNameEN = "DIN DAENG 1", 
                    PlazaNameTH = "ดินแดง 1", 
                    Direction = "IN",
                    Lanes = new List<Lane>()
                    {
                        new Lane() { LaneId = 1, LaneType = "MTC", LaneAbbr = "DD01" },
                        new Lane() { LaneId = 2, LaneType = "MTC", LaneAbbr = "DD02" },
                        new Lane() { LaneId = 3, LaneType = "A/M", LaneAbbr = "DD03" },
                        new Lane() { LaneId = 4, LaneType = "ETC", LaneAbbr = "DD04" }
                    }
                },
                new Plaza() { 
                    PlazaId = "3102", 
                    PlazaNameEN = "DIN DAENG 2", 
                    PlazaNameTH = "ดินแดง 2", 
                    Direction = "OUT",
                    Lanes = new List<Lane>()
                    {
                        new Lane() { LaneId = 11, LaneType = "?", LaneAbbr = "DD11" },
                        new Lane() { LaneId = 12, LaneType = "?", LaneAbbr = "DD12" },
                        new Lane() { LaneId = 13, LaneType = "?", LaneAbbr = "DD13" },
                        new Lane() { LaneId = 14, LaneType = "?", LaneAbbr = "DD14" },
                        new Lane() { LaneId = 15, LaneType = "?", LaneAbbr = "DD15" },
                        new Lane() { LaneId = 16, LaneType = "?", LaneAbbr = "DD16" }
                    }
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "312";
            item.TSBNameEN = "SUTHISARN";
            item.TSBNameTH = "สุทธิสาร";
            item.Plazas = new List<Plaza>()
            {
                new Plaza() { 
                    PlazaId = "3103", 
                    PlazaNameEN = "SUTHISARN", 
                    PlazaNameTH = "สุทธิสาร", 
                    Direction = "",
                    Lanes = new List<Lane>()
                    {
                        new Lane() { LaneId = 1, LaneType = "?", LaneAbbr = "SS01" },
                        new Lane() { LaneId = 2, LaneType = "?", LaneAbbr = "SS02" },
                        new Lane() { LaneId = 3, LaneType = "?", LaneAbbr = "SS03" }
                    }
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "313";
            item.TSBNameEN = "LAD PRAO";
            item.TSBNameTH = "ลาดพร้าว";
            item.Plazas = new List<Plaza>() 
            {
                new Plaza() { 
                    PlazaId = "3104", 
                    PlazaNameEN = "LAD PRAO INBOUND", 
                    PlazaNameTH = "ลาดพร้าว ขาเข้า", 
                    Direction = "IN",
                    Lanes = new List<Lane>()
                    {
                        new Lane() { LaneId = 1, LaneType = "?", LaneAbbr = "LP01" },
                        new Lane() { LaneId = 2, LaneType = "?", LaneAbbr = "LP02" },
                        new Lane() { LaneId = 3, LaneType = "?", LaneAbbr = "LP03" },
                        new Lane() { LaneId = 4, LaneType = "?", LaneAbbr = "LP04" }
                    }
                },
                new Plaza() { 
                    PlazaId = "3105", 
                    PlazaNameEN = "LAD PRAO OUTBOUND", 
                    PlazaNameTH = "ลาดพร้าว ขาออก",
                    Direction = "OUT" ,
                    Lanes = new List<Lane>()
                    {
                        new Lane() { LaneId = 21, LaneType = "?", LaneAbbr = "LP21" },
                        new Lane() { LaneId = 22, LaneType = "?", LaneAbbr = "LP22" },
                        new Lane() { LaneId = 23, LaneType = "?", LaneAbbr = "LP23" }
                    }
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "314";
            item.TSBNameEN = "RATCHADA PHISEK";
            item.TSBNameTH = "รัชดาภิเษก";
            item.Plazas = new List<Plaza>()
            {
                new Plaza() { 
                    PlazaId = "3106", 
                    PlazaNameEN = "RATCHADA PHISEK 1", 
                    PlazaNameTH = "รัชดาภิเษก 1", 
                    Direction = "IN" 
                },
                new Plaza() { 
                    PlazaId = "3107", 
                    PlazaNameEN = "RATCHADA PHISEK 2", 
                    PlazaNameTH = "รัชดาภิเษก 2", 
                    Direction = "OUT" 
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "315";
            item.TSBNameEN = "BANGKHEN";
            item.TSBNameTH = "บางเขน";
            item.Plazas = new List<Plaza>()
            {
                new Plaza() { 
                    PlazaId = "3108", 
                    PlazaNameEN = "BANGKHEN", 
                    PlazaNameTH = "บางเขน", 
                    Direction = "" 
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "316";
            item.TSBNameEN = "CHANGEWATTANA";
            item.TSBNameTH = "แจ้งวัฒนะ";
            item.Plazas = new List<Plaza>()
            {
                new Plaza() { 
                    PlazaId = "3109", 
                    PlazaNameEN = "CHANGEWATTANA 1", 
                    PlazaNameTH = "แจ้งวัฒนะ 1", 
                    Direction = "IN" 
                },
                new Plaza() { 
                    PlazaId = "3110", 
                    PlazaNameEN = "CHANGEWATTANA 2", 
                    PlazaNameTH = "แจ้งวัฒนะ 2", 
                    Direction = "OUT" 
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "317";
            item.TSBNameEN = "LAKSI";
            item.TSBNameTH = "หลักสี่";
            item.Plazas = new List<Plaza>()
            {
                new Plaza() { 
                    PlazaId = "3111", 
                    PlazaNameEN = "LAKSI INBOUND", 
                    PlazaNameTH = "หลักสี่ ขาเข้า", 
                    Direction = "IN" 
                },
                new Plaza() { 
                    PlazaId = "3112", 
                    PlazaNameEN = "LAKSI OUTBOUND", 
                    PlazaNameTH = "หลักสี่ ขาออก", 
                    Direction = "OUT" 
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "318";
            item.TSBNameEN = "DON MUANG";
            item.TSBNameTH = "ดอนเมือง";
            item.Plazas = new List<Plaza>()
            {
                new Plaza() { 
                    PlazaId = "3113", 
                    PlazaNameEN = "DON MUANG 1", 
                    PlazaNameTH = "ดอนเมือง 1", 
                    Direction = "IN" 
                },
                new Plaza() { 
                    PlazaId = "3114", 
                    PlazaNameEN = "DON MUANG 2", 
                    PlazaNameTH = "ดอนเมือง 2", 
                    Direction = "OUT" 
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);

            item = new TSB();
            item.NetworkId = "31";
            item.TSBId = "319";
            item.TSBNameEN = "ANUSORN SATHAN";
            item.TSBNameTH = "อนุสรน์สถาน";
            item.Plazas = new List<Plaza>()
            {
                new Plaza() { 
                    PlazaId = "3115",
                    PlazaNameEN = "ANUSORN SATHAN 1", 
                    PlazaNameTH = "อนุสรน์สถาน 1", 
                    Direction = "IN" 
                },
                new Plaza() { 
                    PlazaId = "3116", 
                    PlazaNameEN = "ANUSORN SATHAN 2", 
                    PlazaNameTH = "อนุสรน์สถาน 2", 
                    Direction = "OUT" 
                }
            };
            if (!TSB.Exists(this.Db, item)) TSB.Save(this.Db, item);
        }

        private void InitRoleAndUsers()
        {
            if (null == Db) return;
            Role item;
            item = new Role()
            {
                RoleId = "QFREE",
                RoleName = "QFree",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserId = "99001",
                        FullNameEN = "QFree User 1",
                        FullNameTH = "QFree User 1",
                        UserName = "qfree1",
                        Password = "1234",
                        CardId = ""
                    }
                }
            };
            if (!Role.Exists(this.Db, item)) Role.Save(this.Db, item);
            item = new Role()
            {
                RoleId = "ADMIN",
                RoleName = "Administrator",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserId = "99901",
                        FullNameEN = "Admin 1",
                        FullNameTH = "Admin 1",
                        UserName = "admin1",
                        Password = "1234",
                        CardId = ""
                    }
                }
            };
            if (!Role.Exists(this.Db, item)) Role.Save(this.Db, item);
            item = new Role()
            {
                RoleId = "AUDIT",
                RoleName = "Auditor",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserId = "85020",
                        FullNameEN = "audit1",
                        FullNameTH = "audit1",
                        UserName = "audit1",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "65401",
                        FullNameEN = "นาย สมชาย ตุยเอียว",
                        FullNameTH = "นาย สมชาย ตุยเอียว",
                        UserName = "audit2",
                        Password = "1234",
                        CardId = ""
                    }
                }
            };
            if (!Role.Exists(this.Db, item)) Role.Save(this.Db, item);
            item = new Role()
            {
                RoleId = "SUPERVISOR",
                RoleName = "Supervisor",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserId = "13566",
                        FullNameEN = "นาย ผจญ สุดศิริ",
                        FullNameTH = "นาย ผจญ สุดศิริ",
                        UserName = "sup1",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "26855",
                        FullNameEN = "นวย วิรชัย ขำหิรัญ",
                        FullNameTH = "นวย วิรชัย ขำหิรัญ",
                        UserName = "sup2",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "30242",
                        FullNameEN = "นาย บุญส่ง บุญปลื้ม",
                        FullNameTH = "นาย บุญส่ง บุญปลื้ม",
                        UserName = "sup3",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "76333",
                        FullNameEN = "นาย สมบูรณ์ สบายดี",
                        FullNameTH = "นาย สมบูรณ์ สบายดี",
                        UserName = "sup4",
                        Password = "1234",
                        CardId = ""
                    }
                }
            };
            if (!Role.Exists(this.Db, item)) Role.Save(this.Db, item);
            item = new Role()
            {
                RoleId = "COLLECTOR",
                RoleName = "Collector",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserId = "14211",
                        FullNameEN = "นาย ภักดี อมรรุ่งโรจน์",
                        FullNameTH = "นาย ภักดี อมรรุ่งโรจน์",
                        UserName = "user1",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "14124",
                        FullNameEN = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์",
                        FullNameTH = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์",
                        UserName = "user2",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "14055",
                        FullNameEN = "นางวิภา สวัสดิวัฒน์",
                        FullNameTH = "นางวิภา สวัสดิวัฒน์",
                        UserName = "user3",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "14321",
                        FullNameEN = "นาย สุเทพ เหมัน",
                        FullNameTH = "นาย สุเทพ เหมัน",
                        UserName = "user4",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "14477",
                        FullNameEN = "นาย ศิริลักษณ์ วงษาหาร",
                        FullNameTH = "นาย ศิริลักษณ์ วงษาหาร",
                        UserName = "user5",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "14566",
                        FullNameEN = "นางสาว สุณิสา อีนูน",
                        FullNameTH = "นางสาว สุณิสา อีนูน",
                        UserName = "user6",
                        Password = "1234",
                        CardId = ""
                    },
                    new User()
                    {
                        UserId = "15097",
                        FullNameEN = "นาง วาสนา ชาญวิเศษ",
                        FullNameTH = "นาง วาสนา ชาญวิเศษ",
                        UserName = "user7",
                        Password = "1234",
                        CardId = ""
                    },
                }
            };
            if (!Role.Exists(this.Db, item)) Role.Save(this.Db, item);
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
                    Db = new SQLiteConnection(path, storeDateTimeAsTicks: true);
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

        // TSB
        public bool Exists(TSB value) { return TSB.Exists(this.Db, value); }
        public void Save(TSB value) { TSB.Save(this.Db, value); }
        public List<TSB> GetTSBs(bool recursive = false)
        {
            return TSB.Gets(this.Db, recursive);
        }
        public TSB GetTSB(string tsbId, bool recursive = false)
        {
            return TSB.Get(this.Db, tsbId, recursive);
        }
        // Plaza
        public bool Exists(Plaza value) { return Plaza.Exists(this.Db, value); }
        public void Save(Plaza value) { Plaza.Save(this.Db, value); }
        public List<Plaza> GetPlazas(bool recursive = false)
        {
            return Plaza.Gets(this.Db, recursive);
        }
        public Plaza GetPlaza(string plazaId, bool recursive = false)
        {
            return Plaza.Get(this.Db, plazaId, recursive);
        }
        // Lane
        public bool Exists(Lane value) { return Lane.Exists(this.Db, value); }
        public void Save(Lane value) { Lane.Save(this.Db, value); }
        public List<Lane> GetLanes(bool recursive = false) 
        { 
            return Lane.Gets(this.Db, recursive); 
        }
        public Lane GetLane(string plazaId, int laneId, bool recursive = false) 
        { 
            return Lane.Get(this.Db, plazaId, laneId, recursive); 
        }
        // Role
        public bool Exists(Role value) { return Role.Exists(this.Db, value); }
        public void Save(Role value) { Role.Save(this.Db, value); }
        public List<Role> GetRoles(bool recursive = false)
        {
            return Role.Gets(this.Db, recursive);
        }
        public Role GetRole(string roleId, bool recursive = false)
        {
            return Role.Get(this.Db, roleId, recursive);
        }
        // User
        public bool Exists(User value) { return User.Exists(this.Db, value); }
        public void Save(User value) { User.Save(this.Db, value); }
        public List<User> GetUsers(bool recursive = false)
        {
            return User.Gets(this.Db, recursive);
        }
        public User Get(string userId, bool recursive = false)
        {
            return User.Get(this.Db, userId, recursive);
        }
        public User LogInByUserId(string userId, string password, bool recursive = false)
        {
            return User.GetByUserId(this.Db, userId, password, recursive);
        }
        public User LogInByUserName(string userName, string password, bool recursive = false)
        {
            return User.GetByUserName(this.Db, userName, password, recursive);
        }
        public User LogInByCardId(string cardId, bool recursive = false)
        {
            return User.GetByCardId(this.Db, cardId, recursive);
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
