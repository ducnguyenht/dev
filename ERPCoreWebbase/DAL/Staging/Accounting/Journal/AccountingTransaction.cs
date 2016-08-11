using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Staging.Accounting.Ledger;
using NAS.DAL.Interface;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Staging.Accounting.Journal
{
    public partial class AccountingTransaction : XPCustomObject, IDALValidate
    {
        public AccountingTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fAccountingTransactionId;
        [Key(true)]
        public Guid AccountingTransactionId
        {
            get { return fAccountingTransactionId; }
            set { SetPropertyValue<Guid>("AccountingTransactionId", ref fAccountingTransactionId, value); }
        }

        private string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        private DateTime fLastUpdateDate;
        public DateTime LastUpdateDate
        {
            get { return fLastUpdateDate; }
            set { SetPropertyValue<DateTime>("LastUpdateDate", ref fLastUpdateDate, value); }
        }

        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }

        //link between 2 tables

        private BusinessArtifact fBusinessArtifactId;
        [Association("AccountingTransactionReferencesBusinessArtifact")]
        public BusinessArtifact BusinessArtifactId
        {
            get { return fBusinessArtifactId; }
            set { SetPropertyValue<BusinessArtifact>("BusinessArtifactId", ref fBusinessArtifactId, value); }
        }

        private AccountingTransactionType fAccountingTransactionTypeId;
        [Association("AccountingTransactionReferencesAccounttingTransactionType")]
        public AccountingTransactionType AccountingTransactionTypeId
        {
            get { return fAccountingTransactionTypeId; }
            set { SetPropertyValue<AccountingTransactionType>("AccountingTrangsactionType", ref fAccountingTransactionTypeId, value); }
        }

        [Association(@"DoubleEntryJournalReferencesAccountingTransaction", typeof(DoubleEntryJournal))]
        public XPCollection<DoubleEntryJournal> DoubleEntryJournals { get { return GetCollection<DoubleEntryJournal>("DoubleEntryJournals"); } }

        [Association(@"DoubleEntryLedgerReferencesAccountingTransaction", typeof(DoubleEntryLedger))]
        public XPCollection<DoubleEntryLedger> DoubleEntryLedgers { get { return GetCollection<DoubleEntryLedger>("DoubleEntryLedgers"); } }

        //END link between 2 tables

        //validate database

        public bool ValidateParameter()
        {
            if (this.Name.Equals(string.Empty))
                return false;
            if (this.Code.Equals(string.Empty))
            {
                return false;
            }
            return true;
        }

        public bool ValidateUnique()
        {
            AccountingTransaction a = this.Session.FindObject<AccountingTransaction>(new BinaryOperator("Code", this.Code, BinaryOperatorType.Equal));
            if(a != null){
                return false;
            }
            return true;
        }

        public bool IsExist()
        {
            return true;
            //throw new NotImplementedException();
        }

        protected override void OnSaving()
        {
            if (ValidateParameter())
            {
                if (ValidateUnique())
                    base.OnSaving();
            }
        }

        //END validate database
    }
}
