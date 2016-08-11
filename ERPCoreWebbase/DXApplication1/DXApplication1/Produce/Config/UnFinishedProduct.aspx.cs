using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Produce.Config
{
    public partial class UnFinishedProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
    
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_UNFINISHEDPRODUCT_ID;
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

        void Bind_data()
        {
            grdataUnFinishedProduct.DataSource = new[] { new { UnFinishedProductID = "DD001", UnFinishedProduct = "Sản Phẩm Dở Dang 1", RowStatus = "Sử Dụng"},
                                               new { UnFinishedProductID = "DD002", UnFinishedProduct = "Sản Phẩm Dở Dang 2", RowStatus = "Tạm Ngưng"},
                                                new { UnFinishedProductID = "DD003", UnFinishedProduct = "Sản Phẩm Dở Dang 3", RowStatus = "Sử Dụng"},
                                                 new { UnFinishedProductID = "DD004", UnFinishedProduct = "Sản Phẩm Dở Dang 4", RowStatus = "Sử Dụng"}
            };
            grdataUnFinishedProduct.DataBind();

            grdataUnFinishedProductGroup.DataSource = new[] { new { UnFinishedProductGroupID = "MH001", UnFinishedProductGroup = "Hàng Hóa 1", UnFinishedProductGroupRowStatus = "Sử Dụng", UnFinishedProductGroupDescription = "Example"},
                 new { UnFinishedProductGroupID = "MH002", UnFinishedProductGroup = "Hàng Hóa 2", UnFinishedProductGroupRowStatus = "Sử Dụng", UnFinishedProductGroupDescription = "Example"},
                  new { UnFinishedProductGroupID = "MH003", UnFinishedProductGroup = "Hàng Hóa 3", UnFinishedProductGroupRowStatus = "Sử Dụng", UnFinishedProductGroupDescription = "Example"},
                   new { UnFinishedProductGroupID = "MH004", UnFinishedProductGroup = "Hàng Hóa 4", UnFinishedProductGroupRowStatus = "Sử Dụng", UnFinishedProductGroupDescription = "Example"}
            };
            grdataUnFinishedProductGroup.DataBind();

            grdataUnFinishedProductUnit.DataSource = new[] { new { UnFinishedProductUnitID = "MH001", UnFinishedProductUnit = "Hàng Hóa 1", UnFinishedProductUnitRowStatus = "Sử Dụng", UnFinishedProductUnitDescription = "Example"},
                 new { UnFinishedProductUnitID = "MH002", UnFinishedProductUnit = "Hàng Hóa 2", UnFinishedProductUnitRowStatus = "Sử Dụng", UnFinishedProductUnitDescription = "Example"},
                  new { UnFinishedProductUnitID = "MH003", UnFinishedProductUnit = "Hàng Hóa 3", UnFinishedProductUnitRowStatus = "Sử Dụng", UnFinishedProductUnitDescription = "Example"},
                   new { UnFinishedProductUnitID = "MH004", UnFinishedProductUnit = "Hàng Hóa 4", UnFinishedProductUnitRowStatus = "Sử Dụng", UnFinishedProductUnitDescription = "Example"}
            };
            grdataUnFinishedProductUnit.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Bind_data();
        }

        //UnFinishedProduct
        protected void grdataUnFinishedProduct_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            grdataUnFinishedProduct.CancelEdit();
            grdataUnFinishedProduct.JSProperties.Add("cpEditUnFinishedProduct", "new");
        }

        protected void grdataUnFinishedProduct_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdataUnFinishedProduct.CancelEdit();
            grdataUnFinishedProduct.JSProperties.Add("cpEditUnFinishedProduct", "edit");
        }


        //UnFinishedProductGroup
        protected void grdataUnFinishedProductGroup_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            grdataUnFinishedProductGroup.CancelEdit();
            grdataUnFinishedProductGroup.JSProperties.Add("cpEditUnFinishedProductGroup", "new");
        }

        protected void grdataUnFinishedProductGroup_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdataUnFinishedProductGroup.CancelEdit();
            grdataUnFinishedProductGroup.JSProperties.Add("cpEditFinishedProductGroup", "edit");
        }

        //UnFinishedProductGroup

        //UnFinisherProductUnit
        protected void grdataUnFinishedProductUnit_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            grdataUnFinishedProductUnit.CancelEdit();
            grdataUnFinishedProductUnit.JSProperties.Add("cpEditUnFinishedProductUnit", "new");
        }

        protected void grdataUnFinishedProductUnit_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdataUnFinishedProductUnit.CancelEdit();
            grdataUnFinishedProductUnit.JSProperties.Add("cpEditUnFinishedProductUnit", "edit");
        }

        //UnFinisherProductUnit
    }
}