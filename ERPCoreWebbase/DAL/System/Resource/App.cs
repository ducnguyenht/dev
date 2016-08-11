using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.System.Resource
{

    public partial class App
    {
        public App(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
