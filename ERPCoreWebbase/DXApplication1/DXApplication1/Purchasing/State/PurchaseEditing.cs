using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Purchasing.State
{
    public class PurchaseEditing : NAS.GUI.Pattern.State
    {
        public PurchaseEditing(System.Web.UI.Control _UIControl, bool o):  base(_UIControl, o) {}

        public PurchaseEditing(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {          
            return true;
        }

        public override bool CRUD()
        {
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().PurchaseEditing_UpdateGUI();
        }

        public ERPCore.Purchasing.UserControl.uPurchaseEdit getOwnerUIControl()
        {
            ERPCore.Purchasing.UserControl.uPurchaseEdit ret = null;
            if (UIControl != null)
            {
                if (UIControl is ERPCore.Purchasing.UserControl.uPurchaseEdit)
                {
                    ret = (ERPCore.Purchasing.UserControl.uPurchaseEdit)UIControl;
                }
            }

            return ret;
        }
    }
}