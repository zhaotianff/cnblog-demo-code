using System.Runtime.InteropServices;
using System.Text;
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
using WPFGetRawInputData.Winapi;

namespace WPFGetRawInputData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TianXiaTech.BlurWindow
    {
        IntPtr mainWindowHandle = IntPtr.Zero;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            mainWindowHandle = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(mainWindowHandle).AddHook(HwndProc);
        }

        public IntPtr HwndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // 处理WM_INPUT消息
            if (msg == User32.WM_INPUT)
            {
                //处理原始输入数据
                ProcessRawInput(lParam);
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //注册
            RAWINPUTDEVICE[] devices = new RAWINPUTDEVICE[2];

            // 注册键盘
            devices[0] = new RAWINPUTDEVICE
            {
                UsagePage = User32.HID_USAGE_PAGE_GENERIC,
                Usage = User32.HID_USAGE_GENERIC_KEYBOARD,
                Flags = User32.RIDEV_INPUTSINK,
                WindowHandle = mainWindowHandle
            };

            // 注册鼠标
            devices[1] = new RAWINPUTDEVICE
            {
                UsagePage = User32.HID_USAGE_PAGE_GENERIC,
                Usage = User32.HID_USAGE_GENERIC_MOUSE,
                Flags = User32.RIDEV_INPUTSINK,
                WindowHandle = mainWindowHandle
            };

            // 调用API注册设备
            var result  = User32.RegisterRawInputDevices(
                devices,
                (uint)devices.Length,
                (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE)));

            if (result == false)
            {
                System.Windows.MessageBox.Show("注册失败");

                //调用GetLastError查看原因
            }
            else
            {
                DisplayMessage("注册成功");
            }
        }

        private void DisplayMessage(string message)
        {
            Dispatcher.Invoke(() => {
                this.listbox.Items.Add(message);
                this.listbox.ScrollToEnd();
            });
        }

        /// <summary>
        /// 解析原始输入数据
        /// </summary>
        /// <param name="lParam"></param>
        private void ProcessRawInput(IntPtr lParam)
        {
            uint dataSize = 0;
            // 第一步：获取数据大小
            User32.GetRawInputData(
                lParam,
                0x10000003, // RID_INPUT：获取原始输入数据
                IntPtr.Zero,
                ref dataSize,
                (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

            if (dataSize == 0) return;

            // 第二步：分配内存并获取数据
            IntPtr dataPtr = Marshal.AllocHGlobal((int)dataSize);
            try
            {
                uint result = User32.GetRawInputData(
                    lParam,
                    0x10000003,
                    dataPtr,
                    ref dataSize,
                    (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

                if (result != dataSize) return;

                // 第三步：解析数据
                RAWINPUT rawInput = Marshal.PtrToStructure<RAWINPUT>(dataPtr);
                switch (rawInput.Header.Type)
                {
                    //键盘
                    case RawInputType.RIM_TYPEKEYBOARD:
                        ProcessKeyboardInput(rawInput.Data.Keyboard);
                        break;
                    //鼠标
                    case RawInputType.RIM_TYPEMOUSE:
                        ProcessMouseInput(rawInput.Data.Mouse);
                        break;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(dataPtr);
            }
        }

        /// <summary>
        /// 处理键盘输入
        /// </summary>
        /// <param name="keyboard"></param>
        private void ProcessKeyboardInput(RAWKEYBOARD keyboard)
        {
            // 判断按键按下（Flags=0）或释放（Flags=1）
            bool isKeyDown = (keyboard.Flags & 0x01) == 0;

            // 转换为键盘按键
            System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)keyboard.VKey;    

            // 输出调试信息（可替换为自定义逻辑）
            string action = isKeyDown ? "按下" : "释放";

            DisplayMessage($"键盘：{key} {action} (扫描码：{keyboard.MakeCode})");
        }

        // 处理鼠标输入
        private void ProcessMouseInput(RAWMOUSE mouse)
        {
            // 鼠标按键状态
            bool leftButtonDown = (mouse.dUMMYSTRUCTNAME.ButtonFlags & 0x0001) != 0;
            bool leftButtonUp = (mouse.dUMMYSTRUCTNAME.ButtonFlags & 0x0002) != 0;
            bool rightButtonDown = (mouse.dUMMYSTRUCTNAME.ButtonFlags & 0x0004) != 0;
            bool rightButtonUp = (mouse.dUMMYSTRUCTNAME.ButtonFlags & 0x0008) != 0;

            // 输出调试信息（可替换为自定义逻辑）
            if (leftButtonDown)
            {
                DisplayMessage("鼠标左键按下");
            }

            if (leftButtonUp)
            {
                DisplayMessage("鼠标左键释放");
            }

            if (rightButtonDown)
            {
                DisplayMessage("鼠标右键按下");
            }

            if (rightButtonUp)
            {
                DisplayMessage("鼠标右键释放");
            }
        }
    }
}