
using Air_3550.Models;
using Air_3550.Services;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Microsoft.Extensions.DependencyInjection;

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

        private readonly UserSessionService userSessionService;

        public EditSchedulePage()
        {
            userSessionService = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.UpdateFlights();
        }


        private async void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            AddFlightDialog dialog = new();
            dialog.XamlRoot = Content.XamlRoot;
            await dialog.ShowAsync();

            //Update if something changed
            if (dialog.Result != null)
            {
                await ViewModel.UpdateFlights();
            }
        }

        private async void RemoveFlight_Click(object sender, RoutedEventArgs _)
        {
            var button = (Button)sender;
            var flight = (Flight)button.CommandParameter;
            await ViewModel.CancelFlight(flight);
        }

        private async void EditFlight_Click(object sender, RoutedEventArgs _)
        {
            var button = (Button)sender;
            Flight flight = (Flight)button.CommandParameter;

            if (userSessionService.Role == Role.MARKETING_MANAGER)
            {
                EditPlaneDialog dialog1 = new EditPlaneDialog(flight);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null) await ViewModel.UpdateFlights();
            }
            else
            {
                EditFlightDialog dialog1 = new(flight);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null)
                {
                    await ViewModel.UpdateFlights();
                }
            }
        }
    }
}
