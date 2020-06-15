#region Using

using System;
using System.Security.Principal;
using System.Windows;

using NLib.Services;

//using DMT.Models;
using DMT.Services;

using Fluent;

#endregion

namespace DMT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DMTServiceOperations.Instance.ServiceMonitor.ScanConpleted += ServiceMonitor_ScanConpleted;
            DMTServiceOperations.Instance.ServiceMonitor.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            DMTServiceOperations.Instance.ServiceMonitor.Shutdown();
            DMTServiceOperations.Instance.ServiceMonitor.ScanConpleted -= ServiceMonitor_ScanConpleted;
        }

        #endregion

        class InstallStatus
        {

        }

        private void ServiceMonitor_ScanConpleted(object sender, EventArgs e)
        {

        }

        #region Button Handlers

        #region Windows Services (Install/Unstall/CheckStatus)

        private void cmdInstall_Click(object sender, RoutedEventArgs e)
        {
            DMTServiceOperations.Instance.Install();
        }

        private void cmdUninstall_Click(object sender, RoutedEventArgs e)
        {
            DMTServiceOperations.Instance.Uninstall();
        }

        private void cmdCheckWindowServiceStatus_Click(object sender, RoutedEventArgs e)
        {
            var status = DMTServiceOperations.Instance.CheckInstalled();
            if (status.PlazaLocalServiceInstalled)
            {
                MessageBox.Show("Plaza Sercice installed and running");
            }
            else
            {
                MessageBox.Show("Plaza Sercice is not installed or stopped");
            }
        }

        #endregion

        #endregion
    }
}
