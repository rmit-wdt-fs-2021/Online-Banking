using AdminApi.Data;
using AdminApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Models.DataManager
{
    public class BillPayManager : DataRepository<BillPay, int>, IBillPayRepository
    {
        public BillPayManager(McbaContext context) : base(context)
        { }

        public void BlockBillPay(int billPayID)
        {
            throw new NotImplementedException();
        }
    }
}
