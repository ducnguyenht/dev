using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Voucher.State;

namespace WebModule.Voucher.Receipt.State
{
    public class ReceiptVoucherCannotBookingEntry : ReceiptVoucherEditing
    {
        public ReceiptVoucherCannotBookingEntry(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected override bool Save(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            try
            {
                context.State = new ReceiptVoucherCannotBookingEntry(_UIControl);
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
                getOwnerUIControl().ReceiptVoucherCannotBookingEntry_UpdateGUI();
        }
    }
}