using AdminApi.Data;
using AdminApi.Models.Repository;
using System;

namespace AdminApi.Models.DataManager
{
    public class UserAccountManager : DataRepository<Login, string>, IUserAccountRepository
    {
        public UserAccountManager(McbaContext context) : base(context) 
        { }

        public void LockAccount(string loginID)
        {
            throw new NotImplementedException();
        }
    }
}
