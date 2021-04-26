using Database.Util;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using Database.Util;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        private Params pageParams;

        public class Params
        {
            public FlightPathWithDate DepartingFlightPathWithDate;
            public FlightPathWithDate ReturnFlightPathWithDate;

            public Params(FlightPathWithDate departingFlightPathWithDate, FlightPathWithDate returnFlightPathWithDate)
            {
                DepartingFlightPathWithDate = departingFlightPathWithDate;
                ReturnFlightPathWithDate = returnFlightPathWithDate;
            }
        }

        public PaymentPage()
        {
            this.InitializeComponent();

            this.Loaded += async (_, __) => await ViewModel.FetchBalances();
        }

        public string GetFormattedTotalCost()
        {
            return ViewModel.TotalCost.FormatAsMoney();
        }

        readonly PaymentViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pageParams = e.Parameter as Params;

            ViewModel.DepartingFlightPathWithDate = pageParams.DepartingFlightPathWithDate;
            ViewModel.ReturnFlightPathWithDate = pageParams.ReturnFlightPathWithDate;

            DepartureFlightPathControl.DataContext = ViewModel.DepartingFlightPathWithDate;

            if (ViewModel.ReturnFlightPathWithDate != null)
            {
                ReturnFlightPathControl.DataContext = ViewModel.ReturnFlightPathWithDate;
            }
        }

        private async void PurchaseButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.PurchaseTrip())
            {
                Frame.Navigate(typeof(MainPage), true);

                Frame.BackStack.Clear();
            }
        }
    }
}
