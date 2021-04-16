﻿using Air_3550.Models;
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

        private Role? _role;

        public Role? Role
        {
            get => _role;
            private set => SetProperty(ref _role, value);
        }

        public void Login(User user)
        {
            UserId = user.UserId;

            Role = user.Role;
        }

        public bool IsLoggedIn
        {
            get => UserId != null;
        }

        public void Logout()
        {
            UserId = null;
            Role = null;
        }
    }
}