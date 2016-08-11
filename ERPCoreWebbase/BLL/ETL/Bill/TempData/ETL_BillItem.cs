using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO.ETL.Bill.TempData
{
    public class ETL_BillItem
    {
        public Unit unit { get; set; }
        public Item item { get; set; }
        public string Asset { get; set; }
        public double Amount { get; set; }
    }
}
