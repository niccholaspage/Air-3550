// AddFlightDialog.xaml.cs - Air 3550 Project
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
 * This class is for the add flight dialog,
 * allowing load engineers to add flights to
 * the airline.
 */

using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class AddFlightDialog : ContentDialog
    {
        public AddFlightDialog()
        {
            this.InitializeComponent();
        }

        readonly AddFlightViewModel ViewModel = new(); // Construct the view model.

        // When the add flight button is clicked, we
        // simply defer to the view model, and cancel
        // the closing of the dialog if the flight does
        // not get created due to validation issues.
        private async void AddFlight_Click(object _, ContentDialogButtonClickEventArgs e)
        {
            Flight result = await ViewModel.CreateFlight();

            if (result == null)
            {
                e.Cancel = true;
            }
        }
    }
}
