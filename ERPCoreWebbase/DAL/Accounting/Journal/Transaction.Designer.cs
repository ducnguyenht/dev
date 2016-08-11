using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Entry;
using NAS.DAL.Inventory.Journal;

namespace NAS.DAL.Accounting.Journal
{
    public partial class Transaction : XPCustomObject
    {
        Guid fTransactionId;
        [Key(true)]
        public Guid TransactionId
        {
            get { return fTransactionId; }
            set { SetPropertyValue<Guid>("TransactionId", ref fTransactionId, value); }
        }

        //Guid fPreviousTransactionId;
        //public Guid PreviousTransactionId
        //{
        //    get { return fPreviousTransactionId; }
        //    set { SetPropertyValue<Guid>("PreviousTransactionId", ref fPreviousTransactionId, value); }
        //}

        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        AccountingPeriod fAccountingPeriodId;
        [Association(@"TransactionReferencesAccountingPeriod")]
        public AccountingPeriod AccountingPeriodId
        {
            get { return fAccountingPeriodId; }
            set { SetPropertyValue<AccountingPeriod>("AccountingPeriodId", ref fAccountingPeriodId, value); }
        }

        double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<double>("Amount", ref fAmount, value); }
        }

        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }

        DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        DateTime fUpdateDate;
        public DateTime UpdateDate
        {
            get { return fUpdateDate; }
            set { SetPropertyValue<DateTime>("UpdateDate", ref fUpdateDate, value); }
        }

        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        [Association("InventoryJournalFinancialReferenceTransaction")]
        public XPCollection<InventoryJournalFinancial> InventoryJournalFinancials
        {
            get
            {
                return GetCollection<InventoryJournalFinancial>("InventoryJournalFinancials");
            }
        }

        [Association(@"TransactionReferencesGeneralJournal", typeof(GeneralJournal))]
        public XPCollection<GeneralJournal> GeneralJournals { get { return GetCollection<GeneralJournal>("GeneralJournals"); } }
        [Association(@"TransactionReferencesGeneralLedger", typeof(GeneralLedger))]
        public XPCollection<GeneralLedger> GeneralLedgers { get { return GetCollection<GeneralLedger>("GeneralLedgers"); } }

        [Association("TransactionObjectReferenceTransaction"), Aggregated]
        public XPCollection<TransactionObject> TransactionObjects
        {
            get
            {
                return GetCollection<TransactionObject>("TransactionObjects");
            }
        }

        public string BookingStatus
        {
            get
            {
                if (this.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    return "Đã ghi sổ";
                }
                else if (this.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE)
                {
                    return "Chưa ghi sổ";
                }
                else
                {
                    return "-";
                }
            }
        }

    }
}
