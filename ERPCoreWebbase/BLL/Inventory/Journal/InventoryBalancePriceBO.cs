using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.Inventory.Journal
{
    public class InventoryBalancePriceBO
    {
        public Guid InventoryJournalId
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public DateTime IssueDate
        {
            get;
            set;
        }

        public double Balance
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }
    }
}
