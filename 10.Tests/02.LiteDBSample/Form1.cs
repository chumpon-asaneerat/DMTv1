using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteDBSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // Create your POCO class entity
        public class User
        {
            //public int Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            //public string[] Phones { get; set; }
            //public bool IsActive { get; set; }
        }

        private LiteDatabase db = null;
        private ILiteCollection<User> users = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != db)
            {
                db.Dispose();
            }
            db = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create connection
            if (null == db)
            {
                db = new LiteDatabase(@".\users.db");
                // Get a collection(or create, if doesn't exist)
                users = db.GetCollection<User>("users");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (null == users) return;
            // Add
            var user = new User
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Text
            };
            users.Insert(user);
            users.EnsureIndex(x => x.UserName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (null == users) return;
            // Use LINQ to query documents (filter, sort, transform)
            var results = users.Query()
                //.Where(x => x.UserName.StartsWith("J"))
                //.OrderBy(x => x.UserName)
                //.Select(x => new { x.UserName, NameUpper = x.UserName.ToUpper() })
                //.Limit(10)
                .ToList();
            dataGridView1.DataSource = results;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (null == users) return;
            for (int i = 0; i < 100000; i++)
            {
                // Add
                var user = new User
                {
                    UserName = txtUserName.Text + i.ToString("D4"),
                    Password = txtPassword.Text
                };
                users.Insert(user);
            }
            users.EnsureIndex(x => x.UserName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (null == users) return;
            users.DeleteAll();

            db.Rebuild(); // shrink database size.
            // Get a collection(or create, if doesn't exist)
            users = db.GetCollection<User>("users");
        }
    }
}
