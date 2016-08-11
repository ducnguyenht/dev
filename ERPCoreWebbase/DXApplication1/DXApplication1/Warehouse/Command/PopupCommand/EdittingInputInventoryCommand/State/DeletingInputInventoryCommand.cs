using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.State
{
    public class DeletingInputInventoryCommand: NAS.GUI.Pattern.State
    {
        public DeletingInputInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Create":
                    context.State = new CreatingInputInventoryCommandByInvoiceArtifact(_UIControl);
                    break;
                case "CreateByBill":
                    context.State = new CreatingInputInventoryCommand(_UIControl);
                    break;
                case "Edit":
                    context.State = new EdittingInputInventoryCommand(_UIControl);
                    break;
                case "Delete":
                    context.State = new DeletingInputInventoryCommand(_UIControl);
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
            getOwnerUIControl().CRUD_DeletingInputInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_DeletingInputInventoryCommand();
            return true;
        }

        public WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.uEdittingInputInventoryCommand getOwnerUIControl()
        {
            WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.uEdittingInputInventoryCommand UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.uEdittingInputInventoryCommand)
                {
                    UI = (WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.uEdittingInputInventoryCommand)UIControl;
                }
            }
            return UI;
        }
    }
}