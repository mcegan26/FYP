using System;
using System.ComponentModel;
using System.Windows.Input;
using SecureHeartbeat.Commands;
using SecureHeartbeat.Core.Impl;
using SecureHeartbeat.Resources;
using SHClassLibrary;

namespace SecureHeartbeat.ViewModels
{
    public class AudioPlaybackViewModel : ViewModel
    {

        //public AudioPlaybackModel audioPlaybackModel;
        public AudioPlaybackViewModel()
        {
            //this.Items = new ObservableCollection<AudioPlaybackModel>();

            CreateCommands();
        }


        public void CreateCommands()
        {
            PlaybackCommand = new PlaybackCommand();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        // public ObservableCollection<AudioPlaybackModel> Items { get; private set; }

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

        public ICommand PlaybackCommand { get; set;  }
/*        public bool ChangeButtonEnabledProperty
        {
            

        }*/

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            
         //   this.Items.Add(new AudioPlaybackModel());
            
            this.IsDataLoaded = true;
        }

        public override void NavigatedTo()
        {
            if (!BackgroundParseCalls.InsideBoundary)
            {
                var rawSoundData = SoundRecorder.Record();
                SoundRecorder.UploadFileToParse(rawSoundData);
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
