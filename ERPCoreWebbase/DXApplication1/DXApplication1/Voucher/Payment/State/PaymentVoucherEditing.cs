using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Voucher.State;
using WebModule.Voucher.Receipt.GUI;
using WebModule.Voucher.Payment.GUI;

namespace WebModule.Voucher.Payment.State
{
    public class PaymentVoucherEditing : VoucherState
    {
        public PaymentVoucherEditing(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected override bool Save(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            try
            {
                context.State = new PaymentVoucherEditing(_UIControl);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            return getOwnerUIControl().PaymentVoucherEditing_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().PaymentVoucherEditing_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().PaymentVoucherEditing_UpdateGUI();
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