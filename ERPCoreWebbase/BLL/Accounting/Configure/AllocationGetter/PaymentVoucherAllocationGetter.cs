using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Accounting.Configure;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.BO.Accounting.Configure.AllocationGetter
{
    public class PaymentVoucherAllocationGetter : NAS.BO.Accounting.Configure.AllocationGetter.AllocationGetter
    {
        public override DevExpress.Xpo.XPCollection<Allocation> GetAllocationCollection(Session session)
        {
            try
            {
                AllocationType allocationType =
                    session.FindObject<AllocationType>(new BinaryOperator("Code", "VOUCHER_PAYMENT"));
                return allocationType.Allocations;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
