using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPointerDemo
{
    class Program
    {
        [DllImport("demo_lib.dll", EntryPoint = "PrintArray")]
        private static extern void PrintArrayRef(ref int pa, int size);

        [DllImport("demo_lib.dll", EntryPoint = "PrintArray")]
        private static extern unsafe void PrintArrayPointer(int* pa, int size);

        static void Main(string[] args)
        {
            PrintArrayRef();
            Console.WriteLine();
            PrintArrayPointer();
        }

        /// <summary>
        /// 使用ref
        /// </summary>
        static void PrintArrayRef()
        {
            int[] array = new int[] { 1, 2, 3 };

            //使用ref关键字传的是引用，ref[0]其实就是传的首地址
            PrintArrayRef(ref array[0], array.Length);
        }

        /// <summary>
        /// 使用指针
        /// </summary>
        static unsafe void PrintArrayPointer()
        {
            int size = 3;
            int* array = stackalloc int[3];

            for (int i = 0; i < size; i++)
            {
                array[i] = i + 1;
            }
            PrintArrayPointer(array, size);
        }
    }
}
