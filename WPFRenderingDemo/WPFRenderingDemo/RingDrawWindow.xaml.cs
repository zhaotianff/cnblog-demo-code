using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WPFRenderingDemo
{
    /// <summary>
    /// RingDrawWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RingDrawWindow : Window
    {
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);

        private RingDraw ringDraw;


        [DllImport("User32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("User32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,int X, int Y, int cx, int cy, uint uFlags);

        public RingDrawWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle| WS_EX_TRANSPARENT);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ringDraw = new RingDraw();
            CompositionTarget.Rendering += ringDraw.Rendering;
            this.canvas.Children.Add(ringDraw);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CompositionTarget.Rendering -= ringDraw.Rendering;
        }
    }
}
