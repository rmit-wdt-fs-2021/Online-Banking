using AdminApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Controllers
{
    [AuthorizeAdmin]
    public class TransactionController
    {
    }
}
