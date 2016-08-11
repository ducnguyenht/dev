using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS_ETLProcess.ETLJob;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL;

namespace NAS_ETLProcess
{
    public class Testing
    {
        static void Main(string[] args)
        {
            FinancialItemInventoryBaseJob job = new FinancialItemInventoryBaseJob();
            job.Run();
        }
    }
}
