using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.ArtifactCodeRuleForm
{
    public class ArtifactCodeRuleCreating : NAS.GUI.Pattern.State
    {

        public ArtifactCodeRuleCreating(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                switch (transition)
                {
                    case "Save":
                        context.State = new ArtifactCodeRuleEditing(_UIControl);
                        break;
                    case "Cancel":
                        context.State = new ArtifactCodeRuleCanceling(_UIControl);
                        break;
                    default:
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
            return getOwnerUIControl().ArtifactCodeRuleCreating_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().ArtifactCodeRuleCreating_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().ArtifactCodeRuleCreating_UpdateGUI();
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