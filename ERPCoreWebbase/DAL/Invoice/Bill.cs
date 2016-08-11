using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Invoice
{

    public partial class Bill
    {
        public Bill(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic

        protected override void OnSaving()
        {
            base.OnSaving();
            if (this.IssuedDate.Year > 1753)
            {
                this.IssuedDate = this.IssuedDate.AddHours(DateTime.Now.Hour - this.IssuedDate.Hour);
                this.IssuedDate = this.IssuedDate.AddMinutes(DateTime.Now.Minute - this.IssuedDate.Minute);
                this.IssuedDate = this.IssuedDate.AddSeconds(DateTime.Now.Second - this.IssuedDate.Second);
                this.IssuedDate = this.IssuedDate.AddMilliseconds(DateTime.Now.Millisecond - this.IssuedDate.Millisecond);
            }
        }

        #endregion
    }
}
