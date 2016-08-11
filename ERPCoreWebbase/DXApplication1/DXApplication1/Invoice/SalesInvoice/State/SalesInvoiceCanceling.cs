using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Invoice.SalesInvoice.GUI;

namespace WebModule.Invoice.SalesInvoice.State
{
    public class SalesInvoiceCanceling : NAS.GUI.Pattern.State
    {
        public SalesInvoiceCanceling(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            throw new NAS.GUI.Pattern.IncompatibleTransitionException();
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return GetOwnerUIControl().SalesInvoiceCanceling_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return GetOwnerUIControl().SalesInvoiceCanceling_CRUD();
        }

        public override bool UpdateGUI()
        {
            return GetOwnerUIControl().SalesInvoiceCanceling_UpdateGUI();
        }

        public SalesInvoiceEditingForm GetOwnerUIControl()
        {
            SalesInvoiceEditingForm ret = null;
            if (UIControl != null)
            {
                if (UIControl is SalesInvoiceEditingForm)
                {
                    ret = (SalesInvoiceEditingForm)UIControl;
                }
            }
            return ret;
        }
    }
}