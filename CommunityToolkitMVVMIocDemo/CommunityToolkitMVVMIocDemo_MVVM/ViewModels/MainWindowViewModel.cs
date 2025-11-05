using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkitMVVMIocDemo_MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityToolkitMVVMIocDemo_MVVM.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private CalculatorService calculatorService;

        private int num1;
        private int num2;

        public int Num1 { get => num1; set => SetProperty(ref num1, value); }
        public int Num2 { get => num2; set => SetProperty(ref num2, value); }

        public RelayCommand AddCommand { get; private set; }

        public RelayCommand SubCommand { get; private set; }

        
        public MainWindowViewModel(CalculatorService calculatorService)
        {
            AddCommand = new RelayCommand(Add);
            SubCommand = new RelayCommand(Sub);
            this.calculatorService = calculatorService;
        }


        private void Add()
        {
            MessageBox.Show(calculatorService.Add(Num1, Num2).ToString());
        }

        private void Sub()
        {
            MessageBox.Show(calculatorService.Sub(Num1, Num2).ToString());
        }

    }
}
