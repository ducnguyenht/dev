using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldDataEditingSingleChoiceList : CustomFieldDataEditing
    {
        public CustomFieldDataEditingSingleChoiceList(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }
        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            return base.Transit(context, transition, _UIControl);
        }
        public override bool CRUD()
        {
            return base.CRUD()
                & getOwnerUIControl().CustomFieldDataEditingSingleChoiceList_CRUD();
        }
        public override bool PreTransitionCRUD(string transition)
        {
            return base.PreTransitionCRUD(transition)
                & getOwnerUIControl().CustomFieldDataEditingSingleChoiceList_PreTransitionCRUD(transition);
        }
        public override bool UpdateGUI()
        {
            return base.UpdateGUI()
                    & getOwnerUIControl().CustomFieldDataEditingSingleChoiceList_UpdateGUI();
        }
    }
}