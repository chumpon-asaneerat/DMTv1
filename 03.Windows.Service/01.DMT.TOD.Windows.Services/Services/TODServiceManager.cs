#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.IO;

using NLib;
using NLib.Data;
using NLib.ServiceProcess;
using NLib.Services;
//using NLib.Wcf;
using NLib.Xml;

//using Rater.Configs;
//using Rater.ServiceModel;

#endregion

namespace DMT.Services
{
    #region TODDataServiceManager (Installer)

    /// <summary>
    /// TOD Data Service Manager.
    /// </summary>
    [RunInstaller(true)]
    public class TODDataServiceManager : NServiceInstaller
    {
        #region Internal Variables

        private TODDataService _service = new TODDataService();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TODDataServiceManager()
            : base()
        {
            ServiceName = AppConsts.WindowsService.TOD.ServiceName;
            DisplayName = AppConsts.WindowsService.TOD.DisplayName;
            Description = AppConsts.WindowsService.TOD.Description;
        }

        #endregion

        #region Override properties

        /// <summary>
        /// Gets the service instance.
        /// </summary>
        public override NServiceBase Service
        {
            get
            {
                return _service;
            }
        }

        #endregion
    }

    #endregion

    #region TODDataService (Core service)

    /// <summary>
    /// TOD Data Service. (Core service).
    /// </summary>
    public class TODDataService : NServiceBase
    {
        #region Internal Variables

        private bool _running = false;
        private bool _pause = true;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TODDataService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TODDataService()
        {
            //RaterCompactService.Instance.Shutdown();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// OnStart
        /// </summary>
        /// <param name="args">The service event args.</param>
        protected override void OnStart(string[] args)
        {
            _running = true;
            _pause = false;

            //RaterCompactService.Instance.Start();
        }
        /// <summary>
        /// OnPause
        /// </summary>
        protected override void OnPause()
        {
            _pause = true;
            //RaterCompactService.Instance.Pause();
        }
        /// <summary>
        /// OnContinue
        /// </summary>
        protected override void OnContinue()
        {
            _pause = false;
            //RaterCompactService.Instance.Continue();
        }
        /// <summary>
        /// OnStop.
        /// </summary>
        protected override void OnStop()
        {
            _running = false;
            _pause = true;

            //RaterCompactService.Instance.Shutdown();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Checks is service running.
        /// </summary>
        public bool IsRunning { get { return _running; } }
        /// <summary>
        /// Checks is service pause.
        /// </summary>
        public bool IsPause { get { return _pause; } }

        #endregion
    }

    #endregion
}
