using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NLib.Controls
{
    /// <summary>
    /// Interaction logic for SquareControl.xaml
    /// </summary>
    public partial class SquareControl : UserControl
    {
        public SquareControl()
        {
            InitializeComponent();
        }

        /*
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            var percentWidthChange = Math.Abs(sizeInfo.NewSize.Width - sizeInfo.PreviousSize.Width) / sizeInfo.PreviousSize.Width;
            var percentHeightChange = Math.Abs(sizeInfo.NewSize.Height - sizeInfo.PreviousSize.Height) / sizeInfo.PreviousSize.Height;

            if (percentWidthChange > percentHeightChange)
                this.Height = sizeInfo.NewSize.Width / _aspectRatio;
            else
                this.Width = sizeInfo.NewSize.Height * _aspectRatio;

            base.OnRenderSizeChanged(sizeInfo);
        }
        */
    }
}
