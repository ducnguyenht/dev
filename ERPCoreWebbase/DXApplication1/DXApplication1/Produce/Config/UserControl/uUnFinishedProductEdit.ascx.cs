using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Produce.Config.UserControl
{
    public partial class uUnFinishedProductEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            treelistUnFinishedProductUnit.DataSource = new[] {   new { OrganizationId=1, ParentOrganizationId=0, UnFinishedProductUnitID="MS001", UnFinishedProductUnit="Thùng lớn", UnFinishedProductUnitAmount="", UnFinishedProductUnitDescription="Thùng lớn là đơn vị tính cao nhất"  },
                new { OrganizationId=2, ParentOrganizationId=1, UnFinishedProductUnitID="LT002", UnFinishedProductUnit="Hợp lớn",  UnFinishedProductUnitAmount="20", UnFinishedProductUnitDescription="1 Thùng lớn chứa 20 Hộp lớn" },
                new { OrganizationId=3, ParentOrganizationId=2, UnFinishedProductUnitID="LT003", UnFinishedProductUnit="Vĩ", UnFinishedProductUnitAmount="30", UnFinishedProductUnitDescription="1 Hộp lớn chứa 30 Vĩ"},
                new { OrganizationId=4, ParentOrganizationId=3, UnFinishedProductUnitID="LT004", UnFinishedProductUnit="Viên nén tròn", UnFinishedProductUnitAmount="12", UnFinishedProductUnitDescription="1 Vĩ chứa 12 Viên nén tròn"  }
            };
            treelistUnFinishedProductUnit.DataBind();

            grdatauUnFinishedProductGroup.DataSource = new[] { new { UnFinishedProductGroupID = "MH001", UnFinishedProductGroup = "Hàng Hóa 1", UnFinishedProductGroupDescription = "Example", UnFinishedProductGroupNote ="Example"},
                 new { UnFinishedProductGroupID = "MH002", UnFinishedProductGroup = "Hàng Hóa 2", UnFinishedProductGroupDescription = "Example", UnFinishedProductGroupNote ="Example"},
                  new { UnFinishedProductGroupID = "MH003", UnFinishedProductGroup = "Hàng Hóa 3", UnFinishedProductGroupDescription = "Example", UnFinishedProductGroupNote ="Example"},
                   new { UnFinishedProductGroupID = "MH004", UnFinishedProductGroup = "Hàng Hóa 4", UnFinishedProductGroupDescription = "Example", UnFinishedProductGroupNote ="Example"}
            };
            grdatauUnFinishedProductGroup.DataBind();
        }

        protected void cbpanelUserUnFinishedProduct_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
    }
}