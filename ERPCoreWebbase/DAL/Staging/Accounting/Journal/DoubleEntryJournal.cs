using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Journal
{
    public partial class DoubleEntryJournal : XPCustomObject, IDALValidate
    {
        public DoubleEntryJournal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private int fDoubleEntryJournalId;
        [Key(true)]
        public int DoubleEntryJournalId
        {
            get { return fDoubleEntryJournalId; }
            set { SetPropertyValue<int>("DoubleEntryJournalId", ref fDoubleEntryJournalId, value); }
        }

        private double fAmountCal;
        public double AmountCal
        {
            get { return fAmountCal; }
            set { SetPropertyValue<double>("AmountCal", ref fAmountCal, value); }
        }

        private double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }

        private double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
        }

        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }

        //link between 2 tables

        private AccountingTransaction fAccountingTransaction;
        [Association("DoubleEntryJournalReferencesAccountingTransaction")]
        public AccountingTransaction AccountingTransactionId
        {
            get { return fAccountingTransaction; }
            set { SetPropertyValue<AccountingTransaction>("AccountingTransactionId", ref fAccountingTransaction, value); }
        }

        private BookingAsset fBookingAsset;
        [Association("DoubleEntryJournalReferencesBookingAsset")]
        public BookingAsset BookingAssetId
        {
            get { return fBookingAsset; }
            set { SetPropertyValue<BookingAsset>("BookingAssetId", ref fBookingAsset, value); }
        }

        private NAS.DAL.Accounting.Journal.AccountingPeriod fAccountingPeriodId;
        [Association("DoubleEntryJournalReferencesNAS.DAL.Accounting.Journal.AccountingPeriod")]
        public NAS.DAL.Accounting.Journal.AccountingPeriod AccountingPeriodId
        {
            get { return fAccountingPeriodId; }
            set { SetPropertyValue<NAS.DAL.Accounting.Journal.AccountingPeriod>("AccountingPeriodId", ref fAccountingPeriodId, value); }
        }

        private NAS.DAL.Accounting.AccountChart.Account fAccountId;
        [Association("DoubleEntryJournalReferencesNAS.DAL.Accounting.AccountChart.Account")]
        public NAS.DAL.Accounting.AccountChart.Account AccountId
        {
            get { return fAccountId; }
            set { SetPropertyValue<NAS.DAL.Accounting.AccountChart.Account>("AccountId", ref fAccountId, value); }
        }

        [Association(@"AccountRoleReferencesDoubleEntryJournal", typeof(AccountRole))]
        public XPCollection<AccountRole> AccountRoles {get{ return GetCollection<AccountRole>("AccountRoles");}}

        //END link between 2 tables

        //validate database

        public bool ValidateParameter()
        {
            return true;
        }

        public bool ValidateUnique()
        {
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
