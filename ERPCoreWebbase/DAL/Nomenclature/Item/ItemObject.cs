using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Item
{

    public partial class ItemObject
    {
        public ItemObject(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
