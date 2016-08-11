using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.System.Resource
{

    public partial class AppComponent
    {
        public AppComponent(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
