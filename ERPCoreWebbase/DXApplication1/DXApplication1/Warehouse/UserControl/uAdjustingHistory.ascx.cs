using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Warehouse.UserControl
{
    public partial class uAdjustingHistory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdData.DataSource =
             new[] { 
                       new { key="1236", verifyid = "MAT001", position = "Kho 1; Kệ 1", date="27-07-2013", recieptamount="100", realamount = "99", editamount = "99",
                             amountdiff= "0", note=""
                    },
                    new { key="12346", verifyid = "MAT002", position = "Kho 2; Kệ 1",date="10-07-2013", recieptamount="99", realamount = "100", editamount = "100",
                             amountdiff= "0", note="Thừa"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();
        }
    }
}