using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLib;
using NLib.Reflection;

namespace DMT.Models.Objects
{
    #region TSB

    /// <summary>
    /// The TSB Object Model class.
    /// </summary>
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

    }

    #endregion

    #region Plaza

    /// <summary>
    /// The Plaza Object Model class.
    /// </summary>
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

    #endregion

    #region Shift

    /// <summary>
    /// The Shift Object Model class.
    /// </summary>
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
    }

    #endregion

    #region Lane

    /// <summary>
    /// The Lane Object Model class.
    /// </summary>
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

        #endregion


    }

    #endregion

    #region Role

    /// <summary>
    /// The Role Object Model Class.
    /// </summary>
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


        #endregion

    }

    #endregion

    #region User

    /// <summary>
    /// The User Object Model Class.
    /// </summary>
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

        public Role Role { get; set; }

        #endregion

    }

    #endregion

    #region Config

    /// <summary>
    /// The Config Object Model Class.
    /// </summary>
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

    }

    #endregion

    #region SupervisorShift

    /// <summary>
    /// The SupervisorShift Object Model class.
    /// </summary>
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

        #region Override Methods

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets SupervisorShiftId
        /// </summary>
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
    }

    #endregion

    #region CollectorJob

    /// <summary>
    /// The CollectorJob Data Model Class.
    /// </summary>
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
    }

    #endregion

    #region CollectorShift

    /// <summary>
    /// The CollectorShift Object Model class.
    /// </summary>
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

        #region Override Methods

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets CollectorShiftId
        /// </summary>
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
    }

    #endregion

    #region CollectorLane

    /// <summary>
    /// The CollectorLane Data Model Class.
    /// </summary>
    public class CollectorLane : DMTModelBase
    {
        #region Intenral Variables

        private int _CollectorLaneId = 0;
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
        [PeropertyMapName("CollectorLaneId")]
        public int CollectorShiftId
        {
            get
            {
                return _CollectorLaneId;
            }
            set
            {
                if (_CollectorLaneId != value)
                {
                    _CollectorLaneId = value;
                    this.RaiseChanged("CollectorLaneId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
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

    }

    #endregion

    #region RevenueEntry

    /// <summary>
    /// The RevenueEntry Object Model class.
    /// </summary>
    public class RevenueEntry : DMTModelBase
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntry() : base() { }

        #endregion

        #region Override Methods

        #endregion

        #region Public Proprties

        #endregion
    }

    #endregion
}
