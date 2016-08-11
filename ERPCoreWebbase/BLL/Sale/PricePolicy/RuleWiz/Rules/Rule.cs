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
    [Serializable]
    public class Rule : ICloneable
    {
        public Rule() : this("New rule")
        {
        }

        public Rule(string name)
        {
            Name = name;
            ApplyNow = false; // turned off by default
            Enabled = true; // turned on by default
            TriggerText = "Apply this rule";
            actions = new RuleActions();
            conditions = new RuleConditions();
            exceptions = new RuleConditions();
        }

        /// <summary>
        /// the name for the rule
        /// </summary>
        public string Name;

        /// <summary>
        /// the text to display when the rule is triggered
        /// </summary>
        public string TriggerText;

        /// <summary>
        /// indicates whether the rule should be executed
        /// </summary>
        public bool Enabled;

        /// <summary>
        /// apply the rule immediately after finishing or loading
        /// </summary>
        public bool ApplyNow;

        private RuleActions actions;

        /// <summary>
        /// list of actions to execute
        /// </summary>
        public RuleActions Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        private RuleConditions conditions;
        /// <summary>
        /// list of conditions that need to be met
        /// </summary>
        public RuleConditions Conditions
        {
            get { return conditions; }
            set { conditions = value; }
        }

        private RuleConditions exceptions;
        /// <summary>
        /// list of exceptional conditions to abort execution
        /// </summary>
        public RuleConditions Exceptions
        {
            get { return exceptions; }
            set { exceptions = value; }
        }

        /// <summary>
        /// will evaluate if all conditions are met
        /// </summary>
        /// <param name="o">the business object to evaluate</param>
        /// <returns>true if all conditions are met and no exceptions, false otherwise</returns>
        public bool Evaluate(object o)
        {
            if (exceptions.EvaluateOne(o))
                return false;
            return conditions.EvaluateAll(o);
        }

        /// <summary>
        /// executes the rule if the evaluation was succesful
        /// </summary>
        /// <param name="o">the business object to evaluate</param>
        /// <returns>true if the execution was succesful</returns>
        public bool Execute(object o)
        {
            bool success = Evaluate(o);
            
            if (success)
                actions.Execute(o);

            return success;
        }

        /// <summary>
        /// checks if all values in the rule have been filled in correctly
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return conditions.IsComplete && actions.IsComplete && exceptions.IsComplete;
            }
        }

        #region ICloneable Members

        public object Clone()
        {
            Rule r = new Rule();
            r.actions = (RuleActions) actions.Clone();
            r.conditions = (RuleConditions)conditions.Clone();
            r.exceptions = (RuleConditions)exceptions.Clone();
            r.Name = this.Name;
            r.ApplyNow = this.ApplyNow;
            r.Enabled = this.Enabled;
            r.TriggerText = this.TriggerText;
            return r;
        }

        #endregion
    }
}
