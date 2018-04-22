using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using PasswordManager.Model;

namespace PasswordManager
{
    public class MasterPasswordContext : DbContext
    {
    public DbSet<MasterPassword> MasterPasswords { get; set; }
    public DbSet<WebsitePassword>WebsitePasswords { get; set; }
    }
}
