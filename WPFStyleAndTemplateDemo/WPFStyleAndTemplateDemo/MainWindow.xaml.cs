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

namespace WPFStyleAndTemplateDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("HelloWorld");
        }

        /// <summary>
        /// ToolTip显示时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whenToolTipOpens(object sender, ToolTipEventArgs e)
        {

        }

        /// <summary>
        /// ToolTip关闭时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whenToolTipCloses(object sender, ToolTipEventArgs e)
        {

        }
    }
}