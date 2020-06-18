#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

#endregion

namespace DMT.TA.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        #endregion

        //private Models.FundEntry plaza = new Models.FundEntry();

        private void InitPlazaFund()
        {
            /*
            plaza.Description = "เงินยืม-ทอน (ด่าน)";
            plaza.BHT1 = 100;
            plaza.BHT2 = 100;
            plaza.BHT5 = 100;
            plaza.BHT10c = 100;
            plaza.BHT20 = 100;
            plaza.BHT50 = 100;
            plaza.BHT100 = 100;
            plaza.BHT500 = 100;
            plaza.BHT1000 = 100;
            plaza.HasRemark = false;
            */
        }

        #region Button (Menu) Command Handlers

        private void plazaReceivedReturnFund_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                // Plaza Fund Received
                var page = new DMT.TA.Pages.Plaza.Fund.PlazaFundReceivedPage();
                /*
                BindingList<Models.FundEntry> funds = new BindingList<Models.FundEntry>();
                Models.FundEntry fund;

                fund = new Models.FundEntry();
                fund.Date = new DateTime(2020, 3, 12, 09, 05, 00);
                fund.StaffId = "14055";
                fund.StaffName = "นางวิภา สวัสดิวัฒน์";
                fund.BHT1 = 10;
                fund.BHT2 = 10;
                fund.BHT5 = 10;
                fund.BHT10c = 10;
                fund.BHT20 = 10;
                fund.BHT50 = 10;
                fund.BHT100 = 10;
                fund.BHT500 = 10;
                fund.BHT1000 = 10;
                funds.Add(fund);

                page.Setup(plaza, funds);
                */
                PageContentManager.Instance.Current = page;
                
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void exchangeBankNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                // Exchange Fund Page.
                var page = new DMT.TA.Pages.Exchange.PlazaRequestExchangePage();
                /*
                BindingList<Models.FundExchange> items = new BindingList<Models.FundExchange>();

                page.Setup(plaza, items);
                */
                PageContentManager.Instance.Current = page;
                
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void plazaReceivedCoupon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                // Plaza Coupon Received
                var page = new DMT.TA.Pages.Plaza.Coupon.PlazaCouponReceivedPage();
                /*
                List<Models.Coupon> coupons = new List<Models.Coupon>();
                Models.Coupon coupon;

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 12, 09, 05, 00);
                coupon.StaffId = "14055";
                coupon.StaffName = "นางวิภา สวัสดิวัฒน์";
                coupon.Count = 10;
                coupon.Type = "คูปอง 35 บาท";
                coupons.Add(coupon);

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 15, 08, 50, 21);
                coupon.StaffId = "14124";
                coupon.StaffName = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์";
                coupon.Count = 20;
                coupon.Type = "คูปอง 35 บาท";
                coupons.Add(coupon);

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 17, 09, 10, 42);
                coupon.StaffId = "14211";
                coupon.StaffName = "นาย ภักดี อมรรุ่งโรจน์";
                coupon.Count = 15;
                coupon.Type = "คูปอง 80 บาท";
                coupons.Add(coupon);

                page.Setup(coupons);
                */
                PageContentManager.Instance.Current = page;
                
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void plazaAllStock_Click(object sender, RoutedEventArgs e)
        {
            
            var win = new DMT.TA.Windows.Plaza.PlazaStockSummaryWindow();
            win.Owner = Application.Current.MainWindow;
            /*
            Models.FundEntry fund = new Models.FundEntry();
            fund.Description = "เงินยืมทอน";
            fund.HasRemark = false;
            fund.BHT1 = 100;
            fund.BHT2 = 100;
            fund.BHT5 = 100;
            fund.BHT10c = 100;
            fund.BHT20 = 100;
            fund.BHT50 = 100;
            fund.BHT100 = 100;
            fund.BHT500 = 100;
            fund.BHT1000 = 100;

            Models.CouponEntry coupon = new Models.CouponEntry();
            coupon.Description = "คุปอง";
            coupon.BHT35 = 32;
            coupon.BHT80 = 43;
            win.Setup(fund, coupon);
            */
            if (win.ShowDialog() == false)
            {
                return;
            }
            try
            {

            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void collectorFund_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var page = new DMT.TA.Pages.Collector.Fund.CollectorFundSummaryPage();
                /*
                Models.FundEntry plazaFund = new Models.FundEntry();
                plazaFund.Description = "เงินยืม-ทอน (ด่าน)";
                plazaFund.HasRemark = false;

                plazaFund.Date = new DateTime(2020, 3, 12, 09, 05, 00);
                plazaFund.StaffId = "14055";
                plazaFund.BHT1 = 1000;
                plazaFund.BHT2 = 1000;
                plazaFund.BHT5 = 1000;
                plazaFund.BHT10c = 500;
                plazaFund.BHT20 = 200;
                plazaFund.BHT50 = 100;
                plazaFund.BHT100 = 200;
                plazaFund.BHT500 = 100;
                plazaFund.BHT1000 = 100;

                BindingList<Models.FundEntry> funds = new BindingList<Models.FundEntry>();
                Models.FundEntry fund;
                // Collector 1
                fund = new Models.FundEntry();
                fund.Description = "นาย สุเทพ เหมัน";
                fund.Date = new DateTime(2020, 3, 17, 09, 05, 00);
                fund.StaffId = "14321";
                fund.Lane = 1;
                fund.BHT1 = 50;
                fund.BHT2 = 50;
                fund.BHT5 = 40;
                fund.BHT10c = 20;
                fund.BHT20 = 10;
                fund.BHT50 = 5;
                fund.BHT100 = 18;
                fund.BHT500 = 10;
                fund.BHT1000 = 5;
                funds.Add(fund);

                // Collector 2
                fund = new Models.FundEntry();
                fund.Description = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์";
                fund.Date = new DateTime(2020, 3, 17, 09, 05, 00);
                fund.StaffId = "13201";
                fund.Lane = 4;
                fund.BHT1 = 20;
                fund.BHT2 = 15;
                fund.BHT5 = 60;
                fund.BHT10c = 45;
                fund.BHT20 = 45;
                fund.BHT50 = 5;
                fund.BHT100 = 24;
                fund.BHT500 = 10;
                fund.BHT1000 = 5;
                funds.Add(fund);

                // Collector 3
                fund = new Models.FundEntry();
                fund.Description = "นางวิภา สวัสดิวัฒน์";
                fund.Date = new DateTime(2020, 3, 17, 09, 05, 00);
                fund.StaffId = "11559";
                fund.Lane = 8;
                fund.BHT1 = 20;
                fund.BHT2 = 15;
                fund.BHT5 = 20;
                fund.BHT10c = 20;
                fund.BHT20 = 10;
                fund.BHT50 = 3;
                fund.BHT100 = 21;
                fund.BHT500 = 14;
                fund.BHT1000 = 2;
                funds.Add(fund);

                // Collector 4
                fund = new Models.FundEntry();
                fund.Description = "นาย ภักดี อมรรุ่งโรจน์";
                fund.Date = new DateTime(2020, 3, 17, 09, 05, 00);
                fund.StaffId = "12866";
                fund.Lane = 5;
                fund.BHT1 = 0;
                fund.BHT2 = 0;
                fund.BHT5 = 0;
                fund.BHT10c = 0;
                fund.BHT20 = 0;
                fund.BHT50 = 0;
                fund.BHT100 = 0;
                fund.BHT500 = 0;
                fund.BHT1000 = 0;
                funds.Add(fund);

                page.Setup(plazaFund, funds);
                */
                PageContentManager.Instance.Current = page;
                
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void collectorFundReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var page = new DMT.TA.Pages.Plaza.Reports.CollectorFundSummaryReportPage();
                PageContentManager.Instance.Current = page;
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void collectorReveivedCoupon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                // Coupon Return
                var page = new DMT.TA.Pages.Collector.Coupon.CollectorBorrowCouponPage();
                /*
                List<Models.Coupon> coupons = new List<Models.Coupon>();
                Models.Coupon coupon;

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 16, 18, 50, 11);
                coupon.StaffId = "14055";
                coupon.StaffName = "นางวิภา สวัสดิวัฒน์";
                coupon.Lane = 6;
                coupon.Count = 5;
                coupon.Type = "คูปอง 80 บาท";
                coupons.Add(coupon);

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 16, 23, 15, 24);
                coupon.StaffId = "14147";
                coupon.StaffName = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์";
                coupon.Lane = 2;
                coupon.Count = 4;
                coupon.Type = "คูปอง 35 บาท";
                coupons.Add(coupon);

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 17, 12, 1, 47);
                coupon.StaffId = "12562";
                coupon.StaffName = "นาย ภักดี อมรรุ่งโรจน์";
                coupon.Lane = 4;
                coupon.Count = 9;
                coupon.Type = "คูปอง 80 บาท";
                coupons.Add(coupon);

                page.Setup(coupons);
                */
                PageContentManager.Instance.Current = page;
                
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void collectorReturnCoupon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                // Coupon Return
                var page = new DMT.TA.Pages.Collector.Coupon.CollectorReturnCouponPage();
                /*
                List<Models.Coupon> coupons = new List<Models.Coupon>();
                Models.Coupon coupon;

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 16, 18, 50, 11);
                coupon.StaffId = "14055";
                coupon.StaffName = "นางวิภา สวัสดิวัฒน์";
                coupon.Lane = 6;
                coupon.Count = 10;
                coupon.Type = "คูปอง 35 บาท";
                coupons.Add(coupon);

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 16, 23, 15, 24);
                coupon.StaffId = "14147";
                coupon.StaffName = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์";
                coupon.Lane = 2;
                coupon.Count = 7;
                coupon.Type = "คูปอง 80 บาท";
                coupons.Add(coupon);

                coupon = new Models.Coupon();
                coupon.Date = new DateTime(2020, 3, 17, 12, 1, 47);
                coupon.StaffId = "12562";
                coupon.StaffName = "นาย ภักดี อมรรุ่งโรจน์";
                coupon.Lane = 4;
                coupon.Count = 8;
                coupon.Type = "คูปอง 35 บาท";
                coupons.Add(coupon);

                page.Setup(coupons);
                */
                PageContentManager.Instance.Current = page;
                
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new DMT.Pages.SignInPage();
        }

        #endregion
    }
}
