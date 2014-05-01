using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Parse;
using SecureHeartbeat.Core.Impl;
using SecureHeartbeat.Models;
using SecureHeartbeat.Resources;
using SHClassLibrary;


namespace SecureHeartbeat.ViewModels
{
    public class StatusViewModel : ViewModel
    {
        public StatusModel deviceUser;

        public StatusViewModel()
        {
            deviceUser = new StatusModel();
            this.Items = new ObservableCollection<StatusModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<StatusModel> Items { get; private set; }

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

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            new StatusModel()
            {
                ID = "0",
                Username = "E.g. John Smith",
                MobileNumber = "e.g. 07911223344",
                WithinBoundary = "e.g. Yes",
                LocationKnown = "e.g. Yes"
            };

            //var parseObjectID = ParseUser.CurrentUser.ObjectId;

            deviceUser.ID = ParseUser.CurrentUser.Get<string>("username");
            deviceUser.Username = ParseUser.CurrentUser.Get<string>("forename") + " " + ParseUser.CurrentUser.Get<string>("surname");
            var numberWithoutZero = ParseUser.CurrentUser.Get<Int64>("mobileNo").ToString();
            deviceUser.MobileNumber = "0" + numberWithoutZero;

            if ((bool) ParseUser.CurrentUser.Get<bool>("withinBoundary"))
            {
                deviceUser.WithinBoundary = "Yes";
            }
            else
            {
                deviceUser.WithinBoundary = "No";
            }

            Items.Clear();
            Items.Add(deviceUser);
        }

        public override void NavigatedTo()
        {
            DeviceStorage.CheckNeedToSaveRecording();
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
