using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Controls.Exchange.View
{
    /// <summary>
    /// Interaction logic for PlazaRequestExchangeView.xaml
    /// </summary>
    public partial class PlazaRequestExchangeView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaRequestExchangeView()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.OnTick += new EventHandler(Instance_OnTick);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.OnTick -= new EventHandler(Instance_OnTick);
        }

        #endregion

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            /*
            Button b = sender as Button;
            Models.FundExchange item = b.CommandParameter as Models.FundExchange;
            if (null != item)
            {
                item.IsEditing = true;

                var win = new DMT.TA.Windows.Exchange.PlazaFundRequestExchangeWindow();
                win.Owner = Application.Current.MainWindow;

                win.Title = "คำร้องขอการแลกเปลี่ยนเงิน";

                win.Setup(DMT.TA.Windows.Exchange.ExchangeWindowMode.Edit, item);
                if (win.ShowDialog() == false)
                {
                    item.IsEditing = false;
                    return;
                }

                item.IsEditing = false;

                if (win.Mode == DMT.TA.Windows.Exchange.ExchangeWindowMode.Cancel)
                {
                    // remove an item if cancel.
                    _items.Remove(item);
                }

                // refresh the update item.
                listView.Items.Refresh();
            }
            */
        }

        private void cmdExchange_Click(object sender, RoutedEventArgs e)
        {
            /*
            Button b = sender as Button;
            Models.FundExchange item = b.CommandParameter as Models.FundExchange;
            if (null != item)
            {
                item.IsEditing = true;

                var win = new DMT.TA.Windows.Exchange.PlazaFundRequestExchangeWindow();
                win.Owner = Application.Current.MainWindow;

                // backup descriptions
                var d1 = item.Request.Description;
                var d2 = item.Exchange.Description;

                // replace descriptions
                item.Request.Description = "รายการขอแลกเงินจากด่าน";
                item.Request.HasRemark = true;

                item.Approve.Description = "รายการอนุมัติจากบัญชี";
                item.Approve.HasRemark = true;

                item.Exchange.Description = "จ่ายออก ธนบัตร/เหรียญ";

                win.Title = "ยืนยันข้อมูลการแลกเปลี่ยนเงิน";
                win.Setup(item);
                if (win.ShowDialog() == false)
                {
                    // restore descriptions
                    item.Request.Description = d1;
                    item.Exchange.Description = d2;
                    item.IsEditing = false;
                    return;
                }

                item.IsEditing = false;

                // append to plaza fund.
                AppendPlazaFund(item);
                // remove current item and update plaza balance.
                if (null != _items)
                {
                    lock (this)
                    {
                        _items.Remove(item);
                    }
                }

                // refresh the items list.
                listView.Items.Refresh();
            }
            */
        }
        /*
        private void AppendPlazaFund(Models.FundExchange item)
        {
            _plaza.BHT1 = _plaza.BHT1 - item.Exchange.BHT1 + item.Request.BHT1;
            _plaza.BHT2 = _plaza.BHT2 - item.Exchange.BHT2 + item.Request.BHT2;
            _plaza.BHT5 = _plaza.BHT5 - item.Exchange.BHT5 + item.Request.BHT5;
            _plaza.BHT10c = _plaza.BHT10c - item.Exchange.BHT10c + item.Request.BHT10c;
            _plaza.BHT20 = _plaza.BHT20 - item.Exchange.BHT20 + item.Request.BHT20;
            _plaza.BHT50 = _plaza.BHT50 - item.Exchange.BHT50 + item.Request.BHT50;
            _plaza.BHT100 = _plaza.BHT100 - item.Exchange.BHT100 + item.Request.BHT100;
            _plaza.BHT500 = _plaza.BHT500 - item.Exchange.BHT500 + item.Request.BHT500;
            _plaza.BHT1000 = _plaza.BHT1000 - item.Exchange.BHT1000 + item.Request.BHT1000;
        }

        private void Approve(Models.FundExchange item)
        {
            item.Approve.BHT1 = item.Request.BHT1;
            item.Approve.BHT2 = item.Request.BHT2;
            item.Approve.BHT5 = item.Request.BHT5;
            item.Approve.BHT10c = item.Request.BHT10c;
            item.Approve.BHT20 = item.Request.BHT20;
            item.Approve.BHT50 = item.Request.BHT50;
            item.Approve.BHT100 = item.Request.BHT100;
            item.Approve.BHT500 = item.Request.BHT500;
            item.Approve.BHT1000 = item.Request.BHT1000;
        }
        */
        private DateTime _lastupdated = DateTime.Now;

        void Instance_OnTick(object sender, EventArgs e)
        {
            /*
            // update status.
            if (null != _items && _items.Count > 0)
            {
                Models.FundExchange[] items = null;
                lock (this)
                {
                    items = _items.ToArray();
                }

                var ts = DateTime.Now - _lastupdated;
                var bChanged = false;
                if (ts.TotalSeconds >= 10)
                {
                    if (null != items)
                    {
                        foreach (var item in items)
                        {
                            // Change status.
                            if (!item.IsEditing && item.StatusId == 0)
                            {
                                bChanged = true;
                                item.StatusId = 1;
                                Approve(item); // approve
                                break;
                            }
                        }
                    }

                    _lastupdated = DateTime.Now;
                    // refresh the items list.
                    if (bChanged)
                    {
                        listView.Items.Refresh();
                    }
                }
            }
            */
        }
        /*
        private Models.FundEntry _plaza;
        private BindingList<Models.FundExchange> _items;

        public void Setup(Models.FundEntry plaza, BindingList<Models.FundExchange> items)
        {
            _plaza = plaza;
            _items = items;
            listView.ItemsSource = _items;
        }
        */
    }
}
