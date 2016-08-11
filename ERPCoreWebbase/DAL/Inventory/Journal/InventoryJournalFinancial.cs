using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;

namespace NAS.DAL.Inventory.Journal
{
    public class InventoryJournalFinancial: XPCustomObject
    {
        public InventoryJournalFinancial(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Fields...
        private Guid _InventoryJournalFinancialId;
        private InventoryJournal _InventoryJournalId;
        private Transaction _TransactionId;
        short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }


        [Key(true)]
        public Guid InventoryJournalFinancialId
        {
            get
            {
                return _InventoryJournalFinancialId;
            }
            set
            {
                SetPropertyValue("InventoryJournalFinancialId", ref _InventoryJournalFinancialId, value);
            }
        }

        [Association("InventoryJournalFinancialReferenceInventoryJournal")]
        public InventoryJournal InventoryJournalId
        {
            get
            {
                return _InventoryJournalId;
            }
            set
            {
                SetPropertyValue("InventoryJournalId", ref _InventoryJournalId, value);
            }
        }

        [Association("InventoryJournalFinancialReferenceTransaction")]
        public Transaction TransactionId
        {
            get
            {
                return _TransactionId;
            }
            set
            {
                SetPropertyValue("TransactionId", ref _TransactionId, value);
            }
        }
    }
}
