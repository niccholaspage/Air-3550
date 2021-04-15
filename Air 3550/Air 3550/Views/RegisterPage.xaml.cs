﻿using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
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

        readonly RegisterViewModel ViewModel = new();

        private async void RegisterButton_Click(object _, RoutedEventArgs _1)
        {
            var loginId = await ViewModel.CreateAccount();

            if (loginId != null)
            {
                Frame.Navigate(typeof(LoginPage), loginId);
            }
        }
    }
}
