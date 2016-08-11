using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.Metadata;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Nomenclature.Organization;
namespace NAS.DAL.Nomenclature.Item
{
    public partial class Item
    {
        public Item(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
