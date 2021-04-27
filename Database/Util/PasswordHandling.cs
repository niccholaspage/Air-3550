// PasswordHandling.cs - Air 3550 Project
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

using System;
using System.Security.Cryptography;
using System.Text;

namespace Database.Util
{
    public class PasswordHandling
    {
        public static string HashPassword(string password)
        {
            // TODO: Ask Larry if this seems reasonable because this is ACTUALLY so stupid and insecure.
            byte[] data = Encoding.UTF8.GetBytes(password);
            SHA512 shaM = new SHA512Managed();
            byte[] result = shaM.ComputeHash(data);
            return Convert.ToBase64String(result);
        }

        public static bool CheckPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
