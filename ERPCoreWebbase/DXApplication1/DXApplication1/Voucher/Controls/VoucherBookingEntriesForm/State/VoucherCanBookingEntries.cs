using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Voucher.Controls.VoucherBookingEntriesForm.State
{
    public class VoucherCanBookingEntries : NAS.GUI.Pattern.State
    {
        public VoucherCanBookingEntries(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition.ToUpper())
            {
                case VoucherBookingEntriesFormStateConstant.TRANSITION_CANCEL:
                    context.State = new VoucherBookingEntriesCanceling(_UIControl);
                    break;
                case VoucherBookingEntriesFormStateConstant.TRANSITION_BOOK_ENTRIES:
                    context.State = new VoucherCanBookingEntries(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().VoucherCanBookingEntries_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().VoucherCanBookingEntries_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().VoucherCanBookingEntries_UpdateGUI();
        }

        public VoucherBookingEntriesForm getOwnerUIControl()
        {
            VoucherBookingEntriesForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is VoucherBookingEntriesForm)
                {
                    ret = (VoucherBookingEntriesForm)UIControl;
                }
            }
            return ret;
        }
    }
}