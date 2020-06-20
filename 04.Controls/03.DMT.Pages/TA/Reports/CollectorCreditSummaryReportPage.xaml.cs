using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Plaza.Reports
{
    /// <summary>
    /// Interaction logic for CollectorCreditSummaryReportPage.xaml
    /// </summary>
    public partial class CollectorCreditSummaryReportPage : UserControl
    {
        public CollectorCreditSummaryReportPage()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new MainMenu();
            PageContentManager.Instance.Current = page;
        }
    }
}
