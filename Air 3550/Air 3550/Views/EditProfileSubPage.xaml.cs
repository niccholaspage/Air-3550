using Air_3550.ViewModels;
using Database.Util;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditProfileSubPage : Page
    {
        public EditProfileSubPage()
        {
            this.InitializeComponent();
        }

        readonly EditProfileViewModel ViewModel = new();

        private async void SaveChangesButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.SaveChanges())
            {

            }
        }

        public string GetFormattedAccountBalance()
        {
            return "Account Balance: " + ViewModel.AccountBalance.FormatAsMoney();
        }

        public string GetRewardPoints()
        {
            return "Reward Points: " + ViewModel.RewardPoints;
        }

        public string GetTotalRewardPointsUsed()
        {
            return "Total Reward Points Used: " + ViewModel.TotalRewardPointsUsed;
        }
    }
}
