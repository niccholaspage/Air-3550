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

using Air_3550.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.Services
{
    class UserSessionService : ObservableObject
    {
        private int? _userId;

        public int? UserId
        {
            get => _userId;
            private set
            {
                SetProperty(ref _userId, value);

                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        private int? _customerDataId;

        public int? CustomerDataId
        {
            get => _customerDataId;
            private set => SetProperty(ref _customerDataId, value);
        }

        private Role? _role;

        public Role? Role
        {
            get => _role;
            private set => SetProperty(ref _role, value);
        }

        public void Login(User user, int? customerDataId)
        {
            UserId = user.UserId;

            Role = user.Role;

            CustomerDataId = customerDataId;
        }

        public bool IsLoggedIn
        {
            get => UserId != null;
        }

        public void Logout()
        {
            UserId = null;
            CustomerDataId = null;
            Role = null;
        }
    }
}
