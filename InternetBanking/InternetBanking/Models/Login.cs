using System;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Models
{
    public class Login
    {
        [Required, StringLength(8)]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }
    }
}
