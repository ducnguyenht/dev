using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Configure;

namespace NAS.BO.Accounting.Configure.AllocationGetter
{
    public abstract class AllocationGetter
    {
        public abstract XPCollection<Allocation> GetAllocationCollection(Session session);
    }
}
