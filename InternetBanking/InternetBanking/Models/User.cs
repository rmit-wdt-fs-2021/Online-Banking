using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName{ get; set; }
        public string PasswordHash { get; set; }

    }
}
