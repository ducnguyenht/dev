using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList.State
{
    public class RuleConditionItemUnitSaving : NAS.GUI.Pattern.State
    {
        public RuleConditionItemUnitSaving(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            this.getOwnerUIControl().CRUD_Saving();
            return true;
        }

        public override bool UpdateGUI()
        {   
            this.getOwnerUIControl().UPdateGUI_Saving();
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