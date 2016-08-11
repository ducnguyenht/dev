using E911.Module.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E911.Web.Controls
{
    public partial class WebUserControl11 : System.Web.UI.UserControl, IXpoSessionAwareControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void UpdateDataSource(DevExpress.Xpo.Session session)
        {
            //throw new NotImplementedException();
        }
    }
}