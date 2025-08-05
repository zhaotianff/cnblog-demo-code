using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTabMVVMDemo
{
    public class MainWindowViewModel : ObservableObject
    {
        private ObservableCollection<TabViewModel> tabList = new ObservableCollection<TabViewModel>();

        public ObservableCollection<TabViewModel> TabList { get => tabList; set => SetProperty(ref tabList,value); }

        public MainWindowViewModel()
        {
            tabList.Add(new TabToolAViewModel() {Detail = "详情...." });
            tabList.Add(new TabToolBViewModel() { ImagePath = "https://img2.baidu.com/it/u=3115165460,1153722234&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=750" });
        }

    }
}
