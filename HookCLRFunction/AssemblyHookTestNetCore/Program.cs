using AssemblyHookLibNetCore;
using System.Reflection;

namespace AssemblyHookTestNetCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pluginPath = @"C:\MyPlugins\AwesomePlugin.dll";
            var loadContext = new CustomAssemblyLoadContext(pluginPath);
            Assembly pluginAssembly = loadContext.LoadFromAssemblyPath(pluginPath);
        }
    }
}
