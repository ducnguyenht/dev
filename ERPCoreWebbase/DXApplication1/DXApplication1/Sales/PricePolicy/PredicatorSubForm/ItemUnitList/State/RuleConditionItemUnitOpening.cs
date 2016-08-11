using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Sales.PricePolicy.PredicatorSubForm.SupplierList.State;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList.State
{
    public class RuleConditionItemUnitOpening : NAS.GUI.Pattern.State
    {
        public RuleConditionItemUnitOpening(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            if (!(context.State is RuleConditionItemUnitOpening))
                return false;
            switch (transition)
            {
                case "SelectUnselectAll":
                    context.State = new RuleConditionItemUnitSelectUnselectAll(_UIControl);
                    break;
                case "Next":
                    context.State = new RuleConditionItemUnitPreview(_UIControl);
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

        public WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList.uSettingItemUnitList getOwnerUIControl()
        {
            WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList.uSettingItemUnitList UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList.uSettingItemUnitList)
                {
                    UI = (WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList.uSettingItemUnitList)UIControl;
                }
            }
            return UI;
        }
    }
}