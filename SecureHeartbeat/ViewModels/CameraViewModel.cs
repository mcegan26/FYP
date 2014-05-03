using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using SecureHeartbeat.Commands;
using SecureHeartbeat.Core.Impl;
using SecureHeartbeat.Resources;
using SHClassLibrary;
using Microsoft.Devices;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;

namespace SecureHeartbeat.ViewModels
{
    public class CameraViewModel: ViewModel
    {
        private Button uiButton;
        private PhotoCamera silentCamera;
        private MediaLibrary photoLibrary;
        private VideoBrush camFeed = new VideoBrush();
        private ICommand _launchAppCommand;

        public ICommand LaunchAppCommand
        {
            get { return _launchAppCommand; }
        }


        public CameraViewModel()
        {
            //this.Items = new ObservableCollection<AudioPlaybackModel>();
            photoLibrary = new MediaLibrary();
            _launchAppCommand = new LaunchAppCommand();
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
            DeviceStorage.CheckNeedToSaveRecording();
            // Check to see if the camera is available on the phone.

            silentCamera = new PhotoCamera(CameraType.Primary);
            // Event is fired when the PhotoCamera object has been initialized.
            silentCamera.Initialized += new EventHandler<CameraOperationCompletedEventArgs>(CameraReady);
            camFeed.SetSource(silentCamera);

            // Add an event handler to run when the camera has taken an image and it is ready to save
            silentCamera.CaptureImageAvailable += new EventHandler<ContentReadyEventArgs>(UploadPhoto);

            // Event is fired when the capture sequence is complete and a thumbnail image is available.
            silentCamera.CaptureThumbnailAvailable += new EventHandler<ContentReadyEventArgs>(SavePhoto);
        }


        // Event listner to ensure the camera takes phtos the correct way by rotating the camera feed 180 degrees
        // when the device has been turned round
        public override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            if (silentCamera != null)
            {
                if (e.Orientation == PageOrientation.LandscapeRight)
                {
                    // Rotate for LandscapeRight orientation.
                    camFeed.RelativeTransform =
                        new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 180 };
                }
                else
                {
                    // Rotate for standard landscape orientation.
                    camFeed.RelativeTransform =
                        new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 0 };
                }
            }

        }

        private void CameraReady(object sender, CameraOperationCompletedEventArgs e)
        {
 	        if (e.Succeeded)
            {
                var listOFResolutionsAvail = silentCamera.AvailableResolutions;
                //var lowResSize = listOFResolutionsAvail.Min();
                var resSize = new Size(1280, 960);
                silentCamera.Resolution = resSize;
                silentCamera.FlashMode = FlashMode.Auto;
                silentCamera.CaptureImage();
            }
        }

        private void UploadPhoto(object sender, ContentReadyEventArgs e)
        {
            var auxStream = new MemoryStream();
            e.ImageStream.CopyTo(auxStream);
            var uploadByteArray = auxStream.ToArray();

            DeviceStorage.UploadImageFileToParse(uploadByteArray);
            auxStream.Dispose();

            Deployment.Current.Dispatcher.BeginInvoke(delegate()
            {
                uiButton.IsEnabled = true;

            });
        }

        
        private void SavePhoto(object sender, ContentReadyEventArgs e)
        {
            var fileName = String.Format("SHBPhoto{0}.jpg", DateTime.Now.ToString("dd-MM-yyyy--HH-mm-ss"));

            // Save the image to the device photo library
            photoLibrary.SavePictureToCameraRoll(fileName, e.ImageStream);
        }

        public override void RegisterUIComponent(Button uiButton)
        {
            this.uiButton = uiButton;
        }

        public override void NavigatedFrom()
        {
            if (silentCamera != null)
            {
                // Dispose camera to minimize power consumption and to expedite shutdown.
                silentCamera.Dispose();

                // Release memory, ensure garbage collection.
                silentCamera.Initialized -= CameraReady;
                silentCamera.CaptureImageAvailable -= UploadPhoto;
                silentCamera.CaptureThumbnailAvailable -= SavePhoto;
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
