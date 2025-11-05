using CommunityToolkitMVVMIocDemo_MVVM.Services;
using CommunityToolkitMVVMIocDemo_MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityToolkitMVVMIocDemo_MVVM
{
    public sealed partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        public new static App Current => (App)Application.Current;


        public IServiceProvider Services { get; private set; }


        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            /*
             * AddTransient： 每次service请求都是获得不同的实例.
             * AddScoped： 对于同一个请求返回同一个实例，不同的请求返回不同的实例.
             * AddSingleton： 每次都是获得同一个实例， 单一实例模式.
             */

            services.AddScoped<CalculatorService>();
            services.AddTransient<MainWindowViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
