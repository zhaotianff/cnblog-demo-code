using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAlignmentTest
{
    class Program
    {
        [DllImport("DataAlignmentLib.dll")]
        public static extern IntPtr Test();

        static void Main(string[] args)
        {
            struct_normal struct_Normal = new struct_normal();
            struct_pack struct_Pack = new struct_pack();

            Console.WriteLine("struct_pack size:" + Marshal.SizeOf(struct_Normal));
            Console.WriteLine("struct_normal size:" + Marshal.SizeOf(struct_Pack));

            IntPtr intPtr = Test();
            struct_Pack = (struct_pack)Marshal.PtrToStructure(intPtr, typeof(struct_pack));

            Console.WriteLine(struct_Pack.a);
            Console.WriteLine(struct_Pack.b);
            Console.WriteLine(struct_Pack.c);
            Console.WriteLine(struct_Pack.d);

            //未进行字节对齐的情况，程序可以执行，但内存里的值对应不起来
            struct_Normal = (struct_normal)Marshal.PtrToStructure(intPtr, typeof(struct_normal));
            Console.WriteLine(struct_Normal.a);
            Console.WriteLine(struct_Normal.b);
            Console.WriteLine(struct_Normal.c);
            Console.WriteLine(struct_Normal.d);
        }
    }


    [StructLayout(layoutKind:LayoutKind.Sequential,Pack = 2)]
    struct struct_pack
    {
        public char a;
        public int b;
        public short c;
        public long d;
    };

    struct struct_normal
    {
        public char a;
        public int b;
        public short c;
        public long d;
    };

}
