using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.ItemInventory;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Inventory
{
    public class ItemInventoryByArtifact: XPCustomObject
    {
        public ItemInventoryByArtifact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid _ItemInventoryByArtifactId;
        [Key(true)]
        public Guid ItemInventoryByArtifactId
        {
            get { return _ItemInventoryByArtifactId; }
            set { SetPropertyValue<Guid>("ItemInventoryByArtifactId", ref _ItemInventoryByArtifactId, value); }
        }

        private decimal _CreditItemSum;
        public decimal CreditItemSum
        {
            get { return _CreditItemSum; }
            set { SetPropertyValue<decimal>("CreditItemSum", ref _CreditItemSum, value); }
        }

        private decimal _DebitItemSum;
        public decimal DebitItemSum
        {
            get { return _DebitItemSum; }
            set { SetPropertyValue<decimal>("DebitItemSum", ref _DebitItemSum, value); }
        }

        private decimal _CreditSum;
        public decimal CreditSum
        {
            get { return _CreditSum; }
            set { SetPropertyValue<decimal>("CreditSum", ref _CreditSum, value); }
        }

        private decimal _DebitSum;
        public decimal DebitSum
        {
            get { return _DebitSum; }
            set { SetPropertyValue<decimal>("DebitSum", ref _DebitSum, value); }
        }

        private decimal _CurrentBalanceItem;
        public decimal CurrentBalanceItem
        {
            get { return _CurrentBalanceItem; }
            set { SetPropertyValue<decimal>("CurrentBalanceItem", ref _CurrentBalanceItem, value); }
        }

        private decimal _CurrentBalance;
        public decimal CurrentBalance
        {
            get { return _CurrentBalance; }
            set { SetPropertyValue<decimal>("CurrentBalance", ref _CurrentBalance, value); }
        }

        private decimal _Price;
        public decimal Price
        {
            get { return _Price; }
            set { SetPropertyValue<decimal>("Price", ref _Price, value); }
        }

        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }
        #endregion

        #region References
        private FinancialItemInventorySummary_Fact _FinancialItemInventorySummary_FactId;
        [Association("ItemInventoryByArtifact-FinancialItemInventorySummary_Fact")]
        public FinancialItemInventorySummary_Fact FinancialItemInventorySummary_FactId
        {
            get { return _FinancialItemInventorySummary_FactId; }
            set { SetPropertyValue<FinancialItemInventorySummary_Fact>("FinancialItemInventorySummary_FactId", ref _FinancialItemInventorySummary_FactId, value); }
        }

        [Association(@"InventoryEntryDetail-ItemInventoryByArtifact", typeof(InventoryEntryDetail))]
        public XPCollection<InventoryEntryDetail> InventoryEntryDetails { get { return GetCollection<InventoryEntryDetail>("InventoryEntryDetails"); } }


        private InventoryCommandDim _InventoryCommandDimId;
        [Association("ItemInventoryByArtifact-InventoryCommandDim")]
        public InventoryCommandDim InventoryCommandDimId
        {
            get { return _InventoryCommandDimId; }
            set { SetPropertyValue<InventoryCommandDim>("InventoryCommandDimId", ref _InventoryCommandDimId, value); }
        }

        private CorrespondFinancialAccountDim _CorrespondFinancialAccountDimId;
        [Association("ItemInventoryByArtifact-CorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return _CorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref _CorrespondFinancialAccountDimId, value); }
        }

        [Association("FinancialEntryDetail-ItemInventoryByArtifact", typeof(FinancialEntryDetail))]
        public XPCollection<FinancialEntryDetail> FinancialEntryDetails { get { return GetCollection<FinancialEntryDetail>("FinancialEntryDetails"); } }

        #endregion
    }
}
