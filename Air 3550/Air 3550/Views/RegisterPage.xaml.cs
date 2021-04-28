// RegisterPage.xaml.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

using System;
using System.Collections.Generic;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
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
