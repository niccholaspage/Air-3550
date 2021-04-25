using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;

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

        private LoginPage.Params.RedirectToPage redirectParams;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is LoginPage.Params.RedirectToPage redirect)
            {
                redirectParams = redirect;
            }
        }

        readonly RegisterViewModel ViewModel = new();

        private async void RegisterButton_Click(object _, RoutedEventArgs __)
        {
            var loginId = await ViewModel.CreateAccount();

            if (loginId != null)
            {
                var newUserParams = new LoginPage.Params.NewUser(loginId);

                if (redirectParams == null)
                {
                    Frame.Navigate(typeof(LoginPage), newUserParams);
                }
                else
                {
                    Frame.Navigate(typeof(LoginPage), new List<LoginPage.Params>() { newUserParams, redirectParams });
                }

                // Remove the registration page from the back
                // stack as well as the login page before it.
                Frame.BackStack.RemoveAt(Frame.BackStackDepth - 1);
                Frame.BackStack.RemoveAt(Frame.BackStackDepth - 1);
            }
        }
    }
}
