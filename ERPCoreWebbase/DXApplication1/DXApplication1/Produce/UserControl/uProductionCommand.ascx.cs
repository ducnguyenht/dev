using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.UserControl
{
    public partial class uProductionCommand : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GrdCommand.DataSource = new[] { new{CID = "CM001",ID = "001", Name = "Công đoạn 1", Date = "09/08/2013"},
                                            new{CID = "CM002",ID = "002", Name = "Công đoạn 2", Date = "09/08/2013"},
                                            new{CID = "CM003",ID = "003", Name = "Công đoạn 3", Date = "09/08/2013"}};
            GrdCommand.KeyFieldName = "CID";
            GrdCommand.DataBind();

            GridCD.DataSource = new[] { new{ID = "CD001", Name = "Công đoạn 1", Date = "09/08/2013", PStatus = "Chưa thực thi"},
                                        new{ID = "CD002", Name = "Công đoạn 2", Date = "09/08/2013", PStatus = "Chưa thực thi"},
                                        new{ID = "CD003", Name = "Công đoạn 3", Date = "09/08/2013", PStatus = "Chưa thực thi"}};
            GridCD.KeyFieldName = "ID";
            GridCD.DataBind();

            Gridcm_detail.DataSource = new[] {   new{ID = "1",Start = "8h30",End = "10h30", Name = "Sản phẩm 1", No = "001", Quantity = "20", Unit = "Hộp"},
                                            new{ID = "2",Start = "10h30",End = "12h30", Name = "Sản phẩm 2", No = "002", Quantity = "50", Unit = "Gói"},
                                            new{ID = "3",Start = "12h30",End = "15h30", Name = "Sản phẩm 3", No = "003", Quantity = "50", Unit = "Hộp"},
                                            new{ID = "4",Start = "15h30",End = "18h30", Name = "Sản phẩm 4", No = "004", Quantity = "50", Unit = "Kg"}};
            Gridcm_detail.KeyFieldName = "ID";
            Gridcm_detail.DataBind();
        }

        protected void GrdCommand_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            //ASPxGridView gr = sender as ASPxGridView;
            //if (e.Expanded == true)
            //{
            //    ASPxGridView grdt1 = gr.FindDetailRowTemplateControl(e.VisibleIndex, "GridDetail_1") as ASPxGridView;
            //    grdt1.DataSource = new[] {   new{stt = "1",ID= "1",Start = "8h30",End = "10h30", Name = "Sản phẩm 1", No = "001", Quantity = "20", Unit = "Hộp"},
            //                                new{stt = "2",ID= "2",Start = "12h30",End = "15h30", Name = "Sản phẩm 3", No = "004", Quantity = "50", Unit = "Hộp"}};
            //  //  grdt1.KeyFieldName="stt";
            //    grdt1.DataBind();
            //}
        }

        protected void GridCD_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            //ASPxGridView gr = sender as ASPxGridView;
            //if (e.Expanded.ToString().Equals("True"))
            //{
            //    ASPxGridView grdt = gr.FindDetailRowTemplateControl(e.VisibleIndex, "GridCD_detail") as ASPxGridView;
            //    grdt.DataSource = new[] {   new{ID = "1",Start = "8h30",End = "10h30", Name = "Sản phẩm 1", No = "001", Quantity = "20", Unit = "Hộp"},
            //                                new{ID = "2",Start = "10h30",End = "12h30", Name = "Sản phẩm 2", No = "002", Quantity = "50", Unit = "Gói"},
            //                                new{ID = "3",Start = "12h30",End = "15h30", Name = "Sản phẩm 3", No = "003", Quantity = "50", Unit = "Hộp"},
            //                                new{ID = "4",Start = "15h30",End = "18h30", Name = "Sản phẩm 4", No = "004", Quantity = "50", Unit = "Kg"}};
            //    grdt.KeyFieldName = "ID";
            //    grdt.DataBind();
            //}
        }

        protected void GridDetail_1_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);
                detailView.DataSource = new[] {   new{stt = "1",ID= "1",Start = "8h30",End = "10h30", Name = "Sản phẩm 1", No = "001", Quantity = "20", Unit = "Hộp"},
                                            new{stt = "2",ID= "2",Start = "12h30",End = "15h30", Name = "Sản phẩm 3", No = "004", Quantity = "50", Unit = "Hộp"}};
                detailView.KeyFieldName = "stt";
            }
            catch (Exception) { }
        }

        void detailView_Load(object sender, EventArgs e)
        {
            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.DataBind();
        }

        protected void GridCD_detail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);
                detailView.DataSource = new[] {   new{ID = "1",Start = "8h30",End = "10h30", Name = "Sản phẩm 1", No = "001", Quantity = "20", Unit = "Hộp"},
                                            new{ID = "2",Start = "10h30",End = "12h30", Name = "Sản phẩm 2", No = "002", Quantity = "50", Unit = "Gói"},
                                            new{ID = "3",Start = "12h30",End = "15h30", Name = "Sản phẩm 3", No = "003", Quantity = "50", Unit = "Hộp"},
                                            new{ID = "4",Start = "15h30",End = "18h30", Name = "Sản phẩm 4", No = "004", Quantity = "50", Unit = "Kg"}};
                detailView.KeyFieldName = "ID";
            }
            catch (Exception) { }
        } 
    }
}