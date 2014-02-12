using System;
using System.Device.Location;
using System.Globalization;
using System.Windows;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Maps.Controls;
using SecureHeartbeat.Core;
using SecureHeartbeat.Core.Interfaces;
using SecureHeartbeat.Maps;
using SecureHeartbeat.ViewModels;

namespace SecureHeartbeat
{
    public partial class LocationPage : PhoneApplicationPage
    {
        /// <summary>
        /// Stores a reference to the view model associated with this application page.
        /// </summary>
        private readonly IViewModel _viewModel;

        public LocationPage()
        {
            InitializeComponent();// Set the data context of the LongListSelector control to the sample data4

            var viewModel = new LocationViewModel(new BingMapAdapter(SHBMap));
        
            _viewModel = viewModel;
            DataContext = viewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           _viewModel.NavigatedTo();


            Map rawr = SHBMap;
        }
    }
}