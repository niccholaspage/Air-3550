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

/**
 * This control is used for the header of the application,
 * exposing back, login, and my account buttons based on the
 * state of the application.
 */

using System.ComponentModel;
using Air_3550.Services;
using Air_3550.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Air_3550.Controls
{
    public sealed partial class HeaderControl : UserControl, INotifyPropertyChanged
    {
        // We need the user's session to determine what
        // buttons we need to show in the header control.
        private readonly UserSessionService userSession;

        public bool ShowLoginButton
        {
            get
            {
                return !userSession.IsLoggedIn
                && ContentFrame.CurrentSourcePageType != typeof(LoginPage)
                && ContentFrame.CurrentSourcePageType != typeof(RegisterPage);
            }
        }

        // We expose a ContentFrame property to allow consumers
        // to bind their own Frame so that the header control can
        // navigate to different pages in the frame.
        public Frame ContentFrame
        {
            get => (Frame)GetValue(ContentFrameProperty);
            set => SetValue(ContentFrameProperty, value);
        }

        public static readonly DependencyProperty ContentFrameProperty = DependencyProperty.Register(nameof(ContentFrame), typeof(Frame), typeof(HeaderControl), new PropertyMetadata(default(Frame)));

        // We setup a PropertyChanged event to properly notify
        // the UI whether to display the login button when the
        // user is already on the login/registration page.
        public event PropertyChangedEventHandler PropertyChanged;

        public HeaderControl()
        {
            // We grab the user session service from our services provider.
            userSession = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();

            // We register and unload our two events we would like to
            // handle in this.Loaded and this.Unloaded.
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

        // In this method, we handle when a user logs in or out
        // so that we can update the display of the login button.
        // We then call the PropertyChanged event passing the name
        // of the ShowLoginButton variable so that our UI
        // updates the visibility of the button.
        private void HandleUserSessionChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserSessionService.UserId))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ShowLoginButton)));
            };
        }

        // In this method, we handle when a frame navigation event
        // occurs so that we can update the display of the login
        // button. We then call the PropertyChanged event passing
        // the name of the ShowLoginButton variable so that our UI
        // updates the visibility of the button.
        private void HandleFrameNavigation(object sender, NavigationEventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ShowLoginButton)));
        }

        // This method handles the login button click by simply
        // navigating the frame to the login page.
        private void LoginButton_Click(object _sender, RoutedEventArgs _1)
        {
            ContentFrame.Navigate(typeof(LoginPage));
        }

        // This method handles the my account button click by simply
        // navigating the frame to the my account page.
        private void AccountInfoButton_Click(object _, RoutedEventArgs _1)
        {
            ContentFrame.Navigate(typeof(MyAccountPage));
        }


        // This method handles the logout button click by logging the
        // user out with the user session, navigating them to the main
        // page, then clearing their entire backstack so they cannot
        // navigate back to authenticated parts of the system.
        private void LogoutButton_Click(object _, RoutedEventArgs _1)
        {
            userSession.Logout();

            ContentFrame.Navigate(typeof(MainPage));

            ContentFrame.BackStack.Clear();
        }

        // This method handles the back button click by simply
        // navigating the content frame back if it can go back.
        private void BackButton_Click(object _, RoutedEventArgs _2)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }
    }
}
