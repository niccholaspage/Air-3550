using System;
using System.Security.Cryptography;
using System.Text;

namespace Database.Util
{
    class PasswordHandling
    {
        public static string HashPassword(string password)
        {
            // TODO: Ask Larry if this seems reasonable because this is ACTUALLY stupid.
            byte[] data = Encoding.UTF8.GetBytes(password);
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
            return Convert.ToBase64String(result);
        }
    }
}
