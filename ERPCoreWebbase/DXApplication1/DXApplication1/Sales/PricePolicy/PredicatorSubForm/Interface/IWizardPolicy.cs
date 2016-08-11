using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.Sale.PricePolicy.RulesWiz.Rules;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.Interface
{
    public interface IWizardPolicy
    {
        Guid KeyValue
        {
            get;
            set;
        }

        RuleCondition RuleObject{
            get;
            set;
        }

        NAS.GUI.Pattern.Context GUIContext
        {
            get;
            set;
        }

        void settingJavascript();

    }
}
