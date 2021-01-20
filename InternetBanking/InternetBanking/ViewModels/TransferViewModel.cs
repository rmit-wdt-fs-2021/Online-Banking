using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.ViewModels
{
    public class TransferViewModel
    {
        [Required]
        public List<SelectListItem> Accounts { get; set; }
    }
}
