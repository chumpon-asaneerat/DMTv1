using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace SL600SmartCardReaderSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdGetDevices_Click(object sender, EventArgs e)
        {
            //Initializing
            NFCReader NFC = new NFCReader();

            //Connecting
            NFC.Connect(); // public bool Connect()

            //Disconnecting
            NFC.Disconnect(); // public void Disconnect()

            //Available Readers 
            List<String> list = NFC.GetReadersList();
            lstDevices.DataSource = list;


            /*
            List<String> list = new List<String>();

            ManagementScope scope = new ManagementScope(@"\\" + Environment.MachineName + @"\root\CIMV2");
            //SelectQuery sq = new SelectQuery("SELECT Name, Caption FROM Win32_PnPEntity");
            SelectQuery sq = new SelectQuery("SELECT Description FROM Win32_USBHub");
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, sq);
            ManagementObjectCollection moc = searcher.Get();

            foreach (ManagementObject mo in moc)
            {
                //object propName = mo.Properties["Name"].Value;
                //object propName = mo.Properties["Description"].Value;
                object propName = mo.Properties["Description"].Value;
                if (propName == null) { continue; }
                list.Add(propName.ToString());
            }

            //lstDevices.DataSource = list;

            lstDevices.Items.Clear();
            var devices = USB.AllUsbDevices;
            foreach(var device in devices)
            {
                lstDevices.Items.Add(device.Name);
            }
        }
        */
        }
    }
}

namespace SL600SmartCardReaderSample
{
    /// <summary>
    /// Plug and Play device information structure
    /// </summary>
    public struct PnPEntityInfo
    {
        public string PNPDeviceID; // device ID
        public string Name; // device name
        public string Description; // device description
        public string Service; // service
        public string Status; // device status
        public UInt16 VendorID; // Vendor ID
        public UInt16 ProductID; // product number 
        public Guid ClassGuid; // device installation class GUID
    }

    /// <summary>
    /// Get USB device information based on WMI
    /// </summary>
    public class USB
    {
        #region UsbDevice
        /// <summary>
        /// Get all USB device entities (filter devices without VID and PID)
        /// </summary>
        public static PnPEntityInfo[] AllUsbDevices
        {
            get
            {
                return WhoUsbDevice(UInt16.MinValue, UInt16.MinValue, Guid.Empty);
            }
        }

        /// <summary>
        /// Query USB device entity (device requires VID and PID)
        /// </summary>
        /// <param name="VendorID">Supplier ID, MinValue ignores</param>
        /// <param name="ProductID">Product number, MinValue ignores</param>
        /// <param name="ClassGuid">Device installation class Guid, Empty ignore </param>
        /// <returns>Device List</returns>
        public static PnPEntityInfo[] WhoUsbDevice(UInt16 VendorID, UInt16 ProductID, Guid ClassGuid)
        {
            List<PnPEntityInfo> UsbDevices = new List<PnPEntityInfo>();

            // Get the USB controller and its associated device entity
            ManagementObjectCollection USBControllerDeviceCollection = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
            if (USBControllerDeviceCollection != null)
            {
                foreach (ManagementObject USBControllerDevice in USBControllerDeviceCollection)
                { // Get the DeviceID of the device entity
                    String Dependent = (USBControllerDevice["Dependent"] as String).Split(new Char[] { '=' })[1];

                    // Filter out USB devices without VID and PID
                    Match match = Regex.Match(Dependent, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        UInt16 theVendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16); // Vendor ID
                        if (VendorID != UInt16.MinValue && VendorID != theVendorID) continue;

                        UInt16 theProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // Product Number
                        if (ProductID != UInt16.MinValue && ProductID != theProductID) continue;

                        ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID=" + Dependent).Get();
                        if (PnPEntityCollection != null)
                        {
                            foreach (ManagementObject Entity in PnPEntityCollection)
                            {
                                Guid theClassGuid = new Guid(Entity["ClassGuid"] as String); // Device installation class GUID
                                if (ClassGuid != Guid.Empty && ClassGuid != theClassGuid) continue;

                                PnPEntityInfo Element;
                                Element.PNPDeviceID = Entity["PNPDeviceID"] as String; // Device ID
                                Element.Name = Entity["Name"] as String; // device name
                                Element.Description = Entity["Description"] as String; // Device Description
                                Element.Service = Entity["Service"] as String; // Service
                                Element.Status = Entity["Status"] as String; // Device Status
                                Element.VendorID = theVendorID; // Vendor ID
                                Element.ProductID = theProductID; // Product Number
                                Element.ClassGuid = theClassGuid; // Device installation class GUID

                                UsbDevices.Add(Element);
                            }
                        }
                    }
                }
            }

            if (UsbDevices.Count == 0) return null; else return UsbDevices.ToArray();
        }

        /// <summary>
        /// Query USB device entity (device requires VID and PID)
        /// </summary>
        /// <param name="VendorID">Supplier ID, MinValue ignores</param>
        /// <param name="ProductID">Product number, MinValue ignores</param>
        /// <returns>Device List</returns>
        public static PnPEntityInfo[] WhoUsbDevice(UInt16 VendorID, UInt16 ProductID)
        {
            return WhoUsbDevice(VendorID, ProductID, Guid.Empty);
        }

        /// <summary>
        /// Query USB device entity (device requires VID and PID)
        /// </summary>
        /// <param name="ClassGuid">Device installation class Guid, Empty ignore </param>
        /// <returns>Device List</returns>
        public static PnPEntityInfo[] WhoUsbDevice(Guid ClassGuid)
        {
            return WhoUsbDevice(UInt16.MinValue, UInt16.MinValue, ClassGuid);
        }

        /// <summary>
        /// Query USB device entity (device requires VID and PID)
        /// </summary>
        /// <param name="PNPDeviceID">Device ID, which can be incomplete information</param>
        /// <returns>Device List</returns>        
        public static PnPEntityInfo[] WhoUsbDevice(String PNPDeviceID)
        {
            List<PnPEntityInfo> UsbDevices = new List<PnPEntityInfo>();

            // Get the USB controller and its associated device entity
            ManagementObjectCollection USBControllerDeviceCollection = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
            if (USBControllerDeviceCollection != null)
            {
                foreach (ManagementObject USBControllerDevice in USBControllerDeviceCollection)
                { // Get the DeviceID of the device entity
                    String Dependent = (USBControllerDevice["Dependent"] as String).Split(new Char[] { '=' })[1];
                    if (!String.IsNullOrEmpty(PNPDeviceID))
                    { // Note: Ignore case
                        if (Dependent.IndexOf(PNPDeviceID, 1, PNPDeviceID.Length - 2, StringComparison.OrdinalIgnoreCase) == -1) continue;
                    }

                    // Filter out USB devices without VID and PID
                    Match match = Regex.Match(Dependent, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID=" + Dependent).Get();
                        if (PnPEntityCollection != null)
                        {
                            foreach (ManagementObject Entity in PnPEntityCollection)
                            {
                                PnPEntityInfo Element;
                                Element.PNPDeviceID = Entity["PNPDeviceID"] as String; // Device ID
                                Element.Name = Entity["Name"] as String; // device name
                                Element.Description = Entity["Description"] as String; // Device Description
                                Element.Service = Entity["Service"] as String; // Service
                                Element.Status = Entity["Status"] as String; // Device Status
                                Element.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16); // Vendor ID   
                                Element.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // Product Number // Product Number
                                Element.ClassGuid = new Guid(Entity["ClassGuid"] as String); // Device installation class GUID

                                UsbDevices.Add(Element);
                            }
                        }
                    }
                }
            }

            if (UsbDevices.Count == 0) return null; else return UsbDevices.ToArray();
        }

        /// <summary>
        /// Target USB device based on service
        /// </summary>
        /// <param name="ServiceCollection">The collection of services to query</param>
        /// <returns>Device List</returns>
        public static PnPEntityInfo[] WhoUsbDevice(String[] ServiceCollection)
        {
            if (ServiceCollection == null || ServiceCollection.Length == 0)
                return WhoUsbDevice(UInt16.MinValue, UInt16.MinValue, Guid.Empty);

            List<PnPEntityInfo> UsbDevices = new List<PnPEntityInfo>();

            // Get the USB controller and its associated device entity
            ManagementObjectCollection USBControllerDeviceCollection = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
            if (USBControllerDeviceCollection != null)
            {
                foreach (ManagementObject USBControllerDevice in USBControllerDeviceCollection)
                { // Get the DeviceID of the device entity
                    String Dependent = (USBControllerDevice["Dependent"] as String).Split(new Char[] { '=' })[1];

                    // Filter out USB devices without VID and PID
                    Match match = Regex.Match(Dependent, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID=" + Dependent).Get();
                        if (PnPEntityCollection != null)
                        {
                            foreach (ManagementObject Entity in PnPEntityCollection)
                            {
                                String theService = Entity["Service"] as String; // Service
                                if (String.IsNullOrEmpty(theService)) continue;

                                foreach (String Service in ServiceCollection)
                                { // Note: Ignore case
                                    if (String.Compare(theService, Service, true) != 0) continue;

                                    PnPEntityInfo Element;
                                    Element.PNPDeviceID = Entity["PNPDeviceID"] as String; // Device ID
                                    Element.Name = Entity["Name"] as String; // device name
                                    Element.Description = Entity["Description"] as String; // Device Description
                                    Element.Service = theService; // Service
                                    Element.Status = Entity["Status"] as String; // Device Status
                                    Element.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16); // Vendor ID   
                                    Element.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // Product Number
                                    Element.ClassGuid = new Guid(Entity["ClassGuid"] as String); // Device installation class GUID

                                    UsbDevices.Add(Element);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (UsbDevices.Count == 0) return null; else return UsbDevices.ToArray();
        }
        #endregion

        #region PnPEntity
        /// <summary>
        /// All Plug and Play device entities (filtering devices without VID and PID)
        /// </summary>
        public static PnPEntityInfo[] AllPnPEntities
        {
            get
            {
                return WhoPnPEntity(UInt16.MinValue, UInt16.MinValue, Guid.Empty);
            }
        }

        /// <summary>
        /// Locate Plug and Play device entities based on VID and PID and device installation GUID
        /// </summary>
        /// <param name="VendorID">Supplier ID, MinValue ignores</param>
        /// <param name="ProductID">Product number, MinValue ignores</param>
        /// <param name="ClassGuid">Device installation class Guid, Empty ignore </param>
        /// <returns>Device List</returns>
        /// <remarks>
        /// HID：{745a17a0-74d3-11d0-b6fe-00a0c90f57da}
        /// Imaging Device：{6bdd1fc6-810f-11d0-bec7-08002be2092f}
        /// Keyboard：{4d36e96b-e325-11ce-bfc1-08002be10318} 
        /// Mouse：{4d36e96f-e325-11ce-bfc1-08002be10318}
        /// Network Adapter：{4d36e972-e325-11ce-bfc1-08002be10318}
        /// USB：{36fc9e60-c465-11cf-8056-444553540000}
        /// </remarks>
        public static PnPEntityInfo[] WhoPnPEntity(UInt16 VendorID, UInt16 ProductID, Guid ClassGuid)
        {
            List<PnPEntityInfo> PnPEntities = new List<PnPEntityInfo>();

            // enumerate plug and play device entities
            String VIDPID;
            if (VendorID == UInt16.MinValue)
            {
                if (ProductID == UInt16.MinValue)
                    VIDPID = "'%VID[_]____&PID[_]____%'";
                else
                    VIDPID = "'%VID[_]____&PID[_]" + ProductID.ToString("X4") + "%'";
            }
            else
            {
                if (ProductID == UInt16.MinValue)
                    VIDPID = "'%VID[_]" + VendorID.ToString("X4") + "&PID[_]____%'";
                else
                    VIDPID = "'%VID[_]" + VendorID.ToString("X4") + "&PID[_]" + ProductID.ToString("X4") + "%'";
            }

            String QueryString;
            if (ClassGuid == Guid.Empty)
                QueryString = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE" + VIDPID;
            else
                QueryString = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE" + VIDPID + " AND ClassGuid='" + ClassGuid.ToString("B") + "'";

            PropertyDataCollection propertyDataCollection;
            bool toShow = false;

            ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher(QueryString).Get();
            if (PnPEntityCollection != null)
            {
                foreach (ManagementObject Entity in PnPEntityCollection)
                {
                    propertyDataCollection = Entity.Properties;
                    String PNPDeviceID = Entity["PNPDeviceID"] as String;
                    Match match = Regex.Match(PNPDeviceID, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {

                        PnPEntityInfo Element;

                        Element.PNPDeviceID = PNPDeviceID; // Device ID
                        var cs = Entity.Properties;
                        foreach (var c in cs)
                        {
                            //Console.WriteLine(c.Value);
                            //Console.WriteLine(c.Name);

                            if (c.Name != null && c.Value != null)
                            {
                                if (c.Name.Contains("DN2SKRKU") || c.Value.ToString().Contains("DN2SKRKU"))
                                {
                                    propertyDataCollection = cs;
                                    toShow = true;
                                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                                }
                            }
                            //Console.WriteLine("-----------------------------------------");
                        }

                        if (toShow)
                        {
                            Console.WriteLine("----------toshow  {0}", toShow);
                            foreach (var c in propertyDataCollection)
                            {
                                Console.WriteLine(c.Value);
                                Console.WriteLine(c.Name);
                                Console.WriteLine("++");
                            }

                            toShow = false;
                        }



                        Element.Name = Entity["Name"] as String; // device name
                        Element.Description = Entity["Description"] as String; // Device Description
                        Element.Service = Entity["Service"] as String; // Service
                        Element.Status = Entity["Status"] as String; // Device Status
                        Element.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16); // Vendor ID
                        Element.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // Product Number
                        Element.ClassGuid = new Guid(Entity["ClassGuid"] as String); // Device installation class GUID

                        PnPEntities.Add(Element);
                    }
                }
            }

            if (PnPEntities.Count == 0) return null; else return PnPEntities.ToArray();
        }

        /// <summary>
        /// Locate Plug and Play device entities based on VID and PID
        /// </summary>
        /// <param name="VendorID">Supplier ID, MinValue ignores</param>
        /// <param name="ProductID">Product number, MinValue ignores</param>
        /// <returns>Device List</returns>
        public static PnPEntityInfo[] WhoPnPEntity(UInt16 VendorID, UInt16 ProductID)
        {
            return WhoPnPEntity(VendorID, ProductID, Guid.Empty);
        }

        /// <summary>
        /// Locate the Plug and Play device entity based on the device installation GUID
        /// </summary>
        /// <param name="ClassGuid">Device installation class Guid, Empty ignore </param>
        /// <returns>Device List</returns>
        public static PnPEntityInfo[] WhoPnPEntity(Guid ClassGuid)
        {
            return WhoPnPEntity(UInt16.MinValue, UInt16.MinValue, ClassGuid);
        }

        /// <summary>
        /// Locate the device based on the device ID
        /// </summary>
        /// <param name="PNPDeviceID">Device ID, which can be incomplete information</param>
        /// <returns>Device List</returns>
        /// <remarks>
        /// Note: For underscore, you need to write "[_]", otherwise it is treated as any character
        /// </remarks>
        public static PnPEntityInfo[] WhoPnPEntity(String PNPDeviceID)
        {
            List<PnPEntityInfo> PnPEntities = new List<PnPEntityInfo>();

            // enumerate plug and play device entities
            String QueryString;
            if (String.IsNullOrEmpty(PNPDeviceID))
            {
                QueryString = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE '%VID[_]____&PID[_]____%'";
            }
            else
            { // There is a backslash character in the LIKE clause that will cause a WQL query exception.
                QueryString = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE '%" + PNPDeviceID.Replace('\\', '_') + "%'";
            }

            ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher(QueryString).Get();
            if (PnPEntityCollection != null)
            {
                foreach (ManagementObject Entity in PnPEntityCollection)
                {
                    String thePNPDeviceID = Entity["PNPDeviceID"] as String;
                    Match match = Regex.Match(thePNPDeviceID, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        PnPEntityInfo Element;

                        Element.PNPDeviceID = thePNPDeviceID; // device ID
                        Element.Name = Entity["Name"] as String; // device name
                        Element.Description = Entity["Description"] as String; // Device Description
                        Element.Service = Entity["Service"] as String; // Service
                        Element.Status = Entity["Status"] as String; // Device Status
                        Element.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16); // Vendor ID
                        Element.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // Product Number
                        Element.ClassGuid = new Guid(Entity["ClassGuid"] as String); // Device installation class GUID

                        PnPEntities.Add(Element);
                    }
                }
            }

            if (PnPEntities.Count == 0) return null; else return PnPEntities.ToArray();
        }

        /// <summary>
        /// Target device based on service
        /// </summary>
        /// <param name="ServiceCollection">The collection of services to be queried, null ignored</param>
        /// <returns>Device List</returns>
        /// <remarks>
        /// Service-related classes:
        ///     Win32_SystemDriverPNPEntity
        ///     Win32_SystemDriver
        /// </remarks>
        public static PnPEntityInfo[] WhoPnPEntity(String[] ServiceCollection)
        {
            if (ServiceCollection == null || ServiceCollection.Length == 0)
                return WhoPnPEntity(UInt16.MinValue, UInt16.MinValue, Guid.Empty);

            List<PnPEntityInfo> PnPEntities = new List<PnPEntityInfo>();

            // enumerate plug and play device entities
            String QueryString = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE '%VID[_]____&PID[_]____%'";
            ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher(QueryString).Get();
            if (PnPEntityCollection != null)
            {
                foreach (ManagementObject Entity in PnPEntityCollection)
                {
                    String PNPDeviceID = Entity["PNPDeviceID"] as String;
                    Match match = Regex.Match(PNPDeviceID, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        String theService = Entity["Service"] as String; // Service
                        if (String.IsNullOrEmpty(theService)) continue;

                        foreach (String Service in ServiceCollection)
                        { // Note: Ignore case
                            if (String.Compare(theService, Service, true) != 0) continue;

                            PnPEntityInfo Element;

                            Element.PNPDeviceID = PNPDeviceID; // Device ID
                            Element.Name = Entity["Name"] as String; // device name
                            Element.Description = Entity["Description"] as String; // Device Description
                            Element.Service = theService; // Service
                            Element.Status = Entity["Status"] as String; // Device Status
                            Element.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16); // Vendor ID
                            Element.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // Product Number
                            Element.ClassGuid = new Guid(Entity["ClassGuid"] as String); // Device installation class GUID

                            PnPEntities.Add(Element);
                            break;
                        }
                    }
                }
            }

            if (PnPEntities.Count == 0) return null; else return PnPEntities.ToArray();
        }
        #endregion
    }

    public class UsbEnumHelper
    {
        static DEVPROPKEY DEVPKEY_Device_BusReportedDeviceDesc;

        #region Dlls
        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern IntPtr SetupDiGetClassDevs(
            ref Guid gClass, UInt32 iEnumerator, UInt32 hParent, DiGetClassFlags nFlags);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern bool SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet, UInt32 MemberIndex, ref SP_DEVINFO_DATA DeviceInterfaceData);
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            SPDRP Property,
            out UInt32 PropertyRegDataType,
            byte[] PropertyBuffer,
            uint PropertyBufferSize,
            out UInt32 RequiredSize);
        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiClassGuidsFromName(string ClassName,
            ref Guid ClassGuidArray1stItem, UInt32 ClassGuidArraySize,
            out UInt32 RequiredSize);
        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiGetDevicePropertyW(
            IntPtr deviceInfoSet,
            [In] ref SP_DEVINFO_DATA DeviceInfoData,
            [In] ref DEVPROPKEY propertyKey,
            [Out] out UInt32 propertyType,
            byte[] propertyBuffer,
            UInt32 propertyBufferSize,
            out UInt32 requiredSize,
            UInt32 flags);
        #endregion

        #region Enums
        public enum SPDRP : uint
        {
            /// <summary>
            /// DeviceDesc (R/W)
            /// </summary>
            SPDRP_DEVICEDESC = 0x00000000,

            /// <summary>
            /// HardwareID (R/W)
            /// </summary>
            SPDRP_HARDWAREID = 0x00000001,

            /// <summary>
            /// CompatibleIDs (R/W)
            /// </summary>
            SPDRP_COMPATIBLEIDS = 0x00000002,

            /// <summary>
            /// unused
            /// </summary>
            SPDRP_UNUSED0 = 0x00000003,

            /// <summary>
            /// Service (R/W)
            /// </summary>
            SPDRP_SERVICE = 0x00000004,

            /// <summary>
            /// unused
            /// </summary>
            SPDRP_UNUSED1 = 0x00000005,

            /// <summary>
            /// unused
            /// </summary>
            SPDRP_UNUSED2 = 0x00000006,

            /// <summary>
            /// Class (R--tied to ClassGUID)
            /// </summary>
            SPDRP_CLASS = 0x00000007,

            /// <summary>
            /// ClassGUID (R/W)
            /// </summary>
            SPDRP_CLASSGUID = 0x00000008,

            /// <summary>
            /// Driver (R/W)
            /// </summary>
            SPDRP_DRIVER = 0x00000009,

            /// <summary>
            /// ConfigFlags (R/W)
            /// </summary>
            SPDRP_CONFIGFLAGS = 0x0000000A,

            /// <summary>
            /// Mfg (R/W)
            /// </summary>
            SPDRP_MFG = 0x0000000B,

            /// <summary>
            /// FriendlyName (R/W)
            /// </summary>
            SPDRP_FRIENDLYNAME = 0x0000000C,

            /// <summary>
            /// LocationInformation (R/W)
            /// </summary>
            SPDRP_LOCATION_INFORMATION = 0x0000000D,

            /// <summary>
            /// PhysicalDeviceObjectName (R)
            /// </summary>
            SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,

            /// <summary>
            /// Capabilities (R)
            /// </summary>
            SPDRP_CAPABILITIES = 0x0000000F,

            /// <summary>
            /// UiNumber (R)
            /// </summary>
            SPDRP_UI_NUMBER = 0x00000010,

            /// <summary>
            /// UpperFilters (R/W)
            /// </summary>
            SPDRP_UPPERFILTERS = 0x00000011,

            /// <summary>
            /// LowerFilters (R/W)
            /// </summary>
            SPDRP_LOWERFILTERS = 0x00000012,

            /// <summary>
            /// BusTypeGUID (R)
            /// </summary>
            SPDRP_BUSTYPEGUID = 0x00000013,

            /// <summary>
            /// LegacyBusType (R)
            /// </summary>
            SPDRP_LEGACYBUSTYPE = 0x00000014,

            /// <summary>
            /// BusNumber (R)
            /// </summary>
            SPDRP_BUSNUMBER = 0x00000015,

            /// <summary>
            /// Enumerator Name (R)
            /// </summary>
            SPDRP_ENUMERATOR_NAME = 0x00000016,

            /// <summary>
            /// Security (R/W, binary form)
            /// </summary>
            SPDRP_SECURITY = 0x00000017,

            /// <summary>
            /// Security (W, SDS form)
            /// </summary>
            SPDRP_SECURITY_SDS = 0x00000018,

            /// <summary>
            /// Device Type (R/W)
            /// </summary>
            SPDRP_DEVTYPE = 0x00000019,

            /// <summary>
            /// Device is exclusive-access (R/W)
            /// </summary>
            SPDRP_EXCLUSIVE = 0x0000001A,

            /// <summary>
            /// Device Characteristics (R/W)
            /// </summary>
            SPDRP_CHARACTERISTICS = 0x0000001B,

            /// <summary>
            /// Device Address (R)
            /// </summary>
            SPDRP_ADDRESS = 0x0000001C,

            /// <summary>
            /// UiNumberDescFormat (R/W)
            /// </summary>
            SPDRP_UI_NUMBER_DESC_FORMAT = 0X0000001D,

            /// <summary>
            /// Device Power Data (R)
            /// </summary>
            SPDRP_DEVICE_POWER_DATA = 0x0000001E,

            /// <summary>
            /// Removal Policy (R)
            /// </summary>
            SPDRP_REMOVAL_POLICY = 0x0000001F,

            /// <summary>
            /// Hardware Removal Policy (R)
            /// </summary>
            SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x00000020,

            /// <summary>
            /// Removal Policy Override (RW)
            /// </summary>
            SPDRP_REMOVAL_POLICY_OVERRIDE = 0x00000021,

            /// <summary>
            /// Device Install State (R)
            /// </summary>
            SPDRP_INSTALL_STATE = 0x00000022,

            /// <summary>
            /// Device Location Paths (R)
            /// </summary>
            SPDRP_LOCATION_PATHS = 0x00000023,
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DEVPROPKEY
        {
            public Guid fmtid;
            public UInt32 pid;
        }
        public struct DeviceInfo
        {
            public string hadware_id;
            public string bus_description;
            public string p_id;
            public string v_id;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVINFO_DATA
        {
            public UInt32 cbSize;
            public Guid ClassGuid;
            public UInt32 DevInst;
            public UIntPtr Reserved;
        };
        [Flags]
        public enum DiGetClassFlags : uint
        {
            DIGCF_DEFAULT = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010,
        }

        const int BUFFER_SIZE = 1024;
        const int utf16terminatorSize_bytes = 2;




        #endregion

        #region Methods

        public static DeviceInfo GetFlickerDeviceInfo()
        {
            DEVPKEY_Device_BusReportedDeviceDesc = new DEVPROPKEY();
            DEVPKEY_Device_BusReportedDeviceDesc.fmtid = new Guid(0x540b947e, 0x8b40, 0x45bc, 0xa8, 0xa2, 0x6a, 0x0b, 0x89, 0x4c, 0xbd, 0xa2);
            DEVPKEY_Device_BusReportedDeviceDesc.pid = 4;

            Guid[] guids = GetClassGUIDs("Usb");
            var hDevInfo = SetupDiGetClassDevs(ref guids[0], 0, 0, DiGetClassFlags.DIGCF_PRESENT);

            try
            {
                UInt32 iMemberIndex = 0;
                while (true)
                {
                    SP_DEVINFO_DATA deviceInfoData = new SP_DEVINFO_DATA();
                    deviceInfoData.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
                    bool success = SetupDiEnumDeviceInfo(hDevInfo, iMemberIndex, ref deviceInfoData);
                    if (!success)
                    {
                        // No more devices in the device information set
                        break;
                    }
                    DeviceInfo deviceInfo = new DeviceInfo();
                    deviceInfo.hadware_id = GetDeviceDescription(hDevInfo, deviceInfoData);
                    deviceInfo.bus_description = GetDeviceBusDescription(hDevInfo, deviceInfoData);
                    if (deviceInfo.bus_description.Contains("Flicker"))
                    {
                        deviceInfo.v_id = deviceInfo.hadware_id.Substring(8, 4);
                        deviceInfo.p_id = deviceInfo.hadware_id.Substring(17, 4);
                        return deviceInfo;
                    }
                    iMemberIndex++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new DeviceInfo();
        }

        private static string GetDeviceDescription(IntPtr hDeviceInfoSet, SP_DEVINFO_DATA deviceInfoData)
        {
            byte[] ptrBuf = new byte[BUFFER_SIZE];
            uint propRegDataType;
            uint RequiredSize;
            bool success = SetupDiGetDeviceRegistryProperty(hDeviceInfoSet, ref deviceInfoData, SPDRP.SPDRP_HARDWAREID,
                out propRegDataType, ptrBuf, BUFFER_SIZE, out RequiredSize);
            if (!success)
            {
                throw new Exception("Can not read registry value PortName for device " + deviceInfoData.ClassGuid);
            }
            return Encoding.Unicode.GetString(ptrBuf, 0, (int)RequiredSize - utf16terminatorSize_bytes);
        }

        private static Guid[] GetClassGUIDs(string className)
        {
            UInt32 requiredSize = 0;
            Guid[] guidArray = new Guid[1];

            bool status = SetupDiClassGuidsFromName(className, ref guidArray[0], 1, out requiredSize);
            if (true == status)
            {
                if (1 < requiredSize)
                {
                    guidArray = new Guid[requiredSize];
                    SetupDiClassGuidsFromName(className, ref guidArray[0], requiredSize, out requiredSize);
                }
            }
            else
                throw new System.ComponentModel.Win32Exception();

            return guidArray;
        }

        private static string GetDeviceBusDescription(IntPtr hDeviceInfoSet, SP_DEVINFO_DATA deviceInfoData)
        {
            byte[] ptrBuf = new byte[BUFFER_SIZE];
            uint propRegDataType;
            uint RequiredSize;
            bool success = SetupDiGetDevicePropertyW(hDeviceInfoSet, ref deviceInfoData, ref DEVPKEY_Device_BusReportedDeviceDesc,
                out propRegDataType, ptrBuf, BUFFER_SIZE, out RequiredSize, 0);
            if (!success)
            {
                //throw new Exception("Can not read Bus provided device description device " + deviceInfoData.ClassGuid);
                return "Err";

            }
            return System.Text.UnicodeEncoding.Unicode.GetString(ptrBuf, 0, (int)RequiredSize - utf16terminatorSize_bytes);
        }

        #endregion
    }
}


