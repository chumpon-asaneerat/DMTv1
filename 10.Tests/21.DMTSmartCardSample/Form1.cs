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
