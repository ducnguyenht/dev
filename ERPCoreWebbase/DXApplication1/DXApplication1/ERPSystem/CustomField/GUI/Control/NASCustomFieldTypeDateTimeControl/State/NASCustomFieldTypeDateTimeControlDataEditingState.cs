using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.ERPSystem.CustomField.GUI.Control.State;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeDateTimeControl.State
{
    [Serializable]
    public class NASCustomFieldTypeDateTimeControlDataEditingState : NASCustomFieldTypeControlState
    {
        public NASCustomFieldTypeDateTimeControlDataEditingState(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        #region Transition
        protected override bool Update(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            context.State = new NASCustomFieldTypeDateTimeControlDataViewingState(_UIControl);
            return true;
        }

        protected override bool Cancel(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            context.State = new NASCustomFieldTypeDateTimeControlDataViewingState(_UIControl);
            return true;
        }

        protected override bool Edit(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            throw new NAS.GUI.Pattern.IncompatibleTransitionException();
        }
        #endregion

        public override bool CRUD()
        {
            return getOwnerUIControl().NASCustomFieldTypeDateTimeControlDataEditingState_CRUD();
        }
        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().NASCustomFieldTypeDateTimeControlDataEditingState_PreTransitionCRUD(transition);
        }
        public override bool UpdateGUI()
        {
            return getOwnerUIControl().NASCustomFieldTypeDateTimeControlDataEditingState_UpdateGUI();
        }

        public NASCustomFieldTypeDateTimeControl getOwnerUIControl()
        {
            NASCustomFieldTypeDateTimeControl ret = null;
            if (UIControl != null)
            {
                if (UIControl is NASCustomFieldTypeDateTimeControl)
                {
                    ret = (NASCustomFieldTypeDateTimeControl)UIControl;
                }
            }
            return ret;
        }

    }
}