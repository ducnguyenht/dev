using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl
{
    public partial class testing : System.Web.UI.Page
    {
        Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            grdLookupDemoXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cpDemo_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //cbodemo.ClientVisible = false;
            //cbodemo.ClientEnabled = false;
            //throw new Exception("Value is changed");
        }
    }
}