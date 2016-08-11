using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.State
{
    public class ClosingInputInventoryCommand : NAS.GUI.Pattern.State
    {
        public ClosingInputInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
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
            getOwnerUIControl().CRUD_ClosingInputInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_ClosingInputInventoryCommand();
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