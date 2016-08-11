using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.BI.Inventory
{
    public class InventoryEntryDetail : XPCustomObject
    {
        public InventoryEntryDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properites
        private Guid _InventoryEntryDetailId;
        [Key(true)]
        public Guid InventoryEntryDetailId
        {
            get { return _InventoryEntryDetailId; }
            set { SetPropertyValue<Guid>("InventoryEntryDetailId", ref _InventoryEntryDetailId, value); }
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
        private InventoryDim _InventoryDimId;
        [Association("InventoryEntryDetail-InventoryDim")]
        public InventoryDim InventoryDimId
        {
            get { return _InventoryDimId; }
            set { SetPropertyValue<InventoryDim>("InventoryDimId", ref _InventoryDimId, value); }
        }

        private InventoryTransactionDim _InventoryTransactionDimId;
        [Association("InventoryEntryDetail-InventoryTransactionDim")]
        public InventoryTransactionDim InventoryTransactionDimId
        {
            get { return _InventoryTransactionDimId; }
            set { SetPropertyValue<InventoryTransactionDim>("InventoryTransactionDimId", ref _InventoryTransactionDimId, value); }
        }

        private CorrespondInventoryDim _CorrespondInventoryDimId;
        [Association("InventoryEntryDetail-CorrespondInventoryDim")]
        public CorrespondInventoryDim CorrespondInventoryDimId
        {
            get { return _CorrespondInventoryDimId; }
            set { SetPropertyValue<CorrespondInventoryDim>("CorrespondInventoryDimId", ref _CorrespondInventoryDimId, value); }
        }

        private ItemInventoryByArtifact _ItemInventoryByArtifactId;
        [Association("InventoryEntryDetail-ItemInventoryByArtifact")]
        public ItemInventoryByArtifact ItemInventoryByArtifactId
        {
            get { return _ItemInventoryByArtifactId; }
            set { SetPropertyValue<ItemInventoryByArtifact>("ItemInventoryByArtifactId", ref _ItemInventoryByArtifactId, value); }
        }
        #endregion
    }
}
