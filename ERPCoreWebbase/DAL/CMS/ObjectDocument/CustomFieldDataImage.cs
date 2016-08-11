using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument
{

    public partial class CustomFieldDataImage
    {
        public CustomFieldDataImage(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
