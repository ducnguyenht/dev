using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.State;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.State
{
    public class NASCustomFieldTypeIntegerControlDataViewingState : NASCustomFieldTypeControlState
    {
        public NASCustomFieldTypeIntegerControlDataViewingState(System.Web.UI.Control _UIControl)
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
            context.State = new NASCustomFieldTypeIntegerControlDataEditingState(_UIControl);
            return true;
        }
        #endregion

        public override bool CRUD()
        {
            return getOwnerUIControl().CRUD_ViewingState();
        }
        public override bool PreTransitionCRUD(string transition)
        {
            return base.PreTransitionCRUD(transition);
        }
        public override bool UpdateGUI()
        {
            return getOwnerUIControl().UpdateGUI_ViewingState();
        }

        public WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.NASCustomFieldTypeIntegerControl getOwnerUIControl()
        {
            WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.NASCustomFieldTypeIntegerControl UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.NASCustomFieldTypeIntegerControl)
                {
                    UI = (WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.NASCustomFieldTypeIntegerControl)UIControl;
                }
            }
            return UI;
        }
    }
}