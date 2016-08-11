using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument
{

    public partial class CustomFieldDataString
    {
        public CustomFieldDataString(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
