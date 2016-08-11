using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.State
{
    public class BookingEntriesOutputInventoryCommand : NAS.GUI.Pattern.State
    {
        public BookingEntriesOutputInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Close":
                    context.State = new ClosingOutputInventoryCommand(_UIControl);
                    break;
                case "Save":
                    context.State = new SavingOutputInventoryCommand(_UIControl);
                    break;
                case "Delete":
                    context.State = new DeletingOutputInventoryCommand(_UIControl);
                    break;
                case "BookEntries":
                    context.State = new BookingEntriesOutputInventoryCommand(_UIControl);
                    break;
                case "BookedEntries":
                    context.State = new BookedEntriesOutputInventoryCommand(_UIControl);
                    break;
                case "CloseBookingPopup":
                    context.State = new ClosingOutputInventoryCommand(_UIControl);
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
            getOwnerUIControl().CRUD_BookingEntriesOutputInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_BookedEntriesOutputInventoryCommand();
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