using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TOD.Pages.Revenue
{
    /// <summary>
    /// Interaction logic for RevenueDateSelectionPage.xaml
    /// </summary>
    public partial class RevenueDateSelectionPage : UserControl
    {
        public RevenueDateSelectionPage()
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
            /*
            // Revenue Entry Page
            var page = new TOD.Revenue.RevenueEntryPage();
            PageContentManager.Instance.Current = page;
            page.Setup(_job, _entry);
            */
        }
        /*
        private Models.Job _job;
        private Models.RevenueEntry _entry;

        public void Setup(Models.Job job, Models.RevenueEntry entry) 
        {
            _job = job;
            _entry = entry;

            entryDate.Text = (null != _entry) ? 
                DateTime.Now.ToThaiDateTimeString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToThaiDateTimeString("yyyy-MM-dd HH:mm:ss");
            revDate.Text = (null != _job) ? 
                _job.BeginDateString : string.Empty;

            grid.Setup(_job.Lanes);
        }
        */
    }
}
