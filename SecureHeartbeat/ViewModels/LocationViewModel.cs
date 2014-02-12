using System;
using System.IO.IsolatedStorage;
using System.Windows;
using SecureHeartbeat.Core.Impl;
using SecureHeartbeat.Maps;
using SecureHeartbeat.Models;
using SecureHeartbeat.Resources;
using System.Windows.Input;
using SecureHeartbeat.Commands;


namespace SecureHeartbeat.ViewModels
{
    /// <summary>
    /// Location View Model
    /// </summary>
    public class LocationViewModel : ViewModel
    {
        private IMap _map;
        private LocationModel _location;
        private Boolean _isLoaded = false;
        private ICommand _locateCommand;
        public ICommand LocateCommand
        {
            get { return _locateCommand; }
            set
            {
                if (value != _locateCommand)
                {
                    _locateCommand = value;
                    OnPropertyChanged("LocateCommand");
                }
            }
        }

        public LocationViewModel(IMap map)
        {
            //this.Items = new ObservableCollection<LocationModel>();
            //_location = new LocationModel();
            //LoadData();

            _location = new LocationModel() { Latitude = 51.51369, Longitude = -0.088137, PostalCode = "EC2R 8AH" };
            LocateCommand = new LocateCommand(_map, _location);

            this._map = map;
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        //public ObservableCollection<LocationModel> Items { get; private set; }


        public LocationModel Location
        {
            get { return _location; }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    OnPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public override void NavigatedTo()
        {
            base.NavigatedTo();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                // User has opted out of Location
                if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] == false)
                    return;
            }
            else
            {
                // TODO Provide a way of doing this within MVVM
                MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

                var isOK = (result == MessageBoxResult.OK);
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = isOK;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }

            _map.SetLocation(Location.Latitude, Location.Longitude);
        }
    }
}
