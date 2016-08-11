using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.System.Privileage
{

    public partial class PrivilegeDepartment
    {
        public PrivilegeDepartment(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
