using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfSetIconicThumbnailDemo
{
    public class DWM
    {
        public const int WM_DWMSENDICONICTHUMBNAIL = 0x0323;
        public const int FORCE_ICONIC_REPRESENTATION = 7;
        public const int HAS_ICONIC_BITMAP = 10;
        public const int DISPLAYFRAME = 1;

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetIconicThumbnail(IntPtr hwnd, IntPtr hbmp, int dwSITFlags);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);
    }
}
