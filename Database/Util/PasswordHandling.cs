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

/**
 * A static class used for password functionality
 * in Air 3550. This class provides the ability to
 * hash passwords and check passwords against a hash.
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace Database.Util
{
    public static class PasswordHandling
    {
        // This method hashes a password passed in as
        // a string. Clearly, the way this method is written
        // is incredibly unsecure and should never make it into
        // production software (this is what proper algorithms for
        // passwords would come in, like bcrypt) but since we are
        // not worried about perfect security, it will work.
        public static string HashPassword(string password)
        {
            // We first get the bytes of the password, using
            // the UTF-8 encoding because who doesn't love emojis
            // in their passwords? In a real program/website, we
            // would want to validate the password before letting
            // it through.
            byte[] data = Encoding.UTF8.GetBytes(password);

            // We construct a new managed instance of the SHA512 class.
            SHA512 shaM = new SHA512Managed();

            // We compute the hash for our password in bytes.
            byte[] result = shaM.ComputeHash(data);

            // Finally, we convert the bytes into a Base64
            // string for easy storage in the database.
            return Convert.ToBase64String(result);
        }

        // A method that makes it easier to check a password against
        // a hash. Since calculating the hash of the same password will
        // always result in the same hash, we simply hash the password and
        // compare it to the hash. If they match, the password is valid!
        public static bool CheckPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
