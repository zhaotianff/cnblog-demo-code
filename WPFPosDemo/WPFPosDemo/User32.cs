using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFPosDemo
{
    public struct POINT
    {
        public int x;
        public int y;
    }

    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    public class User32
    {
        [DllImport("User32.dll")]
        public static extern int GetCursorPos(ref POINT point);

        [DllImport("User32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);       
    }
}
