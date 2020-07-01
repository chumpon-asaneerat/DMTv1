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
using DMT.Smartcard;
using System.Net.Http.Headers;


namespace DMTSmartCardSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SL600SDK sdk = null;
        private Sl600SmartCardReader reader = null;
        private DispatcherTimer timer = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            var factory = SL600SDKFactory.CreateFactory(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MasterRD.dll"));
            //var resolver = CreateResolver();
            sdk = factory.CreateInstance();
            reader = new Sl600SmartCardReader(sdk, 0) { IsEmv = false };

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
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


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (null == reader) return;

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
    }
}
