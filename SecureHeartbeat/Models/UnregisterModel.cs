using System;
using System.ComponentModel;

namespace SecureHeartbeat.Models
{
    public class UnregisterModel : INotifyPropertyChanged
    {
        private string _id;
        /// <summary>
        /// Sample ViewModel property; this property is used to identify the object.
        /// </summary>
        /// <returns></returns>
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _unregisterMessage1 = "You have successfully logged out";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string UnregisterMessage1
        {
            get
            {
                return _unregisterMessage1;
            }
            set
            {
                if (value != _unregisterMessage1)
                {
                    _unregisterMessage1 = value;
                    NotifyPropertyChanged("UnregisterMessage1");
                }
            }
        }

       


        private string _unregisterMessage2 = "This device is now safe to take outside of corporate premises";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string UnregisterMessage2
        {
            get
            {
                return _unregisterMessage2;
            }
            set
            {
                if (value != _unregisterMessage2)
                {
                    _unregisterMessage2 = value;
                    NotifyPropertyChanged("UnregisterMessage2");
                }
            }
        }


        private string _unregisterMessage3 = "Click below to Login as a new User";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string UnregisterMessage3
        {
            get
            {
                return _unregisterMessage3;
            }
            set
            {
                if (value != _unregisterMessage3)
                {
                    _unregisterMessage3 = value;
                    NotifyPropertyChanged("UnregisterMessage3");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
