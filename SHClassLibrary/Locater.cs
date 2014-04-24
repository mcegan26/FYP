using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using System.Reflection;

namespace SHClassLibrary
{
    public class Locater
    {
        public static async Task<Geoposition> GetDeviceLoc()
        {
            // The user has opted out of Location posistioning
            if (!(bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"])
            {
                return null;
            }

            var geolocator = new Geolocator
            {
                DesiredAccuracyInMeters = 20
            };

            try
            {
                var geoposition = await geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromSeconds(22),
                timeout: TimeSpan.FromSeconds(16)
                );

                return geoposition;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
