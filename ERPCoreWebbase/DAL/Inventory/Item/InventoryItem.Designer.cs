//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.System.Privileage;
using NAS.DAL.Nomenclature.Item;
namespace NAS.DAL.Inventory.Item
{

    public partial class InventoryItem : XPCustomObject
    {
        private ItemUnit _ItemUnitId;
        private NAS.DAL.Nomenclature.Inventory.Inventory _InventoryId;
        Guid fInventoryItemId;
        [Key(true)]
        public Guid InventoryItemId
        {
            get { return fInventoryItemId; }
            set { SetPropertyValue<Guid>("InventoryItemId", ref fInventoryItemId, value); }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        NAS.DAL.Nomenclature.Inventory.Inventory fInventoryId;
        [Association(@"InventoryItemReferencesInventory")]
        public NAS.DAL.Nomenclature.Inventory.Inventory InventoryId
        {
            get { return fInventoryId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.Inventory.Inventory>("InventoryId", ref fInventoryId, value); }
        }

        [Association("ItemUnitReferencesInventoryItems")]
        public ItemUnit ItemUnitId
        {
            get
            {
                return _ItemUnitId;
            }
            set
            {
                SetPropertyValue("ItemUnitId", ref _ItemUnitId, value);
            }
        }


		
	}

}
