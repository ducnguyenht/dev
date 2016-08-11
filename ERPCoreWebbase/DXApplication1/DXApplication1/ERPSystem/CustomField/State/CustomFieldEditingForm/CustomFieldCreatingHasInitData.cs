using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldCreatingHasInitData : CustomFieldCreating
    {
        public CustomFieldCreatingHasInitData(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            Transition.CustomFieldCreatingHasInitDataTransition transitionEnum =
                (Transition.CustomFieldCreatingHasInitDataTransition)
                    Enum.Parse(typeof(Transition.CustomFieldCreatingHasInitDataTransition), transition.ToUpper());
            switch (transitionEnum)
            {
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasInitDataTransition.CANCEL:
                    context.State = new CustomFieldEditingCanceled(_UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasInitDataTransition.HAS_NO_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasInitDataTransition.HAS_INIT_DATA:
                    base.Transit(context, transition, _UIControl);
                    break;
                case WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition.CustomFieldCreatingHasInitDataTransition.NEXT:
                    context.State = new CustomFieldDataCreating(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool CRUD()
        {
            return base.CRUD() 
                & getOwnerUIControl().CustomFieldCreatingHasInitData_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return base.PreTransitionCRUD(transition)
                & getOwnerUIControl().CustomFieldCreatingHasInitData_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return base.UpdateGUI() 
                & getOwnerUIControl().CustomFieldCreatingHasInitData_UpdateGUI();
        }

    }
}