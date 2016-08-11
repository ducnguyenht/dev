using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.AllocationConfigure.State.AllocationListing
{

    public class AllocationListing : NAS.GUI.Pattern.State
    {
        public AllocationListing(System.Web.UI.Control _UIControl) : base(_UIControl) { }
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
            return getOwnerUIControl().AllocationListing_CRUD();
        }

        public override bool UpdateGUI()
        {
            return true;
        }

        public GUI.AllocationListing getOwnerUIControl()
        {
            GUI.AllocationListing ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.AllocationListing)
                {
                    ret = (GUI.AllocationListing)UIControl;
                }
            }
            return ret;
        }
    }
}