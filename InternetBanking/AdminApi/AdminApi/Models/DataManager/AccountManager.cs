﻿using AdminApi.AutoGeneratedModel;

namespace AdminApi.Models.DataManager
{
    public class AccountManager : DataRepository<Account, int>
    {
        public AccountManager(McbaContext context) : base(context)
        { }
    }
}
