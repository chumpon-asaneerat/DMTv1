using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Collector.Coupon
{
    /// <summary>
    /// Interaction logic for CollectorBorrowCouponPage.xaml
    /// </summary>
    public partial class CollectorBorrowCouponPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorBorrowCouponPage()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new MainMenu();
            PageContentManager.Instance.Current = page;
        }
        /*
        public void Setup(List<Models.Coupon> coupons)
        {
            var couponTypes = new List<string>();
            couponTypes.Add("คูปอง 35 บาท");
            couponTypes.Add("คูปอง 80 บาท");
            cbCouponTypes.DataContext = couponTypes;
            cbCouponTypes.SelectedIndex = 0;
            grid.Setup(coupons);
        }
        */
    }
}
