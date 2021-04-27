// HeaderControl.xaml.cs - Air 3550 Project
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

using System.ComponentModel;
using Air_3550.Services;
using Air_3550.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public sealed partial class HeaderControl : UserControl, INotifyPropertyChanged
    {
        private readonly UserSessionService userSession;

        public Frame ContentFrame
        {
            get => (Frame)GetValue(ContentFrameProperty);
            set => SetValue(ContentFrameProperty, value);
        }

        public bool ShowLoginButton
        {
            get
            {
                return !userSession.IsLoggedIn
                && ContentFrame.CurrentSourcePageType != typeof(LoginPage)
                && ContentFrame.CurrentSourcePageType != typeof(RegisterPage);
            }
        }

        public static readonly DependencyProperty ContentFrameProperty = DependencyProperty.Register(nameof(ContentFrame), typeof(Frame), typeof(HeaderControl), new PropertyMetadata(default(Frame)));

        public event PropertyChangedEventHandler PropertyChanged;

        public HeaderControl()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();

            this.Loaded += (_, __) =>
            {
                userSession.PropertyChanged += HandleUserSessionChange;
                ContentFrame.Navigated += HandleFrameNavigation;
            };

            this.Unloaded += (_, __) =>
            {
                userSession.PropertyChanged -= HandleUserSessionChange;
                ContentFrame.Navigated -= HandleFrameNavigation;
            };
        }

        private void HandleUserSessionChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserSessionService.UserId))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ShowLoginButton)));
            };
        }

        private void HandleFrameNavigation(object sender, NavigationEventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ShowLoginButton)));
        }

        private void LoginButton_Click(object _sender, RoutedEventArgs _1)
        {
            ContentFrame.Navigate(typeof(LoginPage));
        }

        private void AccountInfoButton_Click(object _, RoutedEventArgs _1)
        {
            ContentFrame.Navigate(typeof(MyAccountPage));
        }

        private void LogoutButton_Click(object _, RoutedEventArgs _1)
        {
            userSession.Logout();

            ContentFrame.Navigate(typeof(MainPage));

            ContentFrame.BackStack.Clear();
        }

        private void BackButton_Click(object _, RoutedEventArgs _2)
        {
            if (ContentFrame.BackStackDepth > 0)
            {
                ContentFrame.GoBack();
            }
        }
    }
}
