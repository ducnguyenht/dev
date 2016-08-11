using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldEditing : NAS.GUI.Pattern.State
    {
        public CustomFieldEditing(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldEditingTransition transitionEnum =
                (Transition.CustomFieldEditingTransition)
                    Enum.Parse(typeof(Transition.CustomFieldEditingTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingTransition.HAS_INIT_DATA:
                    context.State = new CustomFieldEditingHasInitData(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingTransition.HAS_NO_INIT_DATA:
                    context.State = new CustomFieldEditingHasNoInitData(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingTransition.CANCEL:
                    context.State = new CustomFieldEditingCanceled(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingTransition.SAVE:
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldEditing_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CustomFieldEditing_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CustomFieldEditing_UpdateGUI();
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