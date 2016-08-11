using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.ArtifactCode.State.CodeRuleDefinitionForm
{
    public class CodeRuleDefinifionCreatingDateTimeData : NAS.GUI.Pattern.State
    {

        public CodeRuleDefinifionCreatingDateTimeData(System.Web.UI.Control _UIControl) : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            try
            {
                switch (transition)
                {
                    case "UseNumberData":
                        context.State = new CodeRuleDefinifionCreatingNumberData(_UIControl);
                        break;
                    case "UseStringData":
                        context.State = new CodeRuleDefinifionCreatingStringData(_UIControl);
                        break;
                    case "Save":
                        context.State = new CodeRuleDefinifionEditingDateTimeData(_UIControl);
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
            return getOwnerUIControl().CodeRuleDefinifionCreatingDateTimeData_CRUD();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().CodeRuleDefinifionCreatingDateTimeData_PreTransitionCRUD(transition);
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().CodeRuleDefinifionCreatingDateTimeData_UpdateGUI();
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