using ProxyHookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseProxyHook
{
    public class MyCalculator : ICalculator
    {
        /// <summary>
        /// 替换后的Add函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Add(int a, int b)
        {
            return a * b;
        }
    }
}
