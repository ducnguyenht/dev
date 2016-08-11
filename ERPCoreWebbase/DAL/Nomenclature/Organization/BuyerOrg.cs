using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Nomenclature.Organization
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class BuyerOrg : TradingOrg
    {
        public BuyerOrg(Session session)
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
