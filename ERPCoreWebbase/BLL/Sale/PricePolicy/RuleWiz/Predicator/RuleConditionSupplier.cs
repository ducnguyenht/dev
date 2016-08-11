// Copyright 2007 Herre Kuijpers - <herre@xs4all.nl>
//
// This source file(s) may be redistributed, altered and customized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using NAS.BO.Sale.PricePolicy;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Predicator
{
    [Serializable]
    public class RuleConditionSupplier : NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition
    {
        // required to serialize
        public RuleConditionSupplier()
        {
            //labelGUIText = @"Áp dụng cho hàng hóa thuộc";
            //HyperlinkGUIText = @"Nhà cung cấp";
        }

        //public RuleConditionSupplier(string textFormat) : this(textFormat, null)
        //{
        //}

        //public RuleConditionSupplier(string textFormat, object defaultValue)
        //{
        //    DefaultValueText = "Supplier";
        //    TextFormat = textFormat;
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
                    return true;

                foreach (DataGrdSupplierListSelection d in condition)
                {
                    foreach (ItemSupplier IS in cogs.ItemUnitId.ItemId.ItemSuppliers)
                    {
                        if (IS.SupplierOrgId.OrganizationId.Equals(d.OrganizationId))
                            return true;
                    }
                }
            }
            return false;
        }

        public override bool UpdateValue(object sender)
        {
            return false;
        }

        public override object Clone()
        {
            return new RuleConditionSupplier();
        }

        public override void SaveRuleCondition(DevExpress.Xpo.Session session, Guid key, object list)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateSupplierConditionInPolicy(session, key, list as List<DataGrdSupplierListSelection>);
        }

        public override void RemoveRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            bo.updateSupplierConditionInPolicy(session, key, new List<DataGrdSupplierListSelection>());
        }

        public override object LoadRuleCondition(DevExpress.Xpo.Session session, Guid key)
        {
            PricePolicyBO bo = new PricePolicyBO();
            return bo.loadSupplierConditionInPolicy(session, key);
        }
    }
}
