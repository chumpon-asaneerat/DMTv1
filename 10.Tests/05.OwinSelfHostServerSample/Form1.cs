using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Owin.Hosting;

namespace OwinSelfHostServerSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string baseAddress = "http://localhost:9000/";
        private IDisposable server = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = WebApp.Start<Startup>(url: baseAddress);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != server) server.Dispose(); // when dispose the operation will stop.
            server = null;
        }
    }
}
