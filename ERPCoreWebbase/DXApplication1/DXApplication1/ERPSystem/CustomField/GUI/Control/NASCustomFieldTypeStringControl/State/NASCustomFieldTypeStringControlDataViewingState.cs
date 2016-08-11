using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.State;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeStringControl.State
{
    public class NASCustomFieldTypeStringControlDataViewingState : NASCustomFieldTypeControlState
    {
        public NASCustomFieldTypeStringControlDataViewingState(System.Web.UI.Control _UIControl)
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
            context.State = new NASCustomFieldTypeStringControlDataEditingState(_UIControl);
            return true;
        }
        #endregion

        public override bool CRUD()
        {
            return getOwnerUIControl().NASCustomFieldTypeStringControlDataViewingState_CRUD();
        }
        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().NASCustomFieldTypeStringControlDataViewingState_PreTransitionCRUD(transition);
        }
        public override bool UpdateGUI()
        {
            return getOwnerUIControl().NASCustomFieldTypeStringControlDataViewingState_UpdateGUI();
        }

        public NASCustomFieldTypeStringControl getOwnerUIControl()
        {
            NASCustomFieldTypeStringControl ret = null;
            if (UIControl != null)
            {
                if (UIControl is NASCustomFieldTypeStringControl)
                {
                    ret = (NASCustomFieldTypeStringControl)UIControl;
                }
            }
            return ret;
        }

    }
}