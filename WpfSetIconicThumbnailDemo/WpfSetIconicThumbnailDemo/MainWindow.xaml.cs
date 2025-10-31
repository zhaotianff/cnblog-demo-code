using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSetIconicThumbnailDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int size = Marshal.SizeOf(typeof(Int32));
            IntPtr pBool = Marshal.AllocHGlobal(size);
            Marshal.WriteInt32(pBool, 0, 1);  // last parameter 0 (FALSE), 1 (TRUE)

            //设置属性
            var result = DWM.DwmSetWindowAttribute(new WindowInteropHelper(this).Handle, DWM.FORCE_ICONIC_REPRESENTATION, pBool, sizeof(int));
            result = DWM.DwmSetWindowAttribute(new WindowInteropHelper(this).Handle, DWM.HAS_ICONIC_BITMAP, pBool, sizeof(int));

            Marshal.FreeHGlobal(pBool);

            //Windows消息处理函数
            HwndSource.FromHwnd(new WindowInteropHelper(this).Handle).AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case DWM.WM_DWMSENDICONICTHUMBNAIL:

                    //使用本地图片
                    var bmp = (Bitmap)Bitmap.FromFile(Environment.CurrentDirectory + "\\thumb.jpg");
                    var hBitmap = bmp.GetHbitmap();

                    //使用绘制的图片
                    //hBitmap = CreateBitmap(HIWORD(lParam),LOWORD(lParam));

                    DWM.DwmSetIconicThumbnail(hwnd, hBitmap, DWM.DISPLAYFRAME);
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        private short HIWORD(IntPtr lParam)
        {
            return (short)(((ulong)lParam >> 16) & 0xFFFF);
        }

        private short LOWORD(IntPtr lParam)
        {
            return (short)((ulong)lParam & 0xFFFF);
        }

        private IntPtr CreateBitmap(int width,int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawString("HelloWorld", new Font("Arial",13), System.Drawing.Brushes.Red, 20, 20);
            }

            return bitmap.GetHbitmap();
        }
    }
}
