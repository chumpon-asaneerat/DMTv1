#region Using

//using System.Net;
using RestSharp;
//using RestSharp.Authenticators;

#endregion

namespace DMT.Services
{

    public class NRestClient
    {
        #region Enums

        /// <summary>
        /// The WebProtocol enum.
        /// </summary>
        public enum WebProtocol
        {
            /// <summary>
            /// http protocol.
            /// </summary>
            http,
            /// <summary>
            /// https protocol.
            /// </summary>
            https,
            /// <summary>
            /// Web Socket protocol (for future used).
            /// </summary>
            ws
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRestClient() : this(WebProtocol.http, "localhost", 80) { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="protocol">The web protocol (http, https).</param>
        /// <param name="host">The host name or IP address.</param>
        /// <param name="port">The port number.</param>
        public NRestClient(WebProtocol protocol = WebProtocol.http, 
            string host = "localhost", int port = 9000) : base() 
        {
            this.Protocol = protocol;
            this.Host = host;
            this.Port = port;
        }

        #endregion

        #region Public Methods

        public TReturn Execute<TReturn, TPara>(string apiUrl,
            TPara value)
        {
            string actionUrl = (!apiUrl.StartsWith("/")) ? @"/" + apiUrl : apiUrl;
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(actionUrl, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(value);

            TReturn ret = default;
            var response = client.Execute(request);
            if (null != response && null != response.Content)
            {
                ret = response.Content.FromJson<TReturn>();
            }

            client = null;

            return ret;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets web protocol (http, https).
        /// </summary>
        public WebProtocol Protocol { get; set; }
        /// <summary>
        /// Gets or sets host name or IP address.
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Gets or sets The port number.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Gets the host base(root) url.
        /// </summary>
        public string BaseUrl
        {
            get 
            {
                string sProtocol = string.Empty;
                switch (Protocol)
                {
                    case WebProtocol.http:
                        sProtocol = "http";
                        break;
                    case WebProtocol.https:
                        sProtocol = "https";
                        break;
                    case WebProtocol.ws:
                        sProtocol = "ws";
                        break;
                    default:
                        sProtocol = "http";
                        break;
                }
                string sHost = (string.IsNullOrWhiteSpace(Host.Trim())) ? "localhost" : Host;
                if (Port <= 0 || Port > 65535)
                {
                    // no port.
                    return string.Format(@"{0}://{1}", sProtocol, sHost);
                }
                else
                {
                    // has port.
                    return string.Format(@"{0}://{1}:{2}", sProtocol, sHost, Port);
                }
            }
        }

        #endregion
    }
}
