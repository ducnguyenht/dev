using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.BO.Sale.PricePolicy.RulesWiz.Rules;

namespace WebModule.Sales.PricePolicy.State
{
    public class PricePolicyTabCondition: NAS.GUI.Pattern.State
    {
        public PricePolicyTabCondition(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Next":
                        context.State = new PricePolicy.State.PricePolicyTabException(_UIControl);
                    break;
                case "Back":
                        context.State = new PricePolicy.State.PricePolicyTabCommonInfo(_UIControl);
                    break;
                case "Close":
                        context.State = new PricePolicy.State.PricePolicyPoupClosing(_UIControl);
                    break;
                case "UpdateMenu":
                        context.State = new PricePolicy.State.PricePolicyConditionMenu(_UIControl);
                    break;
                default:
                    throw new Exception("current compatibility setting is not supported");
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            WebModule.Sales.PricePolicy.uPricePolicyEditting usrc = (WebModule.Sales.PricePolicy.uPricePolicyEditting)UIControl;
            usrc.UpdateGUI_TabCondition();
            return true;
        }
    }
}