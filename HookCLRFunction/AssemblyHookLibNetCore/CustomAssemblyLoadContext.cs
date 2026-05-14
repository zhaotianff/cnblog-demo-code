using System.Reflection;
using System.Runtime.Loader;

namespace AssemblyHookLibNetCore
{
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        public CustomAssemblyLoadContext(string pluginPath) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            // 1. 尝试用解析器找到依赖的程序集文件路径
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                // 2. 找到路径，则加载它
                return LoadFromAssemblyPath(assemblyPath);
            }

            // 3. 如果解析器找不到，则回退到默认加载逻辑（例如加载应用程序的共享程序集）
            return null;
        }
    }
}
