using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.UserControl
{
    public partial class uHumanRequirement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{key ="1",PRName = "RQ001", PName = "Sản phẩm 1", Degree = "Kỹ sư", Quantity = "1", Time = "8", Total ="240.000", Note = "Ví dụ 1"},
                                                new{key ="2",PRName = "RQ001", PName = "Sản phẩm 2", Degree = "Kỹ sư", Quantity = "1", Time = "8", Total ="240.000", Note = "Ví dụ 2"},
                                                new{key ="3",PRName = "RQ002", PName = "Sản phẩm 1", Degree = "Nhân công", Quantity = "10", Time = "12", Total ="1.800.000", Note = "Ví dụ 3"},
                                                new{key ="4",PRName = "RQ002", PName = "Sản phẩm 2", Degree = "Nhân công", Quantity = "5", Time = "12", Total ="900.000", Note = "Ví dụ 4"},
                                                new{key ="5",PRName = "RQ003", PName = "Sản phẩm 3", Degree = "Nhân công", Quantity = "20", Time = "12", Total ="3.600.000", Note = "Ví dụ 5"}};
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