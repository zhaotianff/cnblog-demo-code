using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibVlcSharpEventDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private const int WH_MOUSE = 7;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_LBUTTONDBLCLK = 0x0203;

        private static IntPtr hookID = IntPtr.Zero;
        private static LowLevelMouseProc mouseProc = HookCallback;

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        public static List<IntPtr> GetAllChildHandles(IntPtr parentHwnd)
        {
            List<IntPtr> result = new List<IntPtr>();
            EnumChildWindows(parentHwnd, (hWnd, lParam) =>
            {
                result.Add(hWnd);
                return true;
            }, IntPtr.Zero);
            return result;
        }

        private LibVLC _libVLC;
        private LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

        public MainWindow()
        {
            InitializeComponent();
            Core.Initialize();
        }

        public async void SetHook(IntPtr hwnd)
        {
            //等待，防止运行较慢，窗口未显示
            await Task.Delay(1000);

            var childWindowList = GetAllChildHandles(hwnd);

            if(childWindowList == null || childWindowList.Count == 0)
            {
                return;
            }

            hookID = SetWindowsHookEx(WH_MOUSE, mouseProc, IntPtr.Zero, GetWindowThreadProcessId(childWindowList[0], out _));
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(hookID);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if((int)wParam == WM_LBUTTONDBLCLK)
                {
                    MessageBox.Show("双击");
                }
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _libVLC = new LibVLC();
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC);
            videoView.MediaPlayer = _mediaPlayer;

            //请将文件替换为你的路径
            string videoPath = @"F:\test.mp4";

            _mediaPlayer.Play(new Media(_libVLC, new Uri(videoPath)));

            var hwnd = this._mediaPlayer.Hwnd;
            SetHook(hwnd);
        }

        private void videoView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                MessageBox.Show("双击");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unhook();
        }
    }
}
