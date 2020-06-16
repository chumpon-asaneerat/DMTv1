#region Using

using System;
using System.Collections.Generic;
using System.Windows;

#endregion

namespace DMT.TOD.Windows.Job
{
    /// <summary>
    /// Interaction logic for EOJWindow.xaml
    /// </summary>
    public partial class EOJWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EOJWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToThaiDateString();
            txtTime.Text = DateTime.Now.ToThaiTimeString();

            //txtShiftId.Text = (null != this.Job) ? this.Job.ShiftId.ToString() : string.Empty;
        }

        #endregion

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (null != this.Job)
            {
                this.Job.EndJob();
            }
            */
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //public Models.Job Job { get; set; }
    }
}
