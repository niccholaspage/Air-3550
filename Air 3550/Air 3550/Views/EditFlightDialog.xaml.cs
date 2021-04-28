// EditFlightDialog.xaml.cs - Air 3550 Project
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
 * This class is for the edit flight dialog,
 * allowing load engineers to edit flights for
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
    public sealed partial class EditFlightDialog : ContentDialog
    {
        readonly EditFlightViewModel ViewModel; // Our variable to hold the view model.

        public EditFlightDialog(Flight flight)
        {
            this.InitializeComponent();
            ViewModel = new(flight); // Construct the view model.
        }

        // When the edit flight button is clicked, we
        // simply defer to the view model, and cancel
        // the closing of the dialog if the flight does
        // not get edited due to validation issues.
        public async void EditFlight_Click(object _, ContentDialogButtonClickEventArgs e)
        {
            var result = await ViewModel.EditFlight();

            if (result == null)
            {
                e.Cancel = true;
            }
        }
    }

}
