using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Inventory;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.ItemInventory
{
    public class FinancialEntryDetail : XPCustomObject
    {
        public FinancialEntryDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid _FinancialEntryDetailId;
        [Key(true)]
        public Guid FinancialEntryDetailId{
            get { return _FinancialEntryDetailId; }
            set { SetPropertyValue<Guid>("FinancialEntryDetailId", ref _FinancialEntryDetailId, value); }
        }

        private decimal _Credit;
        public decimal Credit
        {
            get { return _Credit; }
            set { SetPropertyValue<decimal>("Credit", ref _Credit, value); }
        }

        private decimal _Debit;
        public decimal Debit
        {
            get { return _Debit; }
            set { SetPropertyValue<decimal>("Debit", ref _Debit, value); }
        }

        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }
        #endregion

        #region References
        private ItemInventoryByArtifact _ItemInventoryByArtifactId;
        [Association("FinancialEntryDetail-ItemInventoryByArtifact")]
        public ItemInventoryByArtifact ItemInventoryByArtifactId
        {
            get { return _ItemInventoryByArtifactId; }
            set { SetPropertyValue<ItemInventoryByArtifact>("ItemInventoryByArtifactId", ref _ItemInventoryByArtifactId, value); }
        }

        private FinancialAccountDim _FinancialAccountDimId;
        [Association("FinancialEntryDetail-FinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return _FinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref _FinancialAccountDimId, value); }
        }

        private FinancialTransactionDim _FinancialTransactionDimId;
        [Association("FinancialEntryDetail-FinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return _FinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref _FinancialTransactionDimId, value); }
        }

        private CurrencyDim _CurrencyDimId;
        [Association("FinancialEntryDetail-CurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return _CurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref _CurrencyDimId, value); }
        }

        private CorrespondFinancialAccountDim _CorrespondFinancialAccountDimId;
        [Association("FinancialEntryDetail-CorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return _CorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref _CorrespondFinancialAccountDimId, value); }
        }
        #endregion
    }
}
