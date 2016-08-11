using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPPopulate.Nomenclature.Item
{

    public class ItemEntity
    {

        public ItemEntity()
        {
            this.ItemUnits = new List<ItemUnitEntity>();
        }

        public string Code
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string ManufacturerCode
        {
            get;
            set;
        }

        public string SupplierCode
        {
            get;
            set;
        }

        public List<ItemUnitEntity> ItemUnits { get; set; }

    }
}
