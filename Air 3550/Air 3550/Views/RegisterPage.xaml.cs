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
        
        //On construction a register page is created
        // for the user to enter informaiton in to 
        // create a user for the Air 3550 system
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        // Holds the params of the a log in page to bring
        // The userID over to the log in page
        private LoginPage.Params.RedirectToPage redirectParams;

        // Overides OnNavigatedTo to bring redirectParams
        // if params are RedirectParams then RedirectParams
        // Will be passed
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is LoginPage.Params.RedirectToPage redirect)
            {
                redirectParams = redirect;
            }
        }

        readonly RegisterViewModel ViewModel = new();

        // On click of the Register Button
        // An account will be created with the 
        // Information provided by the user after
        // being validated
        private async void RegisterButton_Click(object _, RoutedEventArgs __)
        {
            var loginId = await ViewModel.CreateAccount();

            //Checks if an account was created
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
