using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.UserControl.uLegalInvoiceArtifact.State
{
    public class LegalInvoiceArtifact_Saving : NAS.GUI.Pattern.State 
    {
        public LegalInvoiceArtifact_Saving(System.Web.UI.Control _UIControl)
            : base(_UIControl) { }

        public override bool Transit(NAS.GUI.Pattern.Context context, string transition, System.Web.UI.Control _UIControl)
        {
            switch (transition)
            {
                case "Save":
                    context.State = new LegalInvoiceArtifact_Saving(_UIControl);
                    break;
                default:
                    throw new Exception("current compatibility setting is not supported");
            }
            return true;
        }

        public override bool PreTransitionCRUD(string transition)
        {
            return true;
        }

        public override bool CRUD()
        {
            getOwnerUIControl().CRUD_LegalInvoiceArtifact_Saving();
            return true;
        }

        public override bool UpdateGUI()
        {
            getOwnerUIControl().Update_LegalInvoiceArtifact_Saving();
            return true;
        }

        public WebModule.Accounting.UserControl.uLegalInvoiceArtifact.uLegalInvoiceArtifact getOwnerUIControl()
        {
            WebModule.Accounting.UserControl.uLegalInvoiceArtifact.uLegalInvoiceArtifact UI = null;
            if (UIControl != null)
            {
                if (UIControl is WebModule.Accounting.UserControl.uLegalInvoiceArtifact.uLegalInvoiceArtifact)
                {
                    UI = (WebModule.Accounting.UserControl.uLegalInvoiceArtifact.uLegalInvoiceArtifact)UIControl;
                }
            }
            return UI;
        }
    }
}