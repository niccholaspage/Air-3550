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

        [Required]
        [MinLength(1)]
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _password;

        [Required]
        [MinLength(1)]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private int? _age = null;

        [Required]
        [Range(0.0, 200.0)]
        public int? Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private string _phoneNumber;

        [Required]
        [Phone]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string _address;

        [Required]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _city;

        [Required]
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _state;

        [Required]
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _zipCode;

        [Required]
        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value);
        }

        private string _creditCardNumber;

        [Required]
        [MinLength(15)]
        [MaxLength(16)]
        // TODO: Figure out how to exclude dashes when doing validation
        public string CreditCardNumber
        {
            get => _creditCardNumber;
            set => SetProperty(ref _creditCardNumber, value);
        }

        public async Task<bool> CreateAccount()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return false;
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
            }


            return true;
        }
    }
}
