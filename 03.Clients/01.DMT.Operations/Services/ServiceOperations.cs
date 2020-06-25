#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using RestSharp;
using RestSharp.Authenticators;

using NLib.ServiceProcess;

#endregion

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

        #endregion
    }

    #endregion

    #region PlazaOperations

    /// <summary>
    /// Plaza Operations class.
    /// </summary>
    public class PlazaOperations
    {
        static PlazaOperations()
        {
            // Required for HTTPS.
            /*
            ServicePointManager.SecurityProtocol = 
                SecurityProtocolType.Tls12 | 
                SecurityProtocolType.Tls11 | 
                SecurityProtocolType.Tls |
                (SecurityProtocolType)768 | (SecurityProtocolType)3072 |
                SecurityProtocolType.SystemDefault;
            */
        }
        #region Public Methods

        /*
        public Models.Objects.Plaza[] GetPlazas()
        {
            Models.Objects.Plaza[] results = new Models.Objects.Plaza[] { };
            return results;
        }

        public void SavePlaza(Models.Objects.Plaza value)
        {

        }
        */

        /*
        public Models.Objects.User GetUser(Models.Objects.User user)
        {

        }
        */

        public string BeginJob()
        {
            var host = @"http://localhost:9000";
            var client = new RestClient(host);
            //client.Authenticator = new HttpBasicAuthenticator(AUTH.PersonalAccessToken, String.Empty);
            var request = new RestRequest(RouteConsts.Job.BeginJob.Url, Method.POST);
            request.RequestFormat = DataFormat.Json;            
            request.AddJsonBody(new { Name = "User 1 end job" });

            var response = client.Execute(request);
            return (null != response) ? response.Content : "No response.";
        }

        public string EndJob()
        {
            var host = @"http://localhost:9000";
            var client = new RestClient(host);
            //client.Authenticator = new HttpBasicAuthenticator(AUTH.PersonalAccessToken, String.Empty);
            var request = new RestRequest(RouteConsts.Job.BeginJob.Url, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { Name = "User 1 end job" });

            var response = client.Execute(request);
            return (null != response) ? response.Content : "No response.";
        }

        #endregion
    }

    #endregion

    #region DMTServiceOperations

    /// <summary>
    /// The DMT Service Operations class.
    /// </summary>
    public partial class DMTServiceOperations
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

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private DMTServiceOperations() : base()
        {
            ServiceMonitor = new NServiceMonitor();
            // Init windows service monitor.
            InitWindowsServices();

            Plaza = new PlazaOperations();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DMTServiceOperations()
        {
            // Shutdown windows service monitor.
            if (null != ServiceMonitor)
            {
                ServiceMonitor.Shutdown();
            }
            ServiceMonitor = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Init windows service list to monitor.
        /// </summary>
        private void InitWindowsServices()
        {
            if (null == ServiceMonitor)
                return;
            // Init Service to monitor
            ServiceMonitor.ServiceNames.Clear();
            string path = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);

            // Append Local Plaza Window Service application
            ServiceMonitor.ServiceNames.Add(
                new NServiceName()
                {
                    // The Service Name must match the name that declare name 
                    // in NServiceInstaller inherited class
                    ServiceName = DMT.AppConsts.WindowsService.Plaza.ServiceName,
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, AppConsts.WindowsService.Plaza.ExecutableFileName)
                });
        }

        #endregion

        #region Public Methods

        #region Install/Uninstall/CheckInstalled

        /// <summary>
        /// Install all registered windows services.
        /// </summary>
        public void Install()
        {
            if (null == ServiceMonitor)
                return;
            ServiceMonitor.InstallAll();
        }
        /// <summary>
        /// Uninstall all registered windows services.
        /// </summary>
        public void Uninstall()
        {
            if (null == ServiceMonitor)
                return;
            ServiceMonitor.UninstallAll();
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
            if (null != ServiceMonitor)
            {
                try
                {
                    NServiceInfo[] srvs = ServiceMonitor.ServiceInformations;
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
        public NServiceMonitor ServiceMonitor { get;  private set; }
        /// <summary>
        /// Gets instance of Plaza Operations.
        /// </summary>
        public PlazaOperations Plaza { get; private set; }

        #endregion
    }

    #endregion
}
