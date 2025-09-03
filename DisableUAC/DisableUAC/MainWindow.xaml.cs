using Microsoft.Win32;
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

namespace DisableUAC
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //说明
            //运行程序需要管理员权限 
            //添加文件->应用程序清单文件->把<requestedExecutionLevel  level="asInvoker" uiAccess="false" />里的asInvoker修改为requireAdministrator

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (DisableUAC() == true)
            {
                if (MessageBox.Show("操作成功，是否重启电脑", "提示信息", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Reboot();
                }
            }
        }

        private bool DisableUAC()
        {
            try
            {
                //可以判断当前是开启还是关闭状态
                //这里默认为是已开启状态
                string path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
                string uac = "EnableLUA";
                RegistryKey key = Registry.LocalMachine.CreateSubKey(path);
                if (key != null)
                {
                    key.SetValue(uac, 0, RegistryValueKind.DWord);
                    key.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 可以同时调用这个函数，在关闭UAC后，系统不会在通知里显示提示
        /// </summary>
        private void SetNoWindowsNotify()
        {
            string path = @"SOFTWARE\Microsoft\Security Center";
            string notify = "UACDisableNotify";
            RegistryKey key = Registry.LocalMachine.CreateSubKey(path);
            if (key != null)
            {
                key.SetValue(notify, 1, RegistryValueKind.DWord);
                key.Close();
            }
        }

        private void Reboot()
        {
            System.Diagnostics.Process.Start("shutdown", " -r -t 0");
        }
    }
}
