using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Sales.State.OrderList
{
    public class OrderListInventoryExporting : NAS.GUI.Pattern.State
    {
        public OrderListInventoryExporting(System.Web.UI.Control _UIControl, bool o):  base(_UIControl, o) {}

        public OrderListInventoryExporting(System.Web.UI.Control _UIControl) : base(_UIControl) { }

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
            return getOwnerUIControl().OrderListInventoryExporting_UpdateGUI();
        }

        public ERPCore.Sales.UserControl.uOrderListEdit getOwnerUIControl()
        {
            ERPCore.Sales.UserControl.uOrderListEdit ret = null;
            if (UIControl != null)
            {
                if (UIControl is ERPCore.Sales.UserControl.uOrderListEdit)
                {
                    ret = (ERPCore.Sales.UserControl.uOrderListEdit)UIControl;
                }
            }

            return ret;
        }
    }
}