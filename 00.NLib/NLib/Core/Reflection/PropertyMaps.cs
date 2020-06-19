#region History
#if HISTORY_COMMENT

// <[History]> 
Update 2020-06-19
=================
- Add code for PropertyMapName and related classes.

======================================================================================================================
// </[History]>

#endif
#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace NLib.Reflection
{
    #region PeropertyMapNameAttribute

    /// <summary>
    /// The PeropertyMapName Attribute class.
    /// </summary>
    public class PeropertyMapNameAttribute : Attribute
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private PeropertyMapNameAttribute() : base() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The map name.</param>
        public PeropertyMapNameAttribute(string name) : base()
        {
            this.Name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets map name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }

    #endregion

    #region PeropertyMapNameCache (internal)

    /// <summary>
    /// The PeropertyMapName Cache class (internal).
    /// </summary>
    internal class PeropertyMapNameCache
    {
        #region Internal Variables

        private Dictionary<Type, PeropertyMapName> _map = new Dictionary<Type, PeropertyMapName>();

        #endregion

        #region Private Methods

        private void InitType(Type type)
        {
            PropertyInfo[] props = type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            foreach (var prop in props)
            {
                /*
                // .NET 4.0
                object[] attrs = prop.GetCustomAttributes(typeof(PeropertyMapNameAttribute), true);
                if (null != attrs && attrs.Length > 0 && attrs[0] is PeropertyMapNameAttribute)
                {
                    PeropertyMapNameAttribute map = attrs[0] as PeropertyMapNameAttribute;
                    if (null != map)
                    {
                        PeropertyMapName mapInfo;
                        if (!_map.ContainsKey(type))
                        {
                            mapInfo = new PeropertyMapName();
                            _map.Add(type, mapInfo);
                        }
                        else mapInfo = _map[type];

                        if (!mapInfo.ContainsKey(map.Name))
                        {
                            mapInfo.Add(map.Name, prop);
                        }
                    }
                }
                */
                // .NET 4.5
                PeropertyMapNameAttribute map = prop.GetCustomAttribute<PeropertyMapNameAttribute>(true);
                if (null != map)
                {
                    PeropertyMapName mapInfo;
                    if (!_map.ContainsKey(type))
                    {
                        mapInfo = new PeropertyMapName();
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

        #endregion

        #region Public Properties

        /// <summary>
        /// Indexer access.
        /// </summary>
        /// <param name="value">The target type.</param>
        /// <returns>
        /// Returns PeropertyMapName instance that match target type. If not found returns null.
        /// </returns>
        public PeropertyMapName this[Type value]
        {
            get
            {
                if (!_map.ContainsKey(value)) InitType(value);
                if (!_map.ContainsKey(value)) return null;
                return _map[value];
            }
        }

        #endregion
    }

    #endregion

    #region PeropertyMapName (internal)

    /// <summary>
    /// The PeropertyMapName class (internal).
    /// </summary>
    internal class PeropertyMapName
    {
        #region Internal Variables

        private Dictionary<string, PropertyInfo> _map = new Dictionary<string, PropertyInfo>();

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PeropertyMapName() : base()
        {
            MapNames = new List<string>();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~PeropertyMapName()
        {
            if (null != MapNames)
            {
                MapNames.Clear();
            }
            MapNames = null;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="name">The map name</param>
        /// <param name="value">The PropertyInfo instance for specificed map name.</param>
        public void Add(string name, PropertyInfo value)
        {
            if (!_map.ContainsKey(name)) _map.Add(name, value);
            if (!MapNames.Contains(name)) MapNames.Add(name); // keep map name.
            else _map[name] = value;
        }
        /// <summary>
        /// Contains Key.
        /// </summary>
        /// <param name="value">The name to check exist.</param>
        /// <returns>Returns true if name is exist.</returns>
        public bool ContainsKey(string value)
        {
            return _map.ContainsKey(value);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Indexer access.
        /// </summary>
        /// <param name="value">The map name.</param>
        /// <returns>
        /// Returns PropertyInfo instance if found otherwise returns null.
        /// </returns>
        public PropertyInfo this[string value]
        {
            get
            {
                if (!_map.ContainsKey(value)) _map.Add(value, null);
                return _map[value];
            }
        }
        /// <summary>
        /// Gets List of Map Names.
        /// </summary>
        public List<string> MapNames { get; private set; }

        #endregion
    }

    #endregion

    #region PeropertyMapNameExtensionMethods

    /// <summary>
    /// The PeropertyMapName Extension Methods.
    /// </summary>
    public static class PeropertyMapNameExtensionMethods
    {
        #region Internal Static Variables

        private static PeropertyMapNameCache _caches = new PeropertyMapNameCache();

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Assign.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="source">The source instance.</param>
        /// <param name="target">The target instance.</param>
        public static void AssignTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            if (null == source || null == target) return;
            Type scrType = typeof(TSource);
            Type dstType = typeof(TTarget);
            PeropertyMapName scrProp = _caches[scrType];
            PeropertyMapName dstProp = _caches[dstType];
            foreach (string name in scrProp.MapNames)
            {
                if (null == scrProp[name] || null == dstProp[name]) continue;
                if (scrProp[name].PropertyType != dstProp[name].PropertyType) continue;
                var val = PropertyAccess.GetValue(source, scrProp[name].Name);
                PropertyAccess.SetValue(target, dstProp[name].Name, val);
            }
        }

        #endregion
    }

    #endregion
}
