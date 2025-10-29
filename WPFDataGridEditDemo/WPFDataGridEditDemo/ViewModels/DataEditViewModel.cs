using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDataGridEditDemo.Command;

namespace WPFDataGridEditDemo.ViewModels
{
    public class DataEditViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private int id;
        private string name;

        public int Num
        {
            get => id;
            set
            {
                id = value;
                RaiseChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaiseChanged();
            }
        }

        private CommandBase saveCommand;
        public CommandBase SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandBase();
                    saveCommand.DoExecute = new Action<object>(obj => {
                        var window = obj as Window;

                        if (window != null)
                            window.DialogResult = true;
                    });
                }

                return saveCommand;
            }
        }
    }
}
