using System;

namespace InternetBanking.Exceptions
{
    public class AccountBalanceUpdateException : Exception
    {
        public AccountBalanceUpdateException(string message) : base(message)
        {

        }
    }
}
