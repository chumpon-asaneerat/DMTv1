using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WebSocketSharp;

namespace WebSocketClientSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int portNo = 4000;
        private WebSocket ws = null;

        private void Connect(string name)
        {
            if (string.IsNullOrEmpty(name.Trim())) return;
            if (null == ws)
            {
                ws = new WebSocket(string.Format("ws://localhost:{0}/Chat", portNo));
                ws.OnMessage += Ws_OnMessage;
                ws.OnError += Ws_OnError;
                ws.OnClose += Ws_OnClose;
                ws.Connect();
                Send(name + " say hello");
            }
        }

        private void Disconnect()
        {
            if (null != ws)
            {
                ws.OnClose -= Ws_OnClose;
                ws.OnError -= Ws_OnError;
                ws.OnMessage -= Ws_OnMessage;
                ws.Close();
            }
            ws = null;
        }

        void Reconnect(ushort code, string error)
        {
            if (code != (ushort)CloseStatusCode.Normal)
            {
                ws.Connect();
            }
        }

        private void Send(string message)
        {
            string name = txtName.Text;
            if (null == ws)
            {
                Connect(name);
            }
            if (null != ws && ws.ReadyState != WebSocketState.Open)
            {
                // auto reconnect.
                try
                {
                    Disconnect();
                    Connect(name);
                }
                catch
                {
                    Disconnect(); // attemp connection failed.
                }
            }
            if (null == ws || ws.ReadyState != WebSocketState.Open) return;
             ws.Send(message);
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            // Cross thread wrapper.
            Invoke(new MethodInvoker(delegate () {
                string message = e.Data;
                ListViewItem item = new ListViewItem();
                item.Text = DateTime.Now.ToString("HH:mm:ss");
                item.SubItems.Add(message);
                lvMessages.Items.Add(item);
            }));
        }

        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            Reconnect(e.Code, e.Reason);
        }

        private void Ws_OnError(object sender, ErrorEventArgs e)
        {
            Disconnect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            Connect(name);
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            if (string.IsNullOrEmpty(message.Trim())) return;
            txtMessage.Text = string.Empty; // clear message.
            Send(message);
        }
    }
}
