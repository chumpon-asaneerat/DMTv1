using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

using System.IO;

using System.Management;
using DMT.Smartcard;

namespace DMTSmartCardSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SL600SDKFactory factory = null;
        private SL600SDK sdk = null;

        private Sl600SmartCardReader reader = null;
        private DispatcherTimer timer = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            factory = SL600SDKFactory.CreateFactory(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MasterRD.dll"));
            //var resolver = CreateResolver();
            sdk = factory.CreateInstance();
            reader = new Sl600SmartCardReader(sdk, 0) { IsEmv = false };

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Start();

            //backgroundWorker1.RunWorkerAsync();
            Scan();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != timer)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
            timer = null;

            if (null != reader) reader.Dispose();
            reader = null;

            if (null != sdk) sdk.Dispose();
            sdk = null;
        }

        private bool onScanning = false;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (onScanning) return;

            onScanning = true;

            if (null != reader)
            {
                if (reader.IsCardExist())
                {
                    lbCardExist.Text = "Card dected.";
                    lbCardExist.ForeColor = Color.ForestGreen;
                }
                else
                {
                    lbCardExist.Text = "No card.";
                    lbCardExist.ForeColor = Color.Red;
                }

                Application.DoEvents();
            }

            onScanning = false;
        }

        /*
        private void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            foreach (var property in instance.Properties)
            {
                Console.WriteLine(property.Name + " = " + property.Value);
            }
        }

        private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            foreach (var property in instance.Properties)
            {
                Console.WriteLine(property.Name + " = " + property.Value);
            }
        }
        */
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
            WqlEventQuery insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");

            ManagementEventWatcher insertWatcher = new ManagementEventWatcher(insertQuery);
            insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
            insertWatcher.Start();

            WqlEventQuery removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            ManagementEventWatcher removeWatcher = new ManagementEventWatcher(removeQuery);
            removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
            removeWatcher.Start();
            */
        }

        private void Scan()
        {
            /*
            string ComputerName = "localhost";
            ManagementScope Scope;
            Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);

            Scope.Connect();

            string query = "SELECT * FROM Win32_PnPEntity Where ClassGuid = NULL";

            ObjectQuery Query = new ObjectQuery(query);

            ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

            lstDevices.Items.Clear();
            foreach (ManagementObject WmiObject in Searcher.Get())
            {
                //textBox1.Text += WmiObject["Name"] + "\r\n";
                if (null != WmiObject["Name"])
                    lstDevices.Items.Add(WmiObject["Name"]);
            }
            */
        }
    }
}

#region Sample Code from https://www.dreamincode.net/forums/topic/155380-rfid-reader-attendance-system/

/*
namespace RFID_C
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Timers;
    using System.Runtime.InteropServices;
    using Microsoft.VisualBasic;
    using MySql.Data.MySqlClient;


    public partial class frmRead : Form
    {
        public frmRead()
        {
            InitializeComponent();
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++=

        [DllImport("kernel32.dll")]
        static extern void Sleep(int dwMilliseconds);
        [DllImport("MasterRD.dll")]
        static extern int lib_ver(ref uint pVer);
        [DllImport("MasterRD.dll")]
        static extern int rf_init_com(int port, int baud);
        [DllImport("MasterRD.dll")]
        static extern int rf_ClosePort();
        [DllImport("MasterRD.dll")]
        static extern int rf_antenna_sta(short icdev, byte mode);
        [DllImport("MasterRD.dll")]
        static extern int rf_init_type(short icdev, byte type);
        [DllImport("MasterRD.dll")]
        static extern int rf_request(short icdev, byte mode, ref ushort pTagType);
        [DllImport("MasterRD.dll")]
        static extern int rf_anticoll(short icdev, byte bcnt, IntPtr pSnr, ref byte pRLength);
        [DllImport("MasterRD.dll")]
        static extern int rf_select(short icdev, IntPtr pSnr, byte srcLen, ref sbyte Size);
        [DllImport("MasterRD.dll")]
        static extern int rf_halt(short icdev);
        [DllImport("MasterRD.dll")]
        static extern int rf_M1_authentication2(short icdev, byte mode, byte secnr, IntPtr key);
        [DllImport("MasterRD.dll")]
        static extern int rf_M1_initval(short icdev, byte adr, Int32 value);
        [DllImport("MasterRD.dll")]
        static extern int rf_M1_increment(short icdev, byte adr, Int32 value);
        [DllImport("MasterRD.dll")]
        static extern int rf_M1_decrement(short icdev, byte adr, Int32 value);
        [DllImport("MasterRD.dll")]
        static extern int rf_M1_readval(short icdev, byte adr, ref Int32 pValue);
        [DllImport("MasterRD.dll")]
        static extern int rf_M1_read(short icdev, byte adr, IntPtr pData, ref byte pLen);
        [DllImport("MasterRD.dll")]
        static extern int rf_M1_write(short icdev, byte adr, IntPtr pData);

        bool bConnectedDevice;

        static char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static int GetHexBitsValue(byte ch)
        {
            int sz = 0;
            if (ch <= '9' && ch >= '0')
                sz = (int)(ch - 0x30);
            if (ch <= 'F' && ch >= 'A')
                sz = (int)(ch - 0x37);
            return sz;
        }

        //public static by
        public static string ToHexString(byte[] ba)
        {
            StringBuilder sb = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        public string Data_Asc_Hex(ref string Data)
        {
            string Data1 = "";
            string sData = "";

            while (Data.Length > 0)
            {
                //first take two hex value using substring.
                //then  convert Hex value into ascii.
                //then convert ascii value into character.

                Data1 = System.Convert.ToChar(System.Convert.ToUInt32(Data.Substring(0, 2), 16)).ToString();
                sData = sData + Data1;
                Data = Data.Substring(2, Data.Length - 2);
                //String.Format("{0:x2}", System.Convert.ToUInt32((letter)));
            }
            return sData;
        }

        public static byte[] ToDigitsBytes(string theHex)
        {
            /// Calculate How many  Bytes in Given String

            // For Example Srini have 3 bytes
            byte[] bytes = new byte[theHex.Length / 2 + (((theHex.Length % 2) > 0) ? 1 : 0)];
            byte[] bytes2 = new byte[theHex.Length];
            if (theHex[0] < 71)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    char lowbits = theHex[i * 2];
                    char highbits;
                    if ((i * 2 + 1) < theHex.Length)

                        highbits = theHex[i * 2 + 1];

                    else
                        highbits = '0';

                    //if (ch <= 'F' && ch >= 'A')  

                    if ((lowbits <= 70 && lowbits >= 48) && (highbits <= 70 && highbits >= 48))
                    {
                        int a = (int)GetHexBitsValue((byte)lowbits);
                        int b = (int)GetHexBitsValue((byte)highbits);

                        bytes[i] = (byte)((a << 4) + b);
                    }
                    // return (byte)(a);
                }
                return bytes;
            }
            else
            {
                for (int i = 0; i < theHex.Length; i++)
                {
                    char lowbits2 = theHex[i * 1];
                    if ((i * 1 + 1) < theHex.Length)

                        lowbits2 = theHex[i * 1];
                    else
                        lowbits2 = '0';

                    int a = (int)GetHexBitsValue((byte)lowbits2);
                    bytes2[i] = (byte)(a);
                }
                return bytes2;
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        string x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11;

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectPort8()
        {
            int port = 2;
            int baud = 0;
            int status;
            port = 2;// Convert.ToInt32(tscbxPort.Text);
            baud = 19200;// Convert.ToInt32(tscbxBaud.Text);
            status = rf_init_com(port, baud);
            if (0 == status)
            {
                bConnectedDevice = true;
                // MessageBox.Show("Reader Port COM OK");
            }
            else
            {
                bConnectedDevice = false;
            }
        }

        private void frmRead_Load(object sender, EventArgs e)
        {
            ConnectPort8();
            re1();

            string sr = DateTime.Now.ToString();
            //string s = textBox1.Text;
            textBox1.Text = sr;
            // MessageBox.Show(s);

            string host = "localhost";
            string database = "absec";
            string user = "root";
            string password = "root";
            //string ass = textBox3.Text;
            string strSQL1 = "INSERT INTO attendance(SERIAL,IN_TIME) VALUES ('" + textBox2.Text + "' , '" + textBox1.Text + "')";
            string strProvider1 = "Data Source=" + host + ";Database=" + database + ";User ID=" + user + ";Password=" + password;
            MySqlConnection mysqlCon1 = new MySqlConnection(strProvider1);
            mysqlCon1.Open();
            MySqlCommand mysqlCmd1 = new MySqlCommand(strSQL1, mysqlCon1);
            MySqlDataReader mysqlReader1 = mysqlCmd1.ExecuteReader();
            mysqlCon1.Close();
        }

        private void re1()
        {
            short icdev = 0x0000;
            int status;
            byte type = (byte)'A';//mifare one 
            byte mode = 0x52;
            ushort TagType = 0;
            byte bcnt = 0x04;//mifare 
            IntPtr pSnr;
            byte len = 255;
            sbyte size = 0;
            if (!bConnectedDevice)
            {
                // MessageBox.Show("Not connect to device!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label1.Text = "Not connect to device!!";
                return;
            }
            pSnr = Marshal.AllocHGlobal(1024);
            for (int i = 0; i < 2; i++)
            {
                status = rf_antenna_sta(icdev, 0);
                if (status != 0)
                    continue;
                Sleep(20);
                status = rf_init_type(icdev, type);
                if (status != 0)
                    continue;
                Sleep(20);
                status = rf_antenna_sta(icdev, 1);
                if (status != 0)
                    continue;
                Sleep(50);
                status = rf_request(icdev, mode, ref TagType);

                if (status != 0)
                    continue;
                status = rf_anticoll(icdev, bcnt, pSnr, ref len);
                if (status != 0)
                    continue;
                status = rf_select(icdev, pSnr, len, ref size);
                if (status != 0)
                    continue;
                byte[] szBytes = new byte[len + 1];
                string str = Marshal.PtrToStringAnsi(pSnr);
                for (int j = 0; j < len; j++)
                {
                    szBytes[j] = (byte)str[j];
                }
                textBox2.Text = ToHexString(szBytes);
                //x11 = ToHexString(szBytes);
                //  textBox2.Text = Data_Asc_Hex(ref x11);
                break;
            }
            Marshal.FreeHGlobal(pSnr);
        }
    }
}
*/

#endregion

