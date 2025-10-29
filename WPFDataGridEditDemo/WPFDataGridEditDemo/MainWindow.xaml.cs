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
using WPFDataGridEditDemo.ViewModels;

namespace WPFDataGridEditDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var mainviewmodel = new MainWindowViewModel();

            mainviewmodel.StudentCollection.Add(new Model.Student() { ID = 1, Name = "标签111111" });
            mainviewmodel.StudentCollection.Add(new Model.Student() { ID = 2, Name = "标签222222" });


            this.DataContext = mainviewmodel;
        }
    }
}
