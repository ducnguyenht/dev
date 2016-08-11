using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.PayReceiving.UserControl.VoucherBookingEntry.State
{
    public class CancelBookingVoucherState : NAS.GUI.Pattern.State
    {
        public CancelBookingVoucherState(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().UpdateGUI_CancelBookingVoucherState();
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