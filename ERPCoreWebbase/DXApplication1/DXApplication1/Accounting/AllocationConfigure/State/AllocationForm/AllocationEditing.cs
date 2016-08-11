using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace WebModule.Accounting.AllocationConfigure.State.AllocationForm
{
    public class AllocationEditing : NAS.GUI.Pattern.State
    {
        public AllocationEditing(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                switch (transition)
                {
                    case "Save":
                        context.State = new AllocationEditing(_UIControl);
                        break;
                    case "Cancel":
                        context.State = new AllocationCanceling(_UIControl);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().AllocationEditing_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().AllocationEditing_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().AllocationEditing_UpdateGUI();
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