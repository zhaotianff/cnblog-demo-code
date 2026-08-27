using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp20
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Model> models = new ObservableCollection<Model>();

        public static int RenderTier = RenderCapability.Tier >> 16;

        public MainWindow()
        {
            InitializeComponent();


            models.Add(new Model() {Content = "image 1",Image = "1.jpg" });
            models.Add(new Model() { Content = "image 2", Image = "2.jpg" });
            models.Add(new Model() { Content = "image 3", Image = "3.jpg" });

            this.list.ItemsSource = models;
            this.list.ItemTemplateSelector = new MyDataTemplateSelector();

            //冻结对象
            if(ellipiseGeometry.CanFreeze)
            {
                ellipiseGeometry.Freeze();
            }

            //尝试修改会报错
            //ellipiseGeometry.RadiusX = 50;

            //判断对象是否冻结
            if(ellipiseGeometry.IsFrozen)
            {
                //如果我们想要修改，可以Clone一个对象
                var newEllipseGeometry = ellipiseGeometry.Clone();
                newEllipseGeometry.RadiusX = 50;
                newEllipseGeometry.Freeze();
                path.Data = newEllipseGeometry;
            }
            else
            {
                ellipiseGeometry.RadiusX = 50;
            }
        }
    }

    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            switch (MainWindow.RenderTier)
            {
                case 0:
                case 1:
                    return Application.Current.MainWindow.FindResource("SimpleTemplate") as DataTemplate;
                case 2:
                    return Application.Current.MainWindow.FindResource("FullTemplate") as DataTemplate;
                    //测试效果
                    //return Application.Current.MainWindow.FindResource("SimpleTemplate") as DataTemplate;
            }

            return null;
        }
    }
    
}
