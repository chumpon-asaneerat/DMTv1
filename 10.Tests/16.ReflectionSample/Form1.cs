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
using System.Net.NetworkInformation;

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

    public class AssignTypeMap
    {
        private Dictionary<Type, AssignMapName> _map = new Dictionary<Type, AssignMapName>();

        private void InitType(Type type)
        {
            PropertyInfo[] props = type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            foreach (var prop in props)
            {
                MapToAttribute map = prop.GetCustomAttribute<MapToAttribute>(true);
                if (null != map)
                {
                    AssignMapName mapInfo;
                    if (!_map.ContainsKey(type))
                    {
                        mapInfo = new AssignMapName();
                        _map.Add(type, mapInfo);
                    }
                    else mapInfo = _map[type];

                    if (!mapInfo.ContainsKey(map.Name))
                    {
                        mapInfo.Add(map.Name, prop);
                    }
                }
            }
        }

        public AssignMapName this[Type value]
        {
            get
            {
                if (!_map.ContainsKey(value)) InitType(value);
                if (!_map.ContainsKey(value)) return null;
                return _map[value];
            }
        }
    }

    public class AssignMapName
    {
        private Dictionary<string, PropertyInfo> _map = new Dictionary<string, PropertyInfo>();
        private List<string> _names = new List<string>();

        public void Add(string name, PropertyInfo value)
        {
            if (!_map.ContainsKey(name)) _map.Add(name, value);
            if (!_names.Contains(name)) _names.Add(name); // keep map name.
            else _map[name] = value;
        }

        public bool ContainsKey(string value)
        {
            return _map.ContainsKey(value);
        }

        public PropertyInfo this[string value]
        {
            get
            {
                if (!_map.ContainsKey(value)) _map.Add(value, null);
                return _map[value];
            }
        }

        public List<string> MapNames { get { return _names; } }
    }

    public static class AssignExtensionMethods
    {
        private static AssignTypeMap _caches = new AssignTypeMap();

        public static void AssignTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            if (null == source || null == target) return;
            Type scrType = typeof(TSource);
            Type dstType = typeof(TTarget);
            AssignMapName scrProp = _caches[scrType];
            AssignMapName dstProp = _caches[dstType];
            foreach (string name in scrProp.MapNames)
            {
                var val = PropertyAccess.GetValue(source, scrProp[name].Name);
                PropertyAccess.SetValue(target, dstProp[name].Name, val);
            }
        }
    }
}
