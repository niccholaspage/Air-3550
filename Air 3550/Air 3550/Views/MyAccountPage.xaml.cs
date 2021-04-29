// MyAccountPage.xaml.cs - Air 3550 Project
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
 * visit their account info after they are
 * signed in. For all users, it shows a
 * change password tab. For customers, it
 * shows an additional bookings tab as well
 * as a profile tab.
 */

using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    public sealed partial class MyAccountPage : Page
    {
        public MyAccountPage()
        {
            this.InitializeComponent();
        }

        readonly MyAccountViewModel ViewModel = new(); // Construct the view model.

        // When the user clicks the bookings button,
        // we tell the view model to set the current
        // tab to the bookings tab.
        private void BookingsButton_Click(object _, RoutedEventArgs __)
        {
            ViewModel.SetCurrentTab(MyAccountViewModel.Tab.BOOKINGS);
        }

        // When the user clicks the profile button,
        // we tell the view model to set the current
        // tab to the profile tab.
        private void ProfileButton_Click(object _, RoutedEventArgs __)
        {
            ViewModel.SetCurrentTab(MyAccountViewModel.Tab.PROFILE);
        }

        // When the user clicks the change password
        // button, we tell the view model to set the
        // current tab to the change password tab.
        private void ChangePasswordButton_Click(object _, RoutedEventArgs __)
        {
            ViewModel.SetCurrentTab(MyAccountViewModel.Tab.CHANGE_PASSWORD);
        }
    }
}
