using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList.State
{
    public class RuleConditionManufacturerPreview : NAS.GUI.Pattern.State
    {
        public RuleConditionManufacturerPreview(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            if (!(context.State is RuleConditionManufacturerPreview))
                return false;
            switch (transition)
            {
                case "Back":
                    context.State = new RuleConditionManufacturerOpening(_UIControl);
                    break;
                case "Save":
                    context.State = new RuleConditionManufacturerSaving(_UIControl);
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
            this.getOwnerUIControl().CRUD_Preview();
            return true;
        }

        public override bool UpdateGUI()
        {
            this.getOwnerUIControl().UPdateGUI_Preview();
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