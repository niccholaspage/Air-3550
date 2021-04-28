// EditProfileSubPage.xaml.cs - Air 3550 Project
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
 * This edit profile subpage is a tab that appears in the
 * account info page that allows a customer to edit their
 * profile and view their account balance and remaining or
 * total reward points. It is only shown to customers.
 */

using System;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    public sealed partial class EditProfileSubPage : Page
    {
        // On construction of the edit profile subpage,
        // we register with the loaded event and
        // tell the view model to fetch the customer's
        // account balance and reward points.
        public EditProfileSubPage()
        {
            this.InitializeComponent();

            this.Loaded += async (_, __) => await ViewModel.FetchBalances();
        }

        readonly EditProfileViewModel ViewModel = new();

        // When the save changes button is clicked,
        // we want check the user's changes and
        // show a success dialog if it succeeds.
        // We defer to the view model to actually
        // attempt to save the changes.
        private async void SaveChangesButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.SaveChanges())
            {
                await SuccessDialog.ShowAsync();
            }
        }
    }
}
