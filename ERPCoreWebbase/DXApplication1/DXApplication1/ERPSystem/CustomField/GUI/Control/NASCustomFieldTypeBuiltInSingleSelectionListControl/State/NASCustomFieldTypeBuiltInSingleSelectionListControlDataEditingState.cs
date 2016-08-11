using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.State;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.State
{
    public class NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState : NASCustomFieldTypeControlState
    {
        public NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        #region Transition
        protected override bool Update(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            context.State = new NASCustomFieldTypeBuiltInSingleSelectionListControlDataViewingState(_UIControl);
            return true;
        }

        protected override bool Cancel(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            context.State = new NASCustomFieldTypeBuiltInSingleSelectionListControlDataViewingState(_UIControl);
            return true;
        }

        protected override bool Edit(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            throw new NAS.GUI.Pattern.IncompatibleTransitionException();
        }
        #endregion

        public override bool CRUD()
        {
            return getOwnerUIControl()
                .NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState_CRUD();
        }
        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl()
                .NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState_PreTransitionCRUD(transition);
        }
        public override bool UpdateGUI()
        {
            return getOwnerUIControl()
                .NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState_UpdateGUI();
        }

        public NASCustomFieldTypeBuiltInSingleSelectionListControl getOwnerUIControl()
        {
            NASCustomFieldTypeBuiltInSingleSelectionListControl UI = null;
            if (UIControl != null)
            {
                if (UIControl is NASCustomFieldTypeBuiltInSingleSelectionListControl)
                {
                    UI = (NASCustomFieldTypeBuiltInSingleSelectionListControl)UIControl;
                }
            }
            return UI;
        }
    }
}