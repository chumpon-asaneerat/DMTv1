using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Collector.Credit
{
    /// <summary>
    /// Interaction logic for CollectorCreditSummaryPage.xaml
    /// </summary>
    public partial class CollectorCreditSummaryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorCreditSummaryPage()
        {
            InitializeComponent();

            //grid.ItemChanged += Grid_ItemChanged;
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

        private void Grid_ItemChanged(object sender, EventArgs e)
        {
            //Calculate();
        }

        private void addCollector_Click(object sender, RoutedEventArgs e)
        {
            /*
            var win = new Windows.TA.Collector.FundBorrowWindow();

            Models.FundEntry fund = new Models.FundEntry();

            Models.FundEntry src = new Models.FundEntry();
            Models.FundEntry obj = new Models.FundEntry();
            Models.FundEntry ret = new Models.FundEntry();

            assign(fund, src);

            _current.Description = "ยอดที่สามารถยืมได้";
            _current.HasRemark = false;

            src.Description = "ยอดยืมปัจจุบัน";
            src.HasRemark = false;
            obj.Description = "ยืมเงิน";
            ret.Description = "ยอดด่านคงเหลือ";
            ret.HasRemark = false;

            win.Owner = Application.Current.MainWindow;
            win.Title = fund.Description;

            win.Setup(_current, src, obj, ret, true);

            if (win.ShowDialog() == false)
            {
                return;
            }

            if (win.StaffId != String.Empty && win.CollectorName != String.Empty)
            {
                obj.StaffId = win.StaffId;
                obj.Description = win.CollectorName;
                obj.Lane = 1;
                obj.Date = DateTime.Now;

                _funds.Add(obj);

                BorrowFund(fund, obj);

                Calculate();
            }
            */
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new MainMenu();
            PageContentManager.Instance.Current = page;
        }
        /*
        private Models.FundEntry _plaza;
        private Models.FundEntry _current = new Models.FundEntry();
        private BindingList<Models.FundEntry> _funds;


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
        }

        private void assign(Models.FundEntry src, Models.FundEntry dst)
        {
            dst.Description = src.Description;

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

        public void Setup(Models.FundEntry plazaFund,
            BindingList<Models.FundEntry> funds)
        {
            _plaza = plazaFund;
            _funds = funds;

            Calculate();

            _current.HasRemark = false;

            plaza.DataContext = _current;
            grid.Setup(_current, _funds);
        }

        public void Calculate()
        {
            if (null == _plaza) return;
            if (null == _current) return;
            if (null == _funds) return;

            plazaBalance.Text = _plaza.BHTTotal.ToString("n0");
            // set init value.
            assign(_plaza, _current);

            foreach (Models.FundEntry fund in _funds)
            {
                _current.BHT1 -= fund.BHT1;
                _current.BHT2 -= fund.BHT2;
                _current.BHT5 -= fund.BHT5;
                _current.BHT10c -= fund.BHT10c;
                _current.BHT20 -= fund.BHT20;
                _current.BHT50 -= fund.BHT50;
                _current.BHT100 -= fund.BHT100;
                _current.BHT500 -= fund.BHT500;
                _current.BHT1000 -= fund.BHT1000;
            }
        }
        */
    }
}
