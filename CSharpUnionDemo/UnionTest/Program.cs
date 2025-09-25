using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace UnionTest
{
    class Program
    {
        [DllImport("UnionLib.dll")]
        public static extern MyUnion GetMyUnion();

        [DllImport("UnionLib.dll")]
        public static extern IntPtr GetMyUnion2();


        [DllImport("UnionLib.dll")]
        public static extern void TestUnion2(MyUnion2_INT mm, int i);

        [DllImport("UnionLib.dll")]
        public static extern void TestUnion2(MyUnion2_STR mm,int i);


        [DllImport("UnionLib.dll")]
        public static extern MyStructUnion GetMyUnion3();

        static void Main(string[] args)
        {
            
            //只有值类型的情况
            var myunion = GetMyUnion();
            Console.WriteLine(myunion.b);

            //引用类型无法直接通过签名返回，需要进行转换
            //如果这里是使用了MYUNION2中的i字段，可以直接使用MyUnion2_INT作为返回值
            var myunion2Ptr = GetMyUnion2();
            MyUnion2_STR unionStr = new MyUnion2_STR();
            unionStr = (MyUnion2_STR)Marshal.PtrToStructure(myunion2Ptr, typeof(MyUnion2_STR));
            Console.WriteLine(unionStr.str);

            //注意：值类型和引用类型不允许重叠
            //在同时使用值类型和引用类型的情况下，无法直接返回union
            MyUnion2_INT mu1 = new MyUnion2_INT();
            mu1.i = 30;
            TestUnion2(mu1, 1);

            MyUnion2_STR mu2 = new MyUnion2_STR();
            mu2.str = "abc";
            TestUnion2(mu2, 2);

            //结构体和联合体一起使用的情况
            //引用类型部分使用指针再进行转换，直接用字符串将会封送失败
            var myStructUnion = GetMyUnion3();
            var str = Marshal.PtrToStringUni(myStructUnion.pStr);
            Console.WriteLine(str);
        }
    }

    /// <summary>
    /// 只有值类型的情况
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct MyUnion
    {
        [FieldOffset(0)]
        public int b;
        [FieldOffset(0)]
        public double d;
    }

    /// <summary>
    /// 如果使用了union中的int字段，就使用这个结构体
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public struct MyUnion2_INT
    {
        [FieldOffset(0)]
        public int i;
    }

    /// <summary>
    /// 如果使用了union中的char[]字段，就使用这个结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MyUnion2_STR
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string str;
    }

#if x86
    /// <summary>
    /// 32位
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 264)]
    public struct MyStructUnion
    {
        [FieldOffset(0)]
        public uint uType;

        [FieldOffset(4)]
        public IntPtr pStr;

        [FieldOffset(4)]
        public IntPtr cStr;
    }

#endif


#if x64
    /// <summary>
    /// 64位
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 272)]
    public struct MyStructUnion
    {
        [FieldOffset(0)]
        public uint uType;

        [FieldOffset(8)]
        public IntPtr pStr;

        [FieldOffset(8)]
        public IntPtr cStr;
    }
#endif
}
