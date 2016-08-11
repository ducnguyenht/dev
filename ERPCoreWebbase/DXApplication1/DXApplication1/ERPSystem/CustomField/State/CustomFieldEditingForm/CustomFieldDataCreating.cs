using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldDataCreating : NAS.GUI.Pattern.State
    {
        public CustomFieldDataCreating(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldDataCreatingTransition transitionEnum =
                (Transition.CustomFieldDataCreatingTransition)
                    Enum.Parse(typeof(Transition.CustomFieldDataCreatingTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataCreatingTransition.BACK:
                    context.State = new CustomFieldCreatingHasInitData(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataCreatingTransition.CANCEL:
                    context.State = new CustomFieldEditingCanceled(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataCreatingTransition.ACCEPT:
                    context.State = new CustomFieldCreatingFinished(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataCreatingTransition.CREATE_SINGLE_CHOICE_LIST_DATA:
                    context.State = new CustomFieldDataCreatingSingleChoiceList(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldDataCreatingTransition.CREATE_MULTI_CHOICE_LIST_DATA:
                    context.State = new CustomFieldDataCreatingMultiChoiceList(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldDataCreating_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CustomFieldDataCreating_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CustomFieldDataCreating_UpdateGUI();
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