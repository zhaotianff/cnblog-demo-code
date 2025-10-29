using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDataGridEditDemo.Command;
using WPFDataGridEditDemo.ViewModels;
using WPFDataGridEditDemo.Views;

namespace WPFDataGridEditDemo.Model
{
    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));//全局通知(给监听此属性的控件)
        }

        private int id;
        private string name;

        public int ID 
        {
            get => id; 
            set
            {
                id = value;
                NotifyChanged();
            }
        }

        public string Name
        { 
            get => name;
            set
            {
                name = value;
                NotifyChanged();
            }
        }

        private CommandBase editCommand;

        public CommandBase EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new CommandBase();
                    editCommand.DoExecute = new Action<object>(obj => {
                        var connectData = obj as Student;
                        DataEditView dataEditView = new DataEditView();
                        DataEditViewModel dataEditViewModel = new DataEditViewModel();
                        dataEditViewModel.Num = connectData.ID;
                        dataEditViewModel.Name = connectData.Name;
                        dataEditView.DataContext = dataEditViewModel;

                        if(dataEditView.ShowDialog() == true)
                        {
                            connectData.ID = dataEditViewModel.Num;
                            connectData.Name = dataEditViewModel.Name;
                        }
                    });
                }
                return editCommand;
            }

        }
    }
}
