using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Produce.UserControl
{
    public partial class uPriceEstimates : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{ID = "SP001",Name = "Sản phẩm 1", MP = "10.000",HP = "5.000", EP = "1.000",Price = "16.000", Note = "Tham khảo"},
                                                new{ID = "SP002",Name = "Sản phẩm 2", MP = "15.000",HP = "4.000", EP = "2.000",Price = "21.000", Note = "Tham khảo"},
                                                new{ID = "SP003",Name = "Sản phẩm 3", MP = "9.000",HP = "7.000", EP = "5.000",Price = "21.000", Note = "Tham khảo"}};
            ASPxGridView1.KeyFieldName = "ID";
            ASPxGridView1.DataBind();
        }
    }
}
        