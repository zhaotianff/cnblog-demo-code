using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTabMVVMDemo
{
    public class TabViewModel : ObservableObject
    {
        private string headerName;

        public string HeaderName 
        { 
            get => headerName; 
            set => SetProperty(ref headerName,value); 
        }
    }
}
