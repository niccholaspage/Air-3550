﻿// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Air_3550.Repository;
using Air_3550.ViewModels;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.System;

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        LoginViewModel ViewModel = new LoginViewModel();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string loginId = e.Parameter as string;

            if (loginId != null)
            {
                ViewModel.Username = loginId;
                AccountCreatedBar.Message = $"Your account has been successfully created. Your ID is {loginId}.";
                AccountCreatedBar.IsOpen = true;
            }
        }

        public async void LoginButton_Clicked(object sender, RoutedEventArgs e)
        {
            await PerformLogin();
        }

        private async void StackPanel_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await PerformLogin();
            }
        }

        private async Task PerformLogin()
        {
            if (await ViewModel.PerformLogin())
            {
                Frame.GoBack();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Switch to a proper header bar for navigating backward and such
            Frame.GoBack();
        }
    }
}
