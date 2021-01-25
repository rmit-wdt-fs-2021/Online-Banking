using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Models.Repository
{
    public interface IBillPayRepository : IDataRepository<BillPay, int>
    {
        public void BlockBillPay(int billPayID);
    }
}
