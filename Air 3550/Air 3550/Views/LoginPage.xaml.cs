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
        public class Params
        {
            public class NewUser : Params
            {
                public string LoginId;

                public NewUser(string LoginId)
                {
                    this.LoginId = LoginId;
                }
            }

            public class PasswordChanged : Params { }

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

        private Type redirectPageType;
        private object redirectParam;

        private readonly UserSessionService userSession;

        public LoginPage()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();
        }

        readonly LoginViewModel ViewModel = new();

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

        public async void LoginButton_Clicked(object _, RoutedEventArgs __)
        {
            await PerformLogin();
        }

        private async void StackPanel_KeyDown(object _, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await PerformLogin();
            }
        }

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

        private void RegisterButton_Click(object _, RoutedEventArgs __)
        {
            Frame.Navigate(typeof(RegisterPage), new LoginPage.Params.RedirectToPage(redirectPageType, redirectParam));
        }
    }
}
