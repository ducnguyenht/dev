using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument
{

    public partial class Object
    {
        public Object(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
