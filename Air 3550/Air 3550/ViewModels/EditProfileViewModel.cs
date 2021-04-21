using Air_3550.Controls;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditProfileViewModel
    {
        private readonly UserSessionService _userSessionService;

        public EditProfileViewModel()
        {
            _userSessionService = App.Current.Services.GetService<UserSessionService>();
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
                var customerData = await db.CustomerDatas.SingleAsync(customerData => customerData.User.UserId == _userSessionService.UserId);

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
