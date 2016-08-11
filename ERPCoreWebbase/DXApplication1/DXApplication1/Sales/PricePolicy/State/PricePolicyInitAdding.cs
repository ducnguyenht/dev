using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxPopupControl;
using NAS.BO.Sale.PricePolicy;

namespace WebModule.Sales.PricePolicy.State
{
    public class PricePolicyInitAdding : NAS.GUI.Pattern.State
    {
        public PricePolicyInitAdding(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            { 
                case "Next":
                    context.State = new PricePolicy.State.PricePolicyTabCondition(_UIControl);
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
            try
            {
                this.getOwnerUIControl().CRUD_initAdding();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public override bool UpdateGUI()
        {
            this.getOwnerUIControl().UpdateGUI_InitAdding();
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