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
// SQLite
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
// Owin SelfHost
using Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Web.Http;

#endregion

namespace DMT.Services
{
    #region TADataServiceManager (Installer)

    /// <summary>
    /// TA Data Service Manager.
    /// </summary>
    [RunInstaller(true)]
    public class TADataServiceManager : NServiceInstaller
    {
        #region Internal Variables

        private TADataService _service = new TADataService();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TADataServiceManager()
            : base()
        {
            ServiceName = AppConsts.WindowsService.TA.ServiceName;
            DisplayName = AppConsts.WindowsService.TA.DisplayName;
            Description = AppConsts.WindowsService.TA.Description;
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

    #region LobalDbServer Implements

    /// <summary>
    /// Local Database Server.
    /// </summary>
    public class LocalDbServer
    {
        #region Internal Variables

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalDbServer() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~LocalDbServer()
        {

        }

        #endregion

        #region Public Methods

        #endregion
    }

    #endregion

    #region WebServer Implements

    /// <summary>
    /// Web Server StartUp class.
    /// </summary>
    public class StartUp
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            appBuilder.UseWebApi(config);
        }
    }

    /// <summary>
    /// Route Controller.
    /// </summary>
    public class TAController : ApiController
    {
        public class TAItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
        }
        TAItem[] items = new TAItem[]
        {
            new TAItem { Id = 1, Name = "TA1", Category = "Groceries", Price = 1 },
            new TAItem { Id = 2, Name = "TA2", Category = "Toys", Price = 3.75M },
            new TAItem { Id = 3, Name = "TA3", Category = "Hardware", Price = 16.99M },
            new TAItem { Id = 4, Name = "TA4", Category = "Hardware", Price = 6.12M }
        };

        public IEnumerable<TAItem> Get()
        {
            return items;
        }

        public TAItem Get(int id)
        {
            var product = items.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<TAItem> Get(string category)
        {
            return items.Where(p => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// Web Server (Self Host).
    /// </summary>
    public class WebServer
    {
        private string baseAddress = "http://localhost:9002/";
        private IDisposable server = null;

        public void Start()
        {
            if (null == server)
            {
                server = WebApp.Start<StartUp>(url: baseAddress);
            }
        }
        public void Shutdown()
        {
            if (null != server)
            {
                server.Dispose();
            }
            server = null;
        }
    }

    #endregion

    #region TADataService (Core service)

    /// <summary>
    /// TA Data Service. (Core service).
    /// </summary>
    public class TADataService : NServiceBase
    {
        #region Internal Variables

        private bool _running = false;
        private bool _pause = true;

        private WebServer _server = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TADataService() : base()
        {
            _server = new WebServer();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TADataService()
        {
            if (null != _server)
            {
                _server.Shutdown();
            }
            _server = null;
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
            if (null != _server) _server.Start();
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
            if (null != _server) _server.Shutdown();
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
