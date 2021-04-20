using Air_3550.Controls;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditProfileViewModel
    {
        public EditAccountInfoValidator Validator = new(false);

        public async Task<bool> SaveChanges()
        {
            Validator.ValidateAllProperties();

            if (Validator.HasErrors)
            {
                return false;
            }

            return true;
        }
    }
}
