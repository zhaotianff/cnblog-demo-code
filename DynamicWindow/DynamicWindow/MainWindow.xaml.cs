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

namespace DynamicWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitCommand();
        }

        private void InitCommand()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (a, b) => SystemCommands.CloseWindow(this), (a, b) => b.CanExecute = true));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (a,b)=> SystemCommands.MinimizeWindow(this), (a, b) => b.CanExecute = true));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, (a, b) => SystemCommands.MaximizeWindow(this), (a, b) => b.CanExecute = true));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (a, b) => SystemCommands.RestoreWindow(this), (a, b) => b.CanExecute = true));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, (a, b) => SystemCommands.MinimizeWindow(this), (a, b) => b.CanExecute = true));
            CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, ShowSystemMenu));
        }


        private void ShowSystemMenu(object sender, ExecutedRoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null)
                return;

            var point = new Point(0, 0);
            if (WindowState == WindowState.Maximized)
            {
                point = new Point(0, element.ActualHeight);
            }
            else
            {
                point = new Point(Left + BorderThickness.Left, element.ActualHeight + Top + BorderThickness.Top);
            }
            point = element.TransformToAncestor(this).Transform(point);
            SystemCommands.ShowSystemMenu(this, point);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mediaelement.Source = new Uri("ab.mp4",UriKind.Relative);
            mediaelement.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mediaelement.Stop();
            mediaelement.Source = null;
        }
    }
}
