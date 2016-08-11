using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.UserControl
{
    public partial class uMaterialRequirement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{key ="1",ID = "NL001", Name = "Nguyên liệu A", Unit = "Kg", Quantity ="10",Total = "100.000", Note = "Ví dụ 1"},
                                                new{key ="2",ID = "NL002", Name = "Nguyên liệu B", Unit = "Củ", Quantity ="50",Total = "50.000", Note = "Ví dụ 2"},
                                                new{key ="3",ID = "NL003", Name = "Nguyên liệu C", Unit = "Kg", Quantity ="20",Total = "200.000", Note = "Ví dụ 3"},
                                                new{key ="4",ID = "NL004", Name = "Nguyên liệu D", Unit = "Củ", Quantity ="70",Total = "80.000", Note = "Ví dụ 4"},
                                                new{key ="5",ID = "NL005", Name = "Nguyên liệu E", Unit = "Kg", Quantity ="60",Total = "1.000.000", Note = "Ví dụ 5"}};
            ASPxGridView1.KeyFieldName = "key";
            ASPxGridView1.DataBind();           
        }

        protected void ASPxGridView1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail)
            {
                ASPxGridView grdetail = ASPxGridView1.FindDetailRowTemplateControl(e.VisibleIndex, "GridDetail") as ASPxGridView;
                grdetail.DataSource = new[] {   new{Key = "1",PRName = "Sản xuất 1",PName = "Sản phẩm 1"},
                                                new{Key = "2",PRName = "Sản xuất 1",PName = "Sản phẩm 2"},
                                                new{Key = "3",PRName = "Sản xuất 2",PName = "Sản phẩm 1"},
                                                new{Key = "4",PRName = "Sản xuất 2",PName = "Sản phẩm 2"},
                                                new{Key = "5",PRName = "Sản xuất 3",PName = "Sản phẩm 3"},};
                grdetail.KeyFieldName = "Key";
                grdetail.DataBind();
            }
        }
    }
}