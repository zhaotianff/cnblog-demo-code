using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFGetRawInputData.Winapi
{
    /// <summary>
    /// 原始输入设备类型枚举
    /// </summary>
    public enum RawInputType : uint
    {
        RIM_TYPEKEYBOARD = 1, // 键盘
        RIM_TYPEMOUSE = 0    // 鼠标
    }

    /// <summary>
    /// 原始输入设备结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTDEVICE
    {
        public ushort UsagePage; // 设备使用页（键盘/鼠标固定值）
        public ushort Usage;     // 设备使用ID（键盘/鼠标固定值）
        public uint Flags;       // 注册标志
        public IntPtr WindowHandle; // 接收输入的窗口句柄
    }

    /// <summary>
    /// 原始输入数据头部
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTHEADER
    {
        public RawInputType Type;
        public uint Size;
        public IntPtr Device;
        public IntPtr WParam;
    }

    /// <summary>
    /// 原始键盘输入结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWKEYBOARD
    {
        public ushort MakeCode;
        public ushort Flags;
        public ushort Reserved;
        public ushort VKey;
        public uint Message;
        public uint ExtraInformation;
    }

    /// <summary>
    /// 原始鼠标输入结构体
    /// </summary>
    [StructLayout(LayoutKind.Explicit,Size = 4)]
    public struct RAWMOUSE
    {
        [FieldOffset(0)]
        public ushort Flags;
        [FieldOffset(4)]
        public uint Buttons;
        [FieldOffset(4)]
        public DUMMYSTRUCTNAME dUMMYSTRUCTNAME;
        [FieldOffset(8)]
        public uint RawButtons;
        [FieldOffset(12)]
        public int LastX;
        [FieldOffset(16)]
        public int LastY;
        [FieldOffset(20)]
        public uint ExtraInformation;
    }

    public struct DUMMYSTRUCTNAME
    {
        public ushort ButtonFlags;
        public ushort ButtonData;
    }

    /// <summary>
    /// 原始输入数据联合体（键盘/鼠标二选一）
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct RAWINPUTDATA
    {
        [FieldOffset(0)]
        public RAWMOUSE Mouse;
        [FieldOffset(0)]
        public RAWKEYBOARD Keyboard;
    }

    /// <summary>
    /// 原始输入结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUT
    {
        public RAWINPUTHEADER Header;
        public RAWINPUTDATA Data;
    }

    public static class User32
    {
        // 注册原始输入设备
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterRawInputDevices(
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]
            RAWINPUTDEVICE[] pRawInputDevices,
            uint uiNumDevices,
            uint cbSize);

        // 获取原始输入数据
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetRawInputData(
            IntPtr hRawInput,
            uint uiCommand,
            IntPtr pData,
            ref uint pcbSize,
            uint cbSizeHeader);

        // 窗口消息常量
        public const uint WM_INPUT = 0x00FF;

        // 键盘使用页/ID
        public const ushort HID_USAGE_PAGE_GENERIC = 0x01;
        public const ushort HID_USAGE_GENERIC_KEYBOARD = 0x06;

        // 鼠标使用页/ID
        public const ushort HID_USAGE_GENERIC_MOUSE = 0x02;

        // 注册标志：输入数据发送到窗口消息队列
        public const uint RIDEV_INPUTSINK = 0x00000100;
    }
}
