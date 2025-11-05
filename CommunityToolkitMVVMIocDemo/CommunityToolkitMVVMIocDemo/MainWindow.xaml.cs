using CommunityToolkitMVVMIocDemo.Services;
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

namespace CommunityToolkitMVVMIocDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculatorService calculatorService = new CalculatorService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //忽略校验
            int.TryParse(this.tbox_num1.Text, out int num1);
            int.TryParse(this.tbox_num2.Text, out int num2);

            MessageBox.Show(calculatorService.Add(num1, num2).ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //忽略校验
            int.TryParse(this.tbox_num1.Text, out int num1);
            int.TryParse(this.tbox_num2.Text, out int num2);

            MessageBox.Show(calculatorService.Sub(num1, num2).ToString());
        }
    }
}
