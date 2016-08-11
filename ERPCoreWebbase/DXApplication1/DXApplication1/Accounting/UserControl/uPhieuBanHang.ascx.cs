using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uPhieuBanHang : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{c1 = "1",sd = "Mua hàng 1",c2 = "Ví dụ 1",c3 = "CTY ABC",c4 = "01/07/2013",c5 = "Đã duyệt"},
                                                new{c1 = "2",sd = "Mua hàng 2",c2 = "Ví dụ 2",c3 = "CTY XYZ",c4 = "03/07/2013",c5 = "Đã duyệt"},
                                                new{c1 = "3",sd = "Mua hàng 1",c2 = "Ví dụ 3",c3 = "Cửa hàng A",c4 = "05/07/2013",c5 = "Chưa duyệt"},
                                                new{c1 = "4",sd = "Mua hàng 1",c2 = "Ví dụ 4",c3 = "Đại lí ABC",c4 = "06/07/2013",c5 = "Chưa duyệt"},
                                                new{c1 = "5",sd = "Mua hàng 2",c2 = "Ví dụ 5",c3 = "Nhà thuốc A",c4 = "07/07/2013",c5 = "Chưa duyệt"},
                                                new{c1 = "6",sd = "Mua hàng 1",c2 = "Ví dụ 6",c3 = "Nhà thuốc B",c4 = "08/07/2013",c5 = "Chưa duyệt"}};
            ASPxGridView1.KeyFieldName = "c1";
            ASPxGridView1.DataBind();
        }
    }
}
