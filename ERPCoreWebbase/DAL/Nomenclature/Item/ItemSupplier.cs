using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Item
{

    public partial class ItemSupplier
    {
        public ItemSupplier(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
