namespace HarmonyPatcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("test.testid");
            harmony.PatchAll();

            System.IO.File.Create("a.txt");
        }
    }
}
