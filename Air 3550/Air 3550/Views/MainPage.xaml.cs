﻿using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        readonly MainViewModel ViewModel = new();

        private void SearchButton_Click(object _, RoutedEventArgs _1)
        {

        }

        private void EditButton_Click(object _, RoutedEventArgs _1)
        {
            Frame.Navigate(typeof(EditSchedulePage));
        }
    }
}
