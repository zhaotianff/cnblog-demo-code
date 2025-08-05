using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTabMVVMDemo
{
    public class TabToolAViewModel : TabViewModel
    {
        private string detail;
        public string Detail { get => detail; set => SetProperty(ref detail,value); }

        public TabToolAViewModel()
        {
            HeaderName = "工具A";
        }

    }
}
