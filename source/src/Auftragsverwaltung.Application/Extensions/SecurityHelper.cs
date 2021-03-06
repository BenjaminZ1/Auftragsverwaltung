using System;
using System.Security.Cryptography;

namespace Auftragsverwaltung.Application.Extensions
{
    public static class SecurityHelper
    {
        public static string GenerateSalt(int nSalt)
        {
            var saltBytes = new byte[nSalt];

            using var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public static byte[] HashPassword(string password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations);
            return rfc2898DeriveBytes.GetBytes(nHash);
        }
    }

}
