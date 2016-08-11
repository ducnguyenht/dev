using System;
using DevExpress.Xpo;

namespace NAS.DAL.Nomenclature.Organization
{

    public class SalesOrg : TradingOrg
    {
        public SalesOrg(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}