using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.AllocationConfigure.State.AllocationForm
{
    public class AllocationCreating : NAS.GUI.Pattern.State
    {
        public AllocationCreating(System.Web.UI.Control _UIControl) : base(_UIControl) { }
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
                        throw new NAS.GUI.Pattern.IncompatibleTransitionException(String.Format("Transition '{0}' is invalid in '{1}' state.", transition,  this.GetType().FullName));
                }
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().AllocationCreating_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().AllocationCreating_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().AllocationCreating_UpdateGUI();
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