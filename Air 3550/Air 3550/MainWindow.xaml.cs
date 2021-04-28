// MainWindow.xaml.cs - Air 3550 Project
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
 * The main and only window of Air 3550.
 * This window contains the main Frame.
 * used to interact with the application
 */

using System;
using System.Runtime.InteropServices;
using Air_3550.Views;
using Microsoft.UI.Xaml;
using Windows.Storage.Pickers;
using WinRT;

namespace Air_3550
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static Window Instance;

        public MainWindow()
        {
            this.InitializeComponent();

            Title = "Air 3550"; // Set the window title.

            ContentFrame.Navigate(typeof(MainPage)); // Navigate the user to the main page.

            Instance = this; // Set the singleton instance to this instance.
        }

        // I am so, so sorry for this awful code Dr. Thomas. We experienced an issue with the
        // FilePicker (an exception when trying to open a file picker dialog) found here:
        // (https://github.com/microsoft/ProjectReunion/issues/466#issuecomment-779621766)
        // To fix this issue, access to Win32 UIs are required, so we needed to implement
        // this code to fix the issue.
        //
        // We start by defining interfaces to access the Win32 Window API.
        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
        internal interface IWindowNative
        {
            IntPtr WindowHandle { get; }
        }

        // This method fixes a FileSavePicker by first grabbing
        // the singleton MainWindow's WindowHandle, then call's
        // the FileSavePicker's Initialize method (from the Win32
        // API), passing in the WindowHandle, fixing the save
        // picker.
        public static void FixPicker(FileSavePicker filePicker)
        {
            // Get the Window's HWND
            var hwnd = Instance.As<IWindowNative>().WindowHandle;

            // Stop the folder picker from crashing
            // when prompted to pick a file.
            var initializeWithWindow = filePicker.As<IInitializeWithWindow>();
            initializeWithWindow.Initialize(hwnd);
        }
    }
}
