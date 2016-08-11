using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument
{

    public partial class CustomFieldDataDateTime
    {
        public CustomFieldDataDateTime(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
