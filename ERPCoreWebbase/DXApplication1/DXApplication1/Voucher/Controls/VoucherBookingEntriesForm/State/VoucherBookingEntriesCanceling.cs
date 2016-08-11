using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Voucher.Controls.VoucherBookingEntriesForm.State
{
    public class VoucherBookingEntriesCanceling : NAS.GUI.Pattern.State
    {
        public VoucherBookingEntriesCanceling(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            throw new NAS.GUI.Pattern.IncompatibleTransitionException();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().VoucherBookingEntriesCanceling_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().VoucherBookingEntriesCanceling_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().VoucherBookingEntriesCanceling_UpdateGUI();
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