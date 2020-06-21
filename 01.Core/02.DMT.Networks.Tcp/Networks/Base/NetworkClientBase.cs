#region Using

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Reflection;

using NLib;

#endregion

namespace DMT.Networks
{
    /// <summary>
    /// The DMTNetworkClientBase abstract class.
    /// </summary>
    public abstract class DMTNetworkClientBase : DMTNetworkBase
    {
        #region Internal Class

        /// <summary>
        /// Client Connection State
        /// </summary>
        protected internal class ClientConnectionState
        {
            #region Internal Variable

            private Socket _socket = null;
            private long _connID = 0;
            private bool _isFinished = false;
            private bool _isTimeout = false;
            private byte[] _incomingMessage = new byte[DMTNetworkConsts.BUFFER_SIZE];
            private string _fullMessage = string.Empty;

            #endregion

            #region Constructor and Destructor

            /// <summary>
            /// Constructor
            /// </summary>
            public ClientConnectionState()
                : base()
            {
            }
            /// <summary>
            /// Destructor
            /// </summary>
            ~ClientConnectionState()
            {
                Free();
            }

            #endregion

            #region Public Method

            /// <summary>
            /// Free resource.
            /// </summary>
            public void Free()
            {
                if (_socket != null && _socket.Connected)
                {
                    try
                    {
                        _socket.Close(500);
                    }
                    catch { }
                }
                _socket = null;
                _incomingMessage = null;
                // Free resource.
                NGC.FreeGC(this);
            }

            #endregion

            #region Public Property

            /// <summary>
            /// Get/Set Socket.
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
            /// Get/Set Thread ID.
            /// </summary>
            public long ThreadID
            {
                get { return _connID; }
                set
                {
                    _connID = value;
                }
            }
            /// <summary>
            /// Get/Set Incomming message buffers.
            /// </summary>
            public byte[] IncomingMessage
            {
                get { return _incomingMessage; }
                set
                {
                    _incomingMessage = value;
                }
            }
            /// <summary>
            /// Get/Set is finished
            /// </summary>
            public bool IsFinished
            {
                get { return _isFinished; }
                set
                {
                    _isFinished = value;
                }
            }
            /// <summary>
            /// Get/Set Is timeout.
            /// </summary>
            public bool IsTimeout
            {
                get { return _isTimeout; }
                set { _isTimeout = value; }
            }
            /// <summary>
            /// Get/Set Full Message in string.
            /// </summary>
            public string FullMessage
            {
                get { return _fullMessage; }
                set
                {
                    _fullMessage = value;
                }
            }

            #endregion
        }

        #endregion

        #region Internal Variable

        private string _hostName = string.Empty;
        private int _portNumber = DMTNetworkConsts.LocalServerPortNumber;
        private Socket _socket = null;
        private ClientConnectionState _state = null;
        private static long _currThreadID = 0;

        //private int timeoutMSec = 5000; // 5 sec.
        private int timeoutMSec = DMTNetworkConsts.WaitAckTimeOut; // 3 sec.

        private ManualResetEvent connectTimeOutObject = new ManualResetEvent(false);
        private Exception connectException = null;
        private bool _isConnectSuccess = false;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DMTNetworkClientBase() : base() { }
        /// <summary>
        /// Destructor
        /// </summary>
        ~DMTNetworkClientBase()
        {
            Disconnect();
        }

        #endregion

        #region Private Method

        private void ConnectCallBack(IAsyncResult asyncresult)
        {
            try
            {
                bool hasError = true;
                _isConnectSuccess = false;
                Socket socket = asyncresult.AsyncState as Socket;

                if (socket != null)
                {
                    socket.EndConnect(asyncresult);
                    try
                    {
                        hasError = socket.Poll(100, SelectMode.SelectError);
                    }
                    catch (Exception selectEx)
                    {
                        connectException = selectEx;
                        hasError = true;
                    }
                    // set connection status
                    _isConnectSuccess = (!hasError && socket.Connected);
                }
            }
            catch (Exception ex)
            {
                // setup exception and connection status
                _isConnectSuccess = false;
                connectException = ex;
            }
            finally
            {
                // change state
                connectTimeOutObject.Set();
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            ClientConnectionState aState = (ClientConnectionState)result.AsyncState;
            aState.IsFinished = true;

            byte[] data = aState.IncomingMessage;

            try
            {
                if (null != aState && null != aState.Socket)
                {
                    int num_read = 0;
                    try
                    {
                        num_read = aState.Socket.EndReceive(result);
                        if (0 != num_read)
                        {
                            UTF8Encoding encoder = new UTF8Encoding();
                            // append message
                            aState.FullMessage += encoder.GetString(data, 0, num_read);

                            if (aState.Socket != null)
                            {
                                aState.Socket.BeginReceive(aState.IncomingMessage, 0, aState.IncomingMessage.Length,
                                    SocketFlags.None, new AsyncCallback(ReceiveCallback), aState);
                            }
                        }
                        else
                        {
                            //state.socket.Close();
                        }
                    }
                    catch //(Exception ex)
                    {
                        // some condition make aState.Socket to null                        
                        if (aState != null && aState.Socket != null)
                        {
                            aState.Socket.Close();
                        }
                    }
                }
            }
            catch (SocketException sockEx)
            {
                med.Err(sockEx, "Detected Socket exception in ReceiveCallback.");

                try
                {
                    if (aState != null && aState.Socket != null)
                    {
                        aState.Socket.Close();
                    }
                }
                catch { }
            }
            catch (ObjectDisposedException)
            {
                if (aState != null && aState.Socket != null)
                {
                    aState.Socket.Close();
                }
            }
        }

        #endregion

        #region Protected Method

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callback">The callback handler for handle ack message.</param>
        protected internal void Send(string message, AckMessageCallback callback)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            if (this.IsConnected)
            {
                _state = new ClientConnectionState();
                if (_currThreadID >= long.MaxValue - 1)
                    _currThreadID = 0;
                _state.ThreadID = _currThreadID++;
                _state.Socket = _socket;
                _state.IsTimeout = false;

                DateTime sendTime = DateTime.Now;
                UTF8Encoding encoder = new System.Text.UTF8Encoding();
                _socket.Send(encoder.GetBytes(message));
                _socket.BeginReceive(_state.IncomingMessage, 0, _state.IncomingMessage.Length,
                    SocketFlags.None,
                    new AsyncCallback(ReceiveCallback),
                    _state);

                while (IsConnected && !_state.IsFinished && !_state.IsTimeout)
                {
                    TimeSpan ts = DateTime.Now - sendTime;
                    if (ts.TotalMilliseconds > timeoutMSec)
                    {
                        _state.IsTimeout = true; // set timeout
                    }

                    if (callback != null)
                    {
                        callback(message, _state.FullMessage, _state.IsFinished, _state.IsTimeout);
                    }

                    ApplicationManager.Instance.DoEvents(); // do something
                    System.Threading.Thread.Sleep(10);
                }

                if (_state.FullMessage != string.Empty)
                {
                    if (callback != null)
                    {
                        callback(message, _state.FullMessage, _state.IsFinished, _state.IsTimeout);
                    }
                }
            }
        }

        #endregion

        #region Public Method

        #region Connect/Disconnect

        /// <summary>
        /// Connect to server.
        /// </summary>
        public ConnectResult Connect()
        {
            if (ApplicationManager.Instance.IsExit)
            {
                return null;
            }
            ConnectResult result = null;
            if (_socket != null)
            {
                #region Already Connect

                result = new ConnectResult();
                result.Status = ConnectStatus.AlreadyConnected;
                result.Error = DMTNetworkException.Create("Already connected.");
                return result; // already connect

                #endregion
            }

            #region set timeout and error object

            connectTimeOutObject.Reset(); // reset connection timeout object.
            connectException = null; // clear exception

            #endregion

            #region Create socket and set options

            _socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            // This code prevent error when rebind listener
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
            // force automaticall discard data send when close timeout expire.
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, false);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, DMTNetworkConsts.SendTimeOut);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, DMTNetworkConsts.RecvTimeOut);

            #endregion

            try
            {
                //_socket.Connect(this.HostName, this.PortNumber);
                _socket.BeginConnect(this.HostName, this.PortNumber,
                    new AsyncCallback(this.ConnectCallBack), _socket);
                if (connectTimeOutObject.WaitOne(timeoutMSec, false))
                {
                    if (_isConnectSuccess)
                    {
                        #region Success

                        result = new ConnectResult();
                        result.Status = ConnectStatus.Success;
                        result.Error = null;

                        #endregion
                    }
                    else
                    {
                        #region Socket error

                        Disconnect();

                        result = new ConnectResult();
                        result.Status = ConnectStatus.Error;
                        result.Error = connectException;

                        #endregion
                    }
                }
                else
                {
                    #region Timeout

                    Disconnect();

                    result = new ConnectResult();
                    result.Status = ConnectStatus.Timeout;
                    result.Error = DMTNetworkException.Create("Connect Timeout.");

                    #endregion
                }
            }
            catch (Exception otherEx)
            {
                #region Other Exception

                Disconnect();

                result = new ConnectResult();
                result.Status = ConnectStatus.Error;
                result.Error = otherEx;

                #endregion
            }
            return result; // socket error
        }
        /// <summary>
        /// Disconnect from server.
        /// </summary>
        public void Disconnect()
        {
            if (_socket != null)
            {
                try { _socket.Shutdown(SocketShutdown.Both); }
                catch { }
                try
                {
                    // set true for reused make process very slow
                    _socket.Disconnect(false);
                }
                catch { }
                try { _socket.Close(50); }
                catch { }
            }

            if (_socket != null)
            {
                NGC.FreeGC(_socket);
            }
            if (_state != null)
            {
                _state.Free();
                NGC.FreeGC(_state);
            }

            _socket = null;
            _state = null;

            NGC.FreeGC(this);
        }

        #endregion

        #endregion

        #region Public Property

        /// <summary>
        /// Get/Set Server Host Name or IP Address.
        /// </summary>
        public string HostName
        {
            get { return _hostName; }
            set
            {
                if (_hostName != value)
                {
                    _hostName = value;
                }
            }
        }
        /// <summary>
        /// Get/Set Server Port Number to connected.
        /// </summary>
        public int PortNumber
        {
            get { return _portNumber; }
            set
            {
                if (_portNumber != value)
                {
                    _portNumber = value;
                }
            }
        }
        /// <summary>
        /// Check is connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (_socket != null && _socket.Connected);
            }
        }

        #endregion
    }
}
