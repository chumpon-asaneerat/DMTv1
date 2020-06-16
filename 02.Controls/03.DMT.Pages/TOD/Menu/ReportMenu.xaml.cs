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

namespace DMT.TOD.Pages
{
    /// <summary>
    /// Interaction logic for ReportMenu.xaml
    /// </summary>
    public partial class ReportMenu : UserControl
    {
        public ReportMenu()
        {
            InitializeComponent();
        }

        private void revSlip_Click(object sender, RoutedEventArgs e)
        {
            
            var search = new Windows.Reports.RevenueSlipSearchWindow();
            search.Owner = Application.Current.MainWindow;
            if (search.ShowDialog() == false)
            {
                return;
            }
            // Revenue Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = this;
            PageContentManager.Instance.Current = page;
            
        }

        private void revSummary_Click(object sender, RoutedEventArgs e)
        {
            
            var search = new Windows.Reports.RevenueSummarySearchWindow();
            search.Owner = Application.Current.MainWindow;
            if (search.ShowDialog() == false)
            {
                return;
            }
            // Daily Revenue Summary Preview
            var page = new Reports.DailyRevenueSummaryPreview();
            PageContentManager.Instance.Current = page;
            
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new MainMenu();
            PageContentManager.Instance.Current = page;
        }
    }
}
