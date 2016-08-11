using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Voucher.State;
using WebModule.Voucher.Receipt.GUI;

namespace WebModule.Voucher.Receipt.State
{
    public class ReceiptVoucherCreating : VoucherState
    {
        public ReceiptVoucherCreating(System.Web.UI.Control _UIControl)
            : base(_UIControl)
        { }

        protected override bool Save(NAS.GUI.Pattern.Context context, System.Web.UI.Control _UIControl)
        {
            try
            {
                context.State = new ReceiptVoucherEditing(_UIControl);
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
                context.State = new ReceiptVoucherCanceling(_UIControl);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return getOwnerUIControl().ReceiptVoucherCreating_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return getOwnerUIControl().ReceiptVoucherCreating_CRUD();
        }

        public override bool UpdateGUI()
        {
            return getOwnerUIControl().ReceiptVoucherCreating_UpdateGUI();
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