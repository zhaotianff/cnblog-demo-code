using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GetOpenFileName多选
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct OpenFileName
    {
        public int lStructSize;
        public IntPtr hwndOwner;
        public IntPtr hInstance;
        public string lpstrFilter;
        public string lpstrCustomFilter;
        public int nMaxCustFilter;
        public int nFilterIndex;
        public IntPtr filePtr;  //多选文件时不能用string或StringBuilder
        public int nMaxFile;
        public string lpstrFileTitle;
        public int nMaxFileTitle;
        public string lpstrInitialDir;
        public string lpstrTitle;
        public int Flags;
        public short nFileOffset;
        public short nFileExtension;
        public string lpstrDefExt;
        public IntPtr lCustData;
        public IntPtr lpfnHook;
        public string lpTemplateName;
        public IntPtr pvReserved;
        public int dwReserved;
        public int flagsEx;
    }

    public class WinAPI
    {

        public const int OFN_READONLY = 0x1;
        public const int OFN_OVERWRITEPROMPT = 0x2;
        public const int OFN_HIDEREADONLY = 0x4;
        public const int OFN_NOCHANGEDIR = 0x8;
        public const int OFN_SHOWHELP = 0x10;
        public const int OFN_ENABLEHOOK = 0x20;
        public const int OFN_ENABLETEMPLATE = 0x40;
        public const int OFN_ENABLETEMPLATEHANDLE = 0x80;
        public const int OFN_NOVALIDATE = 0x100;
        public const int OFN_ALLOWMULTISELECT = 0x200;
        public const int OFN_EXTENSIONDIFFERENT = 0x400;
        public const int OFN_PATHMUSTEXIST = 0x800;
        public const int OFN_FILEMUSTEXIST = 0x1000;
        public const int OFN_CREATEPROMPT = 0x2000;
        public const int OFN_SHAREAWARE = 0x4000;
        public const int OFN_NOREADONLYRETURN = 0x8000;
        public const int OFN_NOTESTFILECREATE = 0x10000;
        public const int OFN_NONETWORKBUTTON = 0x20000;
        public const int OFN_NOLONGNAMES = 0x40000;
        public const int OFN_EXPLORER = 0x80000;
        public const int OFN_NODEREFERENCELINKS = 0x100000;
        public const int OFN_LONGNAMES = 0x200000;
        public const int OFN_ENABLEINCLUDENOTIFY = 0x400000;
        public const int OFN_ENABLESIZING = 0x800000;
        public const int OFN_DONTADDTORECENT = 0x2000000;
        public const int OFN_FORCESHOWHIDDEN = 0x10000000;
        public const int OFN_EX_NOPLACESBAR = 0x1;
        public const int OFN_SHAREFALLTHROUGH = 2;
        public const int OFN_SHARENOWARN = 1;
        public const int OFN_SHAREWARN = 0;

        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    }
}
