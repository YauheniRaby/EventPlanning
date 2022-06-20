using System;
using System.Text;
using XSystem.Security.Cryptography;

namespace EventPlanning.Bl.Helpers
{
    public static class PasswordHelper
    {
        public static string GetHashPassword(string password, string salt)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }

        public static string GetSalt()
        {
            Random r = new();
            string salt = string.Empty;
            int longSalt = r.Next(5, 10);
            for (int i = 0; i < longSalt; i++)
            {
                salt += (char)r.Next(65, 90);
            }

            return salt;
        }

        public static int GetVerifiedSms()
        {
            Random r = new();
            return r.Next(1000, 9999);
        }
    }
}
