using System;
using System.Windows.Input;

namespace Takeway.Mobile.Core.Utilities
{
    public class SimpleCommand : ICommand
    {
        readonly Action _action;
        public event EventHandler CanExecuteChanged;


        public SimpleCommand(Action action)
        {
            this._action = action;
        }
        public bool CanExecute(object parameter)
        {
            return (true);
        }
        public void Execute(object parameter)
        {
            if (this._action != null)
            {
                this._action();
            }
        }


    }
}