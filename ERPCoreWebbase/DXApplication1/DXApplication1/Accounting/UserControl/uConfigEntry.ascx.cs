using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Text.RegularExpressions;

namespace WebModule.Accounting.UserControl
{
    public partial class uConfigEntry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdata_detail.DataSource = new[] { 
                        new{No = "1",Item = "Ví dụ 1",SoTaiKhoan = "131", NoCo = "Có"},
                        new{No = "2",Item = "Ví dụ 2",SoTaiKhoan = "111", NoCo = "Nợ"},
                        new{No = "3",Item = "Ví dụ 3",SoTaiKhoan = "112", NoCo = "Nợ"},                      
                    };
            grdata_detail.KeyFieldName = "No";
            grdata_detail.DataBind();
        }
       

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            String[] par = Regex.Split(e.Parameter, "%%");
            ASPxPopupControl1.HeaderText = "Cấu hình định khoản " + par[1];   
        }
    }
}