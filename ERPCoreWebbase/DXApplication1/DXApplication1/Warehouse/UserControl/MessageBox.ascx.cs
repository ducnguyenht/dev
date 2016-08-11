using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Warehouse.UserControl
{
    public partial class MessageBox : System.Web.UI.UserControl
    {
        private string m_Message;

        public ASPxLabel Message
        {
            get { return lblMassage; }  
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}