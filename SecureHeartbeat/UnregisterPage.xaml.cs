using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace SecureHeartbeat
{
    public partial class UnregisterPage : PhoneApplicationPage
    {
        public UnregisterPage()
        {
            InitializeComponent();
            DataContext = App.Unregistervm;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.Unregistervm.IsDataLoaded)
            {
                App.Unregistervm.LoadData();
            }
        }
    }
}