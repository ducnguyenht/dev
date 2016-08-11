using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.GUI.Control.State
{

    public sealed class NASCustomFieldTypeControlStateTransition
    {
        private string _value;

        public static readonly NASCustomFieldTypeControlStateTransition UpdateTransition =
            new NASCustomFieldTypeControlStateTransition("UPDATE");
        public static readonly NASCustomFieldTypeControlStateTransition CancelTransition =
            new NASCustomFieldTypeControlStateTransition("CANCEL");
        public static readonly NASCustomFieldTypeControlStateTransition EditTransition =
            new NASCustomFieldTypeControlStateTransition("EDIT");

        private NASCustomFieldTypeControlStateTransition(string val)
        {
            _value = val;
        }

        public string TransitionName { get { return _value; } }
    }
    [Serializable]
    public abstract class NASCustomFieldTypeControlState : NAS.GUI.Pattern.State
    {
        public NASCustomFieldTypeControlState(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected abstract bool
            Edit(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl);

        protected abstract bool
            Update(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl);

        protected abstract bool
            Cancel(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl);

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                if (transition.ToUpper().Equals(NASCustomFieldTypeControlStateTransition.EditTransition.TransitionName))
                {
                    return Edit(context, _UIControl);
                }
                else if (transition.ToUpper().Equals(NASCustomFieldTypeControlStateTransition.UpdateTransition.TransitionName))
                {
                    return Update(context, _UIControl);
                }
                else if (transition.ToUpper().Equals(NASCustomFieldTypeControlStateTransition.CancelTransition.TransitionName))
                {
                    return Cancel(context, _UIControl);
                }
                else
                {
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool CRUD()
        {
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            return true;
        }
    }
}