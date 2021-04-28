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

using System;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    public sealed partial class SummaryPage : Page
    {
        readonly SummaryViewModel ViewModel = new();

        // On construction of the Summary page,
        // we register with the loaded event and
        // tell the view model to update the Scheduled
        // flights so we can display them.
        public SummaryPage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.UpdateScheduledFlightsDate();
        }

        // On the click of the Generate CSV
        // the user will be displayed a save window and allowed to        
        // select the location that the user would like to save
        // the CSV file to. The currently shown flights will be saved
        // to that CSV
        public async void SaveCSV_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveSummary();
        }

        // On the click of Update Dates
        // The currently displayed dates will be
        // updated to being only those within the range
        // that the user requested
        public async void UpdateDates_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.UpdateScheduledFlightsDate();
        }

        // On the click of Show Manifest button
        // A manifest will be displayed as a conent dialogue
        // inside the content dialoog is a grid that shows 
        // every passanger that will be on the flight
        public async void ShowManifest_Click(object sender, RoutedEventArgs e)
        {
            //Grabs the origin of the button that was clicked
            Button button = (Button)sender;
            var InterestedFlight = (ScheduledFlightWithManifest)button.CommandParameter;

            //Shows the dialogue page
            ShowManifestDialog dialog1 = new(InterestedFlight.ScheduledFlight);
            dialog1.XamlRoot = this.Content.XamlRoot;
            await dialog1.ShowAsync();


        }
    }
}
