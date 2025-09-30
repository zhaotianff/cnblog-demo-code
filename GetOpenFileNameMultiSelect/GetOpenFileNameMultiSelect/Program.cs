using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GetOpenFileName多选
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var files = BrowseMultiFile("全部文件\0*.*\0\0");
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        static List<string> BrowseMultiFile(string filter)
        {
            int size = 1024;
            List<string> list = new List<string>();
            //多选文件是传出一个指针，这里需要提前分配空间
            //如果是单选文件，使用已经分配大小的StringBuilder或string
            IntPtr filePtr = Marshal.AllocHGlobal(size);

            //清空分配的内存区域
            for (int i = 0; i < size; i++)
            {
                Marshal.WriteByte(filePtr, i, 0);
            }

            OpenFileName openFileName = new OpenFileName();
            openFileName.lStructSize = Marshal.SizeOf(openFileName);
            openFileName.lpstrFilter = filter;
            openFileName.filePtr = filePtr;
            openFileName.nMaxFile = size;
            openFileName.lpstrFileTitle = new string(new char[64]);
            openFileName.nMaxFileTitle = 64;
            openFileName.lpstrInitialDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileName.lpstrFileTitle = "浏览文件";
            openFileName.lpstrDefExt = "*.*";
            openFileName.Flags = WinAPI.OFN_EXPLORER | WinAPI.OFN_FILEMUSTEXIST | WinAPI.OFN_PATHMUSTEXIST | WinAPI.OFN_ALLOWMULTISELECT| WinAPI.OFN_NOCHANGEDIR;
            if(WinAPI.GetOpenFileName(openFileName))
            {
                var file = Marshal.PtrToStringAuto(openFileName.filePtr);
                while(!string.IsNullOrEmpty(file))
                {
                    list.Add(file);
                    //转换为地址
                    long filePointer = (long)openFileName.filePtr;
                    //偏移
                    filePointer += file.Length * Marshal.SystemDefaultCharSize + Marshal.SystemDefaultCharSize;
                    openFileName.filePtr = (IntPtr)filePointer;
                    file = Marshal.PtrToStringAuto(openFileName.filePtr);
                }            
            }

            //第一条字符串为文件夹路径，需要再拼成完整的文件路径
            if (list.Count > 1)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    list[i] = System.IO.Path.Combine(list[0], list[i]);
                }

                list = list.Skip(1).ToList();
            }

            Marshal.FreeHGlobal(filePtr);
            return list;
        }
    }
}
