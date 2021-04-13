using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
            set => SetProperty(ref _fullName, value, true);
        }

        private string _password;

        [Required]
        [MinLength(1)]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, true);
        }

        private string _confirmPassword;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value, true);
        }

        private double _age = double.NaN;

        [Required]
        public double Age
        {
            get => _age;
            set => SetProperty(ref _age, value, true);
        }

        private string _phoneNumber;

        [Required]
        [Phone]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value, true);
        }

        private string _address;

        [Required]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value, true);
        }

        private string _city;

        [Required]
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value, true);
        }

        private string _state;

        [Required]
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value, true);
        }

        private string _zipCode;

        [Required]
        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value, true);
        }

        private string _creditCardNumber;

        [Required]
        [MinLength(15)]
        [MaxLength(16)]
        // TODO: Figure out how to exclude dashes when doing validation
        public string CreditCardNumber
        {
            get => _creditCardNumber;
            set => SetProperty(ref _creditCardNumber, value, true);
        }
    }
}
