using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PasswordManager.Helpers
{
    public static class MasterPasswordHelper
    {
        public static byte[] CreateSalt(int size)
        {
            var salt = new byte[size];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        public static string CreateMasterPassword(string password,  byte[] salt)
        {
            
           string PassHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA256,
           iterationCount: 10000,
           numBytesRequested: 256 / 8));
                return PassHash;
        }
    }
}
