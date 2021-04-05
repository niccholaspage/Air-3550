﻿using Air_3550.Pages;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            using (var db = new AirContext())
            {
                db.Database.Migrate();
            }

            m_window = new MainWindow();

            Frame frame = m_window.Content as Frame;

            if (frame == null)
            {
                frame = new Frame();

                // Previous execution state??
                m_window.Content = frame;
            }

            if (frame.Content == null)
            {
                frame.Navigate(typeof(MainPage));
            }

            m_window.Activate();
        }

        private Window m_window;
    }
}
