using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Phone.Media.Capture;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SecureHeartbeat.Models;
using SecureHeartbeat.Resources;
using SecureHeartbeat.ViewModels;

namespace SecureHeartbeat
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.BaseViewModel;

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.BaseViewModel.IsDataLoaded)
            {
                App.BaseViewModel.LoadData();
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            // NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            int selectedPage = (MainLongListSelector.SelectedItem as ItemViewModel).ID;
            switch (selectedPage)
            {
                case 0:
                    NavigationService.Navigate(new Uri("/StatusPage.xaml", UriKind.Relative));
                    break;
                case 1:
                    NavigationService.Navigate(new Uri("/LocationPage.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/AudioPlaybackPage.xaml", UriKind.Relative));
                    break;
                case 3:
                    NavigationService.Navigate(new Uri("/UnregisterPage.xaml", UriKind.Relative));
                    break;
                default:;
                    break;
            }



            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // Create a new button and set the text value to the localized string from AppResources.
            //ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            //appBarButton.Text = AppResources.AppBarButtonText;
            //ApplicationBar.Buttons.Add(appBarButton);


            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            appBarMenuItem.Click += AppBarAboutItemOnClick;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void AppBarAboutItemOnClick(object sender, EventArgs eventArgs)
        {
            AboutPrompt aboutInfoBox = new AboutPrompt();
            aboutInfoBox.Show("Ronan McEgan", "Roro26_LDN", "rmcegan01@qub.ac.uk", "www.linkedin.com/pub/ronan-mcegan/64/9a8/38");
        }
    }
}