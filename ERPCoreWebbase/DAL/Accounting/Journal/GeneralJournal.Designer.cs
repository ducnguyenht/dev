using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.JournalAllocation;

namespace NAS.DAL.Accounting.Journal
{
    public partial class GeneralJournal : XPCustomObject    
    {

        Guid fGeneralJournalId;
        [Key(true)]
        public Guid GeneralJournalId
        {
            get { return fGeneralJournalId; }
            set { SetPropertyValue<Guid>("GeneralJournalId", ref fGeneralJournalId, value); }
        }

        Account fAccountId;
        [Association(@"AccountReferencesGeneralJournal")]
        public Account AccountId
        {
            get { return fAccountId; }
            set { SetPropertyValue<Account>("AccountId", ref fAccountId, value); }
        }

        double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }

        double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
        }
                
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        char fJournalType;        
        public char JournalType
        {
            get { return fJournalType; }
            set { SetPropertyValue<char>("JournalType", ref fJournalType, value); }
        }

        private DateTime fCreateDate;
        public DateTime CreateDate
        {
            get
            {
                return fCreateDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref fCreateDate, value);
            }
        }

        //GeneralJournal fPlanningGeneralJournalId;        
        //[Association(@"ParentOf")]
        //public GeneralJournal PlanningGeneralJournalId
        //{
        //    get { return fPlanningGeneralJournalId; }
        //    set { SetPropertyValue<GeneralJournal>("PlanningGeneralJournalId", ref fPlanningGeneralJournalId, value); }
        //}

        //[Association(@"ParentOf", typeof(GeneralJournal))]
        //public XPCollection<GeneralJournal> GeneralJournals
        //{
        //    get { return GetCollection<GeneralJournal>("GeneralJournals"); }
        //}

        Transaction fTransactionId;
        [Association(@"TransactionReferencesGeneralJournal")]
        public Transaction TransactionId
        {
            get { return fTransactionId; }
            set { SetPropertyValue<Transaction>("TransactionId", ref fTransactionId, value); }
        }

        private NAS.DAL.Accounting.Currency.Currency fCurrency;
        [Association(@"GeneralJournalReferencesCurrency")]
        public NAS.DAL.Accounting.Currency.Currency CurrencyId
        {
            get { return fCurrency; }
            set { SetPropertyValue<NAS.DAL.Accounting.Currency.Currency>("CurrencyId", ref fCurrency, value); }
        }

        [Association(@"GeneralJournalObjectReferencesGeneralJournal", typeof(GeneralJournalObject)), Aggregated]
        public XPCollection<GeneralJournalObject> GeneralJournalObjects { get { return GetCollection<GeneralJournalObject>("GeneralJournalObjects"); } }

        [Association(@"GeneralJournalCustomTypeReferencesGeneralJournal", typeof(GeneralJournalCustomType)), Aggregated]
        public XPCollection<GeneralJournalCustomType> GeneralJournalCustomTypes { get { return GetCollection<GeneralJournalCustomType>("GeneralJournalCustomTypes"); } }
    }
}
