using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.DAL.Sales.Price;
using NAS.BO.Sale.PricePolicy;

namespace WebModule.Sales.PricePolicy.State
{
    public class PricePolicyListing : NAS.GUI.Pattern.State
    {
        public PricePolicyListing(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition) { 
                case "Delete":
                    context.State = new PricePolicy.State.PricePolicyInitAdding(_UIControl);
                    break;
                default:
                    break;
            }              
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            try
            {
                PricePolicyBO bo = new PricePolicyBO();
                bo.Init_DefaultData(getOwnerUIControl().session);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public override bool UpdateGUI()
        {
            return true;
        }

        public WebModule.Sales.PricePolicy.PricePolicyPage getOwnerUIControl()
        {
            WebModule.Sales.PricePolicy.PricePolicyPage UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Sales.PricePolicy.PricePolicyPage)
                {
                    UI = (WebModule.Sales.PricePolicy.PricePolicyPage)UIControl;
                }
            }
            return UI;
        }
    }
}