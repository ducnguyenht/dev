using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.TempData
{
    public class SplitJournal
    {
        string DebitAccountCode { get; set; }
        string CreditAccountCode { get; set; }
        decimal Amount { get; set; }
        string Currency { get; set; }
    }
}
