namespace SecureHeartbeat.Core.Interfaces
{
    public interface INavigationAware
    {
        /// <summary>
        /// This method will be interacted with when the view which has been associated with 
        /// a view model has been navigated to.
        /// </summary>
        void NavigatedTo();
    }
}