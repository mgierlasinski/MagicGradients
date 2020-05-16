using System;
using System.Windows.Input;

namespace GradientParser.Commands
{
    public class RunActionCommand : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _action != null;

        public void Execute(object parameter) => _action.Invoke();

        public RunActionCommand(Action action)
        {
            _action = action;
        }
    }
}
