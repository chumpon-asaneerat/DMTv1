using System;
using System.Collections.Generic;
using System.Windows;

using SQLite;
using SQLitePCL;
//using SQLite.Net.DateTimeOffset.Attributes;
using System.ComponentModel;

namespace SQLite.Net.Wpf.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private SQLiteConnection db;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            dt.CultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            Console.WriteLine(System.DateTimeOffset.Now.Offset);
            */

            dt.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            dt.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
            dt.TimeFormat = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            dt.TimeFormatString = "HH:mm:ss.fff";

            dt.DefaultValue = DateTime.Now;
            dt.Value = DateTime.Now;

            InitDatabase();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (null != db)
            {
                db.Close();
                db.Dispose();
            }
            db = null;
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void cmdUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            LoadData();
        }

        private void InitDatabase()
        {
            string databasePath = "./stock.db";
            db = new SQLiteConnection(databasePath, true);
            db.CreateTable<Stock>();
            LoadData();
        }

        private void LoadData()
        {
            if (null == db) return;
            var datasource = db.Table<Stock>().ToArray();
            grid.ItemsSource = datasource;
        }

        private void ClearData()
        {
            if (null == db) return;
            db.DeleteAll<Stock>();
        }

        private void Add()
        {
            if (null == db) return;

            if (string.IsNullOrWhiteSpace(txtName.Text.Trim())) return;
            if (!dt.Value.HasValue) return;
            var name = txtName.Text.Trim();
            var dtValue = dt.Value.Value;
            //var dtOffset = new System.DateTimeOffset(dtValue);

            var stock = new Stock()
            {
                Name = name,
                Time = dtValue
                //LocalTime = dtOffset
            };
            db.Insert(stock);

            // clear value.
            txtName.Text = string.Empty;
            dt.DefaultValue = DateTime.Now;

            LoadData();
        }

        private void Update()
        {
            if (null == db) return;
            var item = grid.SelectedItem as Stock;
            if (null != item)
            {
                item.Time2 = DateTime.Now;
                db.Update(item);
                // Raise events
                item.Raise("Time");
                item.Raise("STime");
                //item.Raise("LocalTime");
                item.Raise("Time2");
                item.Raise("STime2");
            }
        }

        public class Stock : INotifyPropertyChanged
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Name { get; set; }
            // Update Time.
            public DateTime Time { get; set; }

            public void Raise(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            // Show time in string.
            [Ignore]
            public string STime
            {
                get
                {
                    return (this.Time != DateTime.MinValue) ?
                        this.Time.ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty;
                }
                set { }
            }

            public DateTime Time2 { get; set; }
            [Ignore]
            public string STime2
            {
                get
                {
                    return (this.Time2 != DateTime.MinValue) ?
                        this.Time2.ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty;
                }
                set { }
            }
            /*
            //[DateTimeOffsetSerialize]
            [DateTimeOffsetSerialize("yyyy-MM-dd HH:mm:ss.fff zzzz")]
            //[DateTimeOffsetSerialize("yyyy-MM-dd HH:mm:ss.fff zzzz", true)]
            public System.DateTimeOffset LocalTime { get; set; }
            
            [Ignore]
            public string SLocalTime
            {
                get
                {
                    return (this.LocalTime != System.DateTimeOffset.MinValue) ?
                        this.LocalTime.ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty;
                }
                set { }
            }
            */

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}
