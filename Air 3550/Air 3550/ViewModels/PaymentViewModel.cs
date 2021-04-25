using Air_3550.Repository;
using Air_3550.Services;
using Air_3550.Util;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Air_3550.ViewModels
{
    class PaymentViewModel : ObservableObject
    {
        private readonly UserSessionService userSession;

        public PaymentViewModel()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();
        }

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

        // A point corresponds to a single cent, so we
        // multiply the cost by 100 to get the total cost
        // in points.
        public int TotalCostInPoints => (int)(TotalCost * 100);

        public bool IsReturnFlight => ReturnFlightPath != null;

        public async Task<bool> PurchaseTrip()
        {
            using (var db = new AirContext())
            {
                var customerDataBalances = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == userSession.UserId)
                    .Select(customerData => new { customerData.RewardPointsBalance, customerData.AccountBalance })
                    .SingleAsync();

                if (SelectedPaymentMethod == PaymentMethod.CREDIT_CARD)
                {
                    Feedback = "Going with credit card, this is always valid.";
                }
                else if (SelectedPaymentMethod == PaymentMethod.ACCOUNT_BALANCE)
                {
                    if (customerDataBalances.AccountBalance < TotalCost)
                    {
                        Feedback = "You do not have enough money in your account balance.";

                        return false;
                    }
                }
                else
                {
                    if (customerDataBalances.RewardPointsBalance < TotalCostInPoints)
                    {
                        Feedback = "You do not have enough points.";

                        return false;
                    }
                }
            }

            Feedback = "Passed.";

            return true;
        }
    }
}
