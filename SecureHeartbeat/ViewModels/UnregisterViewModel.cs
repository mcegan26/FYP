using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Navigation;
using SecureHeartbeat.Core.Impl;
using SecureHeartbeat.Models;
using SecureHeartbeat.Resources;
using SecureHeartbeat.Commands;
using SHClassLibrary;


namespace SecureHeartbeat.ViewModels
{
    public class UnregisterViewModel : ViewModel
    {
        private ICommand _returnToLogin;

        public ICommand ReturnToLogin
        {
            get { return _returnToLogin; }
        }

        public UnregisterViewModel()
        {
            this.Items = new ObservableCollection<UnregisterModel>();
            _returnToLogin = new ReturnToLogin();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<UnregisterModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public override void NavigatedTo()
        {
            DeviceStorage.CheckNeedToSaveRecording();
            DeviceStorage.DeleteSHUserDetails(DeviceStorage.shUserIDFileName);
            DeviceStorage.DeleteSHUserDetails(DeviceStorage.parseObjIDFileName);
            DeviceStorage.DeleteSHUserDetails(DeviceStorage.shUserWithinBoundary);
        }


        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            
            this.Items.Add(new UnregisterModel());
            
            this.IsDataLoaded = true;
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
