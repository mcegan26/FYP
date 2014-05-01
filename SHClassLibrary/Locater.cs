using System;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using Coding4Fun.Toolkit.Controls.Common;
using Microsoft.Phone.Maps.Controls;

namespace SHClassLibrary
{
    public class Locater
    {
        public static async Task<Geoposition> GetDeviceLoc()
        {
            AllowDeviceLocation();

            var geolocator = new Geolocator
            {
                DesiredAccuracyInMeters = 10
            };

            try
            {
                var geoposition = await geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromSeconds(80),
                timeout: TimeSpan.FromSeconds(50)
                );

                return geoposition;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool UserInGeoFence(LocationRectangle geoFence, Geoposition userLoc)
        {
            double userLat = userLoc.Coordinate.Latitude;
            double userLong = userLoc.Coordinate.Longitude;
            double nwCornerLat = geoFence.Northwest.Latitude;
            double seCornerLat = geoFence.Southeast.Latitude;
            double nwCornerLong = geoFence.Northwest.Longitude;
            double seCornerLong = geoFence.Southeast.Longitude;
            bool insideLat = false;
            bool insideLong = false;


            // Compare the latitude of the user's location to the geofence latitude 
            // taking into account edges cases at the meridan lines
            if (nwCornerLat <= 0 && seCornerLat >= 0)
            {
                if ((userLat <= 0 && userLat <= nwCornerLat && userLat <= seCornerLat) ||
                    (userLat >= 0 && userLat >= nwCornerLat && userLat >= seCornerLat))
                {
                    insideLat = true;
                }
            }
            else
            {
                if (userLat <= nwCornerLat && userLat >= seCornerLat)
                {
                    insideLat = true;
                }
            }


            // Compare the longitude of the user's location to the geofence longitude 
            // taking into account edges cases at the meridan lines
            if (nwCornerLong >= 0 && seCornerLong <= 0)
            {
                if ((userLong <= 0 && userLong <= nwCornerLong && userLong <= seCornerLong) ||
                    (userLong >= 0 && userLong >= nwCornerLong && userLong >= seCornerLong))
                {
                    insideLong = true;
                }
            }
            else
            {
                if (userLong >= nwCornerLong && userLong <= seCornerLong)
                {
                    insideLong = true;
                }
            }


            //// Compare the latitude of the user's location to the geofence latitude 
            //// taking into account edges cases at the meridan lines
            //if (nwCornerLat <= 0 && seCornerLat <= 0)
            //{
            //    if(userLat >= nwCornerLat && userLat <= seCornerLat)
            //    {
            //        insideLat = true;
            //    }
            //}
            //else if (nwCornerLat >= 0 && seCornerLat <= 0)
            //{
            //    if(userLat >= nwCornerLat && userLat <= seCornerLat)
            //    {
            //        insideLat = true;
            //    }
            //}
            //else if (nwCornerLat <= 0 && seCornerLat >= 0)
            //{
            //    if(userLat >= nwCornerLat && userLat <= seCornerLat)
            //    {
            //        insideLat = true;
            //    }
            //}
            //else
            //{
            //    if(userLat >= nwCornerLat && userLat <= seCornerLat)
            //    {
            //        insideLat = true;
            //    }
            //}

            return (insideLat && insideLong);
        }

        public static void AllowDeviceLocation()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                // User has opted out of Location
                if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] == false)
                    return;
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

                var isOK = (result == MessageBoxResult.OK);
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = isOK;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

    }
}
