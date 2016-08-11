using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uDuyetThuChi : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter.ToString())
            {
                case "thu":
                    {
                        ASPxFormLayout1.FindItemByFieldName("txttotal").Caption = "Tổng tiền phiếu thu";
                        break;
                    }
                case "chi":
                    {
                        ASPxFormLayout1.FindItemByFieldName("txttotal").Caption = "Tổng tiền phiếu chi";
                        break;
                    }
                case "unc":
                    {
                        ASPxFormLayout1.FindItemByFieldName("txttotal").Caption = "Tổng tiền phiếu ủy nhiệm chi";
                        break;
                    }
                case "unt":
                    {
                        ASPxFormLayout1.FindItemByFieldName("txttotal").Caption = "Tổng tiền phiếu ủy nhiệm thu";
                        break;
                    }
                default:break;
            }

        }
    }
}