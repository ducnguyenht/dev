using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Period;

namespace NAS.BO.ETL.RecycleBin
{
    public class RecycleBin
    {
        public void Clear<T>(Session session)
        {
            AccountingPeriodType type = session.GetObjectByKey<AccountingPeriodType>(Guid.Parse("7ad4ca8c-898d-4850-89ce-86b4d70e50b8"));
        }
    }
}
