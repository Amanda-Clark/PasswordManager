using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace PasswordManager.Helpers
{
    public static class CreateHMAC
    {
        public static  string CreateHMACString(string password, string domain)
        {
            var enc = Encoding.ASCII;
            HMACSHA256 hmac = new HMACSHA256(enc.GetBytes(password));
            hmac.Initialize();

            byte[] buffer = enc.GetBytes(domain);
            return BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
}
