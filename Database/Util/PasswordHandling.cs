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
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
            return Convert.ToBase64String(result);
        }

        public static bool CheckPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
