
using HarmonyLib;

namespace HarmonyUsageDemo
{
    public class MyPatcher
    {
        // make sure DoPatching() is called at start either by
        // the mod loader or by your injector

        public static void DoPatching()
        {
            var harmony = new Harmony("com.example.patch");

            var mOriginal = AccessTools.Method(typeof(System.IO.File), nameof(System.IO.File.Create),new Type[] { typeof(string)});  //指定函数
            var mPrefix = SymbolExtensions.GetMethodInfo(()=> MyPrefix);
            var mPostfix = SymbolExtensions.GetMethodInfo(() => MyPostfix);
            // in general, add null checks here (new HarmonyMethod() does it for you too)

            harmony.Patch(mOriginal, new HarmonyMethod(mPrefix), new HarmonyMethod(mPostfix));
        }

        public static bool MyPrefix(ref string path)
        {
            return true;
        }

        public static void MyPostfix(string path,FileStream __result)
        {
            
        }
    }

    public class Program
    {
        public static void Main()
        {
            System.IO.File.Create("a.txt");
        }
    }
}

