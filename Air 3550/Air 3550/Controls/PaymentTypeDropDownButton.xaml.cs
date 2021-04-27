// PaymentTypeDropDownButton.xaml.cs - Air 3550 Project
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

using System;
using Database.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public sealed partial class PaymentTypeDropDownButton : UserControl
    {
        public static readonly DependencyProperty SelectedPaymentMethodProperty = DependencyProperty.Register(nameof(SelectedPaymentMethod), typeof(PaymentMethod), typeof(EntitySuggestBox), new PropertyMetadata(default(PaymentMethod)));

        public PaymentMethod SelectedPaymentMethod
        {
            get => (PaymentMethod)GetValue(SelectedPaymentMethodProperty);
            set => SetValue(SelectedPaymentMethodProperty, value);
        }

        public PaymentTypeDropDownButton()
        {
            this.InitializeComponent();

            foreach (PaymentMethod paymentMethod in Enum.GetValues(typeof(PaymentMethod)))
            {
                PaymentComboBox.Items.Add(paymentMethod.FormattedString());
            }

            PaymentComboBox.SelectedIndex = 0;

            PaymentComboBox.SelectionChanged += (_, __) =>
            {
                SelectedPaymentMethod = (PaymentMethod)PaymentComboBox.SelectedIndex;
            };
        }
    }
}
