using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using Windows.Devices.Geolocation;
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


            }


            Console.WriteLine("Completed periodic task");
            NotifyComplete();
        }
    }
}