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
    /// <summary>
    /// The DMTNetworkBase abstract class.
    /// </summary>
    public abstract class DMTNetworkBase
    {
        /// <summary>
        /// Get Local IP Address
        /// </summary>
        /// <returns>Return local IP Address.</returns>
        public static IPAddress GetLocalAddress()
        {
            return Utils.NetworkUtils.GetLocalIPAddress();
        }
    }
}
