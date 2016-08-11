using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;

namespace NAS.DAL.Accounting.Period
{
    public partial class AccountingPeriodType : XPCustomObject
    {
        public AccountingPeriodType(Session session) : base(session) { }

        public AccountingPeriodType()
        {
            // TODO: Complete member initialization
        }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fAccountingPeriodTypeId;
        [Key(true)]
        public Guid AccountingPeriodTypeId
        {
            get { return fAccountingPeriodTypeId; }
            set { SetPropertyValue<Guid>("AccountingPeriodTypeId", ref fAccountingPeriodTypeId, value); }
        }

        private string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private bool fIsDefault;
        public bool IsDefault
        {
            get { return fIsDefault; }
            set { SetPropertyValue<bool>("IsDefault", ref fIsDefault, value); }
        }
        #endregion

        #region Properties
        [Association(@"AccountingPeriodReferencesNAS.DAL.Accounting.Period.AccountingPeriodType", typeof(AccountingPeriod))]
        public XPCollection<AccountingPeriod> AccountingPeriods { get { return GetCollection<AccountingPeriod>("AccountingPeriods"); } }
        #endregion
    }
}
