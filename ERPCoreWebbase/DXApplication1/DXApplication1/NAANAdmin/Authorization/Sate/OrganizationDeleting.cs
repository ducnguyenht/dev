using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.GUI.Pattern;
using WebModule.NAANAdmin.Authorization.UserControl;

namespace WebModule.NAANAdmin.Authorization.Sate
{
    public class OrganizationDeleting : NAS.GUI.Pattern.State
    {
        public OrganizationDeleting(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                switch (transition)
                {
                    case "Allow":
                        context.State = null;
                        break;
                    case "Deny":
                        break;
                    default:
                        context.State = null;
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override bool PreTransitionCRUD(string action)
        {
            return true;
        }

        public override bool CRUD()
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            return true;// getOwnerUIControl().OrganizationCanceling_UpdateGUI();
        }

        public uPopupOrganization getOwnerUIControl()
        {
            uPopupOrganization ret = null;
            if (UIControl != null)
            {
                if (UIControl is uPopupOrganization)
                {
                    ret = (uPopupOrganization)UIControl;
                }
            }
            return ret;
        }
    }
}