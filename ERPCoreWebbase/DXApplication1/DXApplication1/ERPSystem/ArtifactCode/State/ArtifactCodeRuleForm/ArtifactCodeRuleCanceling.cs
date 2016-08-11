using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.ArtifactCodeRuleForm
{
    public class ArtifactCodeRuleCanceling : NAS.GUI.Pattern.State
    {

        public ArtifactCodeRuleCanceling(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
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
            return getOwnerUIControl().ArtifactCodeRuleCanceling_UpdateGUI();
        }

        public GUI.ArtifactCodeRuleEditingForm getOwnerUIControl()
        {
            GUI.ArtifactCodeRuleEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.ArtifactCodeRuleEditingForm)
                {
                    ret = (GUI.ArtifactCodeRuleEditingForm)UIControl;
                }
            }
            return ret;
        }

    }
}