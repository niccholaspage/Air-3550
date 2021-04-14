using Air_3550.Models;
using Air_3550.Repository;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class RegisterViewModel : ObservableValidator
    {
        private string _fullName;

        [Required(ErrorMessage = "Please enter your full name.")]
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _password;

        [Required(ErrorMessage = "Please enter your password.")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private int? _age = null;

        [Required(ErrorMessage = "Please enter your age.")]
        [Range(0.0, 200.0)]
        public int? Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private string _phoneNumber;

        [Required(ErrorMessage = "Please enter your phone number.")]
        [Phone]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string _address;

        [Required(ErrorMessage = "Please enter your address.")]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _city;

        [Required(ErrorMessage = "Please enter your city.")]
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _state;

        [Required(ErrorMessage = "Please enter your state.")]
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _zipCode;

        [Required(ErrorMessage = "Please enter your postal code.")]
        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value);
        }

        private string _creditCardNumber;

        [Required(ErrorMessage = "Please enter your credit card number.")]
        [MinLength(15, ErrorMessage = "Please enter a valid credit card number.")]
        [MaxLength(16, ErrorMessage = "Please enter a valid credit card number.")]
        // TODO: Figure out how to exclude dashes when doing validation
        public string CreditCardNumber
        {
            get => _creditCardNumber;
            set => SetProperty(ref _creditCardNumber, value);
        }

        public async Task<string> CreateAccount()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return null;
            }

            using (var db = new AirContext())
            {
                Random random = new();

                string generatedId;

                while (true)
                {
                    // Generates an ID between 100000 (because user IDs cannot start with zero) and 1,000,000 exclusive
                    generatedId = random.Next(100_000, 1_000_000).ToString();
                    if (await db.Users.SingleOrDefaultAsync(user => user.LoginId == generatedId) == null)
                    {
                        break;
                    }
                }

                var user = new User
                {
                    LoginId = generatedId,
                    PasswordHash = PasswordHandling.HashPassword(Password),
                    Role = Role.CUSTOMER
                };

                await db.AddAsync(user);

                await db.CustomerDatas.AddAsync(new CustomerData
                {
                    User = user,
                    Name = FullName,
                    Age = (int)Age,
                    PhoneNumber = PhoneNumber,
                    Address = Address,
                    ZipCode = ZipCode,
                    City = City,
                    State = State,
                    CreditCardNumber = CreditCardNumber
                });

                db.SaveChanges();

                return user.LoginId;
            }
        }
    }
}
