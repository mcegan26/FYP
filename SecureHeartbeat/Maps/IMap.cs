namespace SecureHeartbeat.Maps
{
    /// <summary>
    /// Provides an abstraction layer to a Map implementation.
    /// This provides a decoupling mechanism for interacting with a specific map
    /// API, such as Bing or Google. As a direct result of the decision to use this 
    /// interface the testability of ViewModels will be improved.
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Explicitly refreshes the map data structure
        /// </summary>
        void Refresh();

        /// <summary>
        /// Sets the location within the map
        /// </summary>
        /// <param name="latitude">The latitude parameter</param>
        /// <param name="longitude">The longitude parameter</param>
        void SetLocation(double latitude, double longitude);
    }
}
