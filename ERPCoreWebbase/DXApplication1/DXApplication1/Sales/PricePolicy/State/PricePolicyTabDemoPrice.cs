using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.PricePolicy.State
{
    public class PricePolicyTabDemoPrice : NAS.GUI.Pattern.State
    {
        public PricePolicyTabDemoPrice(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Save":
                    context.State = new PricePolicy.State.PricePolicySaving(_UIControl);
                    break;
                case "Back":
                    context.State = new PricePolicy.State.PricePolicyTabConfigPrice(_UIControl);
                    break;
                case "Close":
                    context.State = new PricePolicy.State.PricePolicyPoupClosing(_UIControl);
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
            WebModule.Sales.PricePolicy.uPricePolicyEditting usrc = (WebModule.Sales.PricePolicy.uPricePolicyEditting)UIControl;
            usrc.CRUD_TabDemoPrice();
            return true;
        }

        public override bool UpdateGUI()
        {
            WebModule.Sales.PricePolicy.uPricePolicyEditting usrc = (WebModule.Sales.PricePolicy.uPricePolicyEditting)UIControl;
            usrc.UpdateGUI_TabDemoPrice();
            return true;
        }
    }
}