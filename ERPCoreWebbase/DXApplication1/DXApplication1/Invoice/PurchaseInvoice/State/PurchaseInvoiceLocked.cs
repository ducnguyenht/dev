using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Invoice.PurchaseInvoice.GUI;

namespace WebModule.Invoice.PurchaseInvoice.State
{
    public class PurchaseInvoiceLocked : NAS.GUI.Pattern.State
    {
        public PurchaseInvoiceLocked(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition.ToUpper())
            {
                case "CANCEL":
                    context.State = new PurchaseInvoiceCanceling(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return GetOwnerUIControl().PurchaseInvoiceLocked_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return GetOwnerUIControl().PurchaseInvoiceLocked_CRUD();
        }

        public override bool UpdateGUI()
        {
            return GetOwnerUIControl().PurchaseInvoiceLocked_UpdateGUI();
        }

        public PurchaseInvoiceEditingForm GetOwnerUIControl()
        {
            PurchaseInvoiceEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is PurchaseInvoiceEditingForm)
                {
                    ret = (PurchaseInvoiceEditingForm)UIControl;
                }
            }
            return ret;
        }
    }
}