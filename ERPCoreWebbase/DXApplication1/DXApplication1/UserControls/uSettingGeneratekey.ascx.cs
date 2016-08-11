using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.UserControls
{
    public partial class uSettingGeneratekey : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] { 
                new {position = "01", name = "Tiếp đầu ngữ", format = "Chuỗi kí tự", 
                    value = "QUA", repeat_rule = ""},
                new {position = "02", name = "Số thứ tự", format = "NN", 
                    value = "", repeat_rule = "Năm"},
                new {position = "03", name = "Năm", format = "YYYY", 
                    value = "", repeat_rule = ""}
            };
            ASPxGridView1.DataBind();
        }
    }
}