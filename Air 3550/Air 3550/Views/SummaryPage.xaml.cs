﻿using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Provider;
using FileSavePicker = Windows.Storage.Pickers.FileSavePicker;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SummaryPage : Page
    {
        public SummaryPage()
        {
            this.InitializeComponent();
            this.Loaded += ViewModel.updateSflights;
        }

        readonly SummaryViewModel ViewModel = new();


    }
}
