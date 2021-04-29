// SummaryPage.xaml.cs - Air 3550 Project
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
 * This page is used by the flight manager and the
 * accountant so that they can see all of the scheduled
 * flights and filter them by date. The accountant sees
 * the income of each scheduled flight as well as the
 * total income across the company, while the flight
 * manager can generate flight manifests for every
 * scheduled flight.
 */

using System;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    public sealed partial class SummaryPage : Page
    {
        readonly SummaryViewModel ViewModel = new();

        // On construction of the summary page,
        // we register the loaded event to
        // tell the view model to update the scheduled
        // flights so we can display them.
        public SummaryPage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.UpdateScheduledFlightsDate();
        }

        // On click of the Generate CSV button the user
        // will be shown a file save picker and allowed
        // to  select the location that the user would
        // like to save the CSV file to. The currently
        // shown flights will be saved to that CSV file.
        public async void SaveCSV_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveSummary();
        }

        // On the click of the Update Dates button,
        // the currently displayed scheduled flights will
        // be updated to be only those within the range
        // that the user requested.
        public async void UpdateDates_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.UpdateScheduledFlightsDate();
        }

        // On click of Show Manifest button, a manifest will
        // in a dialog, where a grid shows every passenger that
        // will be on the flight.
        public async void ShowManifest_Click(object sender, RoutedEventArgs e)
        {
            //Grabs the origin of the button that was clicked.
            Button button = (Button)sender;
            var scheduledFlightWithManifest = (ScheduledFlightWithManifest)button.CommandParameter;

            //Shows the dialogue page for the specific scheduled flight.
            ShowManifestDialog dialog1 = new(scheduledFlightWithManifest.ScheduledFlight);
            dialog1.XamlRoot = this.Content.XamlRoot;
            await dialog1.ShowAsync();
        }
    }
}
