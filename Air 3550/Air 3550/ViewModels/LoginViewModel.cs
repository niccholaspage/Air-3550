// LoginViewModel.cs - Air 3550 Project
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
 * This view model is used to authenticate
 * and log the user into the system. It
 * checks usernames and passwords and
 * determines whether a username and password
 * combo will authenticate a user into the
 * system, then configures their sesion if
 * so.
 */

using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class LoginViewModel : ObservableObject
    {
        private readonly UserSessionService userSession;

        public LoginViewModel()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();
        }

        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        public async Task<bool> PerformLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Feedback = "Please enter a username or password.";

                return false;
            }

            using (var db = new AirContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(user => user.LoginId == Username);

                if (user == null)
                {
                    Feedback = "The username or password is incorrect.";

                    return false;
                }
                else
                {
                    if (PasswordHandling.CheckPassword(Password, user.PasswordHash))
                    {
                        if (user.Role == Role.CUSTOMER)
                        {
                            var customerData = await db.CustomerDatas.SingleAsync(customerData => customerData.UserId == user.UserId);
                            userSession.Login(user, customerData.CustomerDataId);
                        }
                        else
                        {
                            userSession.Login(user, null);
                        }

                        Feedback = null;

                        return true;
                    }
                    else
                    {
                        Feedback = "The username or password is incorrect.";

                        return false;
                    }
                }
            }
        }
    }
}
