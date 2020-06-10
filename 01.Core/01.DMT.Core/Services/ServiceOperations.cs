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
    /// <summary>
    /// The Service Operations class.
    /// </summary>
    public class ServiceOperations
    {
        #region Singelton

        private static ServiceOperations _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static ServiceOperations Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ServiceOperations))
                    {
                        _instance = new ServiceOperations();
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
        private ServiceOperations() : base()
        {
            // Init windows service monitor.
            InitWindowsServices();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~ServiceOperations()
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
                    ServiceName = "My Choice Rater Compact Windows Service",
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
                    ServiceName = "My Choice Rater Compact Windows Service",
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

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Instance of Windows Services Monitor.
        /// </summary>
        public NServiceMonitor ServiceMonitor { get { return _srvMon; } }

        #endregion
    }
}
