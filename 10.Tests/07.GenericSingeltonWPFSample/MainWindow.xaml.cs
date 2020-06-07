using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenericSingeltonWPFSample
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyChildSingleton.Instance.OnTick += Instance_OnTick;
            MyChildSingleton.Instance.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //MyChildSingleton.Instance.Shutdown();
            MyChildSingleton.Instance.OnTick -= Instance_OnTick;
        }

        private void Instance_OnTick(object sender, EventArgs e)
        {
            lbStatus.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }
    }
}
