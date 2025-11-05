using CommunityToolkitMVVMIocDemo_MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

namespace CommunityToolkitMVVMIocDemo_MVVM
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //已通过ServiceLocator设置ViewModel
            //如果没有ServiceLocator，可以通过下面的方式设置ViewModel
            //this.DataContext = App.Current.Services.GetService<MainWindowViewModel>();
        }
    }
}
