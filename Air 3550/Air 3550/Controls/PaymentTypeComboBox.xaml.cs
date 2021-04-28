// PaymentTypeComboBox.xaml.cs - Air 3550 Project
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

// This control simply wraps a ComboBox
// and populates it with payment method
// from the PaymentMethod enumeration.

using System;
using Database.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Controls
{
    public sealed partial class PaymentTypeComboBox : UserControl
    {
        // We define a property consumers can bind to
        // to get the selected payment method of the
        // combo box.
        public PaymentMethod SelectedPaymentMethod
        {
            get => (PaymentMethod)GetValue(SelectedPaymentMethodProperty);
            set => SetValue(SelectedPaymentMethodProperty, value);
        }

        public static readonly DependencyProperty SelectedPaymentMethodProperty = DependencyProperty.Register(nameof(SelectedPaymentMethod), typeof(PaymentMethod), typeof(EntitySuggestBox), new PropertyMetadata(default(PaymentMethod)));


        public PaymentTypeComboBox()
        {
            this.InitializeComponent();

            // We loop through each payment method and add the
            // nicely formatted version of it to the combo box.
            foreach (PaymentMethod paymentMethod in Enum.GetValues(typeof(PaymentMethod)))
            {
                PaymentComboBox.Items.Add(paymentMethod.FormattedString());
            }

            // We set the default selection to the first item.
            PaymentComboBox.SelectedIndex = 0;

            // When the selection changes, we make
            // sure to update our selected payment
            // method.
            PaymentComboBox.SelectionChanged += (_, __) =>
            {
                SelectedPaymentMethod = (PaymentMethod)PaymentComboBox.SelectedIndex;
            };
        }
    }
}
