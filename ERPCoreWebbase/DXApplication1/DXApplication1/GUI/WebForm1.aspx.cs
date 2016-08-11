using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.NASID;
using DevExpress.Xpo;
//using DAL.NASID;

namespace DXApplication1.GUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //private OrganizationBLO organizationBLO;
        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelper.GetNewSession();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //organizationBLO = new OrganizationBLO(session);
            //Guid root = Guid.Parse("f3ca4e28-3bb4-47ec-8673-5bc5c8bd43ed");
            //trlOrganization.DataSource = organizationBLO.getOrganizationHierachy(root);
            //trlOrganization.DataBind();
        }
    }
}