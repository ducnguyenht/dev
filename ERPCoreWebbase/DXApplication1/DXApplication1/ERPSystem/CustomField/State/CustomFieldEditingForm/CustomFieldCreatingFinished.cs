using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldCreatingFinished : NAS.GUI.Pattern.State
    {
        public CustomFieldCreatingFinished(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            context.State = null; 
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldCreatingFinished_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CustomFieldCreatingFinished_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CustomFieldCreatingFinished_UpdateGUI();
        }

        public GUI.CustomFieldEditingForm getOwnerUIControl()
        {
            GUI.CustomFieldEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.CustomFieldEditingForm)
                {
                    ret = (GUI.CustomFieldEditingForm)UIControl;
                }
            }
            return ret;
        }

    }
}