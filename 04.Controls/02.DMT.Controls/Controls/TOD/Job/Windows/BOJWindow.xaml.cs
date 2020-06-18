#region Using

using System;
using System.Collections.Generic;
using System.Windows;

#endregion

namespace DMT.TOD.Windows.Job
{
    /// <summary>
    /// Interaction logic for BOJWindow.xaml
    /// </summary>
    public partial class BOJWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BOJWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToThaiDateString();
            txtTime.Text = DateTime.Now.ToThaiTimeString();
        }

        #endregion


        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            //Models.Job.BeginJob("14077", "นายเอนก หอมจรูง", 2);
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
