﻿using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditSchedulePage : Page
    {

        //readonly EditScheduleViewModel ViewModel = new();

        public EditSchedulePage()
        {
            this.InitializeComponent();
            ViewModel.UpdateFlights();

        }

        readonly EditScheduleViewModel ViewModel = new();

        private async void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            AddFlightDialog dialog1 = new();
            dialog1.XamlRoot = this.Content.XamlRoot;
            var result = await dialog1.ShowAsync();
            //Update if something changed
            if (dialog1.Result != null)
            {
                ViewModel.UpdateFlights();
                ViewModel.Feedback = "FlightID:" + dialog1.Result.Number;
            };
        }

        private async void RemoveFlight_Click(object sender, RoutedEventArgs e)
        {
            Flight cancel = (Flight)displayedList.SelectedItem;
            if (cancel != null)
            {
                await ViewModel.CancelFlight(cancel);
                ViewModel.UpdateFlights();
            }
        }

        private async void EditFlight_Click(object sender, RoutedEventArgs e)
        {
            Flight edit = (Flight)displayedList.SelectedItem;
            if (edit != null)
            {
                EditFlightDialog dialog1 = new(edit);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null) ViewModel.UpdateFlights();
            }
        }

        private void EditPlanes_Click(object _, RoutedEventArgs _1)
        {
            Frame.Navigate(typeof(EditPlanesPage));
        }

    }
}
