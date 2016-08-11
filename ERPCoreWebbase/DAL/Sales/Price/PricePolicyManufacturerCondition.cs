using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.DAL.Sales.Price
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class PricePolicyManufacturerCondition : PricePolicyCondition
    {
        public PricePolicyManufacturerCondition(Session session)
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

        //attribute
        //private Guid _PricePolicyManufacturerConditionId;
        private ManufacturerOrg _ManufacturerOrgId;

        [Association(@"PricePolicyManufacturerConditionReferencesManufacturerOrg")]
        public ManufacturerOrg ManufacturerOrgId
        {
            get
            {
                return _ManufacturerOrgId;
            }
            set
            {
                SetPropertyValue<ManufacturerOrg>("ManufacturerOrgId", ref _ManufacturerOrgId, value);
            }
        }
    }
}
