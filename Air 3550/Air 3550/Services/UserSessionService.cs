// UserSessionService.cs - Air 3550 Project
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
 * This class is registered as a service
 * by the service provider in App.xaml.cs
 * and provides the currently logged in
 * user ID, customer data ID if applicable,
 * and the role of the user.
 */

using Air_3550.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.Services
{
    class UserSessionService : ObservableObject
    {
        private int? _userId;

        // A property representing the currently
        // logged in user's ID.
        public int? UserId
        {
            get => _userId;
            private set
            {
                SetProperty(ref _userId, value);

                // When the property is set, we make sure
                // to notify listeners that the IsLoggedIn
                // property changed.
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        private int? _customerDataId;

        // A property representing the currently
        // logged in user's customer data ID, if
        // applicable.
        public int? CustomerDataId
        {
            get => _customerDataId;
            private set => SetProperty(ref _customerDataId, value);
        }

        private Role? _role;

        // A property representing the currently
        // logged in user's role.
        public Role? Role
        {
            get => _role;
            private set => SetProperty(ref _role, value);
        }

        // This method should be called when a user needs
        // be logged in. This sets the user ID, role, and
        // customer data ID of the session.
        public void Login(User user, int? customerDataId)
        {
            UserId = user.UserId;

            Role = user.Role;

            CustomerDataId = customerDataId;
        }

        // This property simply returns whether a user
        // is logged into the system or not.
        public bool IsLoggedIn => UserId != null;

        // This method logs out a user by clearing out
        // the user ID, customer data ID, and role of
        // session.
        public void Logout()
        {
            UserId = null;
            CustomerDataId = null;
            Role = null;
        }
    }
}
