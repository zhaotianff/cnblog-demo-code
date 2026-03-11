using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WpfDockDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : TianXiaTech.BlurWindow
    {
        private const double DockAreaWidth = 5;
        private const double ContentAreaWidth = 300;

        /// <summary>
        /// 设置是否边缘停靠
        /// </summary>
        private bool isDocking = true;
        public bool IsDocking
        {
            get => isDocking;
            set
            {
                isDocking = value;

                if (this.WindowState == WindowState.Normal)
                {
                    if (isDocking)
                    {
                        HideWindow();
                    }
                    else
                    {
                        ShowWindow();
                    }
                }
            }
        }

        private bool isAnimation = false;
        private bool isDraged = false;

        private System.Windows.Media.Animation.Storyboard hiddenAnimation;
        private System.Windows.Media.Animation.Storyboard showAnimation;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAnimation();
        }

        private void InitializeAnimation()
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            hiddenAnimation = this.FindResource("hiddenAnimation") as System.Windows.Media.Animation.Storyboard;
            var hiddenDoubleAnimation = hiddenAnimation.Children[0] as System.Windows.Media.Animation.DoubleAnimation;
            hiddenDoubleAnimation.From = screenWidth - DockAreaWidth - ContentAreaWidth;
            hiddenDoubleAnimation.To = screenWidth - DockAreaWidth;
            hiddenAnimation.Completed += HiddenAnimation_Completed;

            showAnimation = this.FindResource("showAnimation") as System.Windows.Media.Animation.Storyboard;
            var showDoubleAnimation = showAnimation.Children[0] as System.Windows.Media.Animation.DoubleAnimation;
            showDoubleAnimation.From = screenWidth - DockAreaWidth;
            showDoubleAnimation.To = screenWidth - DockAreaWidth - ContentAreaWidth;
            showAnimation.Completed += ShowAnimation_Completed;
        }

        private void ShowAnimation_Completed(object sender, EventArgs e)
        {
            isAnimation = false;
        }

        private void HiddenAnimation_Completed(object sender, EventArgs e)
        {
            isAnimation = false;
            grid_DockArea.Visibility = Visibility.Visible;
        }

        private void BlurWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    this.DragMove();
                    isDraged = true;
                }
                catch
                {

                }
            }

            if (e.LeftButton == MouseButtonState.Released && isDocking == true && isDraged == true)
            {
                POINT point = new POINT();
                if (User32.GetCursorPos(ref point) == 1)
                {
                    var pos = e.GetPosition(this);

                    if (pos.X < 0 && pos.Y < 0)
                        HideWindow();
                }

                isDraged = false;
            }
        }

        private void HideWindow(double left = -1)
        {
            if (left == -1)
            {
                RECT rect = new RECT();
                User32.GetWindowRect(new WindowInteropHelper(this).Handle, ref rect);
                left = rect.left;
            }

            if (SystemParameters.PrimaryScreenWidth - left - this.Width > 15)
                return;

            if (isAnimation)
                return;

            isAnimation = true;
            hiddenAnimation.Begin();
        }

        private void grid_DockArea_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowWindow();
        }

        private void ShowWindow()
        {
            if (isAnimation)
                return;

            grid_DockArea.Visibility = Visibility.Collapsed;
            isAnimation = true;
            showAnimation.Begin();
        }

        private void main_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isDocking && isDraged == false)
            {
                HideWindow();
            }
        }

        private void main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this);
            if (pos.X >= 0 && pos.Y >= 0)
                isDraged = true;
        }
    }
}
