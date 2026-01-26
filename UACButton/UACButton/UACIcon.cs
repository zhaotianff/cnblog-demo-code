using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace UACButton
{
    public class UACIcon
    {
        public const int MAX_PATH = 260;

        [DllImport("Shell32.dll", SetLastError = false)]
        public static extern Int32 SHGetStockIconInfo(SHSTOCKICONID siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyIcon(IntPtr hIcon);

        public static BitmapSource GetUACIcon()
        {
            BitmapSource shieldSource = null;

            SHSTOCKICONINFO sii = new SHSTOCKICONINFO();
            sii.cbSize = (UInt32)Marshal.SizeOf(typeof(SHSTOCKICONINFO));

            Marshal.ThrowExceptionForHR(SHGetStockIconInfo(SHSTOCKICONID.SIID_SHIELD,
                SHGSI.SHGSI_ICON | SHGSI.SHGSI_LARGEICON,
                ref sii));

            shieldSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                sii.hIcon,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DestroyIcon(sii.hIcon);
            return shieldSource;
        }
    }

    public enum SHSTOCKICONID : uint
    {
        /// <summary>Document of a type with no associated application.</summary>
        SIID_DOCNOASSOC = 0,
        /// <summary>Document of a type with an associated application.</summary>
        SIID_DOCASSOC = 1,
        /// <summary>Generic application with no custom icon.</summary>
        SIID_APPLICATION = 2,
        /// <summary>Folder (generic, unspecified state).</summary>
        SIID_FOLDER = 3,
        /// <summary>Folder (open).</summary>
        SIID_FOLDEROPEN = 4,
        /// <summary>5.25-inch disk drive.</summary>
        SIID_DRIVE525 = 5,
        /// <summary>3.5-inch disk drive.</summary>
        SIID_DRIVE35 = 6,
        /// <summary>Removable drive.</summary>
        SIID_DRIVEREMOVE = 7,
        /// <summary>Fixed drive (hard disk).</summary>
        SIID_DRIVEFIXED = 8,
        /// <summary>Network drive (connected).</summary>
        SIID_DRIVENET = 9,
        /// <summary>Network drive (disconnected).</summary>
        SIID_DRIVENETDISABLED = 10,
        /// <summary>CD drive.</summary>
        SIID_DRIVECD = 11,
        /// <summary>RAM disk drive.</summary>
        SIID_DRIVERAM = 12,
        /// <summary>The entire network.</summary>
        SIID_WORLD = 13,
        /// <summary>A computer on the network.</summary>
        SIID_SERVER = 15,
        /// <summary>A local printer or print destination.</summary>
        SIID_PRINTER = 16,
        /// <summary>The Network virtual folder (FOLDERID_NetworkFolder/CSIDL_NETWORK).</summary>
        SIID_MYNETWORK = 17,
        /// <summary>The Search feature.</summary>
        SIID_FIND = 22,
        /// <summary>The Help and Support feature.</summary>
        SIID_HELP = 23,
        /// <summary>Overlay for a shared item.</summary>
        SIID_SHARE = 28,
        /// <summary>Overlay for a shortcut.</summary>
        SIID_LINK = 29,
        /// <summary>Overlay for items that are expected to be slow to access.</summary>
        SIID_SLOWFILE = 30,
        /// <summary>The Recycle Bin (empty).</summary>
        SIID_RECYCLER = 31,
        /// <summary>The Recycle Bin (not empty).</summary>
        SIID_RECYCLERFULL = 32,
        /// <summary>Audio CD media.</summary>
        SIID_MEDIACDAUDIO = 40,
        /// <summary>Security lock.</summary>
        SIID_LOCK = 47,
        /// <summary>A virtual folder that contains the results of a search.</summary>
        SIID_AUTOLIST = 49,
        /// <summary>A network printer.</summary>
        SIID_PRINTERNET = 50,
        /// <summary>A server shared on a network.</summary>
        SIID_SERVERSHARE = 51,
        /// <summary>A local fax printer.</summary>
        SIID_PRINTERFAX = 52,
        /// <summary>A network fax printer.</summary>
        SIID_PRINTERFAXNET = 53,
        /// <summary>A file that receives the output of a Print to file operation.</summary>
        SIID_PRINTERFILE = 54,
        /// <summary>A category that results from a Stack by command to organize the contents of a folder.</summary>
        SIID_STACK = 55,
        /// <summary>Super Video CD (SVCD) media.</summary>
        SIID_MEDIASVCD = 56,
        /// <summary>A folder that contains only subfolders as child items.</summary>
        SIID_STUFFEDFOLDER = 57,
        /// <summary>Unknown drive type.</summary>
        SIID_DRIVEUNKNOWN = 58,
        /// <summary>DVD drive.</summary>
        SIID_DRIVEDVD = 59,
        /// <summary>DVD media.</summary>
        SIID_MEDIADVD = 60,
        /// <summary>DVD-RAM media.</summary>
        SIID_MEDIADVDRAM = 61,
        /// <summary>DVD-RW media.</summary>
        SIID_MEDIADVDRW = 62,
        /// <summary>DVD-R media.</summary>
        SIID_MEDIADVDR = 63,
        /// <summary>DVD-ROM media.</summary>
        SIID_MEDIADVDROM = 64,
        /// <summary>CD+ (enhanced audio CD) media.</summary>
        SIID_MEDIACDAUDIOPLUS = 65,
        /// <summary>CD-RW media.</summary>
        SIID_MEDIACDRW = 66,
        /// <summary>CD-R media.</summary>
        SIID_MEDIACDR = 67,
        /// <summary>A writable CD in the process of being burned.</summary>
        SIID_MEDIACDBURN = 68,
        /// <summary>Blank writable CD media.</summary>
        SIID_MEDIABLANKCD = 69,
        /// <summary>CD-ROM media.</summary>
        SIID_MEDIACDROM = 70,
        /// <summary>An audio file.</summary>
        SIID_AUDIOFILES = 71,
        /// <summary>An image file.</summary>
        SIID_IMAGEFILES = 72,
        /// <summary>A video file.</summary>
        SIID_VIDEOFILES = 73,
        /// <summary>A mixed file.</summary>
        SIID_MIXEDFILES = 74,
        /// <summary>Folder back.</summary>
        SIID_FOLDERBACK = 75,
        /// <summary>Folder front.</summary>
        SIID_FOLDERFRONT = 76,
        /// <summary>Security shield. Use for UAC prompts only.</summary>
        SIID_SHIELD = 77,
        /// <summary>Warning.</summary>
        SIID_WARNING = 78,
        /// <summary>Informational.</summary>
        SIID_INFO = 79,
        /// <summary>Error.</summary>
        SIID_ERROR = 80,
        /// <summary>Key.</summary>
        SIID_KEY = 81,
        /// <summary>Software.</summary>
        SIID_SOFTWARE = 82,
        /// <summary>A UI item, such as a button, that issues a rename command.</summary>
        SIID_RENAME = 83,
        /// <summary>A UI item, such as a button, that issues a delete command.</summary>
        SIID_DELETE = 84,
        /// <summary>Audio DVD media.</summary>
        SIID_MEDIAAUDIODVD = 85,
        /// <summary>Movie DVD media.</summary>
        SIID_MEDIAMOVIEDVD = 86,
        /// <summary>Enhanced CD media.</summary>
        SIID_MEDIAENHANCEDCD = 87,
        /// <summary>Enhanced DVD media.</summary>
        SIID_MEDIAENHANCEDDVD = 88,
        /// <summary>High definition DVD media in the HD DVD format.</summary>
        SIID_MEDIAHDDVD = 89,
        /// <summary>High definition DVD media in the Blu-ray Disc™ format.</summary>
        SIID_MEDIABLURAY = 90,
        /// <summary>Video CD (VCD) media.</summary>
        SIID_MEDIAVCD = 91,
        /// <summary>DVD+R media.</summary>
        SIID_MEDIADVDPLUSR = 92,
        /// <summary>DVD+RW media.</summary>
        SIID_MEDIADVDPLUSRW = 93,
        /// <summary>A desktop computer.</summary>
        SIID_DESKTOPPC = 94,
        /// <summary>A mobile computer (laptop).</summary>
        SIID_MOBILEPC = 95,
        /// <summary>The User Accounts Control Panel item.</summary>
        SIID_USERS = 96,
        /// <summary>Smart media.</summary>
        SIID_MEDIASMARTMEDIA = 97,
        /// <summary>CompactFlash media.</summary>
        SIID_MEDIACOMPACTFLASH = 98,
        /// <summary>A cell phone.</summary>
        SIID_DEVICECELLPHONE = 99,
        /// <summary>A digital camera.</summary>
        SIID_DEVICECAMERA = 100,
        /// <summary>A digital video camera.</summary>
        SIID_DEVICEVIDEOCAMERA = 101,
        /// <summary>An audio player.</summary>
        SIID_DEVICEAUDIOPLAYER = 102,
        /// <summary>Connect to network.</summary>
        SIID_NETWORKCONNECT = 103,
        /// <summary>The Network and Internet Control Panel item.</summary>
        SIID_INTERNET = 104,
        /// <summary>A compressed file with a .zip file name extension.</summary>
        SIID_ZIPFILE = 105,
        /// <summary>The Additional Options Control Panel item.</summary>
        SIID_SETTINGS = 106,
        /// <summary>High definition DVD drive (any type - HD DVD-ROM, HD DVD-R, HD-DVD-RAM) that uses the HD DVD format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_DRIVEHDDVD = 132,
        /// <summary>High definition DVD drive (any type - BD-ROM, BD-R, BD-RE) that uses the Blu-ray Disc format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_DRIVEBD = 133,
        /// <summary>High definition DVD-ROM media in the HD DVD-ROM format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_MEDIAHDDVDROM = 134,
        /// <summary>High definition DVD-R media in the HD DVD-R format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_MEDIAHDDVDR = 135,
        /// <summary>High definition DVD-RAM media in the HD DVD-RAM format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_MEDIAHDDVDRAM = 136,
        /// <summary>High definition DVD-ROM media in the Blu-ray Disc BD-ROM format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_MEDIABDROM = 137,
        /// <summary>High definition write-once media in the Blu-ray Disc BD-R format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_MEDIABDR = 138,
        /// <summary>High definition read/write media in the Blu-ray Disc BD-RE format.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_MEDIABDRE = 139,
        /// <summary>A cluster disk array.</summary>
        /// <remarks>Windows Vista with SP1 and later.</remarks>
        SIID_CLUSTEREDDRIVE = 140,
        /// <summary>The highest valid value in the enumeration.</summary>
        /// <remarks>Values over 160 are Windows 7-only icons.</remarks>
        SIID_MAX_ICONS = 175
    }

    [Flags]
    public enum SHGSI : uint
    {
        /// <summary>The szPath and iIcon members of the SHSTOCKICONINFO structure receive the path and icon index of the requested icon, in a format suitable for passing to the ExtractIcon function. The numerical value of this flag is zero, so you always get the icon location regardless of other flags.</summary>
        SHGSI_ICONLOCATION = 0,
        /// <summary>The hIcon member of the SHSTOCKICONINFO structure receives a handle to the specified icon.</summary>
        SHGSI_ICON = 0x000000100,
        /// <summary>The iSysImageImage member of the SHSTOCKICONINFO structure receives the index of the specified icon in the system imagelist.</summary>
        SHGSI_SYSICONINDEX = 0x000004000,
        /// <summary>Modifies the SHGSI_ICON value by causing the function to add the link overlay to the file's icon.</summary>
        SHGSI_LINKOVERLAY = 0x000008000,
        /// <summary>Modifies the SHGSI_ICON value by causing the function to blend the icon with the system highlight color.</summary>
        SHGSI_SELECTED = 0x000010000,
        /// <summary>Modifies the SHGSI_ICON value by causing the function to retrieve the large version of the icon, as specified by the SM_CXICON and SM_CYICON system metrics.</summary>
        SHGSI_LARGEICON = 0x000000000,
        /// <summary>Modifies the SHGSI_ICON value by causing the function to retrieve the small version of the icon, as specified by the SM_CXSMICON and SM_CYSMICON system metrics.</summary>
        SHGSI_SMALLICON = 0x000000001,
        /// <summary>Modifies the SHGSI_LARGEICON or SHGSI_SMALLICON values by causing the function to retrieve the Shell-sized icons rather than the sizes specified by the system metrics.</summary>
        SHGSI_SHELLICONSIZE = 0x000000004
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SHSTOCKICONINFO
    {
        public UInt32 cbSize;
        public IntPtr hIcon;
        public Int32 iSysIconIndex;
        public Int32 iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = UACIcon.MAX_PATH)]
        public string szPath;
    }
}
