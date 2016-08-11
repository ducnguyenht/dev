using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldCreating : NAS.GUI.Pattern.State
    {
        public CustomFieldCreating(System.Web.UI.Control _UIControl) : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldCreatingTransition transitionEnum =  
                (Transition.CustomFieldCreatingTransition)
                    Enum.Parse(typeof(Transition.CustomFieldCreatingTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingTransition.HAS_INIT_DATA:
                    context.State = new CustomFieldCreatingHasInitData(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingTransition.HAS_NO_INIT_DATA:
                    context.State = new CustomFieldCreatingHasNoInitData(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingTransition.CANCEL:
                    context.State = new CustomFieldEditingCanceled(_UIControl);
                    break;
                default:
                    break;
            }
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldCreating_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CustomFieldCreating_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CustomFieldCreating_UpdateGUI();
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