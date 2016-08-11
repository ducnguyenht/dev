using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.Config.UserControl
{
    public partial class uProductEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            treelistFinishedProductUnit.DataSource = new[] {   new { OrganizationId=1, ParentOrganizationId=0, FinishedProductUnitID="MS001", FinishedProductUnit="Thùng lớn",FinishedProductUnitAmount="",FinishedProductUnitDescription="Thùng lớn là đơn vị tính cao nhất"  },
                new { OrganizationId=2, ParentOrganizationId=1, FinishedProductUnitID="LT002",FinishedProductUnit="Hợp lớn",  FinishedProductUnitAmount="20", FinishedProductUnitDescription="1 Thùng lớn chứa 20 Hộp lớn" },
                new { OrganizationId=3, ParentOrganizationId=2, FinishedProductUnitID="LT003",FinishedProductUnit="Vĩ", FinishedProductUnitAmount="30", FinishedProductUnitDescription="1 Hộp lớn chứa 30 Vĩ"},
                new { OrganizationId=4, ParentOrganizationId=3, FinishedProductUnitID="LT004",FinishedProductUnit="Viên nén tròn",FinishedProductUnitAmount="12", FinishedProductUnitDescription="1 Vĩ chứa 12 Viên nén tròn"  }
            };
            treelistFinishedProductUnit.DataBind();

            grdatauFinishedProductGroup.DataSource = new[] { new { FinishedProductGroupID = "MH001", FinishedProductGroup = "Hàng Hóa 1", FinishedProductGroupDescription = "Example", FinishedProductGroupNote ="Example"},
                 new { FinishedProductGroupID = "MH002", FinishedProductGroup = "Hàng Hóa 2", FinishedProductGroupDescription = "Example", FinishedProductGroupNote ="Example"},
                  new { FinishedProductGroupID = "MH003", FinishedProductGroup = "Hàng Hóa 3", FinishedProductGroupDescription = "Example", FinishedProductGroupNote ="Example"},
                   new { FinishedProductGroupID = "MH004", FinishedProductGroup = "Hàng Hóa 4", FinishedProductGroupDescription = "Example", FinishedProductGroupNote ="Example"}
            };
            grdatauFinishedProductGroup.DataBind();

            ASPxGridView3.DataSource = new[] { new { ID = "DV001", Name = "Viên nén tròn" }, new { ID = "DV002", Name = "Gói" } };
            ASPxGridView3.KeyFieldName = "ID";
            ASPxGridView3.DataBind();
        }

        protected void cbpanelUserFinishedProduct_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        protected void ASPxGridView3_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewDetailRowEventArgs e)
        {
            if(e.Expanded.ToString().Equals("True"))
            {
                ASPxGridView gr = sender as ASPxGridView;
                ASPxGridView grdt = gr.FindDetailRowTemplateControl(e.VisibleIndex,"ASPxGridView2") as ASPxGridView;
                grdt.DataSource = new[]{    new{ ID = "NL001", Tp = "Nguyên liệu A",      bg = "10", dg = "Bao gồm 10 nguyên liệu A"} ,
                                            new{ ID = "NL002", Tp = "Sản phẩm dở dang A", bg = "2", dg = "Bao gồm 2 sản phẩm dở dang A"},
                                            new{ ID = "NL002", Tp = "Sản phẩm dở dang B", bg = "3", dg = "Bao gồm 3 sản phẩm dở dang B"}};
                grdt.KeyFieldName = "ID";
                grdt.DataBind();
                    
            }
        }

    }
}