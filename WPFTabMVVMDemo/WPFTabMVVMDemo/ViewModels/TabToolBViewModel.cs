using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTabMVVMDemo
{
    public class TabToolBViewModel : TabViewModel
    {
        private string imagePath;
        public string ImagePath { get => imagePath; set => SetProperty(ref imagePath,value); }

        public TabToolBViewModel()
        {
            HeaderName = "工具B";
        }

    }
}
