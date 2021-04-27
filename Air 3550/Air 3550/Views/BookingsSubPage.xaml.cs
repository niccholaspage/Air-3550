using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookingSubPage : Page
    {
        public BookingSubPage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.GetBookings();
        }

        readonly BookingsViewModel ViewModel = new();

        private async void BoardingPass_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var ticket = (Ticket)button.CommandParameter;
            BoardingPass dialog1 = new(ticket, ViewModel.CustomerName);
            dialog1.XamlRoot = this.Content.XamlRoot;
            await dialog1.ShowAsync();
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var flyout = (Flyout) button.FindName("CancelFlyout");
            flyout.Hide();

            var booking = (Booking)button.CommandParameter;
            await ViewModel.CancelBooking(booking);
            booking.Tickets.First().IsCanceled = true;
        }
    }
}
