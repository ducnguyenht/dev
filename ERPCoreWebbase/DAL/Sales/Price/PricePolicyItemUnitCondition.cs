using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;

namespace NAS.DAL.Sales.Price
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class PricePolicyItemUnitCondition : PricePolicyCondition
    {
        public PricePolicyItemUnitCondition(Session session)
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
        //private Guid _PricePolicyItemUnitConditionId;
        private ItemUnit _ItemUnitId;

        //Properties
        //[Key(true)]
        //public Guid PricePolicyItemUnitConditionId
        //{
        //    get
        //    {
        //        return _PricePolicyItemUnitConditionId;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Guid>("PricePolicyItemUnitConditionId", ref _PricePolicyItemUnitConditionId, value);
        //    }
        //}

        [Association(@"PricePolicyItemUnitConditionReferencesItemUnit")]
        public ItemUnit ItemUnitId
        {
            get
            {
                return _ItemUnitId;
            }
            set
            {
                SetPropertyValue<ItemUnit>("ItemUnitId", ref _ItemUnitId, value);
            }
        }
    }
}
