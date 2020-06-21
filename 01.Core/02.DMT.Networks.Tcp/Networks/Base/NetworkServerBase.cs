#region Using

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Reflection;
using System.Text;
using NLib;

#endregion

namespace DMT.Networks
{
    public abstract class ProtocolProcesser
    {
        public abstract string ProcessMessage(string message);
    }

    /// <summary>
    /// The DMTNetworkClientBase abstract class.
    /// </summary>
    public abstract class DMTNetworkServerBase : DMTNetworkBase
    {
        #region Internal Class

        /// <summary>
        /// Local Server Connection Info
        /// </summary>
        protected internal class LocalServerConnectionInfo
        {
            #region Internal Variable

            private Socket _socket = null;
            private byte[] _buffers = null;

            #endregion

            #region Constructor and Destructor

            /// <summary>
            /// Constructor
            /// </summary>
            public LocalServerConnectionInfo() : base() { }
            /// <summary>
            /// Destructor
            /// </summary>
            ~LocalServerConnectionInfo()
            {
                Free();
            }

            #endregion

            #region Public Method

            /// <summary>
            /// Free the socket.
            /// </summary>
            public void Free()
            {
                if ((null != _socket) && _socket.Connected)
                {
                    try
                    {
                        _socket.Shutdown(SocketShutdown.Both);
                    }
                    catch //(Exception ex1)
                    {
                    }
                    try
                    {
                        _socket.Close(500);
                    }
                    catch //(Exception ex2)
                    {
                    }
                }
                // free socket reference.
                _socket = null;
                _buffers = null;

                // release resource.
                NLib.NGC.FreeGC(this);
            }

            #endregion

            #region Public Property

            /// <summary>
            /// Get/Set Socket
            /// </summary>
            public Socket Socket
            {
                get { return _socket; }
                set
                {
                    _socket = value;
                }
            }
            /// <summary>
            /// Get/Set Buffers
            /// </summary>
            public byte[] Buffers
            {
                get { return _buffers; }
                set
                {
                    _buffers = value;
                }
            }

            #endregion
        }

        #endregion

        #region Internal Variable

        private Socket _serverSocket = null;
        private int _portNumber = DMTNetworkConsts.LocalServerPortNumber;
        private List<LocalServerConnectionInfo> _clientConnections = new List<LocalServerConnectionInfo>();
        private long connection_count = 0;
        private ProtocolProcesser _processor = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DMTNetworkServerBase() : base() { }
        /// <summary>
        /// Destructor
        /// </summary>
        ~DMTNetworkServerBase()
        {
            Shutdown(); // call shutdown to clean up resource
            _clientConnections = null; // free collection reference
            // Free Memory
            NGC.FreeGC(this);
        }

        #endregion

        #region Private Method

        #region Server Socket - Create/Free

        /// <summary>
        /// Create Server Socket.
        /// </summary>
        private void CreateServerSocket()
        {
            lock (this)
            {
                try
                {
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, _portNumber);
                    // Create New Socket
                    _serverSocket = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
                    // This code prevent error when rebind listener
                    _serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                    _serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
                    // Bind and Listen
                    _serverSocket.Bind(serverEndPoint); // Bind to EndPoint

                    //_serverSocket.Listen((int)SocketOptionName.MaxConnections); // set to maximum.
                    _serverSocket.Listen(5000); // set to 5000.
                }
                catch //(Exception ex)
                {
                    //Err("CreateServerSocket Detected Exception.");
                    //Err(ex);
                    Shutdown();
                }
            }
        }
        /// <summary>
        /// Free Server Socket
        /// </summary>
        private void FreeServerSocket()
        {
            lock (this)
            {
                if (_serverSocket != null)
                {
                    MethodBase med = MethodBase.GetCurrentMethod();
                    try
                    {
                        _serverSocket.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception shutdownEx)
                    {
                        med.Err(shutdownEx, "FreeServerSocket - Shutdown Server Socket Error.");
                    }
                    try
                    {
                        _serverSocket.Disconnect(true);
                    }
                    catch (Exception disconnectEx)
                    {
                        med.Err(disconnectEx, "FreeServerSocket - Disconnect Server Socket Error.");
                    }
                    try
                    {
                        _serverSocket.Close(DMTNetworkConsts.CloseTimeout);
                    }
                    catch (Exception closeEx)
                    {
                        med.Err(closeEx, "FreeServerSocket - Close Server Socket Error.");
                    }
                }

                // Free resource
                NGC.FreeGC(_serverSocket);
            }

            _serverSocket = null; // Free reference
        }

        #endregion

        #region Server Socket Async Method

        /// <summary>
        /// Async Accept Callback
        /// </summary>
        /// <param name="result">The IAsyncResult instance.</param>
        private void AsyncAcceptCallback(IAsyncResult result)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (_serverSocket != null)
                {
                    try
                    {
                        LocalServerConnectionInfo connection = new LocalServerConnectionInfo();
                        long connection_number;

                        // get client socket
                        connection.Socket = (null != _serverSocket) ?
                            _serverSocket.EndAccept(result) : null;
                        // add connection to list.
                        if (null != connection.Socket)
                        {
                            connection.Buffers = new byte[DMTNetworkConsts.BUFFER_SIZE]; // init buffer size.
                            _clientConnections.Add(connection);
                            // increase counter
                            connection_number = Interlocked.Increment(ref connection_count);
                            // Begin Received current client data
                            connection.Socket.BeginReceive(connection.Buffers,
                                0, connection.Buffers.Length,
                                SocketFlags.None,
                                new AsyncCallback(ReceiveCallback),
                                connection);
                        }
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex, "Detected exception in Begin received client data.");
                    }

                    try
                    {
                        // Begin Accept Next connection
                        if (_serverSocket != null)
                        {
                            _serverSocket.BeginAccept(
                                new AsyncCallback(AsyncAcceptCallback),
                                null);
                        }
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex, "Detected exception in accept new connection.");
                    }
                }
                else
                {
                    Shutdown();
                }
            }
            catch (ObjectDisposedException /*ex*/)
            {
                Shutdown();
            }
            catch (Exception ex)
            {
                med.Err(ex, "Detected exception in accept new connection.");
                Shutdown();
            }
        }
        /// <summary>
        /// Receive Callback
        /// </summary>
        /// <param name="result">The IAsyncResult instance.</param>
        private void ReceiveCallback(IAsyncResult result)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            LocalServerConnectionInfo connection = (LocalServerConnectionInfo)result.AsyncState;

            try
            {
                if (connection != null && connection.Socket != null)
                {
                    int num_read = connection.Socket.EndReceive(result);
                    if (0 != num_read)
                    {
                        byte[] data = new byte[num_read];
                        Buffer.BlockCopy(connection.Buffers, 0, data, 0, num_read);
                        UTF8Encoding encoder = new UTF8Encoding();

                        string ackMsg = ProcessMessage(encoder.GetString(data));

                        if (ackMsg != string.Empty)
                        {
                            byte[] dataToSend = encoder.GetBytes(ackMsg);
                            // test call by IAsyncResult parameter
                            connection.Socket.Send(dataToSend, dataToSend.Length,
                                            SocketFlags.None);
                        }
                        // received next client data
                        connection.Socket.BeginReceive(connection.Buffers,
                            0, connection.Buffers.Length,
                            SocketFlags.None,
                            this.ReceiveCallback/*new AsyncCallback(ReceiveCallback)*/,
                            connection);
                    }
                    else
                    {
                        long connection_number;

                        connection_number = Interlocked.Decrement(ref connection_count);

                        _clientConnections.Remove(connection);
                    }
                }
            }
            catch (SocketException socketException)
            {
                if (socketException.ErrorCode == 10054)
                {
                    // WSAECONNRESET, the other side closed impolitely
                    CloseSocket(connection);
                }
                else
                {
                    // Other error
                    CloseSocket(connection);
                }
            }
            catch (ObjectDisposedException)
            {
                // The socket was closed out from under me
                CloseSocket(connection);
            }
            catch (NullReferenceException)
            {
                // The socket was null
                CloseSocket(connection);
            }
            catch (Exception ex)
            {
                med.Err(ex, "Detected exception in accept new connection.");
                CloseSocket(connection);
            }
        }

        #endregion

        #region Client connection management

        /// <summary>
        /// Disconnect All Clients.
        /// </summary>
        protected internal void DisconnectAllClients()
        {
            if (_clientConnections == null)
                return;

            LocalServerConnectionInfo[] tmpclients = null;

            lock (_clientConnections)
            {
                tmpclients = _clientConnections.ToArray();
            }

            // call DisconnectByServer for all client. do not used with lock because
            // if error occur this will cause deadlock.
            foreach (LocalServerConnectionInfo client in tmpclients)
            {
                if (client != null)
                {
                    client.Free(); // server force client connection disconnect.
                }
            }
            lock (this) // block access
            {
                _clientConnections.Clear();
            }
        }
        /// <summary>
        /// Close Socket.
        /// </summary>
        /// <param name="connection">The connection info.</param>
        protected internal void CloseSocket(LocalServerConnectionInfo connection)
        {
            long connection_number;

            connection_number = Interlocked.Decrement(ref connection_count);

            lock (_clientConnections)
            {
                _clientConnections.Remove(connection);
            }
            try
            {
                // try to free client socket. optional call.
                connection.Free();
            }
            catch { }
        }

        #endregion

        #endregion

        #region Protected Method

        #region Protocol processer

        /// <summary>
        /// Get/Set Protocol processer
        /// </summary>
        protected ProtocolProcesser Processer
        {
            get { return _processor; }
            set { _processor = value; }
        }

        #endregion

        #region OnStart/OnShutdown

        /// <summary>
        /// OnStart
        /// </summary>
        protected virtual void OnStart() { }
        /// <summary>
        /// OnShutdown
        /// </summary>
        protected virtual void OnShutdown() { }

        #endregion

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Process incoming message and return acknownledge message.
        /// </summary>
        /// <param name="message">The incoming message.</param>
        /// <returns>Return the acknownledge message.</returns>
        protected virtual string ProcessMessage(string message)
        {
            if (_processor == null)
                return string.Empty; // no protocol processor

            return _processor.ProcessMessage(message);
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Start server.
        /// </summary>
        public void Start()
        {
            if (ApplicationManager.Instance.IsExit)
            {
                return;
            }
            if (_serverSocket != null)
                return;
            if (_serverSocket == null)
            {
                CreateServerSocket();
                OnStart();
                if (_serverSocket != null)
                {
                    try
                    {
                        _serverSocket.BeginAccept(
                            new AsyncCallback(AsyncAcceptCallback),
                            null);
                    }
                    catch (SocketException /*sockEx*/)
                    {
                        Shutdown();
                    }
                    catch (Exception /*ex*/)
                    {
                        Shutdown();
                    }
                }
            }
        }
        /// <summary>
        /// Shutdown server.
        /// </summary>
        public void Shutdown()
        {
            DisconnectAllClients();
            FreeServerSocket();
            OnShutdown();
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Get/Set Port Number.
        /// </summary>
        public int PortNumber
        {
            get { return _portNumber; }
            set
            {
                if (IsRunning)
                    return; // cannot change port number while running.
                if (_portNumber != value)
                {
                    _portNumber = value;
                }
            }
        }
        /// <summary>
        /// Check is running.
        /// </summary>
        public bool IsRunning
        {
            get { return _serverSocket != null; }
        }

        #endregion
    }
}
