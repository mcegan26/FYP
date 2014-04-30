using System.Windows;

namespace SHClassLibrary
{
    public class ExitTrap
    {
        public static bool AreYouSure()
        {
            MessageBoxResult answer =
                    MessageBox.Show("You are about to log out, are you sure you wish to do this?",
                    "Verify Log out",
                    MessageBoxButton.OKCancel);

            return (answer == MessageBoxResult.OK);
        }
    }
}
