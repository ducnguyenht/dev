using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.NAANAdmin.Authorization.UserControl;

namespace WebModule.NAANAdmin.Authorization.Sate
{
    public class OrganizationEditting : NAS.GUI.Pattern.State
    {
        public OrganizationEditting(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "DepartmentChange":
                case "Save":
                    {
                        context.State = new OrganizationEditting(_UIControl);
                        break;
                    }
                case "Cancel":
                    {
                        context.State = new OrganizationDeleting(_UIControl);
                        break;
                    }
                default:
                    throw new Exception();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().OrganizationEditting_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().OrganizationEditing_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().OrganizationEditing_UpdateGUI();
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