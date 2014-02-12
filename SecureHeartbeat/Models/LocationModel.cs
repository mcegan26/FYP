using System;
using System.ComponentModel;

namespace SecureHeartbeat.Models
{
    public class LocationModel : INotifyPropertyChanged
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

        private string _latitudeLabel = "Latitude: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LatitudeLabel
        {
            get
            {
                return _latitudeLabel;
            }
            set
            {
                if (value != _latitudeLabel)
                {
                    _latitudeLabel = value;
                    NotifyPropertyChanged("LatitudeLabel");
                }
            }
        }

        private double _latitude;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (!value.Equals(_latitude))
                {
                    _latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }


        private string _longitudeLabel = "Longitude: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LongitudeLabel
        {
            get
            {
                return _longitudeLabel;
            }
            set
            {
                if (value != _longitudeLabel)
                {
                    _longitudeLabel = value;
                    NotifyPropertyChanged("LongitudeLabel");
                }
            }
        }

        private double _longitude;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (!value.Equals(_longitude))
                {
                    _longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }

        private string _postalCodeLabel = "Postcode: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PostalCodeLabel
        {
            get
            {
                return _postalCodeLabel;
            }
            set
            {
                if (value != _postalCodeLabel)
                {
                    _postalCodeLabel = value;
                    NotifyPropertyChanged("PostalCodeLabel");
                }
            }
        }

        private string _postalCode;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                if (value != _postalCode)
                {
                    _postalCode = value;
                    NotifyPropertyChanged("PostalCode");
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
