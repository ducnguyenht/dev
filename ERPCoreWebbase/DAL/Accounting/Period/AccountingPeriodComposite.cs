using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;

namespace NAS.DAL.Accounting.Period
{
    public partial class AccountingPeriodComposite : XPCustomObject
    {
        public AccountingPeriodComposite(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic
        #endregion

        #region Properties

        private Guid fAccountingPeriodCompositeId;
        [Key(true)]
        public Guid AccountingPeriodCompositeId
        {
            get { return fAccountingPeriodCompositeId; }
            set { SetPropertyValue<Guid>("AccountingPeriodCompositeId", ref fAccountingPeriodCompositeId, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        #endregion

        #region References
        AccountingPeriod fAccountingPeriodId;
        [Association(@"AccountingPeriodReferencesAccountingPeriodComposite")]
        public AccountingPeriod AccountingPeriodId
        {
            get { return fAccountingPeriodId; }
            set { SetPropertyValue<AccountingPeriod>("AccountingPeriodId", ref fAccountingPeriodId, value); }
        }

        AccountingPeriod fChildrenAccountingPeriodId;
        [Association(@"AccountingPeriodReferencesAccountingPeriodComposite_Children")]
        public AccountingPeriod ChildrenAccountingPeriodId
        {
            get { return fChildrenAccountingPeriodId; }
            set { SetPropertyValue<AccountingPeriod>("ChildrenAccountingPeriodId", ref fChildrenAccountingPeriodId, value); }
        }
        #endregion
    }
}
