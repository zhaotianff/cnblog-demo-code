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
using System.Windows.Shapes;

namespace WebBrowserDemo.Controls
{
    public partial class WebBrowserEx : Window
    {
        public WebBrowserEx()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TargetElementProperty = DependencyProperty.Register("TargetElement", typeof(FrameworkElement), typeof(WebBrowserEx), new PropertyMetadata(TargetElementPropertyChanged));
        public FrameworkElement TargetElement
        {
            get
            {
                return GetValue(TargetElementProperty) as FrameworkElement;
            }
            set
            {
                SetValue(TargetElementProperty, value);
            }
        }


        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(WebBrowserEx), new PropertyMetadata(SourcePropertyChanged));
        public string Source
        {
            get
            {
                return GetValue(SourceProperty) as string;
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        private static void SourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var webBrowserOverlayWindow = sender as WebBrowserEx;

            if (webBrowserOverlayWindow != null)
            {
                webBrowserOverlayWindow.wfBrowser.Navigate(args.NewValue as string);
            }
        }

        private static void TargetElementPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var oldTargetElement = args.OldValue as FrameworkElement;
            var webBrowserOverlayWindow = sender as WebBrowserEx;
            var mainWindow = Window.GetWindow(webBrowserOverlayWindow.TargetElement);

            if (webBrowserOverlayWindow != null && mainWindow != null)
            {
                webBrowserOverlayWindow.Owner = mainWindow;
                webBrowserOverlayWindow.Owner.LocationChanged += webBrowserOverlayWindow.PositionAndResize;
                webBrowserOverlayWindow.TargetElement.LayoutUpdated += webBrowserOverlayWindow.PositionAndResize;

                if (oldTargetElement != null)
                    oldTargetElement.LayoutUpdated -= webBrowserOverlayWindow.PositionAndResize;

                webBrowserOverlayWindow.PositionAndResize(sender, new EventArgs());

                if (webBrowserOverlayWindow.TargetElement.IsVisible && webBrowserOverlayWindow.Owner.IsVisible)
                {
                    webBrowserOverlayWindow.Show();
                }

                webBrowserOverlayWindow.TargetElement.IsVisibleChanged += (x, y) =>
                {
                    if (webBrowserOverlayWindow.TargetElement.IsVisible && webBrowserOverlayWindow.Owner.IsVisible)
                    {
                        webBrowserOverlayWindow.Show();
                    }
                    else
                    {
                        webBrowserOverlayWindow.Hide();
                    }
                };
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Owner.LocationChanged -= PositionAndResize;
            if (TargetElement != null)
            {
                TargetElement.LayoutUpdated -= PositionAndResize;
            }
        }

        private void PositionAndResize(object sender, EventArgs e)
        {
            if (TargetElement != null && TargetElement.IsVisible)
            {
                var point = TargetElement.PointToScreen(new Point());
                Left = point.X + 396;
                Top = point.Y + 326;

                //Height = TargetElement.ActualHeight;
                //Width = TargetElement.ActualWidth;
            }
        }

    }
}
