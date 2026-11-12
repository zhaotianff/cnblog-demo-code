using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowRenderingHandle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window1 window1 = new Window1();
        private Window2 window2 = new Window2();

        public MainWindow()
        {
            InitializeComponent();

            window1.Closing += (sender, args) => { RenderingManager.RemoveSubscription(window1.Update); };
            window2.Closing += (sender, args) => { RenderingManager.RemoveSubscription(window2.Update); };
            window1.Show();
            window2.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //增加窗口1的渲染订阅
            RenderingManager.AddSubscription(window1.Update);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //移除窗口1的渲染订阅
            RenderingManager.RemoveSubscription(window1.Update);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //增加窗口2的渲染订阅
            RenderingManager.AddSubscription(window2.Update);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //移除窗口2的渲染订阅
            RenderingManager.RemoveSubscription(window2.Update);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //主窗口关闭时停止渲染事件处理
            RenderingManager.StopCompositionTargetRendering();
        }
    }
}