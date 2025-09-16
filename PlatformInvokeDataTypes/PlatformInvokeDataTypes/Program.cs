using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PlatformInvokeDataTypes
{
    class Program
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);    

        const int FILE_MAP_READ = 0x0004;
        const int MemorySize = 1024;

        struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        struct Point
        {
            public int x;
            public int y;
        }


        static void Main(string[] args)
        {
            //平台调用时的数据类型对应示例
            //这里只展示使用指针和System.IntPtr的情况

            //使用IntPtr的情况
            ReadWithoutOffset();

            //使用指针的情况
            ReadWithOffset();      
        }

        static void ReadWithoutOffset()
        {
            IntPtr withoutOffsetPtr = OpenFileMapping(FILE_MAP_READ, false, "HelloWorld");
            IntPtr mapView = MapViewOfFile(withoutOffsetPtr, FILE_MAP_READ, 0, 0, MemorySize);
            Rect rect = new Rect();
            var size = Marshal.SizeOf(rect);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            byte[] buffer = new byte[size];
            Marshal.Copy(mapView, buffer, 0, size);
            Marshal.Copy(buffer, 0, ptr, size);
            var obj = Marshal.PtrToStructure(ptr, typeof(Rect));
            rect = (Rect)obj;

            Console.WriteLine($"Rect:left={rect.left},top={rect.top},right={rect.right},bottom={rect.bottom}");

        }

        static void ReadWithOffset()
        {
            //这一部分操作跟上面函数是一样的
            IntPtr withoutOffsetPtr = OpenFileMapping(FILE_MAP_READ, false, "HelloWorld2");
            IntPtr mapView = MapViewOfFile(withoutOffsetPtr, FILE_MAP_READ, 0, 0, MemorySize);
            Rect rect = new Rect();
            var size = Marshal.SizeOf(rect);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            byte[] buffer = new byte[size];
            Marshal.Copy(mapView, buffer, 0, size);
            Marshal.Copy(buffer, 0, ptr, size);
            var obj = Marshal.PtrToStructure(ptr, typeof(Rect));
            rect = (Rect)obj;

            Console.WriteLine($"Rect:left={rect.left},top={rect.top},right={rect.right},bottom={rect.bottom}");

            //使用指针
            unsafe
            {
                byte* b = (byte*)mapView;
                b += size;
                mapView = (IntPtr)b;
            }

            //读取Point结构的值
            Point point = new Point();
            size = Marshal.SizeOf(point);
            ptr = Marshal.AllocHGlobal(size);
            buffer = new byte[size];
            Marshal.Copy(mapView, buffer, 0, size);
            Marshal.Copy(buffer, 0, ptr, size);
            obj = Marshal.PtrToStructure(ptr, typeof(Point));
            point = (Point)obj;

            Console.WriteLine($"Point:x={point.x},y={point.y}");
        }
    }
}
