EECS 3550 Software Engineering Air 3550 Team 4 Project

Steps to get the program running:
1. Download and install the latest version of Visual Studio 2019 or later.
When installing Visual Studio, make sure you include the following components:

In the Workloads tab:
* Universal Windows Platform Development
* .NET Desktop Development

In the Individual components tab:
* Windows 10 SDK (10.0.19041.0)

2. Now launch Visual Studio and install the Project Reunion extension. To do so,
launch Visual Studio. If you do not already have any project open, hit Continue
without code.

3. Now click the Extensions menu item at the top, and click Manage Extensions.
Click the Online section, and search for Project Reunion, and install Project
Reunion by Microsoft. Now restart Visual Studio to finish installation of the
extension.

4. Now, double click the Air 3550.sln file. Make sure that the run configuration
is set to "Debug x64 Air 3550 (Package)", then hit the "Local Machine" button to
launch the application on your machine. These settings can be found at the top of
the screen in Visual Studio.

Accessing the database:
The Database is located at the following directory:
%localappdata%\Packages\13b1f18c-661a-4495-b3a2-861dd126199d_<random characters, just match the prefix>\LocalCache\Roaming\Air 3550 Team 4\

The file is called air.db. If you would like to interact with this database outside
of the program, I would recommend DB Browser for SQLite:
https://sqlitebrowser.org/dl/

Seeded User Logins:
Dummy User 1:
Username: 756967
Password: 1234

Dummy User 2:
Username: 886642
Password: 1234

Seeded Employee Logins:
Username: accountant
Password: accountant

Username: load_engineer
Password: load_engineer

Username: flight_manager
Password: flight_manager

Username: marketing_manager
Password: marketing_manager