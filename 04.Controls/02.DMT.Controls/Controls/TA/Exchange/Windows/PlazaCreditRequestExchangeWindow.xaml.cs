#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TA.Windows.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaCreditRequestExchangeWindow.xaml
    /// </summary>
    public partial class PlazaCreditRequestExchangeWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaCreditRequestExchangeWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancelRequest_Click(object sender, RoutedEventArgs e)
        {
            // change mode to cancel.
            Mode = ExchangeWindowMode.Cancel;
            this.DialogResult = true;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region Public Methods
        /*
        public void Setup(ExchangeWindowMode mode, Models.FundExchange item)
        {
            Mode = mode;

            requestEntry.DataContext = item.Request;
        }
        */
        #endregion

        #region Public Properties

        public ExchangeWindowMode Mode { get; private set; }

        #endregion
    }
}
