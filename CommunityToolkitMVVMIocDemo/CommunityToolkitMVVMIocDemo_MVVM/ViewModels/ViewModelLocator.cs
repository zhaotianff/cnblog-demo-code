using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityToolkitMVVMIocDemo_MVVM.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel Main
        {
            get
            {
                return  App.Current.Services.GetService<MainWindowViewModel>();

                //or
                //return Ioc.Default.GetService<MainWindowViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
