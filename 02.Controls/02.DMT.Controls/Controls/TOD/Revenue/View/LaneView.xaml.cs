#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TOD.Controls.Revenue.View
{
    /// <summary>
    /// Interaction logic for LaneView.xaml
    /// </summary>
    public partial class LaneView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LaneView()
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
        public void Setup(List<Models.Lane> lanes)
        {
            listView.ItemsSource = lanes;
        }
        */
    }
}
