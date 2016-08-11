using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uSettingSaleTotalByProductGrp : System.Web.UI.UserControl
    {
        object[] grv_settingSalesTotalByproGrpSource = new[]{ new { SupplierId = 1, Code = "NHOM01", Name = "Nhóm Zidovudine", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" },
            new { SupplierId = 2, Code = "NHOM02", Name = "Morphine", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" },
            new { SupplierId = 3, Code = "NHOM03", Name = "Penicillin", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" },
            new { SupplierId = 4, Code = "NHOM04", Name = "Nhóm Thuốc Ngủ", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" }};

        protected void Page_Load(object sender, EventArgs e)
        {
            grv_settingSalesTotalByproGrp.DataSource = grv_settingSalesTotalByproGrpSource;
            grv_settingSalesTotalByproGrp.DataBind();
        }
    }
}