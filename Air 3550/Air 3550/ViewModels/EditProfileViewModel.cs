// EditProfileViewModel.cs - Air 3550 Project
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

using Air_3550.Controls;
using Air_3550.Repository;
using Air_3550.Services;
using Air_3550.Util;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditProfileViewModel : ObservableObject
    {
        private readonly UserSessionService userSession;

        private string _formattedAccountBalance;

        public string FormattedAccountBalance
        {
            get => _formattedAccountBalance;
            set => SetProperty(ref _formattedAccountBalance, value);
        }

        private string _formattedRewardPoints;

        public string FormattedRewardPoints
        {
            get => _formattedRewardPoints;
            set => SetProperty(ref _formattedRewardPoints, value);
        }

        private string _formattedTotalRewardPointsUsed;

        public string FormattedTotalRewardPointsUsed
        {
            get => _formattedTotalRewardPointsUsed;
            set => SetProperty(ref _formattedTotalRewardPointsUsed, value);
        }

        public EditProfileViewModel()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();
        }

        public async Task FetchBalances()
        {
            using (var db = new AirContext())
            {
                var accountBalance = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == userSession.UserId)
                .Select(customerData => customerData.AccountBalance)
                .SingleAsync();

                FormattedAccountBalance = "Account Balance: " + accountBalance.FormatAsMoney();

                var pointValues = await PointsHandler.UpdateAndRetrievePointsBalance(db);

                FormattedRewardPoints = "Reward Points: " + pointValues.RewardPointsBalance;
                FormattedTotalRewardPointsUsed = "Total Reward Points Used: " + pointValues.TotalRewardPointsUsed;
            }
        }

        public EditAccountInfoValidator Validator = new(false);

        public async Task<bool> SaveChanges()
        {
            Validator.ValidateAllProperties();

            if (Validator.HasErrors)
            {
                return false;
            }

            using (var db = new AirContext())
            {
                var customerData = await db.CustomerDatas.SingleAsync(customerData => customerData.User.UserId == userSession.UserId);

                customerData.Name = Validator.FullName;
                customerData.Age = (int)Validator.Age;
                customerData.PhoneNumber = Validator.PhoneNumber;
                customerData.Address = Validator.Address;
                customerData.City = Validator.City;
                customerData.State = Validator.State;
                customerData.ZipCode = Validator.ZipCode;
                customerData.CreditCardNumber = Validator.CreditCardNumber;

                await db.SaveChangesAsync();
            }

            // Due to a bug in WinUI, even though validate
            // all properties at this point will have reset
            // all errors, the UI doesn't update... So TODO:
            // figure out how to reset error state of fields
            // in the UI.

            return true;
        }
    }
}
