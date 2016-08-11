using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPPopulate.Accounting
{
    public class AccountEntity
    {
        public string AccountCategory { get; set; }
        public string AccountType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int BalanceType { get; set; }
        public string Comment { get; set; }
    }
}
