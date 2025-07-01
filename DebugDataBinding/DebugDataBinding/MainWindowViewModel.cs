using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugDataBinding
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string myProperty;

        public string MyProperty 
        { 
            get => myProperty; 
            set
            {
                myProperty = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyProperty"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;  
    }
}
