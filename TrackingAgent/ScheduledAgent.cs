using System.Device.Location;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using Windows.Devices.Geolocation;
using Parse;
using SHClassLibrary;

namespace TrackingAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override async void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background
            // Possibilty to double the time interval is preformed and binary state
            
            // Use 3g to find current location
            // Grab the user from SH app (possibly have to save the user ID to the device storage)
            // Obatain the boundary (possibly only every 10 times)
            // Use built in method to calculate if its outside the primeter and flag it and change this bool and update parse
            // If outside boundary record sound for 5 seconds and save to phone
            // Upload sound wave to user's parse record and update the user's loc in parse

            var deviceLoc = await Locater.GetDeviceLoc();

            if (!deviceLoc.Equals(null))
            {
                var shUserID = DeviceStorage.ReadSHUser();
                var getUsersWithMatchingID = from user in ParseObject.GetQuery("User")
                                     where user.Get<string>("username") == shUserID
                                     select user;

                ParseObject currentUser = await getUsersWithMatchingID.FirstAsync();
                var nwLat = currentUser.Get<double>("NWLat");
                var nwLong = currentUser.Get<double>("NWLong");
                var seLat = currentUser.Get<double>("SELat");
                var seLong = currentUser.Get<double>("SELong");


                //new GeoCoordinate(54.58370919, -5.9322165), new GeoCoordinate(54.583709199999994, -5.9322165)
                LocationRectangle geoFence;
                geoFence = new LocationRectangle(new GeoCoordinate(nwLat, nwLong), new GeoCoordinate(seLat, seLong));
                var insideBoundary = Locater.UserInGeoFence(geoFence, deviceLoc);

                if (!insideBoundary)
                {
                    currentUser["withinBoundary"] = false;

                    if (SoundRecorder.RecordCounter % 3 == 0)
                    {
                        SoundRecorder.RecordCounter = 0;
                        var soundFileByteStream = SoundRecorder.Record();
                        var SHSoundFileTime = string.Format("SHBSoudFile{0}.wav", DateTime.Now.ToFileTime());
                        ParseFile soundFile = new ParseFile(SHSoundFileTime, soundFileByteStream);

                        await soundFile.SaveAsync();

                        var SHSoundFile = string.Format("SHBSoudFile{0}", SoundRecorder.SoundFileCounter);
                        currentUser[SHSoundFile] = soundFile;
                        Console.WriteLine("Recordered the soundfile and svaed it as a parse file and uploaded it");
                    }
                    SoundRecorder.RecordCounter++;
                }
                else
                {
                    Console.WriteLine("User is inside the bounds");
                }


                ParseGeoPoint parseDeviceLoc = new ParseGeoPoint(deviceLoc.Coordinate.Latitude, deviceLoc.Coordinate.Longitude);
                currentUser["currentLoc"] = parseDeviceLoc;

                await currentUser.SaveAsync();
                Console.WriteLine("Saved the updated user that is outside the bounds to Parse");
            }


            Console.WriteLine("Completed periodic task");
            NotifyComplete();
        }
    }
}