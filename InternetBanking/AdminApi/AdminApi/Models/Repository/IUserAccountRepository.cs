﻿using AdminApi.AutoGeneratedModel;
using System.Threading.Tasks;

namespace AdminApi.Models.Repository
{
    public interface IUserAccountRepository : IDataRepository<Login, int>
    {
        /// <summary>
        /// Lock a customer's account. A customer with a locked account can no longer log in. After a minute the account is automatically unlocked.
        /// </summary>
        /// <param name="customerID">AccountID to lock</param>
        public Task LockAccountAsync(int customerID);
    }
}
