using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace WpfButtonUISample.Controls
{
    public class ImageButton : ButtonBase
    {
        private const string NormalImageSourcePropertyName = "NormalImageSource";
        private const string MouseOverImageSourcePropertyName = "MouseOverImageSource";
        private const string PressedImageSourcePropertyName = "PressedImageSource";

        public static readonly DependencyProperty NormalImageSourceProperty =
            DependencyProperty.Register(NormalImageSourcePropertyName, typeof(ImageSource), typeof(ImageButton));
        public static readonly DependencyProperty MouseOverImageSourceProperty =
            DependencyProperty.Register(MouseOverImageSourcePropertyName,
            typeof(ImageSource), typeof(ImageButton));
        public static readonly DependencyProperty PressedImageSourceProperty =
            DependencyProperty.Register(PressedImageSourcePropertyName, typeof(ImageSource), typeof(ImageButton));

        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        public ImageSource NormalImageSource
        {
            get
            {
                return (ImageSource)GetValue(NormalImageSourceProperty);
            }
            set
            {
                SetValue(NormalImageSourceProperty, value);
            }
        }

        public ImageSource MouseOverImageSource
        {
            get
            {
                return (ImageSource)GetValue(MouseOverImageSourceProperty);
            }
            set
            {
                SetValue(MouseOverImageSourceProperty, value);
            }
        }

        public ImageSource PressedImageSource
        {
            get
            {
                return (ImageSource)GetValue(PressedImageSourceProperty);
            }
            set
            {
                SetValue(PressedImageSourceProperty, value);
            }
        }
    }
}
