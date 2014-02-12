using System.ComponentModel;

namespace SecureHeartbeat.Core.Interfaces
{
     /// <summary>
     /// Represents the interface expected of a ViewModel.
     /// This should be implemented by other ViewModels
     /// </summary>
    public interface IViewModel : INotifyPropertyChanged, INavigationAware
     {

     }
}
