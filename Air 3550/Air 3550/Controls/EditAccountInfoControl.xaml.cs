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

/**
 * This control can be used to allow a customer
 * to edit their profile, changing their name,
 * age, phone number, and other fields. It can
 * also be used in registration mode, allowing
 * it to be used when a customer is in the process
 * of signing up for the program.
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Air_3550.Repository;
using Air_3550.Services;
using Air_3550.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Controls
{
    // We first define a validator to properly handle
    // data validation for each field in the control.
    public class EditAccountInfoValidator : ObservableValidator
    {
        // A flag that changes the behavior of the form.
        // When it is set to false, the control will
        // automatically display the customer's current
        // data and allow for it to be edited.
        public bool IsRegistering;

        public EditAccountInfoValidator(bool isRegistering)
        {
            IsRegistering = isRegistering;

            // Initialize the feedback to an empty string.
            _feedback = "";

            // If the user is not registering, we need to fill
            // all the fields with the current customer data.
            if (!IsRegistering)
            {
                // Fetch the user session service.
                var userService = App.Current.Services.GetService<UserSessionService>();

                using (var db = new AirContext()) // Create a new database context
                {
                    // We fetch the customer data by the customer data ID saved in our session.
                    var customerData = db.CustomerDatas.Find(userService.CustomerDataId);

                    // We now set all fields in our validator
                    // to the customer data from the database.
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

        // We define a property for feedback that will be shown
        // if the user enters invalid data into the form.
        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            private set => SetProperty(ref _feedback, value);
        }

        // The customer's full name, which is required.
        private string _fullName;

        [Required(ErrorMessage = "Please enter your full name.")]
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        // The user's password, which is required.
        private string _password;

        [Required(ErrorMessage = "Please enter your password.")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        // A confirm password field, used to confirm
        // the user inputs the correct password.
        private string _confirmPassword;

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        // The customer's age, which is required.
        private int? _age = null;

        [Required(ErrorMessage = "Please enter your age.")]
        [Range(0.0, 200.0)]
        public int? Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        // The customer's phone number, which is required.
        private string _phoneNumber;

        [Required(ErrorMessage = "Please enter your phone number.")]
        [Phone]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        // The customer's address, which is required.
        private string _address;

        [Required(ErrorMessage = "Please enter your address.")]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _city;

        // The customer's city, which is required.
        [Required(ErrorMessage = "Please enter your city.")]
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _state;

        // The customer's state, which is required.
        [Required(ErrorMessage = "Please enter your state.")]
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _zipCode;

        // The customer's ZIP code, which is required.
        [Required(ErrorMessage = "Please enter your postal code.")]
        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value);
        }

        // The customer's credit card number, which is
        // required and is validated based on length.
        // While we could use the built-in CreditCard
        // data annotation, it actually do basic
        // input validation on what numbers you enter,
        // so we don't use it here.
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

        // We overide the ValidateAllProperties method and
        // expose it to callers. This method checks if
        // all the fields validate, and clears errors for
        // the password fields if we are not registering,
        // since we will not be entering a password if
        // so. It then sets the feedback to the first
        // error of the validation.
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
        // The header of the control, which shows above the field.
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(EditAccountInfoControl), new PropertyMetadata(default(string)));

        // The text of the button that shows
        // at the bottom right of the control.
        public string ActionButtonText
        {
            get => (string)GetValue(ActionButtonTextProperty);
            set => SetValue(ActionButtonTextProperty, value);
        }

        public static readonly DependencyProperty ActionButtonTextProperty = DependencyProperty.Register(nameof(ActionButtonText), typeof(string), typeof(EditAccountInfoControl), new PropertyMetadata(default(string)));

        // The validator that we expose allows users of
        // this control to bind their own instance of the
        // validator class so that they can get all the
        // fields in their action button click handler.
        public EditAccountInfoValidator Validator
        {
            get => (EditAccountInfoValidator)GetValue(ValidatorProperty);
            set => SetValue(ValidatorProperty, value);
        }

        public static readonly DependencyProperty ValidatorProperty = DependencyProperty.Register(nameof(Validator), typeof(EditAccountInfoValidator), typeof(EditAccountInfoControl), new PropertyMetadata(default(EditAccountInfoValidator)));

        // The event that is called when the action button
        // is clicked. This allows users of the control to
        // register a user for example or edit a customer's
        // profile.
        public event RoutedEventHandler Click;

        public EditAccountInfoControl()
        {
            this.InitializeComponent();
        }

        // When the button is clicked, call the Click event.
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            Click(sender, e);
        }
    }
}
