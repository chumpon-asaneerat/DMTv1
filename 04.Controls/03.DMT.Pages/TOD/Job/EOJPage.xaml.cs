using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;
using DMT.TOD.Windows.Job;
using DMT.TOD.Pages;

namespace DMT.TOD.Job
{
    /// <summary>
    /// Interaction logic for EOJPage.xaml
    /// </summary>
    public partial class EOJPage : UserControl
    {
        public EOJPage()
        {
            InitializeComponent();
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            EOJWindow win = new EOJWindow();
            win.Owner = Application.Current.MainWindow;
            // setup
            //win.Job = _job;

            if (win.ShowDialog() == false)
            {
                return;
            }

            // Main Menu Page
            var page = new Pages.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new MainMenu();
            PageContentManager.Instance.Current = page;
        }

        /*
        private Models.Job _job;

        public void Setup(Models.Job job)
        {
            _job = job;
            if (null != _job)
            {
                txtJobDate.Text = _job.BeginDateString;
                txtCollectorId.Text = _job.StaffId;
                txtShiftId.Text = _job.ShiftId.ToString();
                txtCollectorName.Text = _job.StaffName;

                grid.Setup(_job.Lanes);
            }
            else
            {
                txtJobDate.Text = string.Empty;
                txtShiftId.Text = string.Empty;
                txtCollectorId.Text = string.Empty;
                txtCollectorName.Text = string.Empty;
            }
        }

        */
    }
}
