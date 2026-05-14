using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyHookLib
{
    public class HookCreateFile
    {
        /// <summary>
        /// 在CustomAppDomainManager类中创建实例
        /// </summary>
        internal HookCreateFile()
        {
            if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
                return;

            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }

        private static void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {

            var assemblyName = args.LoadedAssembly.GetName().Name;

            Console.WriteLine("Load Assembly: " + assemblyName);


            //过滤程序集，只Hook所需要的程序集
            //这里是System.IO
            if (assemblyName != "AssemblyHookTest")
            {
                Console.WriteLine("Return");
                return;
            }

            AppDomain.CurrentDomain.AssemblyLoad -= OnAssemblyLoad;

            //获取当前加载的所有程序集
            Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in loadedAssemblies)
            {
                Console.WriteLine("Loaded Aseembly: " + assembly.GetName().Name);
            }

            //假设mscorlib是第一项
            //实际使用时，可根据情况来进行判断
            var mscorlib = loadedAssemblies[0];

            if (mscorlib == null)
            {
                Console.WriteLine("mscorlib Assembly not find...");
                return;
            }

            var targetMethodClass = mscorlib.GetType("System.IO.File");

            Console.WriteLine("Find System.IO.File Type");

            var handlerMethodClass = typeof(HookCreateFile);
            var tramplineMethodClass = typeof(HookCreateFile);

            BindingFlags anyType = BindingFlags.Static |
                                        BindingFlags.Instance |
                                        BindingFlags.Public |
                                        BindingFlags.NonPublic;

            Type[] types = new Type[] { typeof(string) };

            //System.IO.Create(string)
            //target
            MethodInfo target = targetMethodClass.GetMethod("Create", anyType, null, types, null);

            //handler
            MethodInfo handler = handlerMethodClass.GetMethod("CreateHooked", anyType, null, types, null);

            //Trampoline
            MethodInfo trampoline = tramplineMethodClass.GetMethod("CreateTrampoline", anyType, null, types, null);

            Console.WriteLine($"find target={target != null}|find handler={handler != null}|find trampoline={trampoline != null}");

            RuntimeHelpers.PrepareMethod(target.MethodHandle);
            RuntimeHelpers.PrepareMethod(handler.MethodHandle);
            RuntimeHelpers.PrepareMethod(trampoline.MethodHandle);

            IntPtr targetMethodPtr = target.MethodHandle.GetFunctionPointer();
            IntPtr handlerMethodPtr = handler.MethodHandle.GetFunctionPointer();
            IntPtr trampolineMethodPtr = trampoline.MethodHandle.GetFunctionPointer();

            if (MinHook.InstallHook(targetMethodPtr, handlerMethodPtr, trampolineMethodPtr))
            {
                Console.WriteLine("Install hook success...");
            }
            else
            {
                Console.WriteLine("Install hook failed...");
            }
        }

        private static FileStream CreateHooked(string filePath)
        {
            Console.WriteLine("Create Hooked");

            return System.IO.File.Create(filePath, 1024, FileOptions.None);
        }

        private static FileStream CreateTrampoline(string filePath)
        {
            Console.WriteLine("Create trampoline");
            return null;
        }
    }
}
