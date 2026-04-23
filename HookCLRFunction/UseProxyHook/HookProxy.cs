using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UseProxyHook
{
    /// <summary>
    /// 3. 自定义代理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HookProxy<T> : DispatchProxy where T : class
    {
        private T _original;
        private T _target;


        /// <summary>
        /// 设置被代理的目标对象
        /// </summary>
        /// <param name="target"></param>
        public void SetOriginal(T original)
        {
            _original = original;
        }

        /// <summary>
        /// 设置要替换的对象
        /// </summary>
        /// <param name="methodInfo"></param>
        public void SetTarget(T target)
        {
            _target = target;
        }

        /// <summary>
        /// 拦截所有接口方法
        /// </summary>
        /// <param name="targetMethod"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"Before calling {targetMethod.Name}");

            // 调用原始方法获取原始结果
            var result = targetMethod.Invoke(_original, args);

            Console.WriteLine($"After calling {targetMethod.Name}, result: {result}");

            // 执行目标对象中的函数
            var replaceMethod = _target.GetType().GetMethod(targetMethod.Name, BindingFlags.Public | BindingFlags.Instance);

            //未找到
            if(replaceMethod == null)
            {
                return result;
            }

            return replaceMethod.Invoke(_target, args);
        }
    }
}
