using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Journal
{    
    public partial class BookingEntryTransaction
    {
        public BookingEntryTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
