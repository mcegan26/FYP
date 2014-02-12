using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SecureHeartbeat.Models
{
    public class LoginModel : INotifyPropertyChanged
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

        private string _passwordLabel = "Password: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PasswordLabel
        {
            get
            {
                return _passwordLabel;
            }
            set
            {
                if (value != _passwordLabel)
                {
                    _passwordLabel = value;
                    NotifyPropertyChanged("PasswordLabel");
                }
            }
        }

        private string _password;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    NotifyPropertyChanged("MobileNumber");
                }
            }
        }


        

        private string _serverLabel = "Server Address: ";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string ServerLabel
        {
            get
            {
                return _serverLabel;
            }
            set
            {
                if (value != _serverLabel)
                {
                    _serverLabel = value;
                    NotifyPropertyChanged("ServerLabel");
                }
            }
        }

        private string _server;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                if (value != _server)
                {
                    _server = value;
                    NotifyPropertyChanged("Server");
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
