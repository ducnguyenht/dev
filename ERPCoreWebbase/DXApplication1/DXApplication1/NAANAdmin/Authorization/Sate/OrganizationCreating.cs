using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.NAANAdmin.Authorization.UserControl;

namespace WebModule.NAANAdmin.Authorization.Sate
{
    public class OrganizationCreating : NAS.GUI.Pattern.State
    {
        public OrganizationCreating(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Save":
                    context.State = new OrganizationEditting(_UIControl);
                    break;
                case "Cancel":
                    context.State = new OrganizationDeleting(_UIControl);
                    break;
                default:
                    throw new Exception();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string action)
        {
            return GetOrganization().OrganizationCreating_PreTransitionCRUD(action);
        }

        public override bool CRUD()
        {
            return GetOrganization().OrganizationCreating_CRUD();
        }

        public override bool UpdateGUI()
        {
            return GetOrganization().OrganizationCreating_UpdateGUI();
        }
        private uPopupOrganization GetOrganization()
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