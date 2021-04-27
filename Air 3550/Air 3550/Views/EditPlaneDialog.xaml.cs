// EditPlaneDialog.xaml.cs - Air 3550 Project
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

using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPlaneDialog : ContentDialog
    {
        readonly EditPlaneViewModel ViewModel;

        public EditPlaneDialog(Flight flight)
        {
            ViewModel = new(flight);
            this.InitializeComponent();
        }

        public async void EditPlane_Click(object _, ContentDialogButtonClickEventArgs e)
        {
            var result = await ViewModel.EditFlight();

            if (result == null)
            {
                e.Cancel = true;
            }
        }
    }

}
