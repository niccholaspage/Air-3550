// ChangePasswordSubPage.xaml.cs - Air 3550 Project
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

using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChangePasswordSubPage : Page
    {
        public ChangePasswordSubPage()
        {
            this.InitializeComponent();
        }

        readonly ChangePasswordViewModel ViewModel = new();

        private async void ChangePasswordButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.ChangePassword())
            {
                // TODO: This is so ugly... how can we make
                // this better?
                // Successful password change, so clear the
                // backstack and send the user back to the login
                // page.
                var parentContentControl = Parent as ContentControl;

                var parentRelativePanel = parentContentControl.Parent as RelativePanel;

                var parentPage = parentRelativePanel.Parent as Page;

                parentPage.Frame.Navigate(typeof(MainPage));
                parentPage.Frame.BackStack.Clear();
                parentPage.Frame.Navigate(typeof(LoginPage), new LoginPage.Params.PasswordChanged());
            }
        }
    }
}
