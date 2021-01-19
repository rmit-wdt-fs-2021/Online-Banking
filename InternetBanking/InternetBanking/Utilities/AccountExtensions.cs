using InternetBanking.Models;
using System.Linq;

namespace InternetBanking.Utilities
{
    public static class AccountExtensions
    {
        public static bool HasFreeTransactions(this Account account, int freeTransactions)
        {
            var chargeableTransactions = account.Transactions.Count(x => x.TransactionType == TransactionType.Transfer || 
                                                                    x.TransactionType == TransactionType.Withdraw);
            return chargeableTransactions <= freeTransactions;
        }
    }
}
