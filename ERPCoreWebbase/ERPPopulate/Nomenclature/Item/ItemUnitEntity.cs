using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPPopulate.Nomenclature.Item
{
    public class ItemUnitEntity
    {
        public string ItemCode { get; set; }
        public string UnitCode { get; set; }
        public string ParentUnitCode { get; set; }
        public float NumRequired { get; set; }
        public int Level { get; set; }
    }
}
