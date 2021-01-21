using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.ViewModels
{
    public class TransferViewModel
    {
        // TODO : Use when making dynamic dropdown for accounts instead of free-text field.
        public List<SelectListItem> Accounts { get; set; }

        public Account Account { get; set; }

        [Required]
        public int FromAccountNumber { get; set; }

        [Required]
        public int ToAccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Comment { get; set; }
    }
}
