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

        private Page BookingsPage = new LoginPage();
        private Page ProfilePage = new EditProfileSubPage();
        private Page ChangePasswordPage = new ChangePasswordSubPage();

        public Page DisplayedPage
        {
            get
            {
                return _currentTab switch
                {
                    Tab.BOOKINGS => BookingsPage,
                    Tab.PROFILE => ProfilePage,
                    Tab.CHANGE_PASSWORD => ChangePasswordPage,
                    _ => throw new ArgumentException("This shouldn't have been reached.."),
                };
            }
        }
    }
}
