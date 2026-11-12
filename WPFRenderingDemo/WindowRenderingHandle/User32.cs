using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowRenderingHandle
{
    internal class User32
    {
        [DllImport("User32.dll")]
        public static extern int GetCursorPos(ref POINT point);
    }

    public struct POINT
    {
        public int x;
        public int y;
    }
}
