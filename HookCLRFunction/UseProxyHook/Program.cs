using ProxyHookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UseProxyHook
{
    internal class Program
    {
        static void Main()
        {
            // 创建代理实例
            var proxy = DispatchProxy.Create<ICalculator, HookProxy<ICalculator>>();
            var hookProxy = proxy as HookProxy<ICalculator>;

            // 注入原始对象
            var original = new Calculator();
            hookProxy.SetOriginal(original);

            // 设置要替换的对象
            var target = new MyCalculator();
            hookProxy.SetTarget(target);

            // 现在 proxy 是一个拦截器，调用其方法会先经过 Invoke
            int result = proxy.Add(3, 5);
            Console.WriteLine($"Final result: {result}");
        }
    }
}
