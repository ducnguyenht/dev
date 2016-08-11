using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Entry
{
    public partial class BookingEntry
    {
        public BookingEntry(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        protected override void OnSaving()
        {
            base.OnSaving();
            this.IssueDate = this.IssueDate.AddHours(DateTime.Now.Hour - this.IssueDate.Hour);
            this.IssueDate = this.IssueDate.AddMinutes(DateTime.Now.Minute - this.IssueDate.Minute);
            this.IssueDate = this.IssueDate.AddSeconds(DateTime.Now.Second - this.IssueDate.Second);
            this.IssueDate = this.IssueDate.AddMilliseconds(DateTime.Now.Millisecond - this.IssueDate.Millisecond);
        }

        #endregion
    }
}
