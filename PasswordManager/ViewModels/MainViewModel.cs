using System.Windows.Input;
using System.ComponentModel;
using PasswordManager.Helpers;
using System;

namespace PasswordManager
{
    public class MainViewModel
    {
        private MasterPasswordContext _context = new MasterPasswordContext();

        string _EnteredPassword;
        public string EnteredPassword
        {
            get
            {
                return _EnteredPassword;
            }
            set
            {
                if (_EnteredPassword != value)
                {
                    _EnteredPassword = value;
                    
                }
            }
        }

        private RelayCommand addMasterPasswordCommand;
        public ICommand AddMasterPasswordCommand
        {
            get { return addMasterPasswordCommand ?? (addMasterPasswordCommand = new RelayCommand(() => AddMasterPassword())); }
        }
        private void AddMasterPassword()
        {
            DeletePasswords();
            byte[] salt = MasterPasswordHelper.CreateSalt(16);
            string hashed = MasterPasswordHelper.CreateMasterPassword(_EnteredPassword, salt);
            
            var hashedPwd = new MasterPassword()
            {
                Masterhash= hashed,
                Salt = salt
            };
            try
            {
                _context.MasterPasswords.Add(hashedPwd);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.Write(e.InnerException.ToString());
            }
        }

        private void DeletePasswords()
        {
            _context.MasterPasswords.RemoveRange(_context.MasterPasswords);
        }
    }
}
