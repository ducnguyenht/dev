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

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Rules
{
    public class RuleActions : RuleParts<RuleAction>
    {
        public void Execute(object o)
        {
            foreach (RuleAction ra in this)
            {
                ra.Execute(o);
            }
        }

        #region ICloneable Members
        public override object Clone()
        {
            RuleActions list = new RuleActions();
            foreach (RuleAction ra in this)
                list.Add((RuleAction)ra.Clone());

            return list;
        }
        #endregion
    }

    public abstract class RuleAction : RulePart
    {
        /// <summary>
        /// execute the action
        /// </summary>
        /// <param name="o">the business object</param>
        public abstract void Execute(object o);

    }
}
