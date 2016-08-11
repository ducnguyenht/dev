using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.State
{
    public class BookingEntriesInputInventoryCommand : NAS.GUI.Pattern.State
    {
        public BookingEntriesInputInventoryCommand(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Close":
                    context.State = new ClosingInputInventoryCommand(_UIControl);
                    break;
                case "Save":
                    context.State = new SavingInputInventoryCommand(_UIControl);
                    break;
                case "Delete":
                    context.State = new DeletingInputInventoryCommand(_UIControl);
                    break;
                case "BookEntries":
                    context.State = new BookingEntriesInputInventoryCommand(_UIControl);
                    break;
                case "BookedEntries":
                    context.State = new BookedEntriesInputInventoryCommand(_UIControl);
                    break;
                case "CloseBookingPopup":
                    context.State = new ClosingInputInventoryCommand(_UIControl);
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
            getOwnerUIControl().CRUD_BookingEntriesInputInventoryCommand();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_BookedEntriesInputInventoryCommand();
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