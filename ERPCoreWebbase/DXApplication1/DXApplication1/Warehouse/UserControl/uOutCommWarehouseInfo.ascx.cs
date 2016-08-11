using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Warehouse.UserControl
{
    public partial class uOutCommWarehouseInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grvSummaryData.DataSource =
                   new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
            grvSummaryData.KeyFieldName = "key";
            grvSummaryData.DataBind();
        }
    }
}