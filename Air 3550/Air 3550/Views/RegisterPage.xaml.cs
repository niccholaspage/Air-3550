using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        RegisterViewModel ViewModel = new RegisterViewModel();

        private async void RegisterButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var loginId = await ViewModel.CreateAccount();

            if (loginId != null)
            {
                Frame.Navigate(typeof(LoginPage), loginId);
            }
        }

        private void Back_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // TODO: Switch to a proper header bar for navigating backward and such
            Frame.GoBack();
        }
    }
}
