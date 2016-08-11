using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Staging.Accounting.Ledger;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Journal
{

    public partial class AccountRole : XPCustomObject, IDALValidate
    {
        public AccountRole(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fAccountRoleId;
        [Key(true)]
        public Guid AccountRoleId
        {
            get { return fAccountRoleId; }
            set { SetPropertyValue<Guid>("AccountRoleId", ref fAccountRoleId, value); }
        }
        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }

        //link between 2 tables
        private AccountActor fAccountActorId;
        [Association("AccountRoleReferencesAccountActor")]
        public AccountActor AccountActorId
        {
            get { return fAccountActorId; }
            set { SetPropertyValue<AccountActor>("AccountActorId", ref fAccountActorId, value); }
        }

        private AccountRoleType fAccountRoleTypeId;
        [Association("AccountRoleReferencesAccountRoleType")]
        public AccountRoleType AccountRoleTypeId
        {
            get { return fAccountRoleTypeId; }
            set { SetPropertyValue<AccountRoleType>("AccountRoleTypeId", ref fAccountRoleTypeId, value); }
        }

        private DoubleEntryJournal fDoubleEntryJournalId;
        [Association("AccountRoleReferencesDoubleEntryJournal")]
        public DoubleEntryJournal DoubleEntryJournalId
        {
            get{ return fDoubleEntryJournalId;}
            set { SetPropertyValue<DoubleEntryJournal>("DoubleEntryJournalId", ref fDoubleEntryJournalId, value); }
        }

        private DoubleEntryLedger fDoubleEntryLedgerId;
        [Association("AccountRoleReferencesDoubleEntryLedger")]
        public DoubleEntryLedger DoubleEntryLedgerId
        {
            get { return fDoubleEntryLedgerId; }
            set { SetPropertyValue<DoubleEntryLedger>("DoubleEntryLedgerId", ref fDoubleEntryLedgerId, value); }
        }
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
