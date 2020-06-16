using System;
using System.Collections.Generic;
using System.Windows;

namespace DMT.TA.Windows.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaStockSummaryWindow.xaml
    /// </summary>
    public partial class PlazaStockSummaryWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaStockSummaryWindow()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        /*
        public void Setup(Models.FundEntry fund, Models.CouponEntry coupon)
        {
            this.Fund = fund;
            this.Coupon = coupon;
            fundEntry.DataContext = this.Fund;
            couponEntry.DataContext = this.Coupon;
        }

        public Models.FundEntry Fund { get; set; }
        public Models.CouponEntry Coupon { get; set; }
        */
    }
}
