using System;
using System.Device.Location;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Coding4Fun.Toolkit.Audio;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using SecureHeartbeat.Resources;
using SecureHeartbeat.ViewModels;
using Parse;
using SHClassLibrary;


namespace SecureHeartbeat
{
    public partial class App : Application
    {
        private static MainViewModel _baseViewModel = null;
        private static StatusViewModel _statusvm = null;
        private static LocationViewModel _locationvm = null;
        private static AudioPlaybackViewModel _audioPlaybackvm = null;
        private static UnregisterViewModel _unregistervm = null;
        private static LoginViewModel _loginvm = null;
        private static bool _loggedIn = false;

        public static bool LoggedIn
        {
            get
            {
                return _loggedIn;
            }
            set
            {
                _loggedIn = value;
            }
        }

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static MainViewModel BaseViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_baseViewModel == null)
                    _baseViewModel = new MainViewModel();

                return _baseViewModel;
            }
        }

        

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static StatusViewModel Statusvm
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_statusvm == null)
                    _statusvm = new StatusViewModel();

                return _statusvm;
            }
        }
 

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static AudioPlaybackViewModel AudioPlaybackvm
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_audioPlaybackvm == null)
                    _audioPlaybackvm = new AudioPlaybackViewModel();

                return _audioPlaybackvm;
            }
        }

        public static UnregisterViewModel Unregistervm
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_unregistervm == null)
                    _unregistervm = new UnregisterViewModel();

                return _unregistervm;
            }
        }

        public static LoginViewModel Loginvm
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_loginvm == null)
                {
                    _loginvm = new LoginViewModel();
                }
                    

                return _loginvm;
            }
        }


        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();


            //this.InitializeComponent();
            //this.Suspending += OnSuspending;

            ParseClient.Initialize("JO4tBIiydFtLJ8zjDFg10Km8YS84a2WqgC8hUiQ3", "y2dLvFgBeyzt89pv9gLtJBaZlsMn7jiZfIty5Ufb");
            SoundRecorder.SoundFileCounter = 1;

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private async void Application_Launching(object sender, LaunchingEventArgs e)
        {
            /*var testObject = new ParseObject("TestObject");
            testObject["foo"] = "bar";
            await testObject.SaveAsync();

            var user1 = new ParseUser()
            {
                Username = "10001",
                Password = "password1",
                Email = "rmcegan01@qub.ac.uk"
            };

            DateTime user1dob = new DateTime(1992, 3, 26);
            double user1lat = 54.581728;
            double user1long = -5.937756;
            ParseGeoPoint user1GeoPoint = new ParseGeoPoint(user1lat, user1long);

            user1["mobileNo"] = 07920401001;
            user1["forename"] = "Ronan";
            user1["surname"] = "McEgan";
            user1["department"] = "IT";
            user1["dob"] = user1dob;
            user1["withinBoundary"] = true;
            user1["NWLat"] = 54.582353;
            user1["NWLong"] = -5.938432;
            user1["SELat"] = 54.581122;
            user1["SELong"] = -5.936571;
            user1["currentLoc"] = user1GeoPoint;

            var user2 = new ParseUser()
            {
                Username = "10002",
                Password = "password2",
                Email = "AB.Test@test.com"
            };

            DateTime user2dob = new DateTime(1980, 10, 5);
            double user2lat = 54.581728;
            double user2long = -5.937756;
            ParseGeoPoint user2GeoPoint = new ParseGeoPoint(user2lat, user2long);

            user2["mobileNo"] = 07920401002;
            user2["forename"] = "A";
            user2["surname"] = "B";
            user2["department"] = "IT";
            user2["dob"] = user2dob;
            user2["withinBoundary"] = true;
            user2["NWLat"] = 54.582353;
            user2["NWLong"] = -5.938432;
            user2["SELat"] = 54.581122;
            user2["SELong"] = -5.936571;
            user2["currentLoc"] = user2GeoPoint;

            var user3 = new ParseUser()
            {
                Username = "10003",
                Password = "password3",
                Email = "R.H@test.com"
            };

            DateTime user3dob = new DateTime(1992, 1, 17);
            double user3lat = 54.581728;
            double user3long = -5.937756;
            ParseGeoPoint user3GeoPoint = new ParseGeoPoint(user3lat, user3long);

            user3["mobileNo"] = 07920401003;
            user3["forename"] = "R";
            user3["surname"] = "H";
            user3["department"] = "Marketing";
            user3["dob"] = user3dob;
            user3["withinBoundary"] = true;
            user3["NWLat"] = 54.582353;
            user3["NWLong"] = -5.938432;
            user3["SELat"] = 54.581122;
            user3["SELong"] = -5.936571;
            user3["currentLoc"] = user3GeoPoint;

            var user4 = new ParseUser()
            {
                Username = "10004",
                Password = "password4",
                Email = "SA.Test@test.com"
            };

            DateTime user4dob = new DateTime(1970, 5, 8);
            double user4lat = 54.581728;
            double user4long = -5.937756;
            ParseGeoPoint user4GeoPoint = new ParseGeoPoint(user4lat, user4long);

            user4["mobileNo"] = 07920401004;
            user4["forename"] = "S";
            user4["surname"] = "A";
            user4["department"] = "HR";
            user4["dob"] = user4dob;
            user4["withinBoundary"] = true;
            user4["NWLat"] = 54.582353;
            user4["NWLong"] = -5.938432;
            user4["SELat"] = 54.581122;
            user4["SELong"] = -5.936571;
            user4["currentLoc"] = user4GeoPoint;

            await user1.SignUpAsync();
            await user2.SignUpAsync();
            await user3.SignUpAsync();
            await user4.SignUpAsync();*/


            // The background Agent (that is used to track device location and if it leaves the geofence, record sound) has a timeout 
            // of 2 weeks before it is removed, therefore by removing and reinstalling the agent we can refresh this limit.
            string backgroundAgentName = "SecureHeartbeatBA";
            PeriodicTask currentBA = ScheduledActionService.Find(backgroundAgentName) as PeriodicTask;

            if (currentBA != null)
            {
                ScheduledActionService.Remove(backgroundAgentName);
            }

            PeriodicTask shPeriodicTask = new PeriodicTask(backgroundAgentName);
            shPeriodicTask.Description = "Background task to update user's location and record sound files for anaylsis";

            ScheduledActionService.Add(shPeriodicTask);

            //ScheduledActionService.LaunchForTest(shPeriodicTask.Name, TimeSpan.FromSeconds(30));

        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {

            if (ParseUser.CurrentUser == null)
            {
                if (!App.Loginvm.IsDataLoaded)
                {
                    App.Loginvm.LoadData();
                }
            }
            else
            {
                LoggedIn = true;
                // Ensure that application state is restored appropriately
                if (!App.BaseViewModel.IsDataLoaded)
                {
                    App.BaseViewModel.LoadData();
                }
            }
                
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}