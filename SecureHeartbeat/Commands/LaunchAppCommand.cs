using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Parse;
using SecureHeartbeat.Models;
using SHClassLibrary;

namespace SecureHeartbeat.Commands
{
    public class LaunchAppCommand : ICommand
    {

        public LaunchAppCommand()
        {
        }

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
                phoneApplicationFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            
        }
    }
}
