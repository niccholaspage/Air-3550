// BoardingPassDialog.xaml.cs - Air 3550 Project
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
 * This class is for the boarding pass dialog,
 * allowing customers to view their boarding
 * pass for flights that will be departing.
 */

using Air_3550.Models;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    public sealed partial class BoardingPassDialog : ContentDialog
    {
        public Ticket Ticket;       // We store the ticket,

        public string CustomerName; // customer name of the user,

        public string LoginId;      // and login ID (account number).

        // We just set the three fields of our class to the
        // parameters passed into the constructor so that we
        // can bind to these fields in the UI.
        public BoardingPassDialog(Ticket ticket, string customerName, string loginId)
        {
            this.InitializeComponent();
            Ticket = ticket;
            CustomerName = customerName;
            LoginId = loginId;
        }
    }
}
