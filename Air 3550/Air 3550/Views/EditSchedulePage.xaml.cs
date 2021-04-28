// EditSchedulePage.xaml.cs - Air 3550 Project
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
    public sealed partial class EditSchedulePage : Page
    {
        readonly EditScheduleViewModel ViewModel = new();

        public EditSchedulePage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.UpdateFlights();
        }


        private async void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            AddFlightDialog dialog = new();
            dialog.XamlRoot = Content.XamlRoot;
            var result = await dialog.ShowAsync();

            // Update flights if a new flight was added.
            if (result == ContentDialogResult.Primary)
            {
                await ViewModel.UpdateFlights();
            }
        }

        private async void RemoveFlight_Click(object sender, RoutedEventArgs _)
        {
            var button = (Button)sender;
            var flight = (FlightWithDeletionActive)button.CommandParameter;
            await ViewModel.CancelFlight(flight);
        }

        private async void EditFlight_Click(object sender, RoutedEventArgs _)
        {
            var button = (Button)sender;
            FlightWithDeletionActive flightWithDeletion = (FlightWithDeletionActive)button.CommandParameter;

            if (!ViewModel.IsLoadEngineer)
            {
                var dialog1 = new EditPlaneDialog(flightWithDeletion.Flight)
                {
                    XamlRoot = this.Content.XamlRoot
                };
                var result = await dialog1.ShowAsync();

                // Update flights if a flight was edited.
                if (result == ContentDialogResult.Primary)
                {
                    await ViewModel.UpdateFlights();
                }
            }
            else
            {
                EditFlightDialog dialog1 = new(flightWithDeletion.Flight);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();

                // Update flights if a flight was edited.
                if (result == ContentDialogResult.Primary)
                {
                    await ViewModel.UpdateFlights();
                }
            }
        }
    }
}
