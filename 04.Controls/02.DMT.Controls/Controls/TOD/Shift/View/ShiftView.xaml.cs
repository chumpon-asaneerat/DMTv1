#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TOD.Controls.Shift.View
{
    /// <summary>
    /// Interaction logic for ShiftView.xaml
    /// </summary>
    public partial class ShiftView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShiftView()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        /*
        public void Setup(List<Models.RevenueSlip> slips)
        {
            listView.ItemsSource = slips;
        }
        */
    }
}
