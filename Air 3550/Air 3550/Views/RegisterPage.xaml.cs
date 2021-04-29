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

/**
 * This page is shown to the user when they
 * need to register for an account. We do not
 * handle registration for elevated roles, so
 * when making an account through the registration
 * form, a customer will always be created.
 */

using System;
using System.Collections.Generic;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Air_3550.Views
{
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        // Holds the params of the login page's redirect
        // to bring it back over to the login page after
        // an account is created.
        private LoginPage.Params.RedirectToPage redirectParams;

        // Overides OnNavigatedTo to set the redirect params
        // if this page was given them so that we can pass them
        // back to the login page on successful account creation.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is LoginPage.Params.RedirectToPage redirect)
            {
                redirectParams = redirect;
            }
        }

        readonly RegisterViewModel ViewModel = new();  // Construct the view model.

        // On a click of the register button,
        // an account will be created with the 
        // information provided by the user as
        // long as it is validated by the view
        // model.
        private async void RegisterButton_Click(object _, RoutedEventArgs __)
        {
            var loginId = await ViewModel.CreateAccount();

            //Checks if an account was created
            if (loginId != null)
            {
                // If so, we construct a new parameter for the login page,
                // passing in the new user ID so the page will be able to
                // display it.
                var newUserParams = new LoginPage.Params.NewUser(loginId);

                // We also make sure to include the redirect parameters
                // if our register page has them. If so, we pass a list
                // of parameters to the login page. Otherwise, we just
                // pass the new user parameters.
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
