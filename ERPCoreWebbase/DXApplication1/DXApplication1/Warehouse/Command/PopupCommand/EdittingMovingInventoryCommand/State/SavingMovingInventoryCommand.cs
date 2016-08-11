using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.State
{
    public class SavingMovingInventoryCommand : NAS.GUI.Pattern.State
    {
        public SavingMovingInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Close":
                    context.State = new ClosingMovingInventoryCommand(_UIControl);
                    break;
                case "Save":
                    context.State = new SavingMovingInventoryCommand(_UIControl);
                    break;
                case "Delete":
                    context.State = new DeletingMovingInventoryCommand(_UIControl);
                    break;
                default:
                    throw new Exception("current compatibility setting is not supported");
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            getOwnerUIControl().CRUD_SavingMovingInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            return true;
        }

        public WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.uEdittingMovingInventoryCommand getOwnerUIControl()
        {
            WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.uEdittingMovingInventoryCommand UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.uEdittingMovingInventoryCommand)
                {
                    UI = (WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.uEdittingMovingInventoryCommand)UIControl;
                }
            }
            return UI;
        }
    }
}