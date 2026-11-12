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

namespace WPFRenderingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TianXiaTech.BlurWindow
    {
        RingDrawWindow ringDrawWindow;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ringDrawWindow = new RingDrawWindow();
            ringDrawWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ringDrawWindow?.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ringDrawWindow?.Close();
        }
    }
}