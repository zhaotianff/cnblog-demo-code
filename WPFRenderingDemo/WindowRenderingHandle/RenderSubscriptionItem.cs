using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowRenderingHandle
{
    public class RenderSubscriptionItem
    {
        /// <summary>
        /// 渲染回调函数
        /// </summary>
        public Action<Point,double> Action { get; set; }

        /// <summary>
        /// 是否暂停渲染
        /// </summary>
        public bool IsPaused { get; set; }
    }
}
