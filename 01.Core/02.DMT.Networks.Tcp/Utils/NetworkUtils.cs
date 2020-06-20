#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

#endregion

namespace DMT.Utils
{
    /// <summary>
    /// Network Utils.
    /// </summary>
    public class NetworkUtils
    {
        /// <summary>
        /// Gets all active network adaptors not include loopback adaptor.
        /// </summary>
        /// <returns>Returns array of network interface adaptors.</returns>
        public static NetworkInterface[] GetActiveNetworkInterfaces()
        {
            List<NetworkInterface> Interfaces = new List<NetworkInterface>();

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    Interfaces.Add(nic);
                }
            }

            return Interfaces.ToArray();
        }
        /// <summary>
        /// Gets current active network adaptor not include loopback adaptor.
        /// </summary>
        /// <returns>Returns current active network interface adaptor.</returns>
        public static NetworkInterface GetActiveInterface()
        {
            List<NetworkInterface> Interfaces = new List<NetworkInterface>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    Interfaces.Add(nic);
                }
            }

            NetworkInterface result = null;
            foreach (NetworkInterface nic in Interfaces)
            {
                if (result == null)
                {
                    result = nic;
                }
                else
                {
                    if (nic.GetIPProperties().GetIPv4Properties() != null)
                    {
                        if (nic.GetIPProperties().GetIPv4Properties().Index <
                            result.GetIPProperties().GetIPv4Properties().Index)
                            result = nic;
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Gets IP Address from active network adaptor.
        /// </summary>
        /// <returns>Returns IP Address of current active network interface adaptor.</returns>
        public static IPAddress GetLocalIPAddress()
        {
            return GetLocalIPAddress(GetActiveInterface());
        }
        /// <summary>
        /// Gets IP Address from specificed network adaptor.
        /// </summary>
        /// <param name="nic">The network interface adaptor.</param>
        /// <returns>Returns IP Address of specificed network interface adaptor.</returns>
        public static IPAddress GetLocalIPAddress(NetworkInterface nic)
        {
            IPAddress result = null;
            if (null == nic)
                return result;
            IPInterfaceProperties ipProps = nic.GetIPProperties();
            foreach (UnicastIPAddressInformation uni in ipProps.UnicastAddresses)
            {
                if (uni != null && uni.Address != null &&
                    uni.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    // valid
                    result = uni.Address;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Gets the IP address provide by ISP that used in internet.
        /// When call the respose speed is depend on the internet connect speed so when call
        /// UI may be freeze until the response is returns.
        /// </summary>
        /// <returns>Returns external IP address that used in internet.</returns>
        public static IPAddress GetExternalIPAddress()
        {
            IPAddress result = null;
            try
            {
                // Use a web page that displays the IP of the request.  In this case,
                // I use network-tools.com.  This page has been around for years
                // and is always up when I have tried it.  You could use others or
                // your own. 
                WebRequest myRequest = WebRequest.Create("http://checkip.dyndns.org/");

                // Send request, get response, and parse out the IP address on the page.
                using (WebResponse res = myRequest.GetResponse())
                {
                    using (Stream s = res.GetResponseStream())
                    {
                        using (StreamReader sr =
                            new StreamReader(s, System.Text.Encoding.UTF8))
                        {
                            string html = sr.ReadToEnd();
                            Regex regex =
                                new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                            string ipString = regex.Match(html).Value;

                            //Console.WriteLine("Public IP: " + ipString);
                            if (!IPAddress.TryParse(ipString, out result))
                            {
                                result = null;
                            }
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                //Console.WriteLine("Error getting IP Address:\n" + ex.Message);
                result = null;
            }

            return result;
        }
    }
}
