﻿#region Using

using System;
using System.Security.Principal;
using System.Windows;

//using NLib.Services;
//using DMT.Models;
//using DMT.Services;

using Fluent;

#endregion

namespace AvalonDockAndEditSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
