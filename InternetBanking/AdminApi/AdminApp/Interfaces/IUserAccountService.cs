using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Interfaces
{
    public interface IUserAccountService
    {
        Task LockAccountAsync(int id);
    }
}
