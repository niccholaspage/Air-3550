using Air_3550.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using Microsoft.Extensions.DependencyInjection;
using Air_3550.Services;

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
            _currentTab = tab;

            OnPropertyChanged(nameof(ViewingBookingsTab));
            OnPropertyChanged(nameof(ViewingProfileTab));
            OnPropertyChanged(nameof(ViewingChangePasswordTab));
            OnPropertyChanged(nameof(DisplayedPage));
        }

        public Page DisplayedPage
        {
            get
            {
                return _currentTab switch
                {
                    Tab.BOOKINGS => new LoginPage(),
                    Tab.PROFILE => new EditProfileSubPage(),
                    Tab.CHANGE_PASSWORD => new ChangePasswordSubPage(),
                    _ => throw new ArgumentException("This shouldn't have been reached.."),
                };
            }
        }
    }
}
