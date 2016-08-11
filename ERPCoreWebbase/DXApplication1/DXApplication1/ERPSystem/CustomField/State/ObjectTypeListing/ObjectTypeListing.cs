using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.ObjectTypeListing
{
    public class ObjectTypeListing : NAS.GUI.Pattern.State
    {
        public ObjectTypeListing(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Refresh":
                    context.State = new ObjectTypeListing(_UIControl);
                    break;
                default:
                    break;
            }
            return true;
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
            return getOwnerUIControl().ObjectTypeListing_UpdateGUI();
        }

        public GUI.ObjectTypeListing getOwnerUIControl()
        {
            GUI.ObjectTypeListing ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.ObjectTypeListing)
                {
                    ret = (GUI.ObjectTypeListing)UIControl;
                }
            }
            return ret;
        }

    }
}