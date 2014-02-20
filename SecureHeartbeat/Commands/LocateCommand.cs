using System;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using SecureHeartbeat.Maps;
using SecureHeartbeat.Models;

namespace SecureHeartbeat.Commands
{
    public class LocateCommand : ICommand 
    {
        private readonly IMap _map;
        private readonly LocationModel _locationModel;

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
            if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            {
                return;
            }

            var geolocator = new Geolocator
            {
                DesiredAccuracyInMeters = 20
            };

            var geoposition = await geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(5),
                timeout: TimeSpan.FromSeconds(20)
                );

            try
            {
                _locationModel.Latitude = geoposition.Coordinate.Latitude;
                _locationModel.Longitude = geoposition.Coordinate.Longitude;
                _locationModel.PostalCode = geoposition.CivicAddress.PostalCode;
            }
            catch (Exception)
            {
                _locationModel.PostalCode = "N/A";
                //throw new TargetInvocationException("Can't find postcode", new Exception());
            }

            // TODO Inject the IMap reference within my ICommand implementation constructor
            _map.SetLocation(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
            //Point currentViewPoint = SHBMap.ConvertGeoCoordinateToViewportPoint(new GeoCoordinate(51.51369, -0.088137));
 
        }
    }
}
