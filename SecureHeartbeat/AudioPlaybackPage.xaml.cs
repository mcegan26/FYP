using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Navigation;
using Coding4Fun.Toolkit.Audio;
using Coding4Fun.Toolkit.Audio.Helpers;
using Microsoft.Phone.Controls;
using SecureHeartbeat.Commands;
using SHClassLibrary;

namespace SecureHeartbeat
{
    public partial class AudioPlaybackPage : PhoneApplicationPage
    {
        private IsolatedStorageFileStream soundData;
        private String soundFileName ="SHBsoundfile.wav";
        //private MicrophoneRecorder audioRecorder = new MicrophoneRecorder();
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
            App.AudioPlaybackvm.NavigatedTo();
        }

        private void RecordButtonStart(object sender, EventArgs eventArgs)
        {
            PlayButton.IsEnabled = false;
            SaveButton.IsEnabled = false;
            SoundRecorder.audioRecorder.Stop();
            SoundRecorder.audioRecorder.Start(10000);
        }

        private void RecordButtonStop(object sender, EventArgs eventArgs)
        {
            SoundRecorder.audioRecorder.Stop();
            SoundRecorder.SaveRecording(SoundRecorder.audioRecorder.Buffer, SoundRecordingPlayer);
            //saveAudioRecording(SoundRecorder.audioRecorder.Buffer);
            PlayButton.IsEnabled = true;
            SaveButton.IsEnabled = true;  
        }

        private void saveAudioRecording(MemoryStream memoryAudioBuffer)
        {
            if (memoryAudioBuffer != null)
            {
                //if (soundData != null)
                //{
                //    SoundRecordingPlayer.Stop();
                //    SoundRecordingPlayer.Source = null;
                //    soundData.Dispose();
                //}

                //SoundRecorder.SaveRecording(memoryAudioBuffer, SoundRecordingPlayer);
                //SoundRecorder.UploadFileToParse(rawSoundData);

                //soundFileName = string.Format("SHBSoudFile{0}.wav", DateTime.Now.ToFileTime());
                //var rawSoundData = memoryAudioBuffer.GetWavAsByteArray(audioRecorder.SampleRate);

                //using (IsolatedStorageFile deviceStorage = IsolatedStorageFile.GetUserStoreForApplication())
                //{
                //    soundData = deviceStorage.CreateFile(soundFileName);
                //    soundData.Write(rawSoundData, 0, rawSoundData.Length);

                //    SoundRecordingPlayer.SetSource(soundData);
                //}
            }

            

        }


        private void PlaySoundFile(object sender, RoutedEventArgs e)
        {
            SoundRecordingPlayer.Play();
        }

        private void UploadToSever(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = false;
            RecordButton.IsEnabled = false;
            SoundRecorder.audioRecorder.Stop();
            var rawSoundData = SoundRecorder.audioRecorder.Buffer.GetWavAsByteArray(SoundRecorder.audioRecorder.SampleRate);
            SoundRecorder.UploadFileToParse(rawSoundData);
            PlayButton.IsEnabled = true;
            RecordButton.IsEnabled = true;
        }
    }
}