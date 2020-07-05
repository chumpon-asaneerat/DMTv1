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

        private void Form1_Load(object sender, EventArgs e)
        {
            SmartcardService.SecureKey = SL600SDK.DefaultKey;
            SmartcardService.OnIdle += SmartcardService_OnIdle;
            SmartcardService.OnCardRead += SmartcardService_OnCardRead;
            SmartcardService.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SmartcardService.OnCardRead -= SmartcardService_OnCardRead;
            SmartcardService.OnIdle -= SmartcardService_OnIdle;
            SmartcardService.Shutdown();
        }

        private void SmartcardService_OnIdle(object sender, EventArgs e)
        {
            lbCardExist.ForeColor = Color.Red;
            lbCardExist.Text = "Card not avaliable.";
            lbBlock0.Text = "Block 0: ";
            lbBlock1.Text = "Block 1: ";
            lbBlock2.Text = "Block 2: ";
            lbBlock3.Text = "Block 3: ";
        }

        private void SmartcardService_OnCardRead(object sender, M1CardReadEventArgs e)
        {
            lbCardExist.ForeColor = Color.Green;
            lbCardExist.Text = "Card avaliable.";
            lbBlock0.Text = "Block 0: " + e.Block0;
            lbBlock1.Text = "Block 1: " + e.Block1;
            lbBlock2.Text = "Block 2: " + e.Block2;
            lbBlock3.Text = "Block 3: " + e.Block3;
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

