using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.AllocationConfigure.State.AllocationForm
{
    public class AllocationCanceling : NAS.GUI.Pattern.State
    {

        public AllocationCanceling(System.Web.UI.Control _UIControl) : base(_UIControl) { }
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
            return true;
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().AllocationCanceling_UpdateGUI();
        }

        public GUI.AllocationEditingForm getOwnerUIControl()
        {
            GUI.AllocationEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.AllocationEditingForm)
                {
                    ret = (GUI.AllocationEditingForm)UIControl;
                }
            }
            return ret;

        }
    }
}