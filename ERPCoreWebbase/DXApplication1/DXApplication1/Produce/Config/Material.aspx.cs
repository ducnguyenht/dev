using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Produce.Config
{
    public partial class Material : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_MATERIAL_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grdataMaterial.DataSource = new[] { new { MaterialID = "NVL001", Material = "Nguyên Vật Liệu 1",MaterialManufacturer = "Nhà Sản Xuất 1", MaterialRowStatus = "Sử Dụng", MaterialDescription = "Example 1"},
                                              new { MaterialID = "NVL002", Material = "Nguyên Vật Liệu 2",MaterialManufacturer = "Nhà Sản Xuất 2", MaterialRowStatus = "Sử Dụng", MaterialDescription = "Example 2"},
                                              new { MaterialID = "NVL003", Material = "Nguyên Vật Liệu 3",MaterialManufacturer = "Nhà Sản Xuất 3", MaterialRowStatus = "Sử Dụng", MaterialDescription = "Example 3"},
                                                new { MaterialID = "NVL004", Material = "Nguyên Vật Liệu 4",MaterialManufacturer = "Nhà Sản Xuất 4", MaterialRowStatus = "Sử Dụng", MaterialDescription = "Example 4"}
            };
            grdataMaterial.DataBind();

            //MaterialCategoryBindData
            grdataMaterialCategory.DataSource = new[] { new { MaterialCategoryID = "NNVL001", MaterialCategoryName = "Nhóm Nguyên Vật Liệu 1", MaterialCategoryRowStatus = "Sử Dụng", MaterialCategoryDescription = "Example 1"},
                                              new { MaterialCategoryID = "NNVL002", MaterialCategoryName = "Nhóm Nguyên Vật Liệu 2", MaterialCategoryRowStatus = "Sử Dụng", MaterialCategoryDescription = "Example 2"},
                                              new { MaterialCategoryID = "NNVL003", MaterialCategoryName = "Nhóm Nguyên Vật Liệu 3", MaterialCategoryRowStatus = "Sử Dụng", MaterialCategoryDescription = "Example 3"},
                                                new { MaterialCategoryID = "NNVL004", MaterialCategoryName = "Nhóm Nguyên Vật Liệu 4", MaterialCategoryRowStatus = "Sử Dụng", MaterialCategoryDescription = "Example 4"}
            };
            grdataMaterialCategory.DataBind();

            //MaterialUnit

            grdataMaterialUnit.DataSource = new[] { new { MaterialUnitID = "DV001", MaterialUnitName = "Đơn Vị Tính 1", MaterialUnitRowStatus = "Sử Dụng", MaterialUnitDescription = "Example 1"},
                                              new { MaterialUnitID = "DV002", MaterialUnitName = "Đơn Vị Tính  2", MaterialUnitRowStatus = "Sử Dụng", MaterialUnitDescription = "Example 2"},
                                              new { MaterialUnitID = "DV003", MaterialUnitName = "Đơn Vị Tính  3", MaterialUnitRowStatus = "Sử Dụng", MaterialUnitDescription = "Example 3"},
                                                new { MaterialUnitID = "DV004", MaterialUnitName = "Đơn Vị Tính  4", MaterialUnitRowStatus = "Sử Dụng", MaterialUnitDescription = "Example 4"}
            };
            grdataMaterialUnit.DataBind();

        }
        //Material
        protected void grdataMaterial_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdataMaterial.CancelEdit();
            grdataMaterial.JSProperties.Add("cpEditMaterial", "new");
        }


        //MaterialCategory
        protected void grdataMaterialCategory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdataMaterialCategory.CancelEdit();
            grdataMaterialCategory.JSProperties.Add("cpEditMaterialCategory", "new");
        }

        //MaterialCategory

        //MaterialUnit
        protected void grdataMaterialUnit_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdataMaterialUnit.CancelEdit();
            grdataMaterialUnit.JSProperties.Add("cpEditMaterialUnit", "new");
        }
        //MaterialUnit

    }
}