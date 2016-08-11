using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Journal
{
    public partial class Transaction
    {
        public Transaction(Session session) : base(session) { }
        public override void AfterConstruction() { 
            base.AfterConstruction();
            this.CreateDate = DateTime.Now;
        }

        #region Logic

        protected override void OnSaving()
        {
            base.OnSaving();
            if (this.IssueDate.Year > 1753)
            {
                this.IssueDate = this.IssueDate.AddHours(DateTime.Now.Hour - this.IssueDate.Hour);
                this.IssueDate = this.IssueDate.AddMinutes(DateTime.Now.Minute - this.IssueDate.Minute);
                this.IssueDate = this.IssueDate.AddSeconds(DateTime.Now.Second - this.IssueDate.Second);
                this.IssueDate = this.IssueDate.AddMilliseconds(DateTime.Now.Millisecond - this.IssueDate.Millisecond);
            }
        }

        #endregion

    }
}
