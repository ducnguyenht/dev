using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.UserControl
{
    public partial class uProduceRequirement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{ID = "RQ001", Name = "Yêu cầu sản xuất 1", RequiredBy = "Cửa hàng A", Des = "Ví dụ 1", Note = " Ví dụ 1"},
                                                new{ID = "RQ002", Name = "Yêu cầu sản xuất 2", RequiredBy = "Cửa hàng B", Des = "Ví dụ 2", Note = " Ví dụ 2"},
                                                new{ID = "RQ003", Name = "Yêu cầu sản xuất 3", RequiredBy = "Đại lý 1",   Des = "Ví dụ 3", Note = " Ví dụ 3"},
                                                new{ID = "RQ004", Name = "Yêu cầu sản xuất 4", RequiredBy = "Đại lý 2",   Des = "Ví dụ 4", Note = " Ví dụ 4"},
                                                new{ID = "RQ005", Name = "Yêu cầu sản xuất 5", RequiredBy = "Cửa hàng C", Des = "Ví dụ 5", Note = " Ví dụ 5"}};
            ASPxGridView1.KeyFieldName = "ID";
            ASPxGridView1.DataBind();           
        }

        protected void ASPxGridView1_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail)
            {
                ASPxGridView grdetail = ASPxGridView1.FindDetailRowTemplateControl(e.VisibleIndex, "GridDetail") as ASPxGridView;
                grdetail.DataSource = new[] {   new{ID = "SP001",Name = "Sản phẩm 1",Unit = "Hộp",Quantity = "10", ShortestTime = "25/08/2013",Time = "25/08/2013", Priority = "1", Note = "Cần gấp"},
                                            new{ID = "SP002",Name = "Sản phẩm 2",Unit = "Thùng",Quantity = "2", ShortestTime = "25/08/2013", Time = "25/08/2013", Priority = "2", Note = "Cần gấp"},
                                            new{ID = "SP003",Name = "Sản phẩm 3",Unit = "Thùng",Quantity = "4", ShortestTime = "25/08/2013", Time = "25/08/2013", Priority = "3", Note = ""},
                                            new{ID = "SP004",Name = "Sản phẩm 4",Unit = "Hộp",Quantity = "20", ShortestTime = "25/08/2013", Time = "25/08/2013", Priority = "4", Note = "Dự trữ"},
                                            new{ID = "SP005",Name = "Sản phẩm 5",Unit = "Hộp",Quantity = "100", ShortestTime = "25/08/2013", Time = "25/08/2013", Priority = "5", Note = ""}};
                grdetail.KeyFieldName = "ID";
                grdetail.DataBind();
            }
        }
    }
}