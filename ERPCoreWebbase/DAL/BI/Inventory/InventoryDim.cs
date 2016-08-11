using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.BI.Inventory
{
    public class InventoryDim : XPCustomObject
    {
        public InventoryDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private int _InventoryDimId;
        [Key(true)]
        public int InventoryDimId
        {
            get { return _InventoryDimId; }
            set { SetPropertyValue<int>("InventoryDimId", ref _InventoryDimId, value); }
        }

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue<string>("Code", ref _Code, value); }
        }

        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>("Name", ref _Name, value); }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>("Description", ref _Description, value); }
        }

        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }

        #endregion

        #region References
        [Association(@"FinancialItemInventorySummary_Fact-InventoryDim", typeof(FinancialItemInventorySummary_Fact))]
        public XPCollection<FinancialItemInventorySummary_Fact> FinancialItemInventorySummary_Facts { get { return GetCollection<FinancialItemInventorySummary_Fact>("FinancialItemInventorySummary_Facts"); } }

        [Association(@"InventoryEntryDetail-InventoryDim", typeof(InventoryEntryDetail))]
        public XPCollection<InventoryEntryDetail> InventoryEntryDetails { get { return GetCollection<InventoryEntryDetail>("InventoryEntryDetails"); } }
        #endregion
    }
}
