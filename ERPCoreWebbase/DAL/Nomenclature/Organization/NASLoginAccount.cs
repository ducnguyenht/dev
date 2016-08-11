using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class NASLoginAccount
    {
        public NASLoginAccount(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
