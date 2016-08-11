using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.AccountChart;

namespace NAS.DAL.Accounting.Journal
{
    public partial class GeneralLedger : XPCustomObject  
    {
        Guid fGeneralLedgerId;
        [Key(true)]
        public Guid GeneralLedgerId
        {
            get { return fGeneralLedgerId; }
            set { SetPropertyValue<Guid>("GeneralLedgerId", ref fGeneralLedgerId, value); }
        }

        Account fAccountId;
        [Association(@"AccountReferencesGeneralLedger")]
        public Account AccountId
        {
            get { return fAccountId; }
            set { SetPropertyValue<Account>("AccountId", ref fAccountId, value); }
        }

        Transaction fTransactionId;
        [Association(@"TransactionReferencesGeneralLedger")]
        public Transaction TransactionId
        {
            get { return fTransactionId; }
            set { SetPropertyValue<Transaction>("TransactionId", ref fTransactionId, value); }
        }
        
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }

        DateTime fUpdateDate;
        public DateTime UpdateDate
        {
            get { return fUpdateDate; }
            set { SetPropertyValue<DateTime>("UpdateDate", ref fUpdateDate, value); }
        }

        DateTime fIssuedDate;
        public DateTime IssuedDate
        {
            get { return fIssuedDate; }
            set { SetPropertyValue<DateTime>("IssuedDate", ref fIssuedDate, value); }
        }

        double fBalance;
        public double Balance
        {
            get { return fBalance; }
            set { SetPropertyValue<double>("Balance", ref fBalance, value); }
        }

        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
        }

        double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        bool fIsOriginal;
        public bool IsOriginal
        {
            get { return fIsOriginal; }
            set { SetPropertyValue<bool>("IsOriginal", ref fIsOriginal, value); }
        }

        private NAS.DAL.Accounting.Currency.Currency fCurrency;
        [Association(@"GeneralLedgerReferencesCurrency")]
        public NAS.DAL.Accounting.Currency.Currency CurrencyId
        {
            get { return fCurrency; }
            set { SetPropertyValue<NAS.DAL.Accounting.Currency.Currency>("CurrencyId", ref fCurrency, value); }
        }
    }
}
