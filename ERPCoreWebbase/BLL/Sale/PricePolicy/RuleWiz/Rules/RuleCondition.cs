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
using DevExpress.Xpo;
using NAS.BO.Sale.PricePolicy;

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Rules
{

    public class RuleConditions : RuleParts<RuleCondition>
    {
        /// <summary>
        /// verifies that all conditions apply on the object
        /// </summary>
        /// <param name="o">the (business) object to evaluate</param>
        /// <returns>true if all conditions apply, false otherwise</returns>
        public bool EvaluateAll(object o)
        {
            foreach (RuleCondition rc in this)
            {
                if (!rc.Evaluate(o)) 
                    return false;
            }
            return true;
        }

        /// <summary>
        /// verifies if only one (or more) conditions apply on the object
        /// </summary>
        /// <param name="o">the (business) object to evaluate</param>
        /// <returns>true if one or more conditions apply. false if none apply</returns>
        public bool EvaluateOne(object o)
        {
            foreach (RuleCondition rc in this)
            {
                if (rc.Evaluate(o))
                    return true;
            }
            return false;
        }

        #region ICloneable Members
        public override object Clone()
        {
            RuleConditions list = new RuleConditions();
            foreach (RuleCondition rc in this)
                list.Add((RuleCondition)rc.Clone());

            return list;
        }
        #endregion

    }

    public abstract class RuleCondition : RulePart
    {
        /// <summary>
        /// evaluates if the conditions is met
        /// </summary>
        /// <param name="o">the business object to evaluate</param>
        /// <returns>true if the conditions is met, false otherwise</returns>
        /// 
        public RuleCondition()
        { 
        }

        public const string OPEN_TRANSIT = "open";

        private string description;

        public string Description
        {
            get
            {
                return description;
            }
        }
        
        private string labelGUIText;

        public string LabelGUIText {
            get
            {
                return labelGUIText;
            }
        }

        private string hyperlinkGUIText;

        public string HyperlinkGUIText {
            get
            {
                return hyperlinkGUIText;
            }
        }

        private string mainCpClientInstanceName;

        public string MainCpClientInstanceName {
            get {
                return mainCpClientInstanceName;
            }
        }

        public void settingRuleCondition(string labelGUIText, string hyperlinkGUIText, string mainCpClientInstanceName, KEYTYPE keyType, string description)
        {
            this.labelGUIText = labelGUIText;
            this.hyperlinkGUIText = hyperlinkGUIText;
            this.mainCpClientInstanceName = mainCpClientInstanceName;
            this.KeyType = keyType;
            this.description = description;
        }

        public abstract bool Evaluate(object o);

        public abstract void SaveRuleCondition(Session session, Guid key, object list);

        public abstract void RemoveRuleCondition(Session session, Guid key);

        public abstract object LoadRuleCondition(Session session, Guid key);
    }
}
