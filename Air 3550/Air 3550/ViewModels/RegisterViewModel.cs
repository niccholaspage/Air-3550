using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class RegisterViewModel : ObservableValidator
    {
        private string _fullName;

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private double _age = double.NaN;

        public double Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _city;

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _state;

        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _zipCode;

        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value);
        }

        private string _creditCardNumber;

        public string CreditCardNumber
        {
            get => _creditCardNumber;
            set => SetProperty(ref _creditCardNumber, value);
        }
    }
}
