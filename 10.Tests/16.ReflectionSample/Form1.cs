using System;
using System.Windows.Forms;
using NLib.Reflection;

namespace ReflectionSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private A objA = new A();
        private B objB = new B();

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = objA;
            propertyGrid2.SelectedObject = objB;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // A->B
            objA.AssignTo(objB);
            propertyGrid2.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // B->A
            objB.AssignTo(objA);
            propertyGrid1.Refresh();
        }
    }


    public class A
    {
        [PeropertyMapName("Text")]
        public string Text { get; set; }
        [PeropertyMapName("Age")]
        public int Age { get; set; }
        [PeropertyMapName("DOB")]
        public DateTime DOB { get; set; }
        [PeropertyMapName("Extra")]
        public string extra { get; set; }
    }

    public class B
    {
        [PeropertyMapName("Text")]
        public string MyText { get; set; }
        [PeropertyMapName("Age")]
        public int MyAge { get; set; }
        [PeropertyMapName("DOB")]
        public DateTime Dob { get; set; }
        // no map name.
        public string MyExtra { get; set; }
    }
}
