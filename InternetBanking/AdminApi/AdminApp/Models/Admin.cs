using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class Admin
    {
        private const string correctUser = "admin";
        private const string correctPass = "admin";
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
