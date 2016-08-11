using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Warehouse.UserControl
{
    public partial class uInCommWarehouseInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grvSummaryData.DataSource =
                   new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",date = "25/07/2014",
                             amount= "5.000", realamount = "4.800", minusamount = "200", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",date = "25/07/2014",
                             amount= "2.200", realamount = "2000", minusamount = "200", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
            grvSummaryData.KeyFieldName = "key";
            grvSummaryData.DataBind();        
        }
    }
}