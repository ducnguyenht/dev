using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.CodeRuleDefinitionForm
{
    public class CodeRuleDefinitionEditing : NAS.GUI.Pattern.State
    {

        public CodeRuleDefinitionEditing(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                switch (transition)
                {
                    case "UseStringData":
                        context.State = new CodeRuleDefinifionEditingStringData(_UIControl);
                        break;
                    case "UseDateTimeData":
                        context.State = new CodeRuleDefinifionEditingDateTimeData(_UIControl);
                        break;
                    case "UseNumberData":
                        context.State = new CodeRuleDefinifionEditingNumberData(_UIControl);
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
            return getOwnerUIControl().CodeRuleDefinitionEditing_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CodeRuleDefinitionEditing_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CodeRuleDefinitionEditing_UpdateGUI();
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