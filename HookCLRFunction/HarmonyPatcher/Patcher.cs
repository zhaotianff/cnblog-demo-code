using HarmonyLib;
using System;
using System.IO;
using System.Reflection;

namespace HarmonyPatcher
{
    // Hook File.Create(string)
    [HarmonyPatch]
    public class FileCreatePatch
    {
        /// <summary>
        /// 选择重载
        /// File.Create(string path)
        /// </summary>
        /// <returns></returns>
        static MethodBase TargetMethod()
        {
            return AccessTools.Method(
                typeof(File),
                nameof(File.Create),
                new Type[] { typeof(string) });

            //也可以使用特性来实现同样的功能
            //把下面三行替换[HarmonyPatch]
            //[HarmonyPatch(typeof(File))]
            //[HarmonyPatch("Create")]
            //[HarmonyPatch(new Type[] { typeof(string) })]
        }

        /// <summary>
        /// 在File.Create调用前调用
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static bool Prefix(ref string path)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("[HOOK] File.Create");
            Console.WriteLine($"原始路径: {path}");

            // 可以修改参数
            // 例如我把文件路径修改为桌面
            string newPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\a.txt";

            Console.WriteLine($"重定向后的路径: {newPath}");

            path = newPath;
            
            Console.WriteLine("=================================");

            // true = 继续执行原始函数
            return true;
        }

        /// <summary>
        /// 在File.Create调用后调用
        /// </summary>
        /// <param name="path"></param>
        /// <param name="__result"></param>
        static void Postfix(string path, FileStream __result)
        {
            Console.WriteLine("[POSTFIX] 文件已经被创建");

            Console.WriteLine($"文件路径: {path}");

            if (__result != null)
            {
                //获取结果
                Console.WriteLine($"CanWrite: {__result.CanWrite}");
            }

            Console.WriteLine();
        }
    }
}