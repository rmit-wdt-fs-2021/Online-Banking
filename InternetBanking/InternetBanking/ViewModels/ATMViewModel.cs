using InternetBanking.Models;

namespace InternetBanking.ViewModels
{
    public class ATMViewModel
    {
        public int AccountNumber { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}
