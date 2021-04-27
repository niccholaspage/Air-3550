
using Air_3550.Models;
using Air_3550.Services;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Microsoft.Extensions.DependencyInjection;
using static Air_3550.ViewModels.EditScheduleViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                EditPlaneDialog dialog1 = new EditPlaneDialog(flightWithDeletion.Flight);
                dialog1.XamlRoot = this.Content.XamlRoot;
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
