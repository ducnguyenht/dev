using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.UserControl
{
    public partial class uPlanningDetail : System.Web.UI.UserControl
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{ID = "SP001",Name = "Sản phẩm 1", No = "1", Quantity = "10",Unit = "Hộp", Priority = "1"},
                                                new{ID = "SP002",Name = "Sản phẩm 2", No = "1", Quantity = "20",Unit = "Hộp", Priority = "2"},
                                                new{ID = "SP003",Name = "Sản phẩm 3", No = "1", Quantity = "15",Unit = "Thùng", Priority = "3"}};
            ASPxGridView1.KeyFieldName = "ID";
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail)
            {
                ASPxGridView grdetail = ASPxGridView1.FindDetailRowTemplateControl(e.VisibleIndex, "GridDetail1") as ASPxGridView;
                grdetail.DataSource = new[] {   new{ID = "1",No = "1",Quantity = "10"},
                                                new{ID = "2",No = "1",Quantity = "20"},
                                                new{ID = "3",No = "1",Quantity = "10"},};
                grdetail.KeyFieldName = "ID";
                grdetail.DataBind();
            }
            
        }

        protected void GridDetail1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {            
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail)
            {
                ASPxGridView grd = sender as ASPxGridView;
                ASPxGridView grdetail = grd.FindDetailRowTemplateControl(e.VisibleIndex, "GridDetail2") as ASPxGridView;
                grdetail.DataSource = new[] {   new{ID = "CD001",Name = "Công đoạn 1",Time = "8",Type = "Giờ",Start_h = "8h30",Start_d ="09/08/2013",End_h ="16h30", End_d ="09/08/2013"},
                                                new{ID = "CD002",Name = "Công đoạn 2",Time = "6",Type = "Giờ",Start_h = "18h30",Start_d ="09/08/2013",End_h ="24h30", End_d ="09/08/2013"},
                                                new{ID = "CD003",Name = "Công đoạn 3",Time = "7",Type = "Giờ",Start_h = "8h30",Start_d ="10/08/2013",End_h ="15h30", End_d ="10/08/2013"},};
                grdetail.KeyFieldName = "ID";
                grdetail.DataBind();               
            }
        }
    }
}