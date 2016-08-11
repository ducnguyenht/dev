using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.State;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.State
{
    public class NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState : NASCustomFieldTypeControlState
    {
        public NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        #region Transition
        protected override bool Update(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            throw new NAS.GUI.Pattern.IncompatibleTransitionException();
        }

        protected override bool Cancel(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            throw new NAS.GUI.Pattern.IncompatibleTransitionException();
        }

        protected override bool Edit(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            context.State = new NASCustomFieldTypeBuiltInMultiSelectionListControlDataEditingState(_UIControl);
            return true;
        }
        #endregion

        public override bool CRUD()
        {
            return getOwnerUIControl()
                .NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState_CRUD();
        }
        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl()
                .NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState_PreTransitionCRUD(transition);
        }
        public override bool UpdateGUI()
        {
            return getOwnerUIControl()
                .NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState_UpdateGUI();
        }

        public NASCustomFieldTypeBuiltInMultiSelectionListControl getOwnerUIControl()
        {
            NASCustomFieldTypeBuiltInMultiSelectionListControl UI = null;
            if (UIControl != null)
            {
                if (UIControl is NASCustomFieldTypeBuiltInMultiSelectionListControl)
                {
                    UI = (NASCustomFieldTypeBuiltInMultiSelectionListControl)UIControl;
                }
            }
            return UI;
        }
    }
}