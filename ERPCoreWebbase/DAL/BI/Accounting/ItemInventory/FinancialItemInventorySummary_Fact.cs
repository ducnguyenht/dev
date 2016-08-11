using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Item;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Inventory;

namespace NAS.DAL.BI.Accounting.ItemInventory
{
    public class FinancialItemInventorySummary_Fact : XPCustomObject
    {
        public FinancialItemInventorySummary_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private int _FinancialItemInventorySummary_FactId;
        [Key(true)]
        public int FinancialItemInventorySummary_FactId
        {
            get { return _FinancialItemInventorySummary_FactId; }
            set { SetPropertyValue<int>("FinancialItemInventorySummary_FactId", ref _FinancialItemInventorySummary_FactId, value); }
        }

        private decimal _BeginCreditBalance;
        public decimal BeginCreditBalance
        {
            get { return _BeginCreditBalance; }
            set { SetPropertyValue<decimal>("BeginCreditBalance", ref _BeginCreditBalance, value); }
        }

        private decimal _EndCreditBalance;
        public decimal EndCreditBalance
        {
            get { return _EndCreditBalance; }
            set { SetPropertyValue<decimal>("EndCreditBalance", ref _EndCreditBalance, value); }
        }

        private decimal _BeginDebitBalance;
        public decimal BeginDebitBalance
        {
            get { return _BeginDebitBalance; }
            set { SetPropertyValue<decimal>("BeginDebitBalance", ref _BeginDebitBalance, value); }
        }

        private decimal _EndDebitBalance;
        public decimal EndDebitBalance
        {
            get { return _EndDebitBalance; }
            set { SetPropertyValue<decimal>("EndDebitBalance", ref _EndDebitBalance, value); }
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

        private decimal _BeginBalanceItem;
        public decimal BeginBalanceItem
        {
            get { return _BeginBalanceItem; }
            set { SetPropertyValue<decimal>("BeginBalanceItem", ref _BeginBalanceItem, value); }
        }

        private decimal _EndBalanceItem;
        public decimal EndBalanceItem
        {
            get { return _EndBalanceItem; }
            set { SetPropertyValue<decimal>("EndBalanceItem", ref _EndBalanceItem, value); }
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

        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }
        #endregion

        #region References
        private MonthDim _MonthDimId;
        [Association("FinancialItemInventorySummary_Fact-MonthDim")]
        public MonthDim MonthDimId
        {
            get { return _MonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref _MonthDimId, value); }
        }

        private YearDim _YearDimId;
        [Association("FinancialItemInventorySummary_Fact-YearDim")]
        public YearDim YearDimId
        {
            get { return _YearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref _YearDimId, value); }
        }

        private OwnerOrgDim _OwnerOrgDimId;
        [Association("FinancialItemInventorySummary_Fact-OwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return _OwnerOrgDimId; }
            set { SetPropertyValue<OwnerOrgDim>("OwnerOrgDimId", ref _OwnerOrgDimId, value); }
        }

        private ItemDim _ItemDimId;
        [Association("FinancialItemInventorySummary_Fact-ItemDim")]
        public ItemDim ItemDimId
        {
            get { return _ItemDimId; }
            set { SetPropertyValue<ItemDim>("ItemDimId", ref _ItemDimId, value); }
        }

        private UnitDim _UnitDimId;
        [Association("FinancialItemInventorySummary_Fact-UnitDim")]
        public UnitDim UnitDimId
        {
            get { return _UnitDimId; }
            set{ SetPropertyValue<UnitDim>("UnitDimId", ref _UnitDimId, value);}
        }

        private FinancialAccountDim _FinancialAccountDimId;
        [Association("FinancialItemInventorySummary_Fact-FinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return _FinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref _FinancialAccountDimId, value); }
        }

        private InventoryDim _InventoryDimId;
        [Association("FinancialItemInventorySummary_Fact-InventoryDim")]
        public InventoryDim InventoryDimId
        {
            get { return _InventoryDimId; }
            set { SetPropertyValue<InventoryDim>("InventoryDimId", ref _InventoryDimId, value); }
        }

        [Association(@"ItemInventoryByArtifact-FinancialItemInventorySummary_Fact", typeof(ItemInventoryByArtifact))]
        public XPCollection<ItemInventoryByArtifact> ItemInventoryByArtifacts { get { return GetCollection<ItemInventoryByArtifact>("ItemInventoryByArtifacts"); } }
        #endregion

    }
}
