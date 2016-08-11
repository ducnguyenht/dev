using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Voucher.State;
using WebModule.Voucher.Receipt.GUI;
using WebModule.Voucher.Payment.GUI;

namespace WebModule.Voucher.Payment.State
{
    public class PaymentVoucherCanceling : VoucherState
    {
        public PaymentVoucherCanceling(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected override bool Save(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            throw new NotImplementedException();
        }

        protected override bool Cancel(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            throw new NotImplementedException();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().PaymentVoucherCanceling_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().PaymentVoucherCanceling_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().PaymentVoucherCanceling_UpdateGUI();
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