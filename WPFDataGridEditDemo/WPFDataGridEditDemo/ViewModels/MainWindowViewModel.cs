using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDataGridEditDemo.Command;
using WPFDataGridEditDemo.Model;

namespace WPFDataGridEditDemo.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Student> studentCollection = new ObservableCollection<Student>();

        public ObservableCollection<Student> StudentCollection
        {
            get => this.studentCollection;
            set
            {
                studentCollection = value;
                this.RaiseChanged();
            }
        }
    }
}
