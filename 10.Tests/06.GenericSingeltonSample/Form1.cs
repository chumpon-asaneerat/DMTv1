using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericSingeltonSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyChildSingleton.Instance.Start();
            MyChildSingleton.Instance.OnTick += Instance_OnTick;
        }

        private void Instance_OnTick(object sender, EventArgs e)
        {
            lbStatus.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyChildSingleton.Instance.OnTick -= Instance_OnTick;
            MyChildSingleton.Instance.Shutdown();
        }
    }
}
