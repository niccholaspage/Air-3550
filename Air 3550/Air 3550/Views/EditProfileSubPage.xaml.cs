﻿// EditProfileSubPage.xaml.cs - Air 3550 Project
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
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditProfileSubPage : Page
    {
        public EditProfileSubPage()
        {
            this.InitializeComponent();

            this.Loaded += async (_, __) => await ViewModel.FetchBalances();
        }

        readonly EditProfileViewModel ViewModel = new();

        private async void SaveChangesButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.SaveChanges())
            {
                await SuccessDialog.ShowAsync();
            }
        }
    }
}
