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
    public partial class StatusPage : PhoneApplicationPage
    {
        public StatusPage()
        {
            InitializeComponent();// Set the data context of the LongListSelector control to the sample data
            DataContext = App.Statusvm;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.Statusvm.IsDataLoaded)
            {
                App.Statusvm.LoadData();
            }
        }

    }
}