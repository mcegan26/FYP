using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SecureHeartbeat
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();// Set the data context of the LongListSelector control to the sample data
            DataContext = App.Loginvm;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.Loginvm.IsDataLoaded)
            {
                App.Loginvm.LoadData();
            }
        }

        //private void UpdateSource(object sender, RoutedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}