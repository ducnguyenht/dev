using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Voucher.State;

namespace WebModule.Voucher.Payment.State
{
    public class PaymentVoucherCanBookingEntry : PaymentVoucherEditing
    {
        public PaymentVoucherCanBookingEntry(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected override bool Save(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            try
            {
                context.State = new PaymentVoucherCanBookingEntry(_UIControl);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool CRUD()
        {
            return true;
        }

        public override bool UpdateGUI()
        {
            return base.UpdateGUI() &
                getOwnerUIControl().PaymentVoucherCanBookingEntry_UpdateGUI();
        }
    }
}