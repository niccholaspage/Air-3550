using Air_3550.Repository;
using Air_3550.Util;
using Database.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class PaymentViewModel : ObservableObject
    {
        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private FlightPath _departingFlightPath;

        public FlightPath DepartingFlightPath
        {
            get => _departingFlightPath;
            set => SetProperty(ref _departingFlightPath, value);
        }

        private FlightPath _returnFlightPath;

        public FlightPath ReturnFlightPath
        {
            get => _returnFlightPath;
            set => SetProperty(ref _returnFlightPath, value);
        }

        private PaymentMethod _selectedPaymentMethod;

        public PaymentMethod SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set => SetProperty(ref _selectedPaymentMethod, value);
        }

        public decimal TotalCost => DepartingFlightPath.Price + (ReturnFlightPath != null ? ReturnFlightPath.Price : 0.0m);

        public bool IsReturnFlight => ReturnFlightPath != null;

        public async Task<bool> PurchaseTrip()
        {
            if (SelectedPaymentMethod == PaymentMethod.CREDIT_CARD)
            {
                Feedback = "Going with credit card, this is always valid.";
            }
            else if (SelectedPaymentMethod == PaymentMethod.ACCOUNT_BALANCE)
            {
                Feedback = "Check the account balance.";
            }
            else
            {
                Feedback = "Check the points.";
            }

            return true;
        }
    }
}
