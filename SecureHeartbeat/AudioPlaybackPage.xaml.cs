using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Coding4Fun.Toolkit.Audio;
using Coding4Fun.Toolkit.Audio.Helpers;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SecureHeartbeat.Commands;

namespace SecureHeartbeat
{
    public partial class AudioPlaybackPage : PhoneApplicationPage
    {
        private IsolatedStorageFileStream soundData;
        private String soundFileName ="SHBsoundfile.wav";
        private MicrophoneRecorder audioRecorder = new MicrophoneRecorder();
        private Commands.PlaybackCommand playbackCommand = new PlaybackCommand();
        
        public AudioPlaybackPage()
        {
            InitializeComponent();
            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.AudioPlaybackvm;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.AudioPlaybackvm.IsDataLoaded)
            {
                App.AudioPlaybackvm.LoadData();
            }
        }

        private void RecordButtonStart(object sender, EventArgs eventArgs)
        {
            PlayButton.IsEnabled = false;
            SaveButton.IsEnabled = false;
            audioRecorder.Start();
        }

        private void RecordButtonStop(object sender, EventArgs eventArgs)
        {
            audioRecorder.Stop();
            saveAudioRecording(audioRecorder.Buffer);
            PlayButton.IsEnabled = true;
            SaveButton.IsEnabled = true;


        }

        // TODO Move this into the appropiate Command or dedicated helper class
        private void saveAudioRecording(MemoryStream memoryAudioBuffer)
        {
            if (memoryAudioBuffer == null)
                throw new ArgumentNullException("No sound file is in the buffer to save.");

            if (soundData != null)
            {
                SoundRecordingPlayer.Stop();
                SoundRecordingPlayer.Source = null;
                soundData.Dispose();
            }

            soundFileName = string.Format("SHBSoudFile{0}.wav", DateTime.Now.ToFileTime());
            var rawSoundData = memoryAudioBuffer.GetWavAsByteArray(audioRecorder.SampleRate);

            using (IsolatedStorageFile isoSoundFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                soundData = isoSoundFile.CreateFile(soundFileName);
                soundData.Write(rawSoundData, 0, rawSoundData.Length);

                SoundRecordingPlayer.SetSource(soundData);
            }

        }


        private void PlaySoundFile(object sender, RoutedEventArgs e)
        {
            SoundRecordingPlayer.Play();
        }
    }
}