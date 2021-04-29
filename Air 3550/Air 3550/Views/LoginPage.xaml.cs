// LoginPage.xaml.cs - Air 3550 Project
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
 * This page is shown to the user for logging
 * into an account. They can either get taken
 * to the register page to create an account,
 * or login to an existing account, which will
 * then lead to redirection to the previous page
 * they were on or to an elevated role page.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Services;
using Air_3550.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;

namespace Air_3550.Views
{
    public sealed partial class LoginPage : Page
    {
        // The parameters that can be passed when
        // this page is navigated to which changes
        // the initial behavior of the page.
        public class Params
        {
            // This class is used when the login page
            // needs to display a new user's login ID
            // and prefill it in the username box.
            public class NewUser : Params
            {
                public string LoginId;

                public NewUser(string LoginId)
                {
                    this.LoginId = LoginId;
                }
            }

            // This class is used when the login page needs
            // to tell the user that their password has
            // changed.
            public class PasswordChanged : Params { }

            // This class is used to tell the login page
            // where to redirect to after a successful
            // login instead of it's default behavior of
            // going back.
            public class RedirectToPage : Params
            {
                public Type PageType;
                public object Parameter;

                public RedirectToPage(Type pageType, object parameter)
                {
                    PageType = pageType;
                    Parameter = parameter;
                }
            }
        }

        // The redirection page type and parameter,
        // used for navigating to the next page if
        // it is specified.
        private Type redirectPageType;
        private object redirectParam;

        // The user session we will use to truly log
        // the user into the application.
        private readonly UserSessionService userSession;

        // In the constructor, we simply
        // get the user session service.
        public LoginPage()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();
        }

        readonly LoginViewModel ViewModel = new(); // Construct the view model.

        // This method handles login page parameters,
        // either showing the user an info bar saying
        // that their account has been created or their
        // password has been changed, processing the redirect
        // after a successful login.
        private void handleParams(Params param)
        {
            if (param is Params.NewUser newUserParam)
            {
                ViewModel.Username = newUserParam.LoginId;
                InfoBar.Title = "Account Created";
                InfoBar.Message = $"Your account has been successfully created. Your ID is {newUserParam.LoginId}.";
                InfoBar.IsOpen = true;
            }
            else if (param is Params.PasswordChanged)
            {
                InfoBar.Title = "Password Changed";
                InfoBar.Message = $"Your password has been changed. Please login again.";
                InfoBar.IsOpen = true;
            }
            else if (param is Params.RedirectToPage redirect)
            {
                redirectPageType = redirect.PageType;
                redirectParam = redirect.Parameter;
            }
        }

        // Overides OnNavigatedTo handle each login page parameter,
        // either passed as a single login parameter or a list of
        // different parameters.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is List<Params> paramsList)
            {
                foreach (var param in paramsList)
                {
                    handleParams(param);
                }
            }
            else if (e.Parameter is Params param)
            {
                handleParams(param);
            }
        }

        // This method is called when the login button is clicked,
        // and simply calls the PerformLogin method to cause a login.
        public async void LoginButton_Clicked(object _, RoutedEventArgs __)
        {
            await PerformLogin();
        }

        // This method is called when enter is pressed
        // in the stack panel, allowing the user to press
        // enter to login.
        private async void StackPanel_KeyDown(object _, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await PerformLogin();
            }
        }

        // This method actually performs a login. It defers
        // to the view model to perform the login, then if
        // successful, navigates the user to the right page
        // based on their role.
        private async Task PerformLogin()
        {
            if (await ViewModel.PerformLogin())
            {
                // If the user that logged in is a customer,
                // then we just take them back to the previous
                // page. Otherwise, we take them to the proper
                // page based on their role.
                var role = userSession.Role;

                if (role == Role.CUSTOMER)
                {
                    Frame.GoBack();

                    if (redirectPageType != null)
                    {
                        Frame.Navigate(redirectPageType, redirectParam);
                    }
                }
                else
                {
                    if (role == Role.ACCOUNTANT || role == Role.FLIGHT_MANAGER)
                    {
                        Frame.Navigate(typeof(SummaryPage));
                    }
                    else if (role == Role.MARKETING_MANAGER || role == Role.LOAD_ENGINEER)
                    {
                        Frame.Navigate(typeof(EditSchedulePage));
                    }

                    Frame.BackStack.Clear();
                }
            }
        }

        // This method is called when the user clicks the register
        // button, and simply sends the user to the register page,
        // passing in this page's redirect properties.
        private void RegisterButton_Click(object _, RoutedEventArgs __)
        {
            Frame.Navigate(typeof(RegisterPage), new Params.RedirectToPage(redirectPageType, redirectParam));
        }
    }
}
