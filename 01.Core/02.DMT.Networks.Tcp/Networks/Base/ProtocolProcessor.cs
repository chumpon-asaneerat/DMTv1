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
    #region ProtocolMessage

    /// <summary>
    /// Protocol Message.
    /// </summary>
    public class ProtocolMessage
    {
        #region Internal Variable

        private string _message = string.Empty;
        private List<string> _parameters = new List<string>();

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProtocolMessage()
            : base()
        {
        }
        /// <summary>
        /// Destructor
        /// </summary>
        ~ProtocolMessage()
        {
            if (_parameters != null) _parameters.Clear();
            _parameters = null;
            _message = string.Empty;
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Get/Set Message
        /// </summary>
        public string Message { get { return _message; } set { _message = value; } }
        /// <summary>
        /// Access Parameters
        /// </summary>
        public List<string> Parameters { get { return _parameters; } }

        #endregion
    }

    #endregion

    #region ProtocolExecuteResult

    /// <summary>
    /// Protocol Execute Result.
    /// </summary>
    public class ProtocolExecuteResult
    {
        #region Internal Variable

        private List<string> _results = new List<string>();

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProtocolExecuteResult()
            : base()
        {
        }
        /// <summary>
        /// Destructor
        /// </summary>
        ~ProtocolExecuteResult()
        {
            if (_results != null) _results.Clear();
            _results = null;
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Access Results
        /// </summary>
        public List<string> Results { get { return _results; } }

        #endregion
    }

    #endregion

    #region ProtocolProcesser

    /// <summary>
    /// The Protocol Processer.
    /// </summary>
    public abstract class ProtocolProcesser
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProtocolProcesser() : base() { }
        /// <summary>
        /// Destructor
        /// </summary>
        ~ProtocolProcesser()
        {
        }

        #endregion

        #region Abstract Method

        /// <summary>
        /// Extract Message.
        /// </summary>
        /// <param name="message">The incoming message.</param>
        /// <returns>The Protocol Message that generated from incoming message.</returns>
        protected abstract ProtocolMessage ExtractMessage(string message);
        /// <summary>
        /// Execute protocol message.
        /// </summary>
        /// <param name="message">The Incoming Protocol Message.</param>
        /// <returns>Return Protol execute result.</returns>
        protected abstract ProtocolExecuteResult Execute(ProtocolMessage message);
        /// <summary>
        /// Build outgoing message from execute result.
        /// </summary>
        /// <param name="result">The Protocol Execute result.</param>
        /// <returns>Return the outgoing message that build from execute result.</returns>
        protected abstract string BuildMessage(ProtocolExecuteResult result);
        /// <summary>
        /// Build Message to send.
        /// </summary>
        /// <param name="message">The protocol message.</param>
        /// <returns>Return string that need for send.</returns>
        public abstract string BuildSendMessage(ProtocolMessage message);
        /// <summary>
        /// Build Ack Message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Return ProtocolMessage instance.</returns>
        public abstract ProtocolMessage BuildAckMessage(string message);

        #endregion

        #region Public Method

        /// <summary>
        /// Process incoming message and return acknownledge message.
        /// </summary>
        /// <param name="message">The incoming message.</param>
        /// <returns>Return the acknownledge message.</returns>
        public virtual string ProcessMessage(string message)
        {
            string result = string.Empty;
            ProtocolMessage package = this.ExtractMessage(message);

            if (package != null)
            {
                ProtocolExecuteResult execResult = this.Execute(package);
                if (execResult != null)
                {
                    result = BuildMessage(execResult);
                }
            }

            return result;
        }

        #endregion
    }

    #endregion
}
