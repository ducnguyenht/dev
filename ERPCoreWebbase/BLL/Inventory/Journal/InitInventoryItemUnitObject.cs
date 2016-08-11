using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO.Inventory.Journal
{   
    public class InitInventoryItemUnitObject
    {
        public Guid ItemUnitId
        {
            get;
            set;
        }

        public string ItemCode
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public string UnitName
        {
            get;
            set;
        }

        public short IsComplete
        {
            get;
            set;
        }

        public string Manufacturer
        {
            get;
            set;
        }

    }
}
