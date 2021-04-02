using Microsoft.UI.Xaml;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Air_3550.Models;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            Title = "Air 3550";
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var booking = new Booking { Tickets = new List<Ticket>() };
                booking.Tickets.Add(new Ticket { IsCanceled = true });
                await db.AddAsync(booking);
                await db.SaveChangesAsync();
            }
        }
    }
}
