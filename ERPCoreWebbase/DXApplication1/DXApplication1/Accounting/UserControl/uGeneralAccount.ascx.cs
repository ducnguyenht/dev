using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uGeneralAccount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{MCT = "CT001",Des = "Thu tiền khách hàng"},
                                                new{MCT = "CT002",Des = "Chi tiền bảo dưỡng"},
                                                new{MCT = "CT003",Des = "Chi tiền vận chuyển"},
                                                new{MCT = "CT004",Des = "Thu tiền khách hàng 2"},
                                                new{MCT = "CT005",Des = "Chiết khấu từ nhà cung cấp"}};
            ASPxGridView1.KeyFieldName = "MTC";
            ASPxGridView1.DataBind();
            
        }
    }
}