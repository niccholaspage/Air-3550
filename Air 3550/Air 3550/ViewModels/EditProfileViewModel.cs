using Air_3550.Controls;
using Air_3550.Repository;
using Air_3550.Services;
using Air_3550.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditProfileViewModel : ObservableObject
    {
        private readonly UserSessionService userSession;

        private decimal _accountBalance;

        public decimal AccountBalance
        {
            get => _accountBalance;
            set => SetProperty(ref _accountBalance, value);
        }

        private int _rewardPoints;

        public int RewardPoints
        {
            get => _rewardPoints;
            set => SetProperty(ref _rewardPoints, value);
        }

        private int _totalRewardPointsUsed;

        public int TotalRewardPointsUsed
        {
            get => _totalRewardPointsUsed;
            set => SetProperty(ref _totalRewardPointsUsed, value);
        }

        public EditProfileViewModel()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            Task.Run(FetchBalances);
        }

        public async void FetchBalances()
        {
            using (var db = new AirContext())
            {
                var accountBalance = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == userSession.UserId)
                .Select(customerData => customerData.AccountBalance)
                .SingleAsync();

                AccountBalance = accountBalance;

                var pointValues = await PointsHandler.UpdateAndRetrievePointsBalance(db);
                RewardPoints = pointValues.RewardPointsBalance;
                TotalRewardPointsUsed = pointValues.TotalRewardPointsUsed;
            }
        }

        public EditAccountInfoValidator Validator = new(false);

        public async Task<bool> SaveChanges()
        {
            Validator.ValidateAllProperties();

            if (Validator.HasErrors)
            {
                return false;
            }

            using (var db = new AirContext())
            {
                var customerData = await db.CustomerDatas.SingleAsync(customerData => customerData.User.UserId == userSession.UserId);

                customerData.Name = Validator.FullName;
                customerData.Age = (int)Validator.Age;
                customerData.PhoneNumber = Validator.PhoneNumber;
                customerData.Address = Validator.Address;
                customerData.City = Validator.City;
                customerData.State = Validator.State;
                customerData.ZipCode = Validator.ZipCode;
                customerData.CreditCardNumber = Validator.CreditCardNumber;

                await db.SaveChangesAsync();
            }

            // Due to a bug in WinUI, even though validate
            // all properties at this point will have reset
            // all errors, the UI doesn't update... So TODO:
            // figure out how to reset error state of fields
            // in the UI.

            return true;
        }
    }
}
