using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.PayReceiving.UserControl.VoucherBookingEntry.State
{
    public class ReadyBookingVoucherState : NAS.GUI.Pattern.State
    {
        public ReadyBookingVoucherState(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Book":
                    context.State = new BookedVoucherState(_UIControl);
                    break;
                case "Cancel":
                    context.State = new CancelBookingVoucherState(_UIControl);
                    break;
                default:
                    throw new Exception("current compatibility setting is not supported");
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            if (transition.Equals("Book"))
                getOwnerUIControl().PreCRUD_ReadyBookingVoucherState();
            return true;
        }

        public override bool CRUD()
        {
            getOwnerUIControl().CRUD_ReadyBookingVoucherState();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_ReadyBookingVoucherState();
            return true;
        }

        public WebModule.PayReceiving.UserControl.VoucherBookingEntry.uVoucherBookingEntry getOwnerUIControl()
        {
            WebModule.PayReceiving.UserControl.VoucherBookingEntry.uVoucherBookingEntry UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.PayReceiving.UserControl.VoucherBookingEntry.uVoucherBookingEntry)
                {
                    UI = (WebModule.PayReceiving.UserControl.VoucherBookingEntry.uVoucherBookingEntry)UIControl;
                }
            }
            return UI;
        }
    }
}