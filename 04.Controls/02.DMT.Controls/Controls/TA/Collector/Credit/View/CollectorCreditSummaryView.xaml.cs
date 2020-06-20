using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;


namespace DMT.TA.Controls.Collector.Credit.View
{
    /// <summary>
    /// Interaction logic for CollectorCreditSummaryView.xaml
    /// </summary>
    public partial class CollectorCreditSummaryView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorCreditSummaryView()
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
        private Models.FundEntry _plaza;
        private BindingList<Models.FundEntry> _funds;

        public void Setup(Models.FundEntry plaza,
            BindingList<Models.FundEntry> funds)
        {
            _plaza = plaza;
            _funds = funds;

            listView.ItemsSource = _funds;
        }

        private void ReturnFund(Models.FundEntry src, Models.FundEntry dst)
        {
            src.BHT1 -= dst.BHT1;
            src.BHT2 -= dst.BHT2;
            src.BHT5 -= dst.BHT5;
            src.BHT10c -= dst.BHT10c;
            src.BHT20 -= dst.BHT20;
            src.BHT50 -= dst.BHT50;
            src.BHT100 -= dst.BHT100;
            src.BHT500 -= dst.BHT500;
            src.BHT1000 -= dst.BHT1000;

            if (null != ItemChanged) ItemChanged.Invoke(this, EventArgs.Empty);
        }

        private void BorrowFund(Models.FundEntry src, Models.FundEntry dst)
        {
            src.BHT1 += dst.BHT1;
            src.BHT2 += dst.BHT2;
            src.BHT5 += dst.BHT5;
            src.BHT10c += dst.BHT10c;
            src.BHT20 += dst.BHT20;
            src.BHT50 += dst.BHT50;
            src.BHT100 += dst.BHT100;
            src.BHT500 += dst.BHT500;
            src.BHT1000 += dst.BHT1000;

            if (null != ItemChanged) ItemChanged.Invoke(this, EventArgs.Empty);
        }

        private void assign(Models.FundEntry src, Models.FundEntry dst)
        {
            dst.BHT1 = src.BHT1;
            dst.BHT2 = src.BHT2;
            dst.BHT5 = src.BHT5;
            dst.BHT10c = src.BHT10c;
            dst.BHT20 = src.BHT20;
            dst.BHT50 = src.BHT50;
            dst.BHT100 = src.BHT100;
            dst.BHT500 = src.BHT500;
            dst.BHT1000 = src.BHT1000;
        }
        */
        private void cmdReceived_Click(object sender, RoutedEventArgs e)
        {
            /*
            var fund = (Models.FundEntry)((FrameworkElement)sender).DataContext;
            if (null != fund)
            {
                Models.FundEntry src = new Models.FundEntry();
                Models.FundEntry obj = new Models.FundEntry();
                Models.FundEntry ret = new Models.FundEntry();

                assign(fund, src);

                _plaza.Description = "ยอดที่สามารถยืมได้";
                _plaza.HasRemark = false;
                src.Description = "ยอดยืมปัจจุบัน";
                src.HasRemark = false;

                obj.Description = "ยืมเงิน";

                ret.Description = "ยอดด่านคงเหลือ";
                ret.HasRemark = false;

                var win = new Windows.TA.Collector.FundBorrowWindow();
                win.Owner = Application.Current.MainWindow;
                win.Title = fund.Description;

                win.Setup(_plaza, src, obj, ret);

                if (win.ShowDialog() == false)
                {
                    return;
                }

                BorrowFund(fund, obj);
            }
            */
        }

        private void cmdReturn_Click(object sender, RoutedEventArgs e)
        {
            /*
            var fund = (Models.FundEntry)((FrameworkElement)sender).DataContext;
            if (null != fund)
            {
                Models.FundEntry src = new Models.FundEntry();
                Models.FundEntry obj = new Models.FundEntry();
                Models.FundEntry ret = new Models.FundEntry();

                assign(fund, src);
                //src.Description = "ยอดก่อนคืน";
                src.Description = "ยอดยืมปัจจุบัน";
                src.HasRemark = false;

                obj.Description = "คืนเงิน";

                ret.Description = "ยอดรวม";
                ret.HasRemark = false;

                var win = new Windows.TA.Collector.FundReturnWindow();
                win.Owner = Application.Current.MainWindow;
                win.Title = fund.Description;

                win.Setup(src, obj, ret);

                if (win.ShowDialog() == false)
                {
                    return;
                }

                ReturnFund(fund, obj);
            }
            */
        }
            
        //public event System.EventHandler ItemChanged;
    }
}
