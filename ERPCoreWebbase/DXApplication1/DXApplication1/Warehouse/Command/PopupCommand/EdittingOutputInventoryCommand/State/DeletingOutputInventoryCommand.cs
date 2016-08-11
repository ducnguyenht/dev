using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.State
{
    public class DeletingOutputInventoryCommand: NAS.GUI.Pattern.State
    {
        public DeletingOutputInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Create":
                    context.State = new CreatingOutputInventoryCommandByInvoiceArtifact(_UIControl);
                    break;
                case "CreateByBill":
                    context.State = new CreatingOutputInventoryCommand(_UIControl);
                    break;
                case "Edit":
                    context.State = new EdittingOutputInventoryCommand(_UIControl);
                    break;
                case "Delete":
                    context.State = new DeletingOutputInventoryCommand(_UIControl);
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
            getOwnerUIControl().CRUD_DeletingOutputInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_DeletingOutputInventoryCommand();
            return true;
        }

        public WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.uEdittingOutputInventoryCommand getOwnerUIControl()
        {
            WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.uEdittingOutputInventoryCommand UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.uEdittingOutputInventoryCommand)
                {
                    UI = (WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.uEdittingOutputInventoryCommand)UIControl;
                }
            }
            return UI;
        }
    }
}