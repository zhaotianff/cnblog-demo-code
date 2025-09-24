using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CSharpLib
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Computer
    {
        public int cpuId; 
        public string cpuName;
        public int osVersion;
    }

    public delegate void PrintComputerDelegate(Computer computer);

    public class ExplortClass
     {
         public int GetID()
         {
             return 1024;
         }

        public Computer GetComputer()
        {
            Computer computer = new Computer();
            computer.cpuId = 100000000;
            computer.cpuName = "Intel";
            computer.osVersion = 11;
            return computer;
        }

        public void PrintComputer(Computer computer)
        {
            Console.WriteLine(computer.cpuId);
            Console.WriteLine(computer.cpuName);
            Console.WriteLine(computer.osVersion);
        }

        public PrintComputerDelegate GetComputerDelegate() => PrintComputer;
     }  
}
