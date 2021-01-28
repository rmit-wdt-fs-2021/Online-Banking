using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Interfaces
{
    public interface IBillPayService
    {
        Task BlockBillPayAsync(int billPayId);
        Task UnblockBillPayAsync(int billPayId);
    }
}
