using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Invoice.PurchaseInvoice.GUI;

namespace WebModule.Invoice.PurchaseInvoice.State
{
    public class PurchaseInvoiceCanceling : NAS.GUI.Pattern.State
    {
        public PurchaseInvoiceCanceling(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            throw new NAS.GUI.Pattern.IncompatibleTransitionException();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return GetOwnerUIControl().PurchaseInvoiceCanceling_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return GetOwnerUIControl().PurchaseInvoiceCanceling_CRUD();
        }

        public override bool UpdateGUI()
        {
            return GetOwnerUIControl().PurchaseInvoiceCanceling_UpdateGUI();
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