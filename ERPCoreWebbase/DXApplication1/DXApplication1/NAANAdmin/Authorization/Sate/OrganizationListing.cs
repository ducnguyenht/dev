using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.GUI.Pattern;
using WebModule.NAANAdmin.Authorization.UserControl;

namespace WebModule.NAANAdmin.Authorization.Sate
{
    public class OrganizationListing : NAS.GUI.Pattern.State
    {
        public OrganizationListing(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(Context context, string transition, System.Web.UI.Control _UIControl)
        {
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            return true;// getOwnerUIControl().OrganizationCRUD();
        }

        public override bool UpdateGUI()
        {
            return true;
        }

        public Organization getOwnerUIControl()
        {
            Organization ret = null;
            if (UIControl != null)
            {
                if (UIControl is Organization)
                {
                    ret = (Organization)UIControl;
                }
            }
            return ret;
        }
    }
}