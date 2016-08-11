using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Warehouse.UserControl
{
    public partial class uAdjustingInventoryInfo : System.Web.UI.UserControl
    {
        object[] grdDataSource = new[] { 
                     new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", reciept = "HD001",
                             recieptamount="100", realamount="90", difamount="10", unit="Thùng", description = "Mặt hàng 1",
                             entry="Đã điều chỉnh"
                    },
                    new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", reciept = "HD001",
                             recieptamount="150", realamount="130", difamount="20", unit="Hộp", description = "Mặt hàng 2",
                             entry="Chưa điều chỉnh"
                    },
                };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            grvConfirmSummary.DataSource = grdDataSource;
            grvConfirmSummary.DataBind();
        }
    }
}