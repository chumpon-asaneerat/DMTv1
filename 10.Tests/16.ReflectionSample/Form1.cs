using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // B->A
        }
    }


    public class A
    {
        [MapTo("Text")]
        public string Text { get; set; }
        [MapTo("Age")]
        public int Age { get; set; }
        [MapTo("DOB")]
        public DateTime DOB { get; set; }
    }


    public class B
    {
        [MapTo("Text")]
        public string MyText { get; set; }
        [MapTo("Age")]
        public int MyAge { get; set; }
        [MapTo("DOB")]
        public DateTime Dob { get; set; }
    }

    public class MapToAttribute : Attribute
    {
        private MapToAttribute() : base() { }
        public MapToAttribute(string name) : base()
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }

    public static class AssignExtensionMethods
    {
        private static Dictionary<string, Dictionary<Type, string>> _caches = new Dictionary<string, Dictionary<Type, string>>();

        public static void InitType(Type type)
        {
            PropertyInfo[] props = type.GetProperties(
                BindingFlags.Public | BindingFlags.IgnoreCase);
            foreach (var prop in props)
            {
                MapToAttribute map = prop.GetCustomAttribute<MapToAttribute>(true);
                if (null != map)
                {
                    Dictionary<Type, string> typeProps;
                    if (!_caches.ContainsKey(map.Name))
                    {
                        typeProps = new Dictionary<Type, string>();
                        _caches.Add(map.Name, typeProps);
                    }
                    else typeProps = _caches[map.Name];

                    if (!typeProps.ContainsKey(type))
                    {
                        typeProps.Add(type, prop.Name);
                    }
                }
            }
        }


        public static void AssignTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            if (null == source || null == target) return;
            Type scrType = typeof(TSource);
            Type dstType = typeof(TTarget);
            //NLib.Reflection.PropertyAccess;
            InitType(scrType);
            InitType(dstType);
            //_caches
            //MapToAttribute[] maps = scrType.GetCustomAttributes<MapToAttribute>(true).ToArray();
        }
    }
}
