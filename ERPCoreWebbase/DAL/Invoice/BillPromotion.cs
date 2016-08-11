using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Invoice
{

    public partial class BillPromotion
    {
        public BillPromotion(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
