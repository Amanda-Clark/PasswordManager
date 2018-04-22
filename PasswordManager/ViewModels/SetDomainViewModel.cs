using System.Windows.Input;
using System.Linq;
using PasswordManager.Helpers;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PasswordManager.Model;

namespace PasswordManager
{
    public class SetDomainViewModel
    {
        private MasterPasswordContext _context = new MasterPasswordContext();

        string _EnteredDomain;
        public string EnteredDomain
        {
            get
            {
                return _EnteredDomain;
            }
            set
            {
                if (_EnteredDomain != value)
                {
                    _EnteredDomain = value;

                }
            }
        }

        string _EnteredDomainPassword;
        public string EnteredDomainPassword
        {
            get
            {
                return _EnteredDomainPassword;
            }
            set
            {
                if (_EnteredDomainPassword != value)
                {
                    _EnteredDomainPassword = value;

                }
            }
        }


        string _EnteredUserName;
        public string EnteredUserName
        {
            get
            {
                return _EnteredUserName;
            }
            set
            {
                if (_EnteredUserName != value)
                {
                    _EnteredUserName = value;

                }
            }
        }


        string _EnteredMasterPassword;
        public string EnteredMasterPassword
        {
            get
            {
                return _EnteredMasterPassword;
            }
            set
            {
                if (_EnteredMasterPassword != value)
                {
                    _EnteredMasterPassword = value;

                }
            }

        }

        private RelayCommand addDomainPasswordCommand;
        public ICommand AddDomainPasswordCommand
        {    
            get { return addDomainPasswordCommand ?? (addDomainPasswordCommand = new RelayCommand(() => AddDomainPassword())); }
        }
        private void AddDomainPassword()
        {
            var master = _context.MasterPasswords.ToList();
            var service = EnteredDomain;
            var password = EnteredDomainPassword;
            var hashSvc = CreateHMAC.CreateHMACString(master[0].Masterhash, service);
            byte[] salt = CreateSaltHelper.CreateSaltString(32);
            var keyForAes = MasterPasswordHelper.CreateMasterPassword(master[0].Masterhash, master[0].Salt);
            byte[] iv = CreateSaltHelper.CreateSaltString(16);
            string newIv = Convert.ToBase64String(iv);
            newIv = newIv.Substring(0, 16);
            string aesHash = Encrypt(password, keyForAes, Convert.ToBase64String(salt), "SHA1", 2,
                newIv, 256);

            bool wasSuccessful = InsertWebsitePassword(service, aesHash, newIv, Convert.ToBase64String(salt));
        }

        private bool InsertWebsitePassword(string hashSvc, string aesHash, string newIv, string salt)
        {
            try
            {
                var toAdd = new WebsitePassword()
                {
                    UserName = EnteredUserName,
                    SiteName = hashSvc,
                    SitePassword = aesHash,
                    Salt = salt,
                    IV = newIv

                };
                _context.WebsitePasswords.Add(toAdd);
                _context.SaveChanges();
                var pass = true;
                return pass;

            }
            catch(Exception ex)
            {
                var pass = false;
                return pass;
            }
        }


        public static string Encrypt(string PlainText, string Password,
               string Salt, string HashAlgorithm,
               int PasswordIterations, string InitialVector ,
               int KeySize)
           {
               if (string.IsNullOrEmpty(PlainText))
                   return "";
               byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
               byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
               byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
               PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
               byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
               RijndaelManaged SymmetricKey = new RijndaelManaged();
               SymmetricKey.Mode = CipherMode.CBC;
               byte[] CipherTextBytes = null;
               using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
               {
                   using (MemoryStream MemStream = new MemoryStream())
                   {
                       using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                       {
                           CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                           CryptoStream.FlushFinalBlock();
                          CipherTextBytes = MemStream.ToArray();
                           MemStream.Close();
                           CryptoStream.Close();
                       }
                   }
               }
               SymmetricKey.Clear();
               return Convert.ToBase64String(CipherTextBytes);
           }

   
         
        
    }
}
