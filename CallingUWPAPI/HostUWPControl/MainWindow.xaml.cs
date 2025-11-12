using Microsoft.Toolkit.Wpf.UI.XamlHost;
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
using Windows.UI.Popups;

namespace HostUWPControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateUWPControlsFirst();
        }

        private void CreateUWPControlsFirst()
        {
            //初始化UWP容器环境
            Windows.UI.Xaml.Hosting.WindowsXamlManager.InitializeForCurrentThread();

            //创建一个UWP按钮
            Windows.UI.Xaml.Controls.Button myButton = new Windows.UI.Xaml.Controls.Button();
            myButton.Name = "btn_Message";
            myButton.Width = 188;
            myButton.Height = 48;
            myButton.TabIndex = 0;
            myButton.Content = "弹出UWP消息框";
            myButton.Click += MessageButton_Click;
            Microsoft.Toolkit.Wpf.UI.XamlHost.WindowsXamlHost myHostControl = new Microsoft.Toolkit.Wpf.UI.XamlHost.WindowsXamlHost();
            myHostControl.Name = "myWindowsXamlHostControl";
            myHostControl.Child = myButton;
            myHostControl.HorizontalAlignment = HorizontalAlignment.Center;
            myHostControl.VerticalAlignment = VerticalAlignment.Center;
            this.grid.Children.Add(myHostControl);
        }

        private async void MessageButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("这是一个示例的消息框");
            messageDialog.Commands.Add(new UICommand(
                "确定",
                new UICommandInvokedHandler(this.Confirm)));
            messageDialog.Commands.Add(new UICommand(
                "取消",
                new UICommandInvokedHandler(this.Cancel)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private void Confirm(IUICommand command)
        {
            
        }

        private void Cancel(IUICommand command)
        {

        } 
    }
}
