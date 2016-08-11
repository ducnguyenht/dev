using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Voucher.State;
using NAS.GUI.Pattern;
using WebModule.Voucher.Receipt.GUI;
using WebModule.Voucher.Payment.GUI;

namespace WebModule.Voucher.Payment.State
{
    public class PaymentVoucherLocked : VoucherState
    {
        public PaymentVoucherLocked(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected override bool Save(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            throw new IncompatibleTransitionException();
        }

        protected override bool Cancel(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            try
            {
                context.State = new PaymentVoucherCanceling(_UIControl);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().PaymentVoucherLocked_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().PaymentVoucherLocked_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().PaymentVoucherLocked_UpdateGUI();
        }

        public PaymentVoucherEditingForm getOwnerUIControl()
        {
            PaymentVoucherEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is PaymentVoucherEditingForm)
                {
                    ret = (PaymentVoucherEditingForm)UIControl;
                }
            }
            return ret;
        }
    }
}