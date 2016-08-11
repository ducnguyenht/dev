using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.BO.Sale.PricePolicy;
using NAS.DAL.Inventory.Ledger;

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Predicator
{
    public class RuleConditionManufacturer : NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition
    {
        public RuleConditionManufacturer()
        {
        }

        //public RuleConditionManufacturer(string textFormat) : this(textFormat, null)
        //{
        //}

        //public RuleConditionManufacturer(string textFormat, object defaultValue)
        //{
        //    //DefaultValueText = "Manufacturer";
        //    //TextFormat = textFormat;
        //    Value = defaultValue;
        //}

        public override bool Evaluate(object o)
        {
            if (o != null)
            {
                COGS cogs = o as COGS;
                List<DataGrdManufacturerListSelection> condition = Value as List<DataGrdManufacturerListSelection>;

                if (condition != null && condition.Count == 1 &&
                    condition[0].Code.Equals(Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL))
                    return true;
                
                foreach (DataGrdManufacturerListSelection d in condition)
                {
                    if (cogs.ItemUnitId.ItemId.ManufacturerOrgId.OrganizationId.Equals(d.OrganizationId))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override object Clone()
        {
            return new RuleConditionManufacturer();
        }

        public override void SaveRuleCondition(DevExpress.Xpo.Session session, Guid key,
            object list)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateManufacturerConditionInPolicy(session, key, list as List<DataGrdManufacturerListSelection>);
        }

        public override void RemoveRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateManufacturerConditionInPolicy(session, key, new List<DataGrdManufacturerListSelection>());
        }

        public override object LoadRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            return bo.loadManufacturerConditionInPolicy(session, key);
        }
    }
}