using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uSettingTestSalesTotalByProduct : System.Web.UI.UserControl
    {
        object[] grv_settingSalesTotalByproGrpSource = new[]{ 
            new { SupplierId = 1, Code = "HH0001", Name = "Zidovudine ABC", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" },
            new { SupplierId = 2, Code = "HH0002", Name = "Morphine ABC", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" },
            new { SupplierId = 3, Code = "HH0003", Name = "Penicillin ABC", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" },
            new { SupplierId = 4, Code = "HH0004", Name = "Thuốc Ngủ ABC", Description = "Công ty cổ phần NAAN Solution", RowStatus = "Sử Dụng" }};
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_settingSalesTotalByProduct.DataSource = grv_settingSalesTotalByproGrpSource;
            grv_settingSalesTotalByProduct.DataBind();
        }
    }
}