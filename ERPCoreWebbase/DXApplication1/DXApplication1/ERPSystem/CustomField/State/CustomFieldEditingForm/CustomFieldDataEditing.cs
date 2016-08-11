using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldDataEditing : NAS.GUI.Pattern.State
    {
        public CustomFieldDataEditing(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldDataEditingTransition transitionEnum =
                (Transition.CustomFieldDataEditingTransition)
                    Enum.Parse(typeof(Transition.CustomFieldDataEditingTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataEditingTransition.CANCEL:
                    context.State = new CustomFieldEditingCanceled(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataEditingTransition.EDIT_SINGLE_CHOICE_LIST_DATA:
                    context.State = new CustomFieldDataEditingSingleChoiceList(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataEditingTransition.EDIT_MULTI_CHOICE_LIST_DATA:
                    context.State = new CustomFieldDataEditingMultiChoiceList(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataEditingTransition.EDIT_FIELD:
                    context.State = new CustomFieldEditingHasInitData(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldDataEditing_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CustomFieldDataEditing_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CustomFieldDataEditing_UpdateGUI();
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