// ChangePasswordSubPage.xaml.cs - Air 3550 Project
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
 * The change password subpage is a tab that appears in the
 * account info page that allows a user to change their
 * password after they enter their current password and
 * new password.
 */

using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    public sealed partial class ChangePasswordSubPage : Page
    {
        public ChangePasswordSubPage()
        {
            this.InitializeComponent();
        }

        readonly ChangePasswordViewModel ViewModel = new(); // Construct the view model.

        private async void ChangePasswordButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.ChangePassword())
            {
                // This is so ugly... but we did not figure
                // out a way to make this better. On a successful
                // password change, we want to send the user to the
                // login page, so we clear the backstack and send the
                // user back to the login page.
                var parentContentControl = Parent as ContentControl; // get the parent's content control,

                var parentRelativePanel = parentContentControl.Parent as RelativePanel; // then get it's parent's relative panel,

                var parentPage = parentRelativePanel.Parent as Page; // then the panel's parent which is a page.

                // Now we can navigate the frame of the parent page to the main page, clear the
                // back stack, and then take them to the login page (passing in paremeters for
                // a password change so the login page tells the user).
                parentPage.Frame.Navigate(typeof(MainPage));
                parentPage.Frame.BackStack.Clear();
                parentPage.Frame.Navigate(typeof(LoginPage), new LoginPage.Params.PasswordChanged());
            }
        }
    }
}
