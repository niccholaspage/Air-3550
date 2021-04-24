using Air_3550.Util;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        public class Params
        {
            public FlightPath DepartingFlightPath;
            public FlightPath ReturnFlightPath;

            public Params(FlightPath departingFlightPath, FlightPath returnFlightPath)
            {
                DepartingFlightPath = departingFlightPath;
                ReturnFlightPath = returnFlightPath;
            }
        }

        readonly PaymentViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var param = e.Parameter as Params;

            ViewModel.DepartingFlightPath = param.DepartingFlightPath;
            ViewModel.ReturnFlightPath = param.ReturnFlightPath;

            DepartureFlightPathControl.DataContext = ViewModel.DepartingFlightPath;
        }

        public PaymentPage()
        {
            this.InitializeComponent();
        }
    }
}
