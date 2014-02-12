using System.Device.Location;

// Provie an alias for the Map data type so that we can reference it as BingMap within our code
using BingMap = Microsoft.Phone.Maps.Controls.Map;

namespace SecureHeartbeat.Maps
{
    /// <summary>
    /// A concrete implementationt of the IMap interface which encapsulates a 
    /// Bing map reference and provides the methods to interact with the object.
    /// </summary>
    public class BingMapAdapter : IMap
    {
        /// <summary>
        /// Provide a referene to enasulated BingMap, via dependency injection.
        /// Note the explicit alias provided for the data type 'BingMap'
        /// </summary>
        private readonly BingMap _map;

        /// <summary>
        /// Constructs a new instance of the BingMapAdapter which encapsulated a 
        /// BingMap reference
        /// </summary>
        /// <param name="map">A non-null instance of the BingMap</param>
        public BingMapAdapter(BingMap map)
        {
            _map = map;
        }

        /// <summary>
        /// Explicitly refreshes the map data structure
        /// </summary>
        public void Refresh()
        {
           
        }
        
        /// <summary>
        /// Sets the location within the map
        /// </summary>
        /// <param name="latitude">The latitude parameter</param>
        /// <param name="longitude">The longitude parameter</param>
        public void SetLocation(double latitude, double longitude)
        {
            _map.SetView(new GeoCoordinate(latitude, longitude), 15D);
        }
    }
}
