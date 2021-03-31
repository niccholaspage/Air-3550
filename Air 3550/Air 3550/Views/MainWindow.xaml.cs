﻿using Microsoft.UI.Xaml;
using Air_3550.Repository;
using Windows.UI.Popups;
using System;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var plane = db.Planes.First();
                myButton.Content = "Quinn is dumb: " + plane.Model;
            }
        }
    }
}
