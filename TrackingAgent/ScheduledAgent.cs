using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Scheduler;
using System;
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
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
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
                if (!shUserID.Equals(""))
                {
                    //var getUsersWithMatchingID = from user in ParseObject.GetQuery("User")
                    //                             where user.Get<String>("username") == shUserID
                    //                             select user;

                    //var query = ParseObject.GetQuery("GameScore").WhereEqualTo("playerName", "Dan Stemkoski");
                    //IEnumerable<ParseObject> results = await query.FindAsync();

                    if (ParseUser.CurrentUser != null)
                    {
                        var u = ParseUser.CurrentUser.Get<double>("NWLat");
                        Console.WriteLine(u);
                    }
                    else
                    {
                        // show the signup or login screen
                    }


                    try
                    {
                        var getUsersWithMatchingID1 = ParseUser.GetQuery("User").WhereEqualTo("Username", shUserID);
                        var currentUser1 = await getUsersWithMatchingID1.FirstOrDefaultAsync();
                        var y = currentUser1.Get<double>("NWLat");
                        Console.WriteLine("OMGee the NW LAt is " + y);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Epic Fail");
                    }

                    var getUsersWithMatchingID = ParseObject.GetQuery("User").WhereEqualTo("Username", shUserID);

                    ParseObject currentUser = await getUsersWithMatchingID.FirstOrDefaultAsync();
                    var x = 1 + 2;
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

                        var soundFileByteStream = SoundRecorder.Record();
                        var SHSoundFileTime = string.Format("SHBSoudFile{0}.wav", DateTime.Now.ToFileTime());
                        ParseFile soundFile = new ParseFile(SHSoundFileTime, soundFileByteStream);

                        await soundFile.SaveAsync();

                        var SHSoundFile = string.Format("SHBSoudFile{0}", SoundRecorder.SoundFileCounter);
                        currentUser[SHSoundFile] = soundFile;
                        Console.WriteLine("Recordered the soundfile and svaed it as a parse file and uploaded it");
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
                
            }


            Console.WriteLine("Completed periodic task");
            NotifyComplete();
        }
    }
}