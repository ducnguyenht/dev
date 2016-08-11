using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.CodeRuleDefinitionForm
{
    public class CodeRuleDefinitionCreating : NAS.GUI.Pattern.State
    {

        public CodeRuleDefinitionCreating(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                switch (transition)
                {
                    case "UseStringData":
                        context.State = new CodeRuleDefinifionCreatingStringData(_UIControl);
                        break;
                    case "UseDateTimeData":
                        //context.State = new CodeRuleDefinifionEditingDateTimeData(_UIControl);
                        context.State = new CodeRuleDefinifionCreatingDateTimeData(_UIControl);
                        break;
                    case "UseNumberData":
                        //context.State = new CodeRuleDefinifionEditingNumberData(_UIControl);
                        context.State = new CodeRuleDefinifionCreatingNumberData(_UIControl);
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
            return getOwnerUIControl().CodeRuleDefinitionCreating_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CodeRuleDefinitionCreating_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CodeRuleDefinitionCreating_UpdateGUI();
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