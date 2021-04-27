// EditAccountInfoControl.xaml.cs - Air 3550 Project
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public class EditAccountInfoValidator : ObservableValidator
    {
        public bool IsRegistering;

        public EditAccountInfoValidator(bool isRegistering)
        {
            IsRegistering = isRegistering;

            _feedback = "";

            var userService = App.Current.Services.GetService<UserSessionService>();

            if (!IsRegistering)
            {
                using (var db = new AirContext())
                {
                    // TODO: How do we make this async?
                    var customerData = db.CustomerDatas.Single(customerData => customerData.User.UserId == userService.UserId);

                    FullName = customerData.Name;
                    Age = customerData.Age;
                    PhoneNumber = customerData.PhoneNumber;
                    Address = customerData.Address;
                    City = customerData.City;
                    State = customerData.State;
                    ZipCode = customerData.ZipCode;
                    CreditCardNumber = customerData.CreditCardNumber;
                }
            }
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            private set => SetProperty(ref _feedback, value);
        }

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

        public new void ValidateAllProperties()
        {
            base.ValidateAllProperties();

            if (!IsRegistering)
            {
                // If this is not being used for registering,
                // we ignore any password related errors since
                // we do not need the values of those fields.
                ClearErrors(nameof(Password));
                ClearErrors(nameof(ConfirmPassword));
            }

            Feedback = this.GetFirstError();
        }
    }

    public sealed partial class EditAccountInfoControl : UserControl
    {
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(EditAccountInfoControl), new PropertyMetadata(default(string)));

        public string ActionButtonText
        {
            get => (string)GetValue(ActionButtonTextProperty);
            set => SetValue(ActionButtonTextProperty, value);
        }

        public static readonly DependencyProperty ActionButtonTextProperty = DependencyProperty.Register(nameof(ActionButtonText), typeof(string), typeof(EditAccountInfoControl), new PropertyMetadata(default(string)));

        public EditAccountInfoValidator Validator
        {
            get => (EditAccountInfoValidator)GetValue(ValidatorProperty);
            set => SetValue(ValidatorProperty, value);
        }

        public static readonly DependencyProperty ValidatorProperty = DependencyProperty.Register(nameof(Validator), typeof(EditAccountInfoValidator), typeof(EditAccountInfoControl), new PropertyMetadata(default(EditAccountInfoValidator)));

        public event RoutedEventHandler Click;

        public EditAccountInfoControl()
        {
            this.InitializeComponent();
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            Click(sender, e);
        }
    }
}
