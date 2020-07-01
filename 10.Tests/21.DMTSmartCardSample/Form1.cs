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

using System.IO;
using DMT.Smartcard;

namespace DMTSmartCardSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool _running = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            _running = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _running = false;
        }

        private void Run()
        {
            var factory = SL600SDKFactory.CreateFactory(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MasterRD.dll"));
            //var resolver = CreateResolver();
            using (var sdk = factory.CreateInstance())
            {
                using (var reader = new Sl600SmartCardReader(sdk, 0) { IsEmv = false })
                {
                    while (_running)
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

                        Thread.Sleep(500);
                    }
                }
            }
        }
    }
}
