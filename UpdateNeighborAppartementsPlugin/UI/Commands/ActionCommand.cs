using System;
using System.Linq;
using System.Windows.Input;

namespace UpdateNeighborAppartementsPlugin.UI.Commands
{
    public class ActionCommand : ICommand
    {
        private Action executeAction;

        public ActionCommand(Action executeAction)
        {
            this.executeAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(executeAction != null)
            {
                executeAction();
            }
        }
    }
}
