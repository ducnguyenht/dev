using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class TradingOrg
    {
        public TradingOrg(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }

}
