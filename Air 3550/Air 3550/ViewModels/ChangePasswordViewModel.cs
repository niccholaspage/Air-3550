// ChangePasswordViewModel.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

using Air_3550.Repository;
using Air_3550.Services;
using Air_3550.Util;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
                    return new ValidationResult("Your current password is incorrect.");
                }
            }
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            private set => SetProperty(ref _feedback, value);
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

            Feedback = this.GetFirstError();

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

            userSessionService.Logout();

            return true;
        }
    }
}
