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

namespace WPF_FluentDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 设置整个应用程序的主题模式
#pragma warning disable WPF0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            Application.Current.ThemeMode = ThemeMode.Light;
#pragma warning restore WPF0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。

            // 设置当前窗口的主题模式
#pragma warning disable WPF0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            ThemeMode = ThemeMode.Light;
#pragma warning restore WPF0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
        }
    }
}