using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemWatcherSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private FileSystemWatcher watcher;

        private void Form1_Load(object sender, EventArgs e)
        {
            lstMsgs.Items.Clear();

            string root = Path.GetDirectoryName(this.GetType().Assembly.Location);
            string path = Path.Combine(root, "data");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            watcher = new FileSystemWatcher(path, "*.json");

            watcher.Created += Watcher_Created;
            watcher.Renamed += Watcher_Renamed;
            watcher.Changed += Watcher_Changed;
            watcher.Deleted += Watcher_Deleted;
            watcher.Error += Watcher_Error;
            watcher.EnableRaisingEvents = true; 
        }

        private void UpdateMessage(string message)
        {
            Invoke(new Action(() =>
            {
                lstMsgs.Items.Add(message);
            }));            
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            UpdateMessage(string.Format("Created: {0}", e.Name));
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            UpdateMessage(string.Format("Renamed: {0} to {1}", e.OldName, e.Name));
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            UpdateMessage(string.Format("Changed: {0}", e.Name));
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            UpdateMessage(string.Format("Deleted: {0}", e.Name));
        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            UpdateMessage(string.Format("Error: {0}", e.ToString()));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != watcher)
            {
                watcher.Error -= Watcher_Error;
                watcher.Deleted -= Watcher_Deleted;
                watcher.Changed -= Watcher_Changed;
                watcher.Renamed -= Watcher_Renamed;
                watcher.Created -= Watcher_Created;
                watcher.Dispose();
            }
            watcher = null;
        }
    }
}
