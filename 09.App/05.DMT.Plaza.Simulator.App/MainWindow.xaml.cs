#region Using

using System;
using System.Security.Principal;
using System.Windows;

using NLib.Services;

//using DMT.Models;
//using DMT.Services;

using Fluent;

#endregion

namespace DMT
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            Models.Objects.User user = new Models.Objects.User();
            string result = Services.DMTServiceOperations.Instance.Plaza.BeginJob();
            //Console.WriteLine(result);
            MessageBox.Show(result);
            */
            Models.Objects.User user = new Models.Objects.User();
            user.UserId = "99001";
            var ret = Services.DMTServiceOperations.Instance.Plaza.GetUser(user);
            if (null != ret)
                MessageBox.Show("Found :" + ret.UserName);
            else MessageBox.Show("User not Found");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Models.Objects.User user = new Models.Objects.User();
            pgrid.SelectedObject = user;
        }
    }
}
