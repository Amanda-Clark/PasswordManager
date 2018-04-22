using PasswordManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    public class Authentication
    {
        public static bool CheckMasterPassword(string Password)
        {
             MasterPasswordContext _context = new MasterPasswordContext();
             var masterPwd = _context.MasterPasswords.FirstOrDefault();
             if(masterPwd != null)
             {
                string masterHash = masterPwd.Masterhash;
                byte[] salt = masterPwd.Salt;
                var chkHash = MasterPasswordHelper.CreateMasterPassword(Password, salt);
                if(chkHash == masterHash)
                {
                    return true;
                }
                else
                {
                    return false;
                }

             }
            else
            {
                return false;
            }
         
        }

        
    }
}
