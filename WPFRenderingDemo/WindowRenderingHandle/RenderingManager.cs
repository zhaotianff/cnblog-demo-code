using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WindowRenderingHandle
{
    public static class RenderingManager
    {
        /// <summary>
        /// 订阅列表
        /// </summary>
        private static List<RenderSubscriptionItem> renderSubscriptionList = new List<RenderSubscriptionItem>();

        private static TimeSpan? lastRenderingTime;
        private static bool isInitialized = false;

        /// <summary>
        /// 开始处理CompositionTarget.Rendering事件
        /// </summary>
        public static void StartCompositionTargetRendering()
        {
            if (isInitialized == true)
                return;

            CompositionTarget.Rendering += OnRendering;
            isInitialized = true;
        }

        /// <summary>
        /// 停止处理CompositionTarget.Rendering事件
        /// </summary>
        public static void StopCompositionTargetRendering()
        {
            if (isInitialized == false)
                return;

            CompositionTarget.Rendering -= OnRendering;
            isInitialized = false;
            lastRenderingTime = null;
            renderSubscriptionList.Clear();
        }

        /// <summary>
        /// 在这里对订阅列表中的方法进行调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnRendering(object sender, EventArgs e)
        {
            if (renderSubscriptionList.Count == 0)
                return;

            var renderingTime = ((RenderingEventArgs)e).RenderingTime;
            if (lastRenderingTime == renderingTime)
                return;

            //计算当前时间
            //默认以60帧/秒来计算
            var elapsed = lastRenderingTime.HasValue ? (renderingTime - lastRenderingTime.Value).TotalSeconds : 1.0 / 60;
            lastRenderingTime = renderingTime;

            var screenPoint = new POINT();
            if (User32.GetCursorPos(ref screenPoint) == 0)
                return;

            var point = (new Point(screenPoint.x, screenPoint.y));

            for (int i = 0; i < renderSubscriptionList.Count; i++)
            {
                var item = renderSubscriptionList[i];
                if (item.IsPaused == true)
                {
                    continue;
                }

                try
                {
                    //调用订阅中的函数
                    item.Action.Invoke(point, elapsed);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 添加订阅函数（内部调用）
        /// </summary>
        /// <param name="renderSubscriptionItem"></param>
        private static void AddSubscription(RenderSubscriptionItem renderSubscriptionItem)
        {
            bool isExist = false;

            foreach (var item in renderSubscriptionList)
            {
                if (renderSubscriptionItem.Action == item.Action)
                {
                    isExist = true;
                }
            }

            if (isExist == true)
                return;

            renderSubscriptionList.Add(renderSubscriptionItem);

            //自动开始
            StartCompositionTargetRendering();
        }

        /// <summary>
        /// 添加订阅函数
        /// </summary>
        /// <param name="action"></param>
        public static void AddSubscription(Action<Point,double> action)
        {
            AddSubscription(new RenderSubscriptionItem() 
            {
                Action = action,
                IsPaused = false  
            });
        }

        /// <summary>
        /// 移除订阅函数
        /// </summary>
        /// <param name="action"></param>
        public static void RemoveSubscription(Action<Point, double> action)
        {
            for (int i = renderSubscriptionList.Count - 1; i >= 0; i--)
            {
                if (renderSubscriptionList[i].Action == action)
                {
                    renderSubscriptionList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 设置订阅函数状态
        /// </summary>
        /// <param name="renderSubscriptionItem"></param>
        /// <param name="isPaused"></param>
        public static void SetSubscriptionStatus(Action<Point,double> renderSubscriptionItem, bool isPaused)
        {
            foreach (var item in renderSubscriptionList)
            {
                if (renderSubscriptionItem == item.Action)
                {
                    item.IsPaused = isPaused;
                    break;
                }
            }
        }
    }
}
