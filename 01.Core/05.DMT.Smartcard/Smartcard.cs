#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

#endregion

namespace DMT.Smartcard
{
    #region DelegateExtensionMethods

    /// <summary>
    /// Delegate extension methods (Internal for Singelton class).
    /// </summary>
    internal static class SingeltonDelegateExtensionMethods
    {
        /// <summary>
        /// Extension method which marshals events back onto the main thread
        /// </summary>
        /// <param name="multicast">The MulticastDelegate instance.</param>
        /// <param name="sender">The sender instance.</param>
        /// <param name="args">The arguments for delegate.</param>
        public static void Raise(this MulticastDelegate multicast, object sender, EventArgs args)
        {
            foreach (Delegate del in multicast.GetInvocationList())
            {
                // Try for WPF first
                DispatcherObject dispatcherTarget = del.Target as DispatcherObject;
                if (dispatcherTarget != null && !dispatcherTarget.Dispatcher.CheckAccess())
                {
                    // WPF target which requires marshaling
                    dispatcherTarget.Dispatcher.BeginInvoke(del, sender, args);
                }
                else
                {
                    // Maybe its WinForms?
                    ISynchronizeInvoke syncTarget = del.Target as ISynchronizeInvoke;
                    if (syncTarget != null && syncTarget.InvokeRequired)
                    {
                        // WinForms target which requires marshaling
                        syncTarget.BeginInvoke(del, new object[] { sender, args });
                    }
                    else
                    {
                        // Just do it.
                        del.DynamicInvoke(sender, args);
                    }
                }
            }
        }
        /// <summary>
        /// Extension method which marshals actions back onto the main thread
        /// </summary>
        /// <typeparam name="T">The target class.</typeparam>
        /// <param name="action">The action delegate.</param>
        /// <param name="args">The arguments for delegate.</param>
        public static void Raise<T>(this Action<T> action, T args)
        {
            // Try for WPF first
            DispatcherObject dispatcherTarget = action.Target as DispatcherObject;
            if (dispatcherTarget != null && !dispatcherTarget.Dispatcher.CheckAccess())
            {
                // WPF target which requires marshaling
                dispatcherTarget.Dispatcher.BeginInvoke(action, args);
            }
            else
            {
                // Maybe its WinForms?
                ISynchronizeInvoke syncTarget = action.Target as ISynchronizeInvoke;
                if (syncTarget != null && syncTarget.InvokeRequired)
                {
                    // WinForms target which requires marshaling
                    syncTarget.BeginInvoke(action, new object[] { args });
                }
                else
                {
                    // Just do it.
                    action.DynamicInvoke(args);
                }
            }
        }
    }

    #endregion

    #region NTheadSingelton<T> class

    /// <summary>
    /// The NTheadSingelton<T> class
    /// </summary>
    /// <typeparam name="T">The target class.</typeparam>
    public abstract class NTheadSingelton<T> where T : NTheadSingelton<T>
    {
        #region Internal Variables

        private Thread _th;
        private bool _running = false;
        private bool _isExit = false;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected NTheadSingelton()
        {
            if (null != System.Windows.Application.Current)
            {
                System.Windows.Application.Current.Exit += Current_Exit;
                System.Windows.Application.Current.SessionEnding += Current_SessionEnding;
            }
            if (null != System.Windows.Forms.Form.ActiveForm)
            {
                System.Windows.Forms.Application.ThreadExit += Application_ThreadExit;
                System.Windows.Forms.Application.ApplicationExit += Application_ApplicationExit;
            }
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~NTheadSingelton()
        {
            Shutdown();
            if (null != System.Windows.Forms.Form.ActiveForm)
            {
                System.Windows.Forms.Application.ThreadExit -= Application_ThreadExit;
                System.Windows.Forms.Application.ApplicationExit -= Application_ApplicationExit;
            }
            if (null != System.Windows.Application.Current)
            {
                System.Windows.Application.Current.Exit -= Current_Exit;
                System.Windows.Application.Current.SessionEnding -= Current_SessionEnding;
            }
        }

        #endregion

        #region Protected Variables

        /// <summary>
        /// Protected access thread name.
        /// </summary>
        protected string ThreadName = "";
        /// <summary>
        /// Gets is application in exit state.
        /// </summary>
        protected bool IsExit { get { return _isExit; } }

        #endregion

        #region Private Methods


        private void Application_ThreadExit(object sender, EventArgs e)
        {
            //Console.WriteLine("WindowForm:Application.ThreadExit");
            _isExit = true;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            //Console.WriteLine("WindowForm:Application.Exit");
            _isExit = true;
        }

        private void Current_SessionEnding(object sender, System.Windows.SessionEndingCancelEventArgs e)
        {
            //Console.WriteLine("WPF:Current.SessionEnding");
            _isExit = true;
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            //Console.WriteLine("WPF:Current.Exit");
            _isExit = true;
        }

        private void Processing()
        {
            while (null != _th && _running && !_isExit)
            {
                OnProcessing();
            }
            Shutdown();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// OnProcessing virtual method.
        /// </summary>
        protected virtual void OnProcessing() { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start service.
        /// </summary>
        public void Start()
        {
            if (null == _th)
            {
                _th = new Thread(this.Processing);
                _th.Priority = ThreadPriority.BelowNormal;
                _th.Name = ThreadName;
                _th.IsBackground = true;

                _running = true;

                _th.Start();
            }
        }
        /// <summary>
        /// Shutdown service.
        /// </summary>
        public void Shutdown()
        {
            Console.WriteLine("Shutdown");
            _running = false;
            if (null != _th)
            {
                try
                {
                    _th.Abort();
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
                _th = null;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets is server is running.
        /// </summary>
        public bool IsRunning { get { return (null != _th && _running); } }

        #endregion

        #region Singelton

        /// <summary>
        /// Static Readonly property.
        /// </summary>
        private static readonly Lazy<T> Lazy =
            new Lazy<T>(() => {
                T ret = default(T);
                lock (typeof(T))
                {
                    ret = Activator.CreateInstance(typeof(T), true) as T;
                }
                return ret;
            });
        /// <summary>
        /// Gets singleton instance.
        /// </summary>
        public static T Instance => Lazy.Value;

        #endregion
    }

    #endregion

    #region SmartcardService

    /// <summary>
    /// The Smartcard Service class.
    /// </summary>
    public class SmartcardService : NTheadSingelton<SmartcardService>
    {
        #region Internal Variables

        private DateTime _lastUpdate = DateTime.MinValue;
        private string _cardSN = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected SmartcardService() : base()
        {
            this.ThreadName = "Smartcard service thread";
        }

        #endregion

        #region Override methods

        /// <summary>
        /// OnProcessing override.
        /// </summary>
        protected override void OnProcessing()
        {
            TimeSpan ts = DateTime.Now - _lastUpdate;
            if (ts.TotalMilliseconds > 250)
            {
                // TODO: Read card from device.

                // raise event.
                OnTick.Raise(this, EventArgs.Empty);
                _lastUpdate = DateTime.Now;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update Card Serial Number.
        /// </summary>
        /// <param name="cardSN">The card serial number.</param>
        public void Update(string cardSN)
        {
            _cardSN = cardSN;
            // raise event.
            OnCardRead.Raise(this, EventArgs.Empty);
        }
        /// <summary>
        /// Clear card serial number.
        /// </summary>
        public void Clear()
        {
            _cardSN = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the last card serial number (4 bytes) in string.
        /// </summary>
        public string CardSN { get { return _cardSN; } }

        #endregion

        #region Public events

        /// <summary>
        /// OnTick EventHandler.
        /// </summary>
        public event EventHandler OnTick;
        /// <summary>
        /// OnCardRead EventHandler.
        /// </summary>
        public event EventHandler OnCardRead;

        #endregion
    }

    #endregion
}
