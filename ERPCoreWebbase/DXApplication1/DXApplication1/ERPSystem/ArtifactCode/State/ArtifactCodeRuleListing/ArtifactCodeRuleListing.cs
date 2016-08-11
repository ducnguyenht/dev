using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.ArtifactCodeRuleListing
{
    public class ArtifactCodeRuleListing : NAS.GUI.Pattern.State
    {

        public ArtifactCodeRuleListing(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            return true;
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().ArtifactCodeRuleListingCRUD();
        }

        public override bool PreTransitionCRUD(string action)
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            return true;
        }

        public GUI.ArtifactCodeRuleListing getOwnerUIControl()
        {
            GUI.ArtifactCodeRuleListing ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.ArtifactCodeRuleListing)
                {
                    ret = (GUI.ArtifactCodeRuleListing)UIControl;
                }
            }
            return ret;
        }

    }
}