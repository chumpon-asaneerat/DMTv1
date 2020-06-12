#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

#endregion

namespace DMT.Pages
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SignInPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        #endregion

        #region Button Handler(s)

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            // Init Main Menu
            //PageContentManager.Instance.Current = new Pages.TA.MainMenu();
        }

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
