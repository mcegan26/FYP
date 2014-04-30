using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using SecureHeartbeat.Core.Interfaces;
using SecureHeartbeat.Maps;
using SecureHeartbeat.ViewModels;

namespace SecureHeartbeat
{
    public partial class LocationPage : PhoneApplicationPage
    {
        /// <summary>
        /// Stores a reference to the view model associated with this application page.
        /// </summary>
        private readonly IViewModel _viewModel;

        public LocationPage()
        {
            InitializeComponent();

            var viewModel = new LocationViewModel(new BingMapAdapter(SHBMap));
        
            _viewModel = viewModel;
            DataContext = viewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           _viewModel.NavigatedTo();
           //Map map = SHBMap;
        }
    }
}