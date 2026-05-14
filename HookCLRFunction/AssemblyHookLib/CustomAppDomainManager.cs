using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyHookLib
{
    /// <summary>
    /// 继承自AppDomainManager的类，会在程序集加载时，自动创建实例
    /// </summary>
    public class CustomAppDomainManager : AppDomainManager
    {
        private readonly HookCreateFile hookCreateFile = new HookCreateFile();
    }
}
