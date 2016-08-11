using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Voucher.State;
using WebModule.Voucher.Receipt.GUI;

namespace WebModule.Voucher.Receipt.State
{
    public class ReceiptVoucherCanceling : VoucherState
    {
        public ReceiptVoucherCanceling(System.Web.UI.Control _UIControl)
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
            return getOwnerUIControl().ReceiptVoucherCanceling_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().ReceiptVoucherCanceling_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().ReceiptVoucherCanceling_UpdateGUI();
        }

        public ReceiptVoucherEditingForm getOwnerUIControl()
        {
            ReceiptVoucherEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is ReceiptVoucherEditingForm)
                {
                    ret = (ReceiptVoucherEditingForm)UIControl;
                }
            }
            return ret;
        }
    }
}