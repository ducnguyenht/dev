using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Voucher.Controls.VoucherBookingEntriesForm.State
{
    public class VoucherLockedBookingdEntries : NAS.GUI.Pattern.State
    {
        public VoucherLockedBookingdEntries(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition.ToUpper())
            {
                case VoucherBookingEntriesFormStateConstant.TRANSITION_CANCEL:
                    context.State = new VoucherBookingEntriesCanceling(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().VoucherLockedBookingdEntries_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().VoucherLockedBookingdEntries_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().VoucherLockedBookingdEntries_UpdateGUI();
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