using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.BO.Sale.PricePolicy;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Predicator
{
    public class RuleExceptionSupplier : NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition
    {
        // required to serialize
        public RuleExceptionSupplier()
        {
        }

        //public RuleExceptionSupplier(string textFormat) : this(textFormat, null)
        //{
        //}

        //public RuleExceptionSupplier(string textFormat, object defaultValue)
        //{
        //    //DefaultValueText = "Supplier";
        //    //TextFormat = textFormat;
        //    Value = defaultValue;
        //}

        public override bool Evaluate(object o)
        {
            if (o != null)
            {
                COGS cogs = o as COGS;
                List<DataGrdSupplierListSelection> condition = Value as List<DataGrdSupplierListSelection>;

                if (condition != null && condition.Count == 1 &&
                    condition[0].Code.Equals(Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL))
                    return false;

                foreach (DataGrdSupplierListSelection d in condition)
                {
                    foreach(ItemSupplier IS in cogs.ItemUnitId.ItemId.ItemSuppliers)
                    {
                        if (IS.SupplierOrgId.OrganizationId.Equals(d.OrganizationId))
                            return false;
                    }
                }
            }
            return true;
        }

        public override bool UpdateValue(object sender)
        {
            return false;
        }

        public override object Clone()
        {
            return null;
            //return new RuleExceptionSupplier(TextFormat, Value);
        }

        public override void SaveRuleCondition(DevExpress.Xpo.Session session, Guid key, object list)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateSupplierExceptionInPolicy(session, key, list as List<DataGrdSupplierListSelection>);
        }

        public override void RemoveRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateSupplierExceptionInPolicy(session, key, new List<DataGrdSupplierListSelection>());
        }

        public override object LoadRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            return bo.loadSupplierExceptionInPolicy(session, key);
        }
    }
}