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

using NLib;
using NLib.Services;

namespace DMT.TOD.Pages.Reports
{
    /// <summary>
    /// Interaction logic for DailyRevenueSummaryPreview.xaml
    /// </summary>
    public partial class DailyRevenueSummaryPreview : UserControl
    {
        public DailyRevenueSummaryPreview()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = new ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = new ReportMenu();
            PageContentManager.Instance.Current = page;
        }
    }
}
