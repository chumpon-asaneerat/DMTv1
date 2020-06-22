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

// Owin SelfHost
using Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Web.Http;
using NLib.Reflection;

#endregion

namespace DMT.Services
{
    #region PlazaDataServiceManager (Installer)

    /// <summary>
    /// Plaza Data Service Manager.
    /// </summary>
    [RunInstaller(true)]
    public class PlazaDataServiceManager : NServiceInstaller
    {
        #region Internal Variables

        private PlazaDataService _service = new PlazaDataService();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaDataServiceManager()
            : base()
        {
            ServiceName = AppConsts.WindowsService.Plaza.ServiceName;
            DisplayName = AppConsts.WindowsService.Plaza.DisplayName;
            Description = AppConsts.WindowsService.Plaza.Description;
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

            // Controllers with Actions
            
            // To handle routes like `/api/controller/action`
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}"
            );

            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */
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

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class SearchOptions
    {
        public bool Active { get; set; }
    }

    public class Supervisor : User
    {
        public bool Active { get; set; }
    }

    public class Collector : User
    {
        public string SupervisorId { get; set; }
        public bool Active { get; set; }
    }

    public class SupervisorController : ApiController
    {
        private List<User> users = new List<User>();

        public SupervisorController()
        {
            var items = new User[] {
                new Supervisor() { Id = "1", Name = "Sup1", Active = false },
                new Collector() { Id = "1", SupervisorId = "1" },
                new Collector() { Id = "2", SupervisorId = "1" },
                new Collector() { Id = "3", SupervisorId = "1" },

                new Supervisor() { Id = "2", Name = "Sup2", Active = true },
                new Collector() { Id = "3", SupervisorId = "2" },
                new Collector() { Id = "4", SupervisorId = "2" },
                new Collector() { Id = "5", SupervisorId = "2" }
            };
            users.AddRange(items);
        }

        [HttpPost]
        [ActionName("ActiveSups")]
        public List<Supervisor> GetSupervisors([FromBody] SearchOptions options)
        {
            List<Supervisor> results = new List<Supervisor>();

            var matchs = users.FindAll(
                p => p is Supervisor && (p as Supervisor).Active == options.Active
            ).ToList();

            matchs.ForEach(p =>
            {
                if (p is Supervisor) results.Add(p as Supervisor);
            });
            
            return results;
        }
        [HttpPost]
        [ActionName("ListCollectorsUnderSup")]
        public List<Collector> GetCollectors([FromBody] Supervisor sup)
        {
            List<Collector> results = new List<Collector>();

            if (null != sup)
            {
                var matchs = users.FindAll(
                    p => p is Collector && (p as Collector).SupervisorId == sup.Id
                ).ToList();
                matchs.ForEach(p =>
                {
                    if (p is Collector) results.Add(p as Collector);
                });
            }
            else
            {
                var matchs = users.FindAll(
                    p => p is Collector
                ).ToList();
                matchs.ForEach(p =>
                {
                    if (p is Collector) results.Add(p as Collector);
                });
            }

            return results;
        }
    }

    /*
    /// <summary>
    /// Route Controller.
    /// </summary>
    public class PlazaController : ApiController
    {
        public class Plaza
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
        }
        Plaza[] plazas = new Plaza[]
        {
            new Plaza { Id = 1, Name = "Dindang", Category = "Groceries", Price = 1 },
            new Plaza { Id = 2, Name = "Ramintra", Category = "Toys", Price = 3.75M },
            new Plaza { Id = 3, Name = "Doanmeung", Category = "Hardware", Price = 16.99M },
            new Plaza { Id = 4, Name = "Bangna", Category = "Hardware", Price = 6.12M }
        };

        public IEnumerable<Plaza> Get()
        {
            return plazas;
        }

        public Plaza Get(int id)
        {
            var product = plazas.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Plaza> Get(string category)
        {
            return plazas.Where(p => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }

        public string Post([FromBody] Plaza value)
        {
            Console.WriteLine("Plaza: {0}", value.Name);
            return "OK!";
        }
    }

    /// <summary>
    /// Route Controller.
    /// </summary>
    public class TODController : ApiController
    {
        public class TODItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
        }
        TODItem[] items = new TODItem[]
        {
            new TODItem { Id = 1, Name = "Change Shift", Category = "Groceries", Price = 1 },
            new TODItem { Id = 2, Name = "Revenue Entry", Category = "Toys", Price = 3.75M },
            new TODItem { Id = 3, Name = "Print Slip", Category = "Hardware", Price = 16.99M },
            new TODItem { Id = 4, Name = "Daily Reprots", Category = "Hardware", Price = 6.12M }
        };

        public IEnumerable<TODItem> Get()
        {
            return items;
        }

        public TODItem Get(int id)
        {
            var product = items.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<TODItem> Get(string category)
        {
            return items.Where(p => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
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
    */

    /// <summary>
    /// Web Server (Self Host).
    /// </summary>
    public class WebServer
    {
        private string baseAddress = string.Format(@"http://localhost:{0}/", 
            AppConsts.WindowsService.Plaza.LocaWebServer.PortNumber);
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

    #region PlazaDataService (Core service)

    /// <summary>
    /// Plaza Data Service. (Core service).
    /// </summary>
    public class PlazaDataService : NServiceBase
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
        public PlazaDataService() : base()
        {
            _server = new WebServer();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~PlazaDataService()
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
