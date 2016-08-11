using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Inventory;

namespace NAS.DAL.BI.Accounting.GoodsInTransit
{
    public class GoodsInTransitForSaleDetail : XPCustomObject
    {
        public GoodsInTransitForSaleDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fGoodsInTransitForSaleDetailId;
        [Key(true)]
        public Guid GoodsInTransitForSaleDetailId
        {
            get { return fGoodsInTransitForSaleDetailId; }
            set { SetPropertyValue<Guid>("GoodsInTransitForSaleDetailId", ref fGoodsInTransitForSaleDetailId, value); }
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

        private GoodsInTransitForSaleSummary_Fact fGoodsInTransitForSaleSummary_FactId;
        [Association(@"GoodsInTransitForSaleDetailReferencesGoodsInTransitForSaleSummary_Fact")]
        public GoodsInTransitForSaleSummary_Fact GoodsInTransitForSaleSummary_FactId
        {
            get { return fGoodsInTransitForSaleSummary_FactId; }
            set { SetPropertyValue<GoodsInTransitForSaleSummary_Fact>("GoodsInTransitForSaleSummary_FactId", ref fGoodsInTransitForSaleSummary_FactId, value); }
        }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association(@"GoodsInTransitForSaleDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private FinancialTransactionDim fFinancialTransactionDimId;
        [Association(@"GoodsInTransitForSaleDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }

        private CurrencyDim fCurrencyDimId;
        [Association(@"GoodsInTransitForSaleDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }

        private CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association(@"GoodsInTransitForSaleDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }

        private InventoryCommandDim fInventoryCommandDimId;
        [Association(@"GoodsInTransitForSaleDetailReferencesInventoryCommandDim")]
        public InventoryCommandDim InventoryCommandDimId
        {
            get { return fInventoryCommandDimId; }
            set { SetPropertyValue<InventoryCommandDim>("InventoryCommandDimId", ref fInventoryCommandDimId, value); }
        }
    }
}
