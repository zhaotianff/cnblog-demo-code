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

namespace ControlInternalDemo
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

            var list = new List<dynamic>()
            {
                new { Id = 1,Name = "张三" },
                new { Id = 2,Name = "李四"},
                new { Id = 3,Name = "王五"}
            };

            this.listbox.ItemsSource = list;
        }

        private void WritePoint(string str)
        {
            if (lengthDic.ContainsKey(lineMouseRow) && lengthDic[lineMouseRow] > str.Length)
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            lineMouseRow = 0;

            //获取控件模板
            var children = TreeHelpers.GetVisualChildren(this.button);

            //层级结构如下：
            /*
             *   System.Windows.Controls.Button: 按钮1
                     System.Windows.Controls.Border
                          System.Windows.Controls.Grid
                               System.Windows.Controls.ContentPresenter
                                    System.Windows.Controls.TextBlock
            */

            WritePoint("控件层级结构");
            WritePoint("System.Windows.Controls.Button: 按钮1");
            WritePoint("    System.Windows.Controls.Border");
            WritePoint("        System.Windows.Controls.Grid");
            WritePoint("            System.Windows.Controls.ContentPresenter");
            WritePoint("                System.Windows.Controls.TextBlock");

            Border border = children.ElementAt(1) as Border;
            TextBlock tb = children.ElementAt(4) as TextBlock;

            var point = tb.TranslatePoint(new Point(0, 0), border);
            WritePoint($"TextBlock相对于Border的位置:{point}");

            var pos2 = tb.TransformToVisual(this).Transform(new Point(0, 0));
            WritePoint($"TextBlock相对于Window的位置:{pos2}");
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var element = this.InputHitTest(new Point(e.GetPosition(this).X, e.GetPosition(this).Y)) as UIElement;

            this.lbl_Display.Content = element.ToString();
        }
    }
}
