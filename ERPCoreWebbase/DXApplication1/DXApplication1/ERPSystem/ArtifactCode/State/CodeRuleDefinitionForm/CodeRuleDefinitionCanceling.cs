using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.CodeRuleDefinitionForm
{
    public class CodeRuleDefinitionCanceling : NAS.GUI.Pattern.State
    {

        public CodeRuleDefinitionCanceling(System.Web.UI.Control _UIControl) : base(_UIControl) { }

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
            return getOwnerUIControl().CodeRuleDefinitionCanceling_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CodeRuleDefinitionCanceling_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CodeRuleDefinitionCanceling_UpdateGUI();
        }

        public GUI.CodeRuleDefinitionEditingForm getOwnerUIControl()
        {
            GUI.CodeRuleDefinitionEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is GUI.CodeRuleDefinitionEditingForm)
                {
                    ret = (GUI.CodeRuleDefinitionEditingForm)UIControl;
                }
            }
            return ret;
        }

    }
}