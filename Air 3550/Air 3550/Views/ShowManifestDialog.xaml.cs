// ShowManifestDialog.xaml.cs - Air 3550 Project
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
 * This dialog is shown to the flight manager
 * when they click the button in the summary
 * to view the flight manifest. This contains
 * a grid of each name on a scheduled flight.
 */

using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    public sealed partial class ShowManifestDialog : ContentDialog
    {
        // On construction of the ShowManifestDialog,
        // a new view model is created with the object
        // of the scheduled fight that the user
        // requested to view.
        public ShowManifestDialog(ScheduledFlight scheduledFlight)
        {
            this.InitializeComponent();

            //Create the new view model
            ViewModel = new(scheduledFlight);
        }

        readonly ShowManifestViewModel ViewModel; // Construct the view model.
    }
}
