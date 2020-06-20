using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace WebSocketServerSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int portNo = 4000;
        private HttpServer httpsv = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            httpsv = new HttpServer(portNo);
            // Add web socket service
            httpsv.AddWebSocketService<Chat>("/Chat");
            httpsv.Start();
            if (httpsv.IsListening)
            {
                lbStatus.Text = string.Format("Listening on port {0}, and providing WebSocket services:", httpsv.Port);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != httpsv)
            {
                httpsv.Stop();
            }
            httpsv = null;
        }
    }

    public class Chat : WebSocketBehavior
    {
        private string _name;
        private static int _number = 0;
        private string _prefix;

        public Chat() : this(null)
        {
        }
        public Chat(string prefix)
        {
            _prefix = !prefix.IsNullOrEmpty() ? prefix : "anon#";
        }

        private string getName()
        {
            var name = Context.QueryString["name"];
            return !name.IsNullOrEmpty() ? name : _prefix + getNumber();
        }

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnOpen()
        {
            _name = getName();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            try
            {
                Sessions.Broadcast(string.Format("{0} got logged off...", _name));
            }
            catch (Exception) { }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                Sessions.Broadcast(string.Format("{0}: {1}", _name, e.Data));
            }
            catch (Exception) { }
        }
    }
}
