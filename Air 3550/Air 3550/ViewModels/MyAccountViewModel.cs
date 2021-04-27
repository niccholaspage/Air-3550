// MyAccountViewModel.cs - Air 3550 Project
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

using Air_3550.Services;
using Air_3550.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Air_3550.ViewModels
{
    class MyAccountViewModel : ObservableObject
    {
        public enum Tab
        {
            BOOKINGS,
            PROFILE,
            CHANGE_PASSWORD
        }

        public bool IsCustomer;

        private Tab _currentTab;

        public MyAccountViewModel()
        {
            var userService = App.Current.Services.GetService<UserSessionService>();

            IsCustomer = userService.Role == Models.Role.CUSTOMER;

            if (IsCustomer)
            {
                _currentTab = Tab.BOOKINGS;
            }
            else
            {
                _currentTab = Tab.CHANGE_PASSWORD;
            }
        }

        public bool ViewingBookingsTab
        {
            get => _currentTab == Tab.BOOKINGS;
        }

        public bool ViewingProfileTab
        {
            get => _currentTab == Tab.PROFILE;
        }

        public bool ViewingChangePasswordTab
        {
            get => _currentTab == Tab.CHANGE_PASSWORD;
        }

        public void SetCurrentTab(Tab tab)
        {

            if (_currentTab != tab)
            {
                _currentTab = tab;

                OnPropertyChanged(nameof(ViewingBookingsTab));
                OnPropertyChanged(nameof(ViewingProfileTab));
                OnPropertyChanged(nameof(ViewingChangePasswordTab));
                OnPropertyChanged(nameof(DisplayedPage));
            }
            else
            {
                // If we don't call property changes on all tabs,
                // the AppBarToggleButton will cause the button
                // to toggle even though we don't want it to.
                OnPropertyChanged(nameof(ViewingBookingsTab));
                OnPropertyChanged(nameof(ViewingProfileTab));
                OnPropertyChanged(nameof(ViewingChangePasswordTab));
            }
        }

        private readonly Lazy<Page> BookingsPage = new(() => new BookingSubPage());
        private readonly Lazy<Page> ProfilePage = new(() => new EditProfileSubPage());
        private readonly Lazy<Page> ChangePasswordPage = new(() => new ChangePasswordSubPage());

        public Page DisplayedPage
        {
            get
            {
                return _currentTab switch
                {
                    Tab.BOOKINGS => BookingsPage.Value,
                    Tab.PROFILE => ProfilePage.Value,
                    Tab.CHANGE_PASSWORD => ChangePasswordPage.Value,
                    _ => throw new ArgumentException("This shouldn't have been reached.."),
                };
            }
        }
    }
}
