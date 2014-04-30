using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Scheduler;
using System;
using Microsoft.Phone.Shell;
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
            ParseClient.Initialize("JO4tBIiydFtLJ8zjDFg10Km8YS84a2WqgC8hUiQ3", "y2dLvFgBeyzt89pv9gLtJBaZlsMn7jiZfIty5Ufb");
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
            // ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(120));
            //TODO: Add code to perform your task in background
            // Possibilty to double the time interval is preformed and binary state
            
            // Use 3g to find current location
            // Grab the user from SH app (possibly have to save the user ID to the device storage)
            // Obatain the boundary (possibly only every 10 times)
            // Use built in method to calculate if its outside the primeter and flag it and change this bool and update parse
            // If outside boundary record sound for 5 seconds and save to phone
            // Upload sound wave to user's parse record and update the user's loc in parse

            
            var shUserParseObjID = DeviceStorage.ReadSHUserDetails(DeviceStorage.parseObjIDFileName);
            var shUserID = DeviceStorage.ReadSHUserDetails(DeviceStorage.shUserIDFileName);
                
            if (!shUserParseObjID.Equals(""))
            {
                var deviceLoc = await Locater.GetDeviceLoc();

                if (deviceLoc != null)
                {
                    var userQuery = ParseUser.Query.Where(user => user.Get<string>("username") == shUserID);
                    var currentUser1 = await userQuery.FirstOrDefaultAsync();

                    ParseObject currentUser = currentUser1;
                    if (currentUser != null)
                    {
                        var nwLat = currentUser.Get<double>("NWLat");
                        var nwLong = currentUser.Get<double>("NWLong");
                        var seLat = currentUser.Get<double>("SELat");
                        var seLong = currentUser.Get<double>("SELong");


                        //new GeoCoordinate(54.58370919, -5.9322165), new GeoCoordinate(54.583709199999994, -5.9322165)
                        LocationRectangle geoFence;
                        geoFence = new LocationRectangle(new GeoCoordinate(nwLat, nwLong),
                            new GeoCoordinate(seLat, seLong));
                        var insideBoundary = Locater.UserInGeoFence(geoFence, deviceLoc);

                        if (!insideBoundary)
                        {
                            currentUser["withinBoundary"] = false;

                            ShellToast goToAppToast = new ShellToast
                            {
                                Title = "Secure Heartbeat",
                                Content = "Device Extracted From GeoFence",
                                NavigationUri = new Uri("/LoginPage.xaml", UriKind.Relative)
                            };
                            goToAppToast.Show();
                            //var soundFileByteStream = SoundRecorder.Record();
                            //var SHSoundFileTime = string.Format("SHBSoudFile{0}.wav", DateTime.Now.ToFileTime());
                            //ParseFile soundFile = new ParseFile(SHSoundFileTime, soundFileByteStream);

                            //await soundFile.SaveAsync();

                            //var SHSoundFile = string.Format("SHBSoudFile{0}", SoundRecorder.SoundFileCounter);
                            //currentUser[SHSoundFile] = soundFile;
                        }


                        ParseGeoPoint parseDeviceLoc = new ParseGeoPoint(deviceLoc.Coordinate.Latitude,
                            deviceLoc.Coordinate.Longitude);
                        currentUser["currentLoc"] = parseDeviceLoc;

                        await currentUser.SaveAsync();
                        Console.WriteLine("Saved the updated user that is outside the bounds to Parse");
                    }
                }
                
            }
            Console.WriteLine("Completed periodic task");
            NotifyComplete();
        }
    }
}