using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Staging.Accounting.Journal;
using NAS.DAL.Staging.Accounting.Ledger;
using NAS.DAL.Accounting.Configure;
using NAS.DAL.Vouches.Allocation;

namespace NAS.DAL.Accounting.AccountChart
{
    public partial class Account : XPCustomObject
    {
        Guid fAccountId;
        [Key(true)]
        public Guid AccountId
        {
            get { return fAccountId; }
            set { SetPropertyValue<Guid>("AccountId", ref fAccountId, value); }
        }

        AccountType fAccountTypeId;
        [Association(@"AccountReferencesAccountType")]
        public AccountType AccountTypeId
        {
            get { return fAccountTypeId; }
            set { SetPropertyValue<AccountType>("AccountTypeId", ref fAccountTypeId, value); }
        }
        
        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        int fLevel;        
        public int Level
        {
            get { return fLevel; }
            set { SetPropertyValue<int>("Level", ref fLevel, value); }
        }

        int fBalanceType;
        public int BalanceType
        {
            get { return fBalanceType; }
            set { SetPropertyValue<int>("BalanceType", ref fBalanceType, value); }
        }

        string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        Organization fOrganizationId;
        [Association(@"OrganizationReferencesAccount")]
        public Organization OrganizationId
        {
            get { return fOrganizationId; }
            set { SetPropertyValue<Organization>("OrganizationId", ref fOrganizationId, value); }
        }

        Account fParentAccountId;
        [Association(@"ParentOf")]
        public Account ParentAccountId
        {
            get { return fParentAccountId; }
            set { SetPropertyValue<Account>("ParentAccountId", ref fParentAccountId, value); }
        }

        [Association(@"ParentOf", typeof(Account)), Aggregated]
        public XPCollection<Account> Accounts
        {
            get
            {
                return GetCollection<Account>("Accounts");
            }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
    

        [Association(@"AccountReferencesGeneralJournal", typeof(GeneralJournal)), Aggregated]
        public XPCollection<GeneralJournal> GeneralJournals { get { return GetCollection<GeneralJournal>("GeneralJournals"); } }

        [Association(@"InventoryJournalReferencesAccount", typeof(NAS.DAL.Inventory.Journal.InventoryJournal)), Aggregated]
        public XPCollection<NAS.DAL.Inventory.Journal.InventoryJournal> InventoryJournals { get { return GetCollection<NAS.DAL.Inventory.Journal.InventoryJournal>("InventoryJournals"); } }

        [Association(@"AccountReferencesGeneralLedger", typeof(GeneralLedger)), Aggregated]
        public XPCollection<GeneralLedger> GeneralLedgers { get { return GetCollection<GeneralLedger>("GeneralLedgers"); } }

        [Association(@"InventoryLedgerReferencesAccount", typeof(NAS.DAL.Inventory.Ledger.InventoryLedger)), Aggregated]
        public XPCollection<NAS.DAL.Inventory.Ledger.InventoryLedger> InventoryLedgers { get { return GetCollection<NAS.DAL.Inventory.Ledger.InventoryLedger>("InventoryLedgers"); } }

        [Association(@"DoubleEntryJournalReferencesNAS.DAL.Accounting.AccountChart.Account", typeof(DoubleEntryJournal))]
        public XPCollection<DoubleEntryJournal> DoubleEntryJournals { get { return GetCollection<DoubleEntryJournal>("DoubleEntryJournals"); } }

        [Association(@"DoubleEntryLedgerReferencesNAS.DAL.Accounting.AccountChart.Account", typeof(DoubleEntryLedger))]
        public XPCollection<DoubleEntryLedger> DoubleEntryLedgers { get { return GetCollection<DoubleEntryLedger>("DoubleEntryLedgers"); } }

        [Association(@"AllocationAccountTemplatesRefencesAccount",typeof(AllocationAccountTemplate))]
        public XPCollection<AllocationAccountTemplate> AllocationAccountTemplates
        {
            get
            {
                return GetCollection<AllocationAccountTemplate>("AllocationAccountTemplates");
            }
        }

        [Association(@"VoucherAllocationBookingAccountReferencesNAS.DAL.Accounting.AccoutChart.Accout", typeof(VoucherAllocationBookingAccount))]
        public XPCollection<VoucherAllocationBookingAccount> VoucherAllocationBookingAccounts { get { return GetCollection<VoucherAllocationBookingAccount>("VoucherAllocationBookingAccounts"); } }
    }
}
