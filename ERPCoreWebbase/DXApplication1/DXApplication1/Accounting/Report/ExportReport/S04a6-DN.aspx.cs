using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;

namespace WebModule.Accounting.Report.ExportReport
{
    public partial class S04a6_DN : System.Web.UI.Page
    {
        Session session;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            DBMonth.Session = session;
            DBYear.Session = session;
        }
    }
}