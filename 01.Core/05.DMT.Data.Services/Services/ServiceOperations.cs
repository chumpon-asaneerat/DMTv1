#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#endregion

using NLib.ServiceProcess;

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
        /// Gets (or internal set) is Plaza Local Service installed.
        /// </summary>
        public bool PlazaLocalServiceInstalled { get; internal set; }
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

        private NServiceMonitor _srvMon = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private DMTServiceOperations() : base()
        {
            _srvMon = new NServiceMonitor();
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

            // Append Local Plaza Window Service application
            _srvMon.ServiceNames.Add(
                new NServiceName()
                {
                    // The Service Name must match the name that declare name 
                    // in NServiceInstaller inherited class
                    ServiceName = DMT.AppConsts.WindowsService.Plaza.ServiceName,
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, AppConsts.WindowsService.Plaza.ExecutableFileName)
                });

            // Append Local TOD Window Service application
            _srvMon.ServiceNames.Add(
                new NServiceName()
                {
                    // The Service Name must match the name that declare name 
                    // in NServiceInstaller inherited class
                    ServiceName = DMT.AppConsts.WindowsService.TOD.ServiceName,
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, AppConsts.WindowsService.TOD.ExecutableFileName)
            });

            // Append Local TA Window Service application
            _srvMon.ServiceNames.Add(
                new NServiceName()
                {
                    // The Service Name must match the name that declare name 
                    // in NServiceInstaller inherited class
                    ServiceName = DMT.AppConsts.WindowsService.TA.ServiceName,
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, AppConsts.WindowsService.TA.ExecutableFileName)
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
            result.PlazaLocalServiceInstalled = false;
            result.TODLocalServiceInstalled = false;
            result.TALocalServiceInstalled = false;
            if (null != _srvMon)
            {
                try
                {
                    NServiceInfo[] srvs = _srvMon.ServiceInformations;
                    if (null != srvs)
                    {
                        result.ServiceCount = srvs.Length;
                        foreach (NServiceInfo srvInfo in srvs)
                        {
                            if (srvInfo.IsInstalled)
                            {
                                ++result.InstalledCount;
                                if (srvInfo.ServiceName == AppConsts.WindowsService.Plaza.ServiceName)
                                {
                                    result.PlazaLocalServiceInstalled = true;
                                }
                                if (srvInfo.ServiceName == AppConsts.WindowsService.TOD.ServiceName)
                                {
                                    result.TODLocalServiceInstalled = true;
                                }
                                if (srvInfo.ServiceName == AppConsts.WindowsService.TA.ServiceName)
                                {
                                    result.TALocalServiceInstalled = true;
                                }
                            }
                        }
                    }
                }
                catch { }
            }            
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
