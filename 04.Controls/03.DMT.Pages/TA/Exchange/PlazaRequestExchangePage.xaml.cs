using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaRequestExchangePage.xaml
    /// </summary>
    public partial class PlazaRequestExchangePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaRequestExchangePage()
        {
            InitializeComponent();
        }

        #endregion


        private void cmdRequest_Click(object sender, RoutedEventArgs e)
        {
            var win = new DMT.TA.Windows.Exchange.PlazaFundRequestExchangeWindow();
            win.Owner = Application.Current.MainWindow;
            /*
            var obj = Models.FundExchange.CreateNewRequest(_plaza, "14055", "นางวิภา สวัสดิวัฒน์", 0);

            win.Title = "คำร้องขอการแลกเปลี่ยนเงิน";
            win.Setup(Windows.TA.Exchange.ExchangeWindowMode.New, obj);
            
            if (win.ShowDialog() == false)
            {
                return;
            }

            if (win.Mode == Windows.TA.Exchange.ExchangeWindowMode.New)
            {
                // append item.
                _items.Add(obj);
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
        private BindingList<Models.FundExchange> _items;

        public void Setup(Models.FundEntry plaza, BindingList<Models.FundExchange> items)
        {
            _plaza = plaza;
            _items = items;

            this.plaza.DataContext = _plaza;
            this.grid.Setup(_plaza, _items);
        }
        */
    }
}
