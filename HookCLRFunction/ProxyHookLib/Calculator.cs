using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyHookLib
{
    /// <summary>
    /// 1.定义接口
    /// </summary>
    public interface ICalculator
    {
        int Add(int a, int b);
    }

    /// <summary>
    /// 2. 原始实现类
    /// </summary>
    public class Calculator : ICalculator
    {
        public int Add(int a, int b) => a + b;
    }
}
