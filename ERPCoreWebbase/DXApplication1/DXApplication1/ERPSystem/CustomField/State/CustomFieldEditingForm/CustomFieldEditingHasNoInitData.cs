using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldEditingHasNoInitData : CustomFieldEditing
    {
        public CustomFieldEditingHasNoInitData(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldEditingHasNoInitDataTransition transitionEnum =
                (Transition.CustomFieldEditingHasNoInitDataTransition)
                    Enum.Parse(typeof(Transition.CustomFieldEditingHasNoInitDataTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasNoInitDataTransition.CANCEL:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasNoInitDataTransition.HAS_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasNoInitDataTransition.HAS_NO_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasNoInitDataTransition.SAVE:
                    context.State = new CustomFieldEditingHasNoInitData(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldEditingHasNoInitData_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return base.PreTransitionCRUD(transition)
                & getOwnerUIControl().CustomFieldEditingHasNoInitData_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return base.UpdateGUI() 
                & getOwnerUIControl().CustomFieldEditingHasNoInitData_UpdateGUI();
        }
    }
}