using System;
using System.ComponentModel;

namespace SecureHeartbeat.Models
{
    public class StatusModel : INotifyPropertyChanged
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

        private string _usernameLabel = "Username: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string UsernameLabel
        {
            get
            {
                return _usernameLabel;
            }
            set
            {
                if (value != _usernameLabel)
                {
                    _usernameLabel = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }
        
        private string _username;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }


        private string _mobileNumberLabel = "Mobile Number: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string MobileNumberLabel
        {
            get
            {
                return _mobileNumberLabel;
            }
            set
            {
                if (value != _mobileNumberLabel)
                {
                    _mobileNumberLabel = value;
                    NotifyPropertyChanged("MobileNumberLabel");
                }
            }
        }

        private string _mobileNumber;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                if (value != _mobileNumber)
                {
                    _mobileNumber = value;
                    NotifyPropertyChanged("MobileNumber");
                }
            }
        }

        private string _withinBoundaryLabel = "Inside Fortress: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string WithinBoundaryLabel
        {
            get
            {
                return _withinBoundaryLabel;
            }
            set
            {
                if (value != _withinBoundaryLabel)
                {
                    _withinBoundaryLabel = value;
                    NotifyPropertyChanged("WithinBoundaryLabel");
                }
            }
        }

        private string _withinBoundary;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string WithinBoundary
        {
            get
            {
                return _withinBoundary;
            }
            set
            {
                if (value != _withinBoundary)
                {
                    _withinBoundary = value;
                    NotifyPropertyChanged("WithinBoundary");
                }
            }
        }

        private string _locationKnownLabel = "Location Available: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LocationKnownLabel
        {
            get
            {
                return _locationKnownLabel;
            }
            set
            {
                if (value != _locationKnownLabel)
                {
                    _withinBoundary = value;
                    NotifyPropertyChanged("LocationKnownLabel");
                }
            }
        }


        private string _locationKnown;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LocationKnown
        {
            get
            {
                return _locationKnown;
            }
            set
            {
                if (value != _locationKnown)
                {
                    _withinBoundary = value;
                    NotifyPropertyChanged("LocationKnown");
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
