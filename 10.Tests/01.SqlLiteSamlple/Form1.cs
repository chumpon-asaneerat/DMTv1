using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlLiteSamlple
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Database db = new Database();

        private void Form1_Load(object sender, EventArgs e)
        {
            db.FileName = "sample.db";
            // Create
            db.Create();
            // update grid.
            dataGridView1.DataSource = db.Load<Stock>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Add
            db.AddStock(txtSymbol.Text);
            // update grid.
            dataGridView1.DataSource = db.Load<Stock>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.UpdateStock(1, txtSymbol.Text);
            // update grid.
            dataGridView1.DataSource = db.Load<Stock>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Query
            dataGridView1.DataSource = db.Load<Stock>();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.GetPersons();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Query
            dataGridView1.DataSource = db.Load<Person>();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Add data
            db.AddLanes();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime bDT, eDT;
            // Filter on 2020-06-01 (2 records - OK).
            //bDT = new DateTime(2020, 6, 1, 0, 0, 0, 0);
            //eDT = new DateTime(2020, 6, 1, 23, 59, 59, 999);

            // Filter on 2020-06-01 1.00 to 3.00 (1 record - OK).
            bDT = new DateTime(2020, 6, 1, 0, 0, 0, 0);
            eDT = new DateTime(2020, 6, 1, 2, 59, 59, 999);

            // Filter on 2020-06-01 1.00 to 2020-06-02 4.00 (3 records - OK).
            bDT = new DateTime(2020, 6, 1, 0, 0, 0, 0);
            eDT = new DateTime(2020, 6, 2, 3, 59, 59, 999);

            dataGridView1.DataSource = db.Db.GetAllWithChildren<Lane>(p => p.Begin >= bDT && p.Begin <= eDT);
        }
    }
}
