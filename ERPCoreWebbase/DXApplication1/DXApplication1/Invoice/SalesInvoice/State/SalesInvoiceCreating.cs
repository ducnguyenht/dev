using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModule.Invoice.SalesInvoice.GUI;

namespace WebModule.Invoice.SalesInvoice.State
{
    public class SalesInvoiceCreating : NAS.GUI.Pattern.State
    {
        public SalesInvoiceCreating(System.Web.UI.Control control) : base(control) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition.ToUpper())
            {
                case "CANCEL":
                    context.State = new SalesInvoiceCanceling(_UIControl);
                    break;
                case "SAVE":
                    context.State = new SalesInvoiceEditing(_UIControl);
                    break;
                default:
                    throw new NAS.GUI.Pattern.IncompatibleTransitionException();
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return GetOwnerUIControl().SalesInvoiceCreating_PreTransitionCRUD(transition);
        }

        public override bool CRUD()
        {
            return GetOwnerUIControl().SalesInvoiceCreating_CRUD();
        }

        public override bool UpdateGUI()
        {
            return GetOwnerUIControl().SalesInvoiceCreating_UpdateGUI();
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