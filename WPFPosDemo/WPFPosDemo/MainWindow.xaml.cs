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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPosDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int lineMouseRow = Console.CursorTop;
        private Dictionary<int, int> lengthDic = new Dictionary<int, int>();
        int fixedInfoRowCount = 5;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            lineMouseRow = fixedInfoRowCount;
            OutputPosInfo(e);
        }

        private void OutputPosInfo(MouseEventArgs e)
        {
            //鼠标位置相对于Image的位置
            var pos1 = e.GetPosition(this.image);
            WritePoint($"鼠标位置相对于Image的位置:{pos1}");

            //鼠标位置相对于Window的位置
            var pos2 = e.GetPosition(this.window);
            WritePoint($"鼠标位置相对于Window的位置:{pos2}");

            //也可以使用Mouse.GetPosition
            //var pos3 = Mouse.GetPosition(this.image);

            //判断鼠标位置是否在Image范围内
            //方法1、借助前面的鼠标位置相对于Image的位置功能来判断
            if(pos1.X >= 0 && pos1.Y >=0 && pos1.X <= this.image.Width && pos1.Y <= this.image.Height)
            {
                WritePoint("当前鼠标在Image范围内");
            }
            else
            {
                WritePoint("当前鼠标不在Image范围内");
            }

            //方法2、使用PointFromScreen方法
            //首先获取鼠标位置(相对于屏幕)
            POINT pp = new POINT();
            User32.GetCursorPos(ref pp);
            var mousePos = new Point(pp.x, pp.y);
            var imageRelativePos = this.image.PointFromScreen(mousePos);

            if (imageRelativePos.X >= 0 && imageRelativePos.Y >= 0 && imageRelativePos.X <= this.image.Width && imageRelativePos.Y <= this.image.Height)
            {
                WritePoint("当前鼠标在Image范围内");
            }
            else
            {
                WritePoint("当前鼠标不在Image范围内");
            }

            //鼠标位置相对于屏幕的位置
            var mousePos2 = this.PointToScreen(pos2);
            WritePoint($"鼠标位置相对于屏幕位置:{mousePos2}");
        }

        private void WriteFixedPosInfo()
        {
            //Image在Canvas中的位置
            var pos1 = new Point(Canvas.GetLeft(this.image), Canvas.GetTop(this.image));
            WritePoint($"Image在Canvas中的位置:{pos1}");

            //Image在Grid中的位置
            var transform1 = this.image.TransformToAncestor(this.grid);
            var pos2 = transform1.Transform(new Point(0, 0));

            //可以合并上面的两步如下：
            var pos2t = this.image.TranslatePoint(new Point(0, 0), this.grid);
            WritePoint($"Image在Grid中的位置:{pos2}");

            //Image在Window中的位置
            var transform2 = this.image.TransformToAncestor(this.window);
            var pos3 = transform2.Transform(new Point(0, 0));
            WritePoint($"Image在Window中的位置:{pos3}");

            //Grid相对于Image的位置
            var transform3 = this.grid.TransformToDescendant(this.image);
            var pos4 = transform3.Transform(new Point(0, 0));
            WritePoint($"Grid相对于Image的位置:{pos4}");

            //Rect相对于Image的位置
            var transform4 = this.rect.TransformToVisual(this.image);
            var pos5 = transform4.Transform(new Point(0, 0));
            WritePoint($"Rect相对于Image的位置:{pos5}");
        }

        private void WritePoint(string str)
        {
            if(lengthDic.ContainsKey(lineMouseRow) && lengthDic[lineMouseRow] > str.Length)
            {
                Console.SetCursorPosition(0, lineMouseRow);
                Console.Write(new char[lengthDic[lineMouseRow] * 2]);
            }
            
            Console.SetCursorPosition(0, lineMouseRow);
            Console.Write(str);

            if (lengthDic.ContainsKey(lineMouseRow) == false)
            {
                lengthDic[lineMouseRow] = str.Length;
            }
            else
            {
                if (lengthDic[lineMouseRow] < str.Length)
                {
                    lengthDic[lineMouseRow] = str.Length;
                }
            }

            lineMouseRow++;
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            lineMouseRow = 0;
            WriteFixedPosInfo();
        }
    }
}
