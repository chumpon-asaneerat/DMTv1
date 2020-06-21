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
}
