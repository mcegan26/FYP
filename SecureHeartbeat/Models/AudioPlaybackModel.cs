using System;
using System.ComponentModel;


namespace SecureHeartbeat.Models
{
    public class AudioPlaybackModel : INotifyPropertyChanged
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

        private string _recordButtonLabel = "Record";
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string RecordButtonLabel
        {
            get
            {
                return _recordButtonLabel;
            }
            set
            {
                if (value != _recordButtonLabel)
                {
                    _recordButtonLabel = value;
                    NotifyPropertyChanged("RecordButtonLabel");
                }
            }
        }

        private string _playbackLabel = "Playback";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PlaybackLabel
        {
            get
            {
                return _playbackLabel;
            }
            set
            {
                if (value != _playbackLabel)
                {
                    _playbackLabel = value;
                    NotifyPropertyChanged("PlaybackLabel");
                }
            }
        }

        private string _saveAudioLabel = "Save Recording";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string SaveAudioLabel
        {
            get
            {
                return _saveAudioLabel;
            }
            set
            {
                if (value != _saveAudioLabel)
                {
                    _saveAudioLabel = value;
                    NotifyPropertyChanged("SaveAudioLabel");
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
