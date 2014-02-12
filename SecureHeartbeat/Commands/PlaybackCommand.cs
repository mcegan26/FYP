using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecureHeartbeat.Commands
{
    public class PlaybackCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        private Boolean buttonState = true;

        public bool CanExecute(object parameter)
        {
            if (buttonState)
                buttonState = false;
            else 
                buttonState = true;

            return buttonState;

        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Play!");
        }


    }
}
