using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace SecureHeartbeat.Commands
{
    class ReturnToLogin : ICommand
    {

        public ReturnToLogin() {}

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
            {
                phoneApplicationFrame.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            }
        }

    }
}
