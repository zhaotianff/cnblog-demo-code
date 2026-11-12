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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace WindowRenderingHandle
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly DrawingVisual visual = new DrawingVisual();

        public const int MaxNodes = 256;
        public const double Lifetime = 0.5;
        public int FixedCount = 1;
        private float trailHeadOpacity = 0.69f;
        private float trailTailOpacity = 0f;
        private int trailSize = 18;

        private readonly List<TrailNode> nodes = new List<TrailNode>(MaxNodes);
        private Point? lastPoint;

        public Window1()
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
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                //移除0.5秒后的点
                if (node.Age >= Lifetime)
                    nodes.RemoveAt(i);
                else
                    nodes[i] = node;
            }

            if (!lastPoint.HasValue || cursor != lastPoint.Value)
            {
                //只保留256个点
                if (nodes.Count == MaxNodes)
                    nodes.RemoveAt(0);

                nodes.Add(new TrailNode { Position = cursor });
                lastPoint = cursor;
            }

            using (var context = visual.RenderOpen())
            {
                DrawTrail(context, nodes);
            }
        }

        private void DrawTrail(DrawingContext context, IReadOnlyList<TrailNode> nodes)
        {
            if (nodes.Count == 0)
                return;

            // 拖尾主色
            var color = (Color)ColorConverter.ConvertFromString("#BA55D3");
            var fade = Math.Clamp(1 - nodes[nodes.Count - 1].Age / Lifetime, 0, 1);
            var distances = new double[nodes.Count];
            for (var i = 1; i < nodes.Count; i++)
            {
                distances[i] = distances[i - 1] + (nodes[i].Position - nodes[i - 1].Position).Length;
            }

            // 拖尾总长度（从尾到头）
            var totalLength = distances[nodes.Count - 1];

            if (totalLength == 0)
            {
                var brush = new SolidColorBrush(WithOpacity(color, trailHeadOpacity * fade));
                context.DrawEllipse(brush, null, nodes[0].Position, trailSize / 2, trailSize / 2);
                return;
            }

            // 逐段绘制。每一段负责节点 i 与前/后节点的中点之间的一小段贝塞尔曲线。
            for (var i = 0; i < nodes.Count; i++)
            {
                var point = nodes[i].Position;
                var start = i == 0 ? point : Midpoint(nodes[i - 1].Position, point);
                var end = i == nodes.Count - 1 ? point : Midpoint(point, nodes[i + 1].Position);
                var startProgress = i == 0 ? 0 : (distances[i - 1] + distances[i]) / (2 * totalLength);
                var endProgress = i == nodes.Count - 1 ? 1 : (distances[i] + distances[i + 1]) / (2 * totalLength);
                var startOpacity = trailTailOpacity + (trailHeadOpacity - trailTailOpacity) * startProgress;
                var endOpacity = trailTailOpacity + (trailHeadOpacity - trailTailOpacity) * endProgress;
                Brush brush;
                if ((end - start).LengthSquared < 0.000001)
                {
                    brush = new SolidColorBrush(WithOpacity(color, (startOpacity + endOpacity) * 0.5 * fade));
                }
                else
                {
                    brush = new LinearGradientBrush(WithOpacity(color, startOpacity * fade), WithOpacity(color, endOpacity * fade), start, end)
                    {
                        MappingMode = BrushMappingMode.Absolute
                    };
                }
                brush.Freeze();
                var pen = new Pen(brush, trailSize)
                {
                    StartLineCap = i == 0 ? PenLineCap.Round : PenLineCap.Flat,
                    EndLineCap = i == nodes.Count - 1 ? PenLineCap.Round : PenLineCap.Flat,
                    LineJoin = PenLineJoin.Round
                };
                pen.Freeze();

                // 用 StreamGeometry 绘制二次贝塞尔（曲线自然经过每段端点、并向节点"靠近"，形成贴合轨迹的光滑曲线）
                var geometry = new StreamGeometry();
                using (var path = geometry.Open())
                {
                    path.BeginFigure(start, false, false);
                    path.QuadraticBezierTo(point, end, true, true);
                }
                geometry.Freeze();
                context.DrawGeometry(null, pen, geometry);
            }
        }

        private Point Midpoint(Point first, Point second)
        {
            return first + (second - first) * 0.5;
        }

        private Color WithOpacity(Color color, double opacity)
        {
            return Color.FromArgb((byte)Math.Round(Math.Clamp(opacity, 0, 1) * 255), color.R, color.G, color.B);
        }
    }

    internal struct TrailNode
    {
        public Point Position;
        public double Age;
    }
}
