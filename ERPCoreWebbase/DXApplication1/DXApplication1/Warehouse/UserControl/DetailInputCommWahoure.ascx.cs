using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Warehouse.UserControl
{
    public partial class DetailInputCommWahoure : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource =
                   new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123", date = "25/07/2014",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",  date = "25/07/2014",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
            ASPxGridView1.KeyFieldName = "key";
            ASPxGridView1.DataBind();
        }

        protected void txtCode_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void txtName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            formSupplierGroupEdit.ShowOnPageLoad = false;
        }
    }
}