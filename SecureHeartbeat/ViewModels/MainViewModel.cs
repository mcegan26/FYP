using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Windows;
using Parse;
using SecureHeartbeat.Core.Impl;
using SecureHeartbeat.Models;
using SecureHeartbeat.Resources;
using SHClassLibrary;

namespace SecureHeartbeat.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private IsolatedStorageFileStream soundData;

        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

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
            
            this.Items.Add(new ItemViewModel() { ID = 0, LineOne="Device Status", LineTwo = "Current user details", LineThree = "View a report of the curent user logged into this device"});
            this.Items.Add(new ItemViewModel() { ID = 1, LineOne = "Current Location", LineTwo = "Show device on a map", LineThree = "Show the current location of this device within Bing Maps" });
            this.Items.Add(new ItemViewModel() { ID = 2, LineOne = "Sound Files", LineTwo = "Listen back to the sound files", LineThree = "Small media player to select from the list of sound files that have been recorded on this device and listen back to them (in .wav format)" });
            this.Items.Add(new ItemViewModel() { ID = 3, LineOne = "Unregister Device", LineTwo = "Take the device outside corporate environment securely", LineThree = "Remove location tracking and device from corporate boundary" });
           

            this.IsDataLoaded = true;
        }

        public override void NavigatedTo()
        {
            BackgroundParseCalls.DeviceInsideBoundary();
            if (!BackgroundParseCalls.InsideBoundary)
            {
                SoundRecorder.StartRecord();
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