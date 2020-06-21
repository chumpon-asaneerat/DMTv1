#region Using

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Text;

#endregion

namespace DMT.Networks
{
    #region DMTNetworkConsts

    /// <summary>
    /// The DMTNetworkConsts class.
    /// </summary>
    public class DMTNetworkConsts
    {
        /// <summary>
        /// The default Buffer Size
        /// </summary>
        public static int BUFFER_SIZE = 16 * 1024; // 16KB
        /// <summary>
        /// LocalServerServer Port Number. Fixed room inquiry server port to 2480.
        /// </summary>
        public static int LocalServerPortNumber = 2480;
        /// <summary>
        /// General Send timeout value.
        /// </summary>
        public static int SendTimeOut = 3000; // 3 sec.
        /// <summary>
        /// General Recv timeout value.
        /// </summary>
        public static int RecvTimeOut = 3000; // 3 sec.
        /// <summary>
        /// General Wait for Ack timeout value.
        /// </summary>
        public static int WaitAckTimeOut = 3000; // 3 sec.
        /// <summary>
        /// General close port time out.
        /// </summary>
        public static int CloseTimeout = 500; // 500 ms.
    }

    #endregion

    #region Connect Status

    /// <summary>
    /// Connect Status
    /// </summary>
    public enum ConnectStatus
    {
        /// <summary>
        /// Initialize state
        /// </summary>
        None = 0,
        /// <summary>
        /// Connect success.
        /// </summary>
        Success = 1,
        /// <summary>
        /// Connect fail because timeout.
        /// </summary>
        Timeout = 2,
        /// <summary>
        /// Connect fail because other error.
        /// </summary>
        Error = 3,
        /// <summary>
        /// Already connected.
        /// </summary>
        AlreadyConnected = 4
    }

    #endregion

    #region Connect Result

    /// <summary>
    /// Connect Result
    /// </summary>
    public class ConnectResult
    {
        /// <summary>
        /// The connect status
        /// </summary>
        public ConnectStatus Status = ConnectStatus.None;
        /// <summary>
        /// The exception instance
        /// </summary>
        public Exception Error;
    }

    #endregion

    #region DMT Network Exception

    /// <summary>
    /// DMT Network Exception
    /// </summary>
    public class DMTNetworkException : Exception
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The error message.</param>
        public DMTNetworkException(string message) : base(message) { }

        #endregion

        #region Static Method

        /// <summary>
        /// Create exception.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <returns>Return new exception instance.</returns>
        public static DMTNetworkException Create(string message)
        {
            return new DMTNetworkException(message);
        }

        #endregion
    }

    #endregion

    #region Ack Message Callback Delegate

    /// <summary>
    /// Ack Message Callback
    /// </summary>
    /// <param name="sourceMessage">The source message.</param>
    /// <param name="ackMessage">The ack message.</param>
    /// <param name="isfinished">The finished.</param>
    /// <param name="isTimeout">The timeout.</param>
    public delegate void AckMessageCallback(string sourceMessage,
        string ackMessage, bool isfinished, bool isTimeout);

    #endregion
}
