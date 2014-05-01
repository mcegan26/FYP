using System;
using System.IO.IsolatedStorage;
using System.Windows;
using SecureHeartbeat.Core.Impl;
using SecureHeartbeat.Maps;
using SecureHeartbeat.Models;
using SecureHeartbeat.Resources;
using System.Windows.Input;
using SecureHeartbeat.Commands;
using SHClassLibrary;


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
                    _locateCommand = new LocateCommand(_map, _location);
                    OnPropertyChanged("LocateCommand");
                }
            }
        }

        public IMap Map
        {
            get { return _map; }
        }

        public LocationViewModel(IMap map)
        {
            //_location = new LocationModel() { Latitude = 51.51369, Longitude = -0.088137, PostalCode = "EC2R 8AH" };
            _location = new LocationModel() { Latitude = 0, Longitude = 0, PostalCode = "Unknown" };
            _map = map;
            _locateCommand = new LocateCommand(Map, _location);
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
            Locater.AllowDeviceLocation();
            DeviceStorage.CheckNeedToSaveRecording();
            //_map.SetLocation(Location.Latitude, Location.Longitude);
        }
    }
}
