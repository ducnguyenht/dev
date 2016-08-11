using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.CodeRuleDefinitionForm
{
    public class CodeRuleDefinifionCreatingNumberData : NAS.GUI.Pattern.State
    {

        public CodeRuleDefinifionCreatingNumberData(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                switch (transition)
                {
                    case "UseDateTimeData":
                        context.State = new CodeRuleDefinifionCreatingDateTimeData(_UIControl);
                        break;
                    case "UseStringData":
                        context.State = new CodeRuleDefinifionCreatingStringData(_UIControl);
                        break;
                    case "Save":
                        context.State = new CodeRuleDefinifionEditingNumberData(_UIControl);
                        break;
                    case "Cancel":
                        context.State = new CodeRuleDefinitionCanceling(_UIControl);
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
            return getOwnerUIControl().CodeRuleDefinifionCreatingNumberData_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CodeRuleDefinifionCreatingNumberData_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CodeRuleDefinifionCreatingNumberData_UpdateGUI();
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