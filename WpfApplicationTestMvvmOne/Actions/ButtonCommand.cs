using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplicationTestMvvmOne.ViewModels;

namespace WpfApplicationTestMvvmOne.Actions
{
    public class ButtonCommand : ICommand
    {
        private Action WhatToExecute;
        private Func<bool> WhenToExecute;

        //private CustomerViewModel obj;
        public ButtonCommand(Action What, Func<bool> When)
        {
            WhatToExecute = What;
            WhenToExecute = When;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return WhenToExecute();
        }

        public void Execute(object parameter)
        {
            WhatToExecute();
        }
    }
}
