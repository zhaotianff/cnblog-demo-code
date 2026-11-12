using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WindowRenderingHandle
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public const int MaxNodes = 192;
        public const double Lifetime = 0.8;
        private readonly double[] LayerWidths = { 3.2, 2.1, 1, 0.22 };
        private readonly double[] LayerOpacities = { 0.06, 0.13, 0.85, 0.95 };
        private readonly DrawingVisual visual = new DrawingVisual();
        private readonly List<RibbonNode> nodes = new List<RibbonNode>(MaxNodes);
        private readonly Color[] colors =
        {
            (Color)ColorConverter.ConvertFromString("#00F0FF"),
            (Color)ColorConverter.ConvertFromString("#9E44FF"),
            (Color)ColorConverter.ConvertFromString("#FF37B4")
        };
        private Point? lastPoint;

        public Window2()
        {
            InitializeComponent();

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
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            return visual;
        }

        /// <summary>
        /// 渲染更新函数
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="elapsedSeconds"></param>
        public void Update(Point cursor, double elapsedSeconds)
        {
            if (!double.IsFinite(elapsedSeconds) || elapsedSeconds <= 0 || !double.IsFinite(cursor.X) || !double.IsFinite(cursor.Y))
                return;

            //转换为窗口坐标
            cursor = this.PointFromScreen(cursor);

            for (var i = nodes.Count - 1; i >= 0; i--)
            {
                var node = nodes[i];
                node.Age += elapsedSeconds;

                if (node.Age >= Lifetime)
                {
                    nodes.RemoveAt(i);
                }
                else
                {
                    nodes[i] = node;
                }
            }

            var distance = lastPoint.HasValue ? (cursor - lastPoint.Value).Length : 0;
            if (!lastPoint.HasValue || (double.IsFinite(distance) && distance >= 2))
            {
                if (nodes.Count == MaxNodes) nodes.RemoveAt(0);
                nodes.Add(new RibbonNode { Position = cursor });
                lastPoint = cursor;
            }

            using (var context = visual.RenderOpen())
            {
                context.PushOpacity(0.9f);
                DrawRibbon(context, nodes);
                context.Pop();
            }
        }

        /// <summary>
        /// 绘制一条多层叠加的发光缎带。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="nodes"></param>
        private void DrawRibbon(DrawingContext context, IReadOnlyList<RibbonNode> nodes)
        {
            if (nodes.Count < 2)
                return;

            //构建一条连续的光滑路径
            var geometry = new StreamGeometry();
            using (var path = geometry.Open())
            {
                path.BeginFigure(nodes[0].Position, false, false);
                for (var i = 0; i < nodes.Count; i++)
                {
                    var end = i == nodes.Count - 1 ? nodes[i].Position : Midpoint(nodes[i].Position, nodes[i + 1].Position);
                    path.QuadraticBezierTo(nodes[i].Position, end, true, true);
                }
            }
            geometry.Freeze();

            //确定线性渐变的方向（起点 -> 终点）
            var start = nodes[0].Position;
            var finish = nodes[nodes.Count - 1].Position;
            if ((finish - start).LengthSquared < 0.000001)
            {
                start = geometry.Bounds.TopLeft;
                finish = geometry.Bounds.BottomRight;
            }
            if ((finish - start).LengthSquared < 0.000001) 
                return;

            var fade = Math.Clamp(1 - nodes[nodes.Count - 1].Age / Lifetime, 0, 1);

            //逐层绘制
            for (var layer = 0; layer < LayerWidths.Length; layer++)
            {
                var brush = new LinearGradientBrush
                {
                    StartPoint = start,
                    EndPoint = finish,
                    MappingMode = BrushMappingMode.Absolute,
                    Opacity = fade * fade * LayerOpacities[layer]
                };
                for (var i = 0; i < colors.Length; i++)
                {
                    var color = layer == 3 ? Blend(colors[i], Colors.White, 0.8) : colors[i];
                    brush.GradientStops.Add(new GradientStop(color, i * 0.5));
                }
                brush.Freeze();
                var pen = new Pen(brush, 12 * Math.Sqrt(fade) * LayerWidths[layer])
                {
                    StartLineCap = PenLineCap.Flat,
                    EndLineCap = PenLineCap.Flat,
                    LineJoin = PenLineJoin.Round
                };
                pen.Freeze();
                context.DrawGeometry(null, pen, geometry);
            }
        }

        /// <summary>
        /// RGB 线性插值混色（颜色线性过渡）
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private Color Blend(Color first, Color second, double amount)
        {
            return Color.FromRgb((byte)Math.Round(first.R + (second.R - first.R) * amount),
                (byte)Math.Round(first.G + (second.G - first.G) * amount),
                (byte)Math.Round(first.B + (second.B - first.B) * amount));
        }

        /// <summary>
        /// 计算两点之间的几何中点
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private Point Midpoint(Point first, Point second) => first + (second - first) * 0.5;
    }

    internal struct RibbonNode
    {
        public Point Position;
        public double Age;
    }
}
