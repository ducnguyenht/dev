using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Produce.Config.UserControl
{
    public partial class uMaterialEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdataUserMaterialGroup.DataSource = new[] { new { materialcategorycode=1, MaterialGroupID = "NNVL001", MaterialGroup = " Nhóm Nguyên Vật Liệu 1",MaterialGroupDescription = "Example 1", MaterialGroupNote = "Example 1"},
                                              new { materialcategorycode=2,MaterialGroupID = "NNVL002", MaterialGroup = "Nhóm Nguyên Vật Liệu 2",MaterialGroupDescription = "Example 2", MaterialGroupNote = "Example 2"},
                                              new { materialcategorycode=3,MaterialGroupID = "NNVL003", MaterialGroup = "Nhóm Nguyên Vật Liệu 3",MaterialGroupDescription = "Example 3", MaterialGroupNote = "Example 3"},
                                                new { materialcategorycode=4,MaterialGroupID = "NNVL004", MaterialGroup = "Nhóm Nguyên Vật Liệu 4",MaterialGroupDescription = "Example 4", MaterialGroupNote = "Example 4"}
            };
            //grdataUserMaterialGroup.DataBind();
        }

        protected void cbpanelUserMaterial_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            
        }
    }
}