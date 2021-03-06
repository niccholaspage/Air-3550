// Role.cs - Air 3550 Project
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
 * The Role enumeration specifies
 * a user role, which can either be
 * a customer, accountant, load engineer,
 * flight manager, or marketing manager.
 * This is used by the user model to determine
 * what privileges the user has in the system.
 */

namespace Air_3550.Models
{
    public enum Role
    {
        CUSTOMER,
        ACCOUNTANT,
        LOAD_ENGINEER,
        FLIGHT_MANAGER,
        MARKETING_MANAGER
    }
}
