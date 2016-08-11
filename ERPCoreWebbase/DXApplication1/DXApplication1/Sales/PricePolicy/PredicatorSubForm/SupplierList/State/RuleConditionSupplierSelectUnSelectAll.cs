using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.SupplierList.State
{
    public class RuleConditionSupplierSelectUnSelectAll : NAS.GUI.Pattern.State
    {
        public RuleConditionSupplierSelectUnSelectAll(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            if (!(context.State is RuleConditionSupplierSelectUnSelectAll))
                return false;
            switch (transition)
            {
                case "SelectUnselectAll":
                    context.State = new RuleConditionSupplierSelectUnSelectAll(_UIControl);
                    break;
                case "Next":
                    context.State = new RuleConditionSupplierPreview(_UIControl);
                    break;
                case "Close":
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
            this.getOwnerUIControl().UpdateGUI_SelectUnSelectAll();
            return true;
        }

        public WebModule.Sales.PricePolicy.PredicatorSubForm.SupplierList.uSettingSuppliersList getOwnerUIControl()
        {
            WebModule.Sales.PricePolicy.PredicatorSubForm.SupplierList.uSettingSuppliersList UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Sales.PricePolicy.PredicatorSubForm.SupplierList.uSettingSuppliersList)
                {
                    UI = (WebModule.Sales.PricePolicy.PredicatorSubForm.SupplierList.uSettingSuppliersList)UIControl;
                }
            }
            return UI;
        }
    }
}