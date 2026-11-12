using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace WPFRenderingDemo
{

    public struct POINT
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// ParticleDraw.xaml 的交互逻辑
    /// </summary>
    public partial class RingDraw : UIElement
    {
        [DllImport("User32.dll")]
        public static extern int GetCursorPos(ref POINT point);

        private static readonly double[] LayerOpacities = { 0.05, 0.09, 0.16, 1 };
        private readonly DrawingVisual visual = new DrawingVisual();
        private Pen[] ringPens;
        private TimeSpan? lastRenderingTime;
        private double cyclePhase;

        public RingDraw()
        {
            //创建画笔(冻结状态，不需要修改)
            ringPens = CreatePens();

            IsHitTestVisible = false;

            //把自定义的visual对象注册为当前这个可视化父元素的可视化子节点
            AddVisualChild(visual);
        }

        /// <summary>
        /// 重写VisualChildrenCount，代表当前只有一个Visual子对象
        /// </summary>
        protected override int VisualChildrenCount => 1;

        /// <summary>
        /// 获取自己创建的Visual子对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return visual;
        }

        /// <summary>
        /// 创建画笔
        /// </summary>
        /// <returns></returns>
        private Pen[] CreatePens()
        {
            var brush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#41E1FF"), 0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#9B6EFF"), 0.5),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#FF78C8"), 1)
                }
            };
            brush.Freeze();
            var widths = new[] { 12.0, 8, 5, 2 };
            var pens = new Pen[widths.Length];
            for (var i = 0; i < widths.Length; i++)
            {
                pens[i] = new Pen(brush, widths[i]);
                pens[i].Freeze();
            }
            return pens;
        }

        public void Rendering(object sender, EventArgs e)
        {
            var renderingTime = ((RenderingEventArgs)e).RenderingTime;
            if (lastRenderingTime == renderingTime)
                return;

            var elapsed = lastRenderingTime.HasValue ? (renderingTime - lastRenderingTime.Value).TotalSeconds : 1.0 / 60;
            lastRenderingTime = renderingTime;
            var screenPoint = new POINT();
            if (GetCursorPos(ref screenPoint) == 0)
                return;

            var point = this.PointFromScreen(new Point(screenPoint.x, screenPoint.y));

            Update(point, elapsed);
        }

        /// <summary>
        /// 逐帧更新
        /// </summary>
        /// <param name="cursor">鼠标位置</param>
        /// <param name="elapsedSeconds">时间</param>
        public void Update(Point cursor, double elapsedSeconds)
        {
            if (!double.IsFinite(elapsedSeconds) || elapsedSeconds <= 0 || !double.IsFinite(cursor.X) || !double.IsFinite(cursor.Y))
                return;

            var period = 1 / 0.4d;
            var sizeScale = 1.25;
            cyclePhase = (cyclePhase + elapsedSeconds % period / period) % 1;
            using var context = visual.RenderOpen();
            var breath = (1 - Math.Cos(cyclePhase * Math.PI * 2)) * 0.5;
            var radius = (22 + 4 * breath) * sizeScale;

            for (var i = 0; i < ringPens.Length; i++)
            {
                context.PushOpacity(LayerOpacities[i]);
                context.DrawEllipse(null, ringPens[i], cursor, radius, radius);
                context.Pop();
            }
        }
    }
}
