using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.BO.Sale.PricePolicy;
using NAS.DAL.Inventory.Ledger;

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Predicator
{
    public class RuleExceptionItemUnit : NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition
    {
        public RuleExceptionItemUnit()
        {
            //labelGUIText = @"Áp dụng cho các";
            //HyperlinkGUIText = @"Hàng hóa";
        }

        //public RuleExceptionItemUnit(string textFormat) : this(textFormat, null)
        //{
        //}

        //public RuleExceptionItemUnit(string textFormat, object defaultValue)
        //{
        //    DefaultValueText = "ItemUnit";
        //    TextFormat = textFormat;
        //    Value = defaultValue;
        //}

        public override bool Evaluate(object o)
        {
            if (o != null)
            {
                COGS cogs = o as COGS;
                List<DataGrdItemUnitListSelection> condition = Value as List<DataGrdItemUnitListSelection>;

                if (condition != null && condition.Count == 1 &&
                    condition[0].RowStatus == Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL)
                    return false;
                
                foreach (DataGrdItemUnitListSelection d in condition)
                {
                    if (d.ItemUnitId.Equals(cogs.ItemUnitId.ItemUnitId))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override object Clone()
        {
            return new RuleExceptionItemUnit();
        }

        public override void SaveRuleCondition(DevExpress.Xpo.Session session, Guid key, object list)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateItemUnitExceptionInPolicy(session, key, list as List<DataGrdItemUnitListSelection>);
        }

        public override void RemoveRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateItemUnitExceptionInPolicy(session, key, new List<DataGrdItemUnitListSelection>());
        }

        public override object LoadRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            return bo.loadItemUnitExceptionInPolicy(session, key);
        }
    }
}