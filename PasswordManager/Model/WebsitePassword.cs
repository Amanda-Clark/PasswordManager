using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    public class WebsitePassword
    {
        [Key]
        public int PasswordId { get; set; }
        public string UserName { get; set; }
        public string SiteName { get; set; }
        public string SitePassword { get; set; }
        public string Salt { get; set; }
        public string IV { get; set; }

    }
}
