using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.System.Resource
{

    public partial class AppComponentContent
    {
        public AppComponentContent(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
