using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList.State
{
    public class RuleConditionManufacturerOpening : NAS.GUI.Pattern.State
    {
        public RuleConditionManufacturerOpening(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            if (!(context.State is RuleConditionManufacturerOpening))
                return false;
            switch (transition)
            {
                case "SelectUnselectAll":
                    context.State = new RuleConditionManufacturerSelectUnselectAll(_UIControl);
                    break;
                case "Next":
                    context.State = new RuleConditionManufacturerPreview(_UIControl);
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
            this.getOwnerUIControl().CRUD_Opening();
            return true;
        }

        public override bool UpdateGUI()
        {
            this.getOwnerUIControl().UpdateGUI_Opening();
            return true;
        }

        public WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList.uSettingManufacturersList getOwnerUIControl()
        {
            WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList.uSettingManufacturersList UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList.uSettingManufacturersList)
                {
                    UI = (WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList.uSettingManufacturersList)UIControl;
                }
            }
            return UI;
        }
    }
}