using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.PricePolicy.State
{
    public class PricePolicyTabConfigPrice : NAS.GUI.Pattern.State
    {
        public PricePolicyTabConfigPrice(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Next":
                        context.State = new PricePolicy.State.PricePolicyTabDemoPrice(_UIControl);
                    break;
                case "Back":
                        context.State = new PricePolicy.State.PricePolicyTabException(_UIControl);
                    break;
                case "SaveConfigPrice":
                    context.State = new PricePolicy.State.PricePolicyTabConfigPriceSaving(_UIControl);
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
            if (transition.Equals("Next") || transition.Equals("SaveConfigPrice"))
            {
                WebModule.Sales.PricePolicy.uPricePolicyEditting usrc = (WebModule.Sales.PricePolicy.uPricePolicyEditting)UIControl;
                string msg = "";
                bool rs = usrc.PreTransitionCRUD_ConfigPrice(out msg);
                if (!rs)
                    throw new Exception(msg);
            }
            return true;
        }

        public override bool CRUD()
        {
            WebModule.Sales.PricePolicy.uPricePolicyEditting usrc = (WebModule.Sales.PricePolicy.uPricePolicyEditting)UIControl;
            usrc.CRUD_TabConfigPrice();
            return true;
        }

        public override bool UpdateGUI()
        {
            WebModule.Sales.PricePolicy.uPricePolicyEditting usrc = (WebModule.Sales.PricePolicy.uPricePolicyEditting)UIControl;
            usrc.UpdateGUI_TabConfigPrice();
            return true;
        }
    }
}