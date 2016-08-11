using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm
{
    public class CustomFieldDataEditingMultiChoiceList : CustomFieldDataEditing
    {
        public CustomFieldDataEditingMultiChoiceList(System.Web.UI.Control _UIControl)
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
                & getOwnerUIControl().CustomFieldDataEditingMultiChoiceList_CRUD();
        }
        public override bool PreTransitionCRUD(string transition)
        {
            return base.PreTransitionCRUD(transition)
                & getOwnerUIControl().CustomFieldDataEditingMultiChoiceList_PreTransitionCRUD(transition);
        }
        public override bool UpdateGUI()
        {
            return base.UpdateGUI() 
                    & getOwnerUIControl().CustomFieldDataEditingMultiChoiceList_UpdateGUI();
        }
    }
}