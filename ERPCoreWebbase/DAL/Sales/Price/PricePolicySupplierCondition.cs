using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.DAL.Sales.Price
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class PricePolicySupplierCondition : PricePolicyCondition
    {
        public PricePolicySupplierCondition(Session session)
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
        //private Guid _PricePolicySupplierConditionId;
        private SupplierOrg _SupplierOrgId;

        //[Key(true)]
        //public Guid PricePolicySupplierConditionId
        //{
        //    get
        //    {
        //        return _PricePolicySupplierConditionId;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Guid>("PricePolicySupplierConditionId", ref _PricePolicySupplierConditionId, value);
        //    }
        //}

        [Association(@"PricePolicySupplierConditionReferencesSupplierOrg")]
        public SupplierOrg SupplierOrgId
        {
            get
            {
                return _SupplierOrgId;
            }
            set
            {
                SetPropertyValue<SupplierOrg>("SupplierOrgId", ref _SupplierOrgId, value);
            }
        }
    }
}
