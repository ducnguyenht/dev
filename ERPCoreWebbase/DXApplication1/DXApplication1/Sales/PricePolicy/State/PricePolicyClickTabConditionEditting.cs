using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.PricePolicy.State
{
    public class PricePolicyClickTabConditionEditting: NAS.GUI.Pattern.State
    {
        public PricePolicyClickTabConditionEditting(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "EdittingClickCommon":
                    context.State = new PricePolicy.State.PricePolicyClickTabCommonEditting(_UIControl);
                    break;
                case "EdittingClickCondition":
                    context.State = new PricePolicy.State.PricePolicyClickTabConditionEditting(_UIControl);
                    break;
                case "EdittingClickException":
                    context.State = new PricePolicy.State.PricePolicyClickTabExceptionEditting(_UIControl);
                    break;
                case "EdittingClickConfigFormula":
                    context.State = new PricePolicy.State.PricePolicyClickTabConfigPriceEditting(_UIControl);
                    break;
                case "EdittingClickPriceReference":
                    context.State = new PricePolicy.State.PricePolicyClickTabDemoPriceEditting(_UIControl);
                    break;
                case "Close":
                    context.State = new PricePolicy.State.PricePolicyPoupClosing(_UIControl);
                    break;
                case "UpdateMenu":
                    context.State = new PricePolicy.State.PricePolicyConditionMenuEditting(_UIControl);
                    break;
                case "UpdateExceptionMenu":
                    context.State = new PricePolicy.State.PricePolicyExceptionMenuEditting(_UIControl);
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
            WebModule.Sales.PricePolicy.uPricePolicyEditting usrc = this.getOwnerUIControl();
            usrc.UpdateGUI_ClickTabConditionEditting();
            return true;
        }

        public WebModule.Sales.PricePolicy.uPricePolicyEditting getOwnerUIControl()
        {
            WebModule.Sales.PricePolicy.uPricePolicyEditting UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Sales.PricePolicy.uPricePolicyEditting)
                {
                    UI = (WebModule.Sales.PricePolicy.uPricePolicyEditting)UIControl;
                }
            }
            return UI;
        }
    }
}