using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PasswordManager.Model;
using System.Security.Cryptography;
using System.IO;
using PasswordManager.Helpers;

namespace PasswordManager
{
    public class ShowPasswordsViewModel
    {
        private MasterPasswordContext _context = new MasterPasswordContext();
        public string GlobalMasterPassword = "";
        private ObservableCollection<WebsitePassword> _websitePasswords;
        public ObservableCollection<WebsitePassword> WebsitePasswords
        {
            get { return _websitePasswords; }
            set
            {
                _websitePasswords = value;
                
            }
        }

        public ShowPasswordsViewModel()
        {
            var pwdData = _context.WebsitePasswords.ToList();
            var masterPwd = _context.MasterPasswords.FirstOrDefault();
            var master = _context.MasterPasswords.ToList();
            foreach (var data in pwdData)
            {
                var keyForAes = MasterPasswordHelper.CreateMasterPassword(master[0].Masterhash, master[0].Salt);
                var decryptPwd = Decrypt(data.SitePassword, keyForAes, data.Salt, "SHA1", 2, data.IV, 256);
                 data.SitePassword = decryptPwd;
            }
            WebsitePasswords = new ObservableCollection<WebsitePassword>(pwdData);

        }

        public static string Decrypt(string CipherText, string Password,
              string Salt, string HashAlgorithm,
              int PasswordIterations, string InitialVector,
              int KeySize = 256)
        {
            if (string.IsNullOrEmpty(CipherText))
                return "";
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = 0;
            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {

                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
        }


    }
}
