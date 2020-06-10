#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#endregion

using NLib.ServiceProcess;
//using Rater.ServiceModel;

namespace DMT.Services
{
    #region DMTServiceInstalledStatus

    /// <summary>
    /// The DMT Window Service Installed Status class.
    /// </summary>
    public class DMTServiceInstalledStatus
    {
        #region Public properties

        /// <summary>
        /// Gets (or internal set) all service count.
        /// </summary>
        public int ServiceCount { get; internal set; }
        /// <summary>
        /// Gets (or internal set) service install count.
        /// </summary>
        public int InstalledCount { get; internal set; }
        /// <summary>
        /// Gets (or internal set) is TOD Local Service installed.
        /// </summary>
        public bool TODLocalServiceInstalled { get; internal set; }
        /// <summary>
        /// Gets (or internal set) is TA Local Service installed.
        /// </summary>
        public bool TALocalServiceInstalled { get; internal set; }

        #endregion
    }

    #endregion

    #region DMTServiceOperations

    /// <summary>
    /// The DMT Service Operations class.
    /// </summary>
    public class DMTServiceOperations
    {
        #region Singelton

        private static DMTServiceOperations _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static DMTServiceOperations Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(DMTServiceOperations))
                    {
                        _instance = new DMTServiceOperations();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private NServiceMonitor _srvMon = new NServiceMonitor();

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private DMTServiceOperations() : base()
        {
            // Init windows service monitor.
            InitWindowsServices();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DMTServiceOperations()
        {
            // Shutdown windows service monitor.
            if (null != _srvMon)
            {
                _srvMon.Shutdown();
            }
            _srvMon = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Init windows service list to monitor.
        /// </summary>
        private void InitWindowsServices()
        {
            if (null == _srvMon)
                return;
            // Init Service to monitor
            _srvMon.ServiceNames.Clear();
            string path = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);

            // Append Local TOD Window Service application
            _srvMon.ServiceNames.Add(
                new NServiceName()
                {
                    // The Service Name must match the name that declare name 
                    // in NServiceInstaller inherited class
                    ServiceName = "TOD Local Web Service",
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, @"DMT.TOD.Data.Services.exe")
            });

            // Append Local TA Window Service application
            _srvMon.ServiceNames.Add(
                new NServiceName()
                {
                    // The Service Name must match the name that declare name 
                    // in NServiceInstaller inherited class
                    ServiceName = "TA Local Web Service",
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, @"DMT.TA.Data.Services.exe")
                });
        }

        #endregion

        #region Public Methods

        #region Windows Services Methods

        /// <summary>
        /// Install all registered windows services.
        /// </summary>
        public void Install()
        {
            if (null == _srvMon)
                return;
            _srvMon.InstallAll();
        }
        /// <summary>
        /// Uninstall all registered windows services.
        /// </summary>
        public void Uninstall()
        {
            if (null == _srvMon)
                return;
            _srvMon.UninstallAll();
        }
        /// <summary>
        /// Checks services installed status.
        /// </summary>
        /// <returns>Returns ServiceStatus instance.</returns>
        public DMTServiceInstalledStatus CheckInstalled()
        {
            DMTServiceInstalledStatus result = new DMTServiceInstalledStatus();
            result.ServiceCount = 0;
            result.InstalledCount = 0;
            result.TODLocalServiceInstalled = false;
            result.TALocalServiceInstalled = false;

            NServiceInfo[] srvs = null;
            try
            {
                srvs = _srvMon.ServiceInformations;
                if (null != srvs)
                {
                    result.ServiceCount = srvs.Length;
                    foreach (NServiceInfo srvInfo in srvs)
                    {
                        if (srvInfo.IsInstalled) ++result.InstalledCount;
                        if (srvInfo.ServiceName == "TOD Local Web Service") 
                        {
                            result.TODLocalServiceInstalled = true;
                        }
                        if (srvInfo.ServiceName == "TA Local Web Service")
                        {
                            result.TALocalServiceInstalled = true;
                        }
                    }
                }
            }
            catch { }
            
            return result; // return scan result.
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Instance of Windows Services Monitor.
        /// </summary>
        public NServiceMonitor ServiceMonitor { get { return _srvMon; } }

        #endregion
    }

    #endregion
}
