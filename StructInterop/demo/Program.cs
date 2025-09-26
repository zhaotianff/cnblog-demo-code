using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace demo
{
    struct struct_basic
    {
        public ushort value_1;
        public int value_2;
        public uint value_3;
        public uint value_4;
        public bool value_5;
    }

    [StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
    struct struct_advanced
    {
        public ushort id;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst =256)]
        public string message;
    };

    class Program
    {
        [DllImport("lib.dll")]
        public static extern void get_basic(ref struct_basic basic);

        [DllImport("lib.dll")]
        public static extern void get_advanced(ref struct_advanced advanced);

        static void Main(string[] args)
        {
            TestStructBasic();

            TestStructAdvanced();
        }

        static void TestStructBasic()
        {
            struct_basic basic = new struct_basic();
            get_basic(ref basic);
            Console.WriteLine($"{basic.value_1}  {basic.value_2}  {basic.value_3}  {basic.value_4}  {basic.value_5}");
        }

        static void TestStructAdvanced()
        {
            struct_advanced advanced = new struct_advanced();
            get_advanced(ref advanced);
            Console.WriteLine($"{advanced.id}  {advanced.message}");
        }
    }
}
