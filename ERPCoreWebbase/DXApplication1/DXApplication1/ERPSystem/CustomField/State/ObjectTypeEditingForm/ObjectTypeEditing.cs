using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.ObjectTypeEditingForm
{
    public class ObjectTypeEditing : NAS.GUI.Pattern.State
    {
        public ObjectTypeEditing(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Refresh":
                    context.State = new ObjectTypeEditing(_UIControl);
                    break;
                default:
                    break;
            }
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().ObjectTypeEditing_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().ObjectTypeEditing_UpdateGUI();
        }

        public GUI.ObjectTypeCustomFieldListing getOwnerUIControl()
        {
            GUI.ObjectTypeCustomFieldListing ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.ObjectTypeCustomFieldListing)
                {
                    ret = (GUI.ObjectTypeCustomFieldListing)UIControl;
                }
            }
            return ret;
        }

    }
}