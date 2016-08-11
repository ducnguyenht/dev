using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.State
{
    public class ClosingAuditingInventoryCommand : NAS.GUI.Pattern.State
    {
        public ClosingAuditingInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Delete":
                    context.State = new DeletingAuditingInventoryCommand(_UIControl);
                    break;
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            //getOwnerUIControl().CRUD_ClosingAuditingInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            //getOwnerUIControl().UpdateGUI_ClosingAuditingInventoryCommand();
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