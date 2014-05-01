using System;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Parse;
using SecureHeartbeat.Maps;
using SecureHeartbeat.Models;
using SHClassLibrary;


namespace SecureHeartbeat.Commands
{
    public class LocateCommand : ICommand 
    {
        private IMap _map;
        private LocationModel _locationModel;

        public LocateCommand(IMap map, LocationModel locationModel)
        {
            _map = map;
            _locationModel = locationModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public async void Execute(object parameter)
        {
            // The user has opted out of Location posistioning
            if (!(bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"])
            {
                return;
            }

            var geolocator = new Geolocator
            {
                DesiredAccuracyInMeters = 10
            };

            var geoposition = await geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromSeconds(25),
                timeout: TimeSpan.FromSeconds(16)
                );

            try
            {
                if (geoposition.Coordinate.Latitude == null)
                {
                    _locationModel.Latitude = 0;
                }
                else
                {
                    _locationModel.Latitude = geoposition.Coordinate.Latitude;
                }
                
                if (geoposition.Coordinate.Longitude == null)
                {
                    _locationModel.Longitude = 0;
                }
                else
                {
                    _locationModel.Longitude = geoposition.Coordinate.Longitude;
                }
                
                if (geoposition.CivicAddress.PostalCode == null)
                {
                    _locationModel.PostalCode = "N/A";
                }
                else
                {
                    _locationModel.PostalCode = geoposition.CivicAddress.PostalCode;
                }
                
            }
            catch (Exception)
            {
                // Common for Civic address to be unable obtainable so need to catch a null
                // pointer exception to stop app from crashing but issue handled above for actual exception
            }



            _map.SetLocation(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);

            BackgroundParseCalls.DeviceInsideFortress(geoposition);

        }
    }
}
