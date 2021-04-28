// App.xaml.cs - Air 3550 Project
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
 * The entrypoint into the application.
 * Here, we setup our service provider,
 * perform our database migration, and
 * launch our main window!
 */

using System;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Air_3550
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        // A singleton referring to the current app that basically
        // overrides the base Application's Current variable to
        // remove the need of casting.
        public static new App Current => (App)Application.Current;

        // property that allows other parts of the program to
        // get the services we provide.
        public IServiceProvider Services { get; }

        // A field containing the main window of the application.
        private Window m_window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            // We configure our services so that
            // other parts in our app can access
            // them.
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        // In this method, we configure the services
        // for our app. The only service we have is
        // our UserSessionService, which maintains
        // the currently signed in user, their role,
        // and their customer data if they have it.
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<UserSessionService>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Right before we show the main window,
            // we migrate the database. This will ensure
            // that if the database is not created, it will
            // be created. Afterwards, all migrations will
            // be ran so the schema is up to date.
            using (var db = new AirContext())
            {
                db.Database.Migrate();
            }

            // We then construct our main window
            // and activate it which shows it to
            // the user.
            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}
