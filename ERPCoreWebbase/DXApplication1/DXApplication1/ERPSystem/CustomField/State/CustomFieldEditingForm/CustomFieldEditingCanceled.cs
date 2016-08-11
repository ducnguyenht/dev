using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldEditingCanceled : NAS.GUI.Pattern.State
    {
        public CustomFieldEditingCanceled(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldEditingCanceled_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CustomFieldEditingCanceled_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CustomFieldEditingCanceled_UpdateGUI();
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