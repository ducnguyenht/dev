using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.usercontrol
{
    public partial class uViewConditionReceivePolicy : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_chinhsach.DataSource = new[] { 
                new {policyid = "CS0001", policyname = "Chính sách 1", 
                    from = "20/10/2011", to = "20/10/2015", numofday = "10", priceunit = "20.000", reponsibility = "Phạt chi phí bảo quản"},
                new {policyid = "CS0002", policyname = "Chính sách 2", 
                    from = "20/10/2011", to = "20/10/2015", numofday = "10", priceunit = "20.000", reponsibility = "Phạt chi phí bảo quản, chi phí vận tải"}
            };
            grv_chinhsach.DataBind();
        }
    }
}