using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Inventory.Ledger;
namespace NAS.DAL.Inventory.Journal
{

    public partial class InventoryTransaction : XPCustomObject
    {
        public InventoryTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        protected override void OnSaving()
        {
            base.OnSaving();
            if (this.IssueDate.Year > 1753)
            {
                this.IssueDate = this.IssueDate.AddHours(DateTime.Now.Hour - this.IssueDate.Hour);
                this.IssueDate = this.IssueDate.AddMinutes(DateTime.Now.Minute - this.IssueDate.Minute);
                this.IssueDate = this.IssueDate.AddSeconds(DateTime.Now.Second - this.IssueDate.Second);
                this.IssueDate = this.IssueDate.AddMilliseconds(DateTime.Now.Millisecond - this.IssueDate.Millisecond);
            }
        }

        #endregion

        private AccountingPeriod _AccountingPeriodId;
        Guid fInventoryTransactionId;
        [Key(true)]
        public Guid InventoryTransactionId
        {
            get { return fInventoryTransactionId; }
            set { SetPropertyValue<Guid>("InventoryTransactionId", ref fInventoryTransactionId, value); }
        }
        //Guid fPreviousInventoryTransactionId;
        //public Guid PreviousInventoryTransactionId
        //{
        //    get { return fPreviousInventoryTransactionId; }
        //    set { SetPropertyValue<Guid>("PreviousInventoryTransactionId", ref fPreviousInventoryTransactionId, value); }
        //}
        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        private DateTime _CreateDate;
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

        private DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get
            {
                return _UpdateDate;
            }
            set
            {
                SetPropertyValue("UpdateDate", ref _UpdateDate, value);
            }
        }
        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        private DateTime _IssueDate;
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

        [Association("InventoryTransactionReferencesAccountingPeriod")]
        public AccountingPeriod AccountingPeriodId
        {
        	get
        	{
        		return _AccountingPeriodId;
        	}
        	set
        	{
        	  SetPropertyValue("AccountingPeriodId", ref _AccountingPeriodId, value);
        	}
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        [Association(@"InventoryJournalReferencesInventoryTransaction", typeof(InventoryJournal))]
        public XPCollection<InventoryJournal> InventoryJournals { get { return GetCollection<InventoryJournal>("InventoryJournals"); } }
        [Association(@"InventoryLedgerReferencesInventoryTransaction", typeof(InventoryLedger))]
        public XPCollection<InventoryLedger> InventoryLedgers { get { return GetCollection<InventoryLedger>("InventoryLedgers"); } }
        [Association(@"InventoryCOGSReferencesInventoryTransaction", typeof(COGS))]
        public XPCollection<COGS> COGSs { get { return GetCollection<COGS>("COGSs"); } }

        [Association("InventoryTransactionObjectReferenceInventoryTransaction", typeof(InventoryTransactionObject)), Aggregated]
        public XPCollection<InventoryTransactionObject> InventoryTransactionObjects { get { return GetCollection<InventoryTransactionObject>("InventoryTransactionObjects"); } }

        [Association("InventoryTransactionCustomTypeReferencesInventoryTransaction", typeof(InventoryTransactionCustomType)), Aggregated]
        public XPCollection<InventoryTransactionCustomType> InventoryTransactionCustomTypes { get { return GetCollection<InventoryTransactionCustomType>("InventoryTransactionCustomTypes"); } }
    
    }

}
