using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Phone.Controls;
using SecureHeartbeat.Core.Interfaces;

namespace SecureHeartbeat.Core.Impl
{
    /// <summary>
    /// Represents the base implementation of a view model within the application.
    /// It is expected that all viewmodels extend this class.
    /// 
    /// Provides  a base implementation to the IViewModel interface.
    /// </summary>
    public abstract class ViewModel : IViewModel
    {
        /// <summary>
        /// A public event which allows property changed listeners to bind to the delegate event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Generic implemetnation of the OnPropertyChanged method.
        /// </summary>
        /// <param name="propertyName">The property name which has just changed</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// This method will be interacted with when the view which has been associated with 
        /// a view model has been navigated to.
        /// </summary>
        public virtual void NavigatedTo()
        {
            // By default this implementation will do nothing
            // Instead extended classes should override this implementation if such functionality is required
        }

        public virtual void NavigatedFrom()
        {
            // By default this implementation will do nothing
            // Instead extended classes should override this implementation if such functionality is required
        }

        public virtual void RegisterUIComponent(System.Windows.Controls.Button uiButton)
        {
            // By default this implementation will do nothing
            // Instead extended classes should override this implementation if such functionality is required
        }

        public virtual void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            // By default this implementation will do nothing
            // Instead extended classes should override this implementation if such functionality is required
        }
    }
}
