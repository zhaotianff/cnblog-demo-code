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

namespace HostUWPControl_XAML
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyCalendar_ChildChanged(object sender, EventArgs e)
        {
            WindowsXamlHost windowsXamlHost = (WindowsXamlHost)sender;
            var calendarView = (Windows.UI.Xaml.Controls.CalendarView)windowsXamlHost.Child;
            if (calendarView != null)
            {
                calendarView.SelectedDatesChanged += (obj, args) =>
                {
                    if (args.AddedDates.Count > 0)
                    {
                        MessageBox.Show("当前选择的日期是： " + args.AddedDates[0].DateTime.ToString());
                    }
                };
            }
        }
    }
}
