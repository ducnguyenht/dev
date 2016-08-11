using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.Accounting.UserControl.uLegalInvoiceArtifact;

namespace WebModule.Accounting
{
    public partial class LegalInvoiceArtifact : System.Web.UI.Page
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            DBLegalInvoiceArtifact.Session = session;

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}