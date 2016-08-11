using System;
using NAS.GUI.Pattern;
using WebModule.NAANAdmin.Authorization.UserControl;
namespace WebModule.NAANAdmin.Authorization.Sate
{
    public class PersonEditing : State
    {

        public PersonEditing(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        {
        }

        public override bool Transit(Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Save":
                    context.State = new PersonEditing(_UIControl);
                    break;
                case "Cancel":
                    break;
                default:
                    throw new Exception();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            if (getOwnerUIControl == null)
                return false;
            return getOwnerUIControl.PersonEditing_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            if (getOwnerUIControl == null)
                return false;
            return getOwnerUIControl.PersonEditing_CRUD();
        }

        public override bool UpdateGUI()
        {
            if (getOwnerUIControl == null)
                return false;
            return getOwnerUIControl.PersonEditing_UpdateGUI();
        }

        public uPopupUserCreating getOwnerUIControl
        {
            get
            {
                uPopupUserCreating ret = null;
                if (UIControl != null)
                {
                    if (UIControl is uPopupUserCreating)
                    {
                        ret = (uPopupUserCreating)UIControl;
                    }
                }
                return ret;
            }
        }
    }
}