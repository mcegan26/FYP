using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;
using Coding4Fun.Toolkit.Audio.Helpers;
using Microsoft.Phone.Controls;
using Parse;
using SHClassLibrary;

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
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.Statusvm.IsDataLoaded)
            {
                App.Statusvm.LoadData();
            }
            App.Statusvm.NavigatedTo();
        }

    }
}