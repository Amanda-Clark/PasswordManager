using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager
{
    public class MasterPassword
    {
        [Key]
        public int MasterPasswordId { get; set; }
        public string Masterhash { get; set; }
        public byte[] Salt { get; set; }
    }
}
