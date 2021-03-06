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
using NAS.DAL.Inventory.Item;
namespace NAS.DAL.Inventory.StockCart
{

    public partial class StockCart : XPCustomObject
    {
        NAS.DAL.Nomenclature.Inventory.Inventory _InventoryId;
        private DateTime _LastUpdate;
        private DateTime _IssueDate;
        private string _Description;
        private DateTime _CreateDate;
        private string _Code;
        Guid fStockCartId;
        [Key(true)]
        public Guid StockCartId
        {
            get { return fStockCartId; }
            set { SetPropertyValue<Guid>("StockCartId", ref fStockCartId, value); }
        }

        [Size(36)]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue("Description", ref _Description, value);
            }
        }

        public DateTime IssueDate
        {
            get
            {
                return _IssueDate;
            }
            set
            {
                SetPropertyValue("IssueDate", ref _IssueDate, value);
            }
        }

        public DateTime LastUpdate
        {
            get
            {
                return _LastUpdate;
            }
            set
            {
                SetPropertyValue("LastUpdate", ref _LastUpdate, value);
            }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        [Association("StockCartReferencesInventory")]
        public NAS.DAL.Nomenclature.Inventory.Inventory InventoryId
        {
            get
            {
                return _InventoryId;
            }
            set
            {
                SetPropertyValue("InventoryId", ref _InventoryId, value);
            }
        }

        [Association(@"StockCartActorReferencesStockCart", typeof(StockCartActor))]
        public XPCollection<StockCartActor> StockCartActors { get { return GetCollection<StockCartActor>("StockCartActors"); } }

        [Association(@"StockCartItemReferencesStockCart", typeof(StockCartItem))]
        public XPCollection<StockCartItem> StockCartItems { get { return GetCollection<StockCartItem>("StockCartItems"); } }

	}

}
