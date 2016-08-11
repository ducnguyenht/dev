using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldCreatingHasNoInitData : CustomFieldCreating
    {
        public CustomFieldCreatingHasNoInitData(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldCreatingHasNoInitDataTransition transitionEnum =
                (Transition.CustomFieldCreatingHasNoInitDataTransition)
                    Enum.Parse(typeof(Transition.CustomFieldCreatingHasNoInitDataTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasNoInitDataTransition.CANCEL:
                    context.State = new CustomFieldEditingCanceled(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasNoInitDataTransition.ACCEPT:
                    context.State = new CustomFieldCreatingFinished(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasNoInitDataTransition.HAS_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasNoInitDataTransition.HAS_NO_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool CRUD()
        {
            return base.CRUD() 
                & getOwnerUIControl().CustomFieldCreatingHasNoInitData_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return base.PreTransitionCRUD(transition)
                & getOwnerUIControl().CustomFieldCreatingHasNoInitData_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return base.UpdateGUI() 
                & getOwnerUIControl().CustomFieldCreatingHasNoInitData_UpdateGUI();
        }

    }
}