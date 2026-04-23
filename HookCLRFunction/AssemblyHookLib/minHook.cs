using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static AssemblyHookLib.MinHook.NativeMethods;

namespace AssemblyHookLib
{
    /// <summary>
    /// minHook的封装
    /// </summary>
    /// <remarks>
    /// 这个类可以自动加载minHook动态库中的导出函数，源码来自 https://github.com/tandasat/DotNetHooking.git
    /// 使用这个类，需要下载minHook的动态库放到程序执行路径下（注意：需要区分x86和x64版本）
    /// </remarks>
    internal static class MinHook
    {
        //
        // Helper function to install hook using MinHook.
        //
        internal
        static
        bool
        InstallHook(
            IntPtr TargetAddr,
            IntPtr HookHandlerAddr,
            IntPtr TrampolineAddr
            )
        {
            //
            // This code expects either MinHook.x86.dll or MinHook.x64.dll is
            // located in any of the DLL search path. Such as the current folder
            // and %PATH%.
            //
            string architecture = (IntPtr.Size == 4) ? "x86" : "x64";
            string dllPath = "MinHook." + architecture + ".dll";
            IntPtr moduleHandle = LoadLibrary(dllPath);
            if (moduleHandle == IntPtr.Zero)
            {
                Console.WriteLine("[-] An inline hook DLL not found. Did you locate " +
                                  dllPath + " under the DLL search path?");
                return false;
            }

            var MH_Initialize = GetExport<MH_InitializeType>(moduleHandle, "MH_Initialize");
            var MH_CreateHook = GetExport<MH_CreateHookType>(moduleHandle, "MH_CreateHook");
            var MH_EnableHook = GetExport<MH_EnableHookType>(moduleHandle, "MH_EnableHook");


            MH_STATUS status = MH_Initialize();
            Trace.Assert(status == MH_STATUS.MH_OK);

            //
            // Modify the target method to jump to the HookHandler method. The
            // original receives an address of trampoline code to call the
            // original implementation of the target method.
            //
            status = MH_CreateHook(TargetAddr, HookHandlerAddr, out IntPtr original);
            Trace.Assert(status == MH_STATUS.MH_OK);

            //
            // Modify the Trampoline method to jump to the original
            // implementation of the target method.
            //
            status = MH_CreateHook(TrampolineAddr, original, out _);
            Trace.Assert(status == MH_STATUS.MH_OK);

            //
            // Commit and activate the above two hooks.
            //
            status = MH_EnableHook(MH_ALL_HOOKS);
            Trace.Assert(status == MH_STATUS.MH_OK);

            return true;
        }

        //
        // Helper function to resolve an export of a DLL.
        //
        private
        static
        ProcType
        GetExport<ProcType>(
            IntPtr ModuleHandle,
            string ExportName
            ) where ProcType : class
        {
            //
            // Get a function pointer, convert it to delegate, and return it as
            // a requested type.
            //
            IntPtr pointer = GetProcAddress(ModuleHandle, ExportName);
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            Delegate function = Marshal.GetDelegateForFunctionPointer(
                                                            pointer,
                                                            typeof(ProcType));
            return function as ProcType;
        }

        [SuppressUnmanagedCodeSecurity]
        internal static class NativeMethods
        {
            [DllImport("kernel32.dll",
                        EntryPoint = "LoadLibraryW",
                        SetLastError = true,
                        CharSet = CharSet.Unicode)]
            internal
            static
            extern
            IntPtr
            LoadLibrary(
                string FileName
                );

            [DllImport("kernel32.dll",
                        EntryPoint = "GetProcAddress",
                        SetLastError = true,
                        CharSet = CharSet.Ansi,
                        BestFitMapping = false)]
            internal
            static
            extern
            IntPtr
            GetProcAddress(
                IntPtr Module,
                string ProcName
                );

            //
            // MinHook specific.
            //
            internal static IntPtr MH_ALL_HOOKS = IntPtr.Zero;
            internal enum MH_STATUS
            {
                MH_OK = 0,
            }

            [UnmanagedFunctionPointer(CallingConvention.Winapi)]
            internal
            delegate
            MH_STATUS
            MH_InitializeType(
                );

            [UnmanagedFunctionPointer(CallingConvention.Winapi)]
            internal
            delegate
            MH_STATUS
            MH_CreateHookType(
                IntPtr Target,
                IntPtr Detour,
                out IntPtr Original
                );

            [UnmanagedFunctionPointer(CallingConvention.Winapi)]
            internal
            delegate
            MH_STATUS
            MH_EnableHookType(
                IntPtr Target
                );
        }
    }
}
