using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.BO.Sale.PricePolicy;
using NAS.DAL.Inventory.Ledger;

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Predicator
{
    public class RuleExceptionManufacturer : NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition
    {
        public RuleExceptionManufacturer()
        {
        }

        public override bool Evaluate(object o)
        {
            if (o != null)
            {
                COGS cogs = o as COGS;
                List<DataGrdManufacturerListSelection> condition = Value as List<DataGrdManufacturerListSelection>;

                if (condition != null && condition.Count == 1 &&
                    condition[0].Code.Equals(Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL))
                    return false;

                foreach (DataGrdManufacturerListSelection d in condition)
                {
                    if (cogs.ItemUnitId.ItemId.ManufacturerOrgId.OrganizationId.Equals(d.OrganizationId))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override object Clone()
        {
            return new RuleExceptionManufacturer();
        }

        public override void SaveRuleCondition(DevExpress.Xpo.Session session, Guid key,
            object list)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateManufacturerExceptionInPolicy(session, key, list as List<DataGrdManufacturerListSelection>);
        }

        public override void RemoveRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateManufacturerExceptionInPolicy(session, key, new List<DataGrdManufacturerListSelection>());
        }

        public override object LoadRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            return bo.loadManufacturerExceptionInPolicy(session, key);
        }
    }
}