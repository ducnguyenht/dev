using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.System.Privileage
{

    public partial class SpecialPrivilege
    {
        public SpecialPrivilege(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
