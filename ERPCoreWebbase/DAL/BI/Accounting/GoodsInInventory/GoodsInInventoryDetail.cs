using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Inventory;

namespace NAS.DAL.BI.Accounting.GoodsInInventory
{
    public class GoodsInInventoryDetail : XPCustomObject
    {
        public GoodsInInventoryDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fGoodsInInventoryDetailId;
        [Key(true)]
        public Guid GoodsInInventoryDetailId
        {
            get { return fGoodsInInventoryDetailId; }
            set { SetPropertyValue<Guid>("GoodsInInventoryDetailId", ref fGoodsInInventoryDetailId, value); }
        }

        private double fQuantity;
        public double Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<double>("Quantity", ref fQuantity, value); }
        }

        private decimal fCredit;
        public decimal Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<decimal>("Credit", ref fCredit, value); }
        }

        private decimal fDebit;
        public decimal Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<decimal>("Debit", ref fDebit, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        private GoodsInInventorySummary_Fact fGoodsInInventorySummary_FacftId;
        [Association(@"GoodsInInventoryDetailReferencesGoodsInInventorySummary_Fact")]
        public GoodsInInventorySummary_Fact GoodsInInventorySummary_FacftId
        {
            get { return fGoodsInInventorySummary_FacftId; }
            set { SetPropertyValue<GoodsInInventorySummary_Fact>("GoodsInInventorySummary_FacftId", ref fGoodsInInventorySummary_FacftId, value); }
        }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association(@"GoodsInInventoryDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private FinancialTransactionDim fFinancialTransactionDimId;
        [Association(@"GoodsInInventoryDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }

        private CurrencyDim fCurrencyDimId;
        [Association(@"GoodsInInventoryDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }

        private CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association(@"GoodsInInventoryDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }

        private InventoryCommandDim fInventoryCommandDimId;
        [Association(@"GoodsInInventoryDetailReferencesInventoryCommandDim")]
        public InventoryCommandDim InventoryCommandDimId
        {
            get { return fInventoryCommandDimId; }
            set { SetPropertyValue<InventoryCommandDim>("InventoryCommandDimId", ref fInventoryCommandDimId, value); }
        }
    }
}
