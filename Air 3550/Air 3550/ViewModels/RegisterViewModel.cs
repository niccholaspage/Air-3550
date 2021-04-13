using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class RegisterViewModel : ObservableValidator
    {
        private string _fullName;

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value, true);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, true);
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value, true);
        }

        private double _age = double.NaN;

        public double Age
        {
            get => _age;
            set => SetProperty(ref _age, value, true);
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value, true);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value, true);
        }

        private string _city;

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value, true);
        }

        private string _state;

        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value, true);
        }

        private string _zipCode;

        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value, true);
        }

        private string _creditCardNumber;

        public string CreditCardNumber
        {
            get => _creditCardNumber;
            set => SetProperty(ref _creditCardNumber, value, true);
        }
    }
}
