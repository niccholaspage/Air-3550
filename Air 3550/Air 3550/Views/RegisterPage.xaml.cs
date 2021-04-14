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
            var result = await ViewModel.CreateAccount();

            if (result)
            {
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void Back_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
