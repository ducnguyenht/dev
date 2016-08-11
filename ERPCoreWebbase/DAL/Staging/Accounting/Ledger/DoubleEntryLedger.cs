using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Staging.Accounting.Journal;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Ledger
{
    public partial class DoubleEntryLedger : XPCustomObject, IDALValidate
    {
        public DoubleEntryLedger(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private int fDoubleEntryLedgerId;
        [Key(true)]
        public int DoubleEntryLedgerId
        {
            get { return fDoubleEntryLedgerId; }
            set { SetPropertyValue<int>("DoubleEntryLedgerId", ref fDoubleEntryLedgerId, value); }
        }

        private double fBalance;
        public double Balance
        {
            get { return fBalance; }
            set { SetPropertyValue<double>("Balance", ref fBalance, value); }
        }

        //link between 2 tables

        private NAS.DAL.Accounting.AccountChart.Account fAccount;
        [Association("DoubleEntryLedgerReferencesNAS.DAL.Accounting.AccountChart.Account")]
        public NAS.DAL.Accounting.AccountChart.Account Account
        {
            get { return fAccount; }
            set { SetPropertyValue<NAS.DAL.Accounting.AccountChart.Account>("Account", ref fAccount, value); }
        }

        private AccountingTransaction fAccountingTransactionId;
        [Association("DoubleEntryLedgerReferencesAccountingTransaction")]
        public AccountingTransaction AccountingTransactionId
        {
            get { return fAccountingTransactionId; }
            set { SetPropertyValue<AccountingTransaction>("AccountingTransactionId", ref fAccountingTransactionId, value); }
        }

        private NAS.DAL.Accounting.Journal.AccountingPeriod fAccountingPeriodId;
        [Association("DoubleEntryLedgerReferencesNAS.DAL.Accounting.Journal.AccountingPeriod")]
        public NAS.DAL.Accounting.Journal.AccountingPeriod AccountingPeriodId
        {
            get { return fAccountingPeriodId; }
            set { SetPropertyValue<NAS.DAL.Accounting.Journal.AccountingPeriod>("AccountingPeriodId", ref fAccountingPeriodId, value); }
        }

        private BookingAsset fBookingAssetId;
        [Association("DoubleEntryLedgerReferencesBookingAsset")]
        public BookingAsset BookingAssetId
        {
            get { return fBookingAssetId; }
            set { SetPropertyValue<BookingAsset>("BookingAssetId", ref fBookingAssetId, value);}
        }

        [Association(@"AccountRoleReferencesDoubleEntryLedger", typeof(AccountRole))]
        public XPCollection<AccountRole> AccountRoles { get { return GetCollection<AccountRole>("AccountRoles"); } }
        
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
