using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Util;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class ChangePasswordViewModel : ObservableValidator
    {
        private readonly UserSessionService userSessionService = App.Current.Services.GetService<UserSessionService>();

        private string currentPasswordHash;

        public ChangePasswordViewModel()
        {
            using (var db = new AirContext())
            {
                // TODO: Make async?
                var user = db.Users.Single(user => user.UserId == userSessionService.UserId);

                currentPasswordHash = user.PasswordHash;
            }
        }

        private class CurrentPasswordValidator : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as ChangePasswordViewModel;

                if (PasswordHandling.CheckPassword(value as string, viewModel.currentPasswordHash))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Your current password was incorrect.");
                }
            }
        }

        private string _currentPassword;

        [Required(ErrorMessage = "Please enter your current password.")]
        [CurrentPasswordValidator]
        public string CurrentPassword
        {
            get => _currentPassword;
            set => SetProperty(ref _currentPassword, value);
        }

        private string _newPassword;

        [Required(ErrorMessage = "Please enter your new password.")]
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        private string _confirmNewPassword;

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set => SetProperty(ref _confirmNewPassword, value);
        }

        public async Task<bool> ChangePassword()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return false;
            }

            using (var db = new AirContext())
            {
                var user = await db.Users.SingleAsync(user => user.UserId == userSessionService.UserId);

                user.PasswordHash = PasswordHandling.HashPassword(_newPassword);

                currentPasswordHash = user.PasswordHash;

                await db.SaveChangesAsync();
            }

            return true;
        }
    }
}
