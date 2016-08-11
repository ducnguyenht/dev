using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Journal
{
    public partial class GeneralJournalBalanceForward
    {
        public GeneralJournalBalanceForward(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
