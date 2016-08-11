using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.State
{
    public class LoadingAuditingInventoryCommand : NAS.GUI.Pattern.State
    {
        public LoadingAuditingInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl, true) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {                       
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            getOwnerUIControl().PreTransitionCRUD_LoadingAuditingInventoryCommand(transition);
            return true;
        }

        public override bool CRUD()
        {
            getOwnerUIControl().CRUD_LoadingAuditingInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_LoadingAuditingInventoryCommand();
            return true;
        }


        public WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.uAuditingInventoryCommand getOwnerUIControl()
        {
            WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.uAuditingInventoryCommand UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.uAuditingInventoryCommand)
                {
                    UI = (WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.uAuditingInventoryCommand)UIControl;
                }
            }

            return UI;
        }
    }
}