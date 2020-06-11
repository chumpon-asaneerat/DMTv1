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
            [BsonId]
            public ObjectId Id { get; set; }
            public string CardId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            //public string[] Phones { get; set; }
            public bool IsActive { get; set; }
            [BsonIgnore]
            public string Status
            {
                get { return (IsActive) ? "Active" : "Inactive"; }
            }
        }

        public class Lane
        {
            public int LaneNo { get; set; }
            //[BsonRef("activities")]
            public List<Activity> Activities { get; set; }
        }

        public class Activity
        {
            public ObjectId Id { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            [BsonRef("users")]
            public User User { get; set; }
        }

        /*
        BsonMapper.Global.Entity<Activity>()
            .DbRef(x => x.User, "users"); // where "users" are User collection name
        */

        private LiteDatabase db = null;
        //private ILiteCollection<User> users = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create connection
            if (null == db)
            {
                db = new LiteDatabase(@"FileName=.\users.db;Connection=shared");
                // Get a collection(or create, if doesn't exist)
                var users = db.GetCollection<User>("users");
                if (null != users && users.Query().ToList().Count <= 0) initusers(users);
                var lanes = db.GetCollection<Lane>("lanes");
                if (null != lanes && lanes.Query().ToList().Count <= 0) initlanes(lanes);
                var activities = db.GetCollection<Activity>("activities");
                if (null != activities && activities.Query().ToList().Count <= 0) initactivities(activities);
            }
        }

        private void initusers(ILiteCollection<User> values)
        {
            for (int i = 1; i < 10; i++)
            {
                var inst = new User
                {
                    Id = ObjectId.NewObjectId(),
                    CardId = i.ToString("D5"),
                    UserName = "User - " + i.ToString(),
                    Password = "1234",
                    IsActive = (DateTime.Today.Millisecond % 2 == 0) ? true : false
                };
                values.Insert(inst);
            }
            values.EnsureIndex(x => x.UserName);
        }

        private void initlanes(ILiteCollection<Lane> values)
        {
            for (int i = 1; i < 10; i++)
            {
                var inst = new Lane
                {
                    LaneNo = i
                };
                values.Insert(inst);
            }
            values.EnsureIndex(x => x.LaneNo);
        }

        private void initactivities(ILiteCollection<Activity> values)
        {
            Random rand = new Random();
            var users = db.GetCollection<User>("users").Query().ToList();
            int iCnt = users.Count;
            for (int i = 1; i < 10; i++)
            {
                var inst = new Activity
                {
                    Id = ObjectId.NewObjectId(),
                    start = DateTime.Today.AddSeconds(-1 * rand.Next(5000)),
                    end = DateTime.Today.AddSeconds(-1 * rand.Next(5000)),
                    User = users[rand.Next(iCnt)]
                };
                values.Insert(inst);
            }
            values.EnsureIndex(x => x.start);
            values.EnsureIndex(x => x.end);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != db)
            {
                db.Dispose();
            }
            db = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (null == db) return;
            // Get a collection(or create, if doesn't exist)
            var users = db.GetCollection<User>("users");
            // Add
            var user = new User
            {
                CardId = "",
                UserName = txtUserName.Text,
                Password = txtPassword.Text
            };
            users.Insert(user);
            users.EnsureIndex(x => x.UserName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (null == db) return;
            // Get a collection(or create, if doesn't exist)
            var users = db.GetCollection<User>("users");
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
            if (null == db) return;
            // Get a collection(or create, if doesn't exist)
            var users = db.GetCollection<User>("users");
            users.DeleteAll();

            db.Rebuild(); // shrink database size.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // get users
            if (null == db) return;
            var users = db.GetCollection<User>("users");
            // Use LINQ to query documents (filter, sort, transform)
            var results = users.Query()
                //.Where(x => x.UserName.StartsWith("J"))
                //.OrderBy(x => x.UserName)
                //.Select(x => new { x.UserName, NameUpper = x.UserName.ToUpper() })
                //.Limit(10)
                .ToList();
            dataGridView1.DataSource = results;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // get lanes
            if (null == db) return;
            var lanes = db.GetCollection<Lane>("lanes");
            var results = lanes.Query().ToList();
            dataGridView1.DataSource = results;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // get activities
            if (null == db) return;
            var activities = db.GetCollection<Activity>("activities");
            var results = activities.Query().ToList();
            dataGridView1.DataSource = results;
        }
    }
}
