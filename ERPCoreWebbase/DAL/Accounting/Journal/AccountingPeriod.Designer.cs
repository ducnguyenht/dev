using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Staging.Accounting.Journal;
using NAS.DAL.Staging.Accounting.Ledger;
using NAS.DAL.Accounting.Period;
namespace NAS.DAL.Accounting.Journal
{
    public partial class AccountingPeriod : XPCustomObject
    {

        #region Properties
        Guid fAccountingPeriodId;
        [Key(true)]
        public Guid AccountingPeriodId
        {
            get { return fAccountingPeriodId; }
            set { SetPropertyValue<Guid>("AccountingPeriodId", ref fAccountingPeriodId, value); }
        }

        //private AccountingPeriod fParentAccountingPeriodId;
        //[Association(@"AccountingPeriodParentAccountingPeriod")]
        //public AccountingPeriod ParentAccountingPeriodId
        //{
        //    get { return fParentAccountingPeriodId; }
        //    set { SetPropertyValue<AccountingPeriod>("ParentAccountingPeiodId", ref fParentAccountingPeriodId, value); }
        //}

        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        bool fIsActive;
        public bool IsActive
        {
            get { return fIsActive; }
            set { SetPropertyValue<bool>("IsActive", ref fIsActive, value); }
        }

        DateTime fFromDateTime;
        public DateTime FromDateTime
        {
            get { return fFromDateTime; }
            set { SetPropertyValue<DateTime>("FromDateTime", ref fFromDateTime, value); }
        }

        DateTime fToDateTime;
        public DateTime ToDateTime
        {
            get { return fToDateTime; }
            set { SetPropertyValue<DateTime>("ToDateTime", ref fToDateTime, value); }
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
        #endregion

        #region References

        Organization fOrganizationId;
        [Association(@"AccountingReferencesOrganization")]
        public Organization OrganizationId
        {
            get { return fOrganizationId; }
            set { SetPropertyValue<Organization>("OrganizationId", ref fOrganizationId, value); }
        }

        private AccountingPeriodType fAccountingPeriodTypeId;
        [Association(@"AccountingPeriodReferencesNAS.DAL.Accounting.Period.AccountingPeriodType")]
        public AccountingPeriodType AccountingPeriodTypeId{
            get{return fAccountingPeriodTypeId;}
            set{ SetPropertyValue<AccountingPeriodType>("AccountingPeriodTypeId",ref fAccountingPeriodTypeId, value);}
        }

        [Association(@"TransactionReferencesAccountingPeriod", typeof(Transaction)), Aggregated]
        public XPCollection<Transaction> Transactions { get { return GetCollection<Transaction>("Transactions"); } }
        [Association(@"InventoryTransactionReferencesAccountingPeriod", typeof(InventoryTransaction)), Aggregated]
        public XPCollection<InventoryTransaction> InventoryTransactions { get { return GetCollection<InventoryTransaction>("InventoryTransactions"); } }

        [Association(@"DoubleEntryJournalReferencesNAS.DAL.Accounting.Journal.AccountingPeriod", typeof(DoubleEntryJournal)), Aggregated]
        public XPCollection<DoubleEntryJournal> DoubleEntryJournals { get { return GetCollection<DoubleEntryJournal>("DoubleEntryJournals"); } }

        [Association(@"DoubleEntryLedgerReferencesNAS.DAL.Accounting.Journal.AccountingPeriod", typeof(DoubleEntryLedger))]
        public XPCollection<DoubleEntryLedger> DoubleEntryLedgers { get { return GetCollection<DoubleEntryLedger>("DoubleEntryLedgers"); } }

        //[Association(@"AccountingPeriodParentAccountingPeriod")]
        //public XPCollection<AccountingPeriod> AccountingPeriods { get { return GetCollection<AccountingPeriod>("AccountingPeriods"); } }

        [Association(@"AccountingPeriodReferencesAccountingPeriodComposite", typeof(AccountingPeriodComposite))]
        public XPCollection<AccountingPeriodComposite> AccountingPeriodComposite_Children { get { return GetCollection<AccountingPeriodComposite>("AccountingPeriodComposite_Children"); } }

        [Association(@"AccountingPeriodReferencesAccountingPeriodComposite_Children", typeof(AccountingPeriodComposite))]
        public XPCollection<AccountingPeriodComposite> AccountingPeriodComposites { get { return GetCollection<AccountingPeriodComposite>("AccountingPeriodComposites"); } }

        #endregion
    }
}
