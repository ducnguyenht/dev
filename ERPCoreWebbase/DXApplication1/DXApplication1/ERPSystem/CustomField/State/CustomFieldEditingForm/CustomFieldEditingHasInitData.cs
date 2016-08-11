using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldEditingHasInitData : CustomFieldEditing
    {
        public CustomFieldEditingHasInitData(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldEditingHasInitDataTransition transitionEnum =
                (Transition.CustomFieldEditingHasInitDataTransition)
                    Enum.Parse(typeof(Transition.CustomFieldEditingHasInitDataTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasInitDataTransition.CANCEL:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasInitDataTransition.HAS_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasInitDataTransition.HAS_NO_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasInitDataTransition.SAVE:
                    context.State = new CustomFieldEditingHasInitData(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldEditingHasInitDataTransition.EDIT_DATA:
                    context.State = new CustomFieldDataEditing(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().CustomFieldEditingHasInitData_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return base.PreTransitionCRUD(transition)
                & getOwnerUIControl().CustomFieldEditingHasInitData_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return base.UpdateGUI() 
                & getOwnerUIControl().CustomFieldEditingHasInitData_UpdateGUI();
        }
    }
}