using Air_3550.ViewModels;
using Air_3550.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Database.Util;

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

        private async void TicketsDisplayedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel.SelectedTicket != null)
            {
                BoardingPass dialog1 = new(ViewModel.SelectedTicket, ViewModel.CustomerName);
                dialog1.XamlRoot = this.Content.XamlRoot;
                await dialog1.ShowAsync();
            }
        }

        private void BoardingPass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
