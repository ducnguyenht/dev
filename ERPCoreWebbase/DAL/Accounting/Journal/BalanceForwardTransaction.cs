using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Journal
{
    public partial class BalanceForwardTransaction
    {
        public BalanceForwardTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        protected override void OnSaving()
        {
            base.OnSaving();
            //this.IssueDate = this.AccountingPeriodId.FromDateTime;
        }

        #endregion
    }
}
