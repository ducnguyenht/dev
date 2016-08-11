using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Produce.Config
{
    public partial class FinishedProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_FINISHEDPRODUCT_ID;
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
            grdataFinishedProduct.DataSource = new[] { new { FinishedProductID = "SP001", FinishedProduct = "Thành Phẩm 1", RowStatus = "Sử Dụng"},
                                               new { FinishedProductID = "SP002", FinishedProduct = "Thành Phẩm  2", RowStatus = "Tạm Ngưng"},
                                                new { FinishedProductID = "SP003", FinishedProduct = "Thành Phẩm  3", RowStatus = "Sử Dụng"},
                                                 new { FinishedProductID = "SP004", FinishedProduct = "Thành Phẩm  4", RowStatus = "Sử Dụng"}
            };
            grdataFinishedProduct.DataBind();

            grdataFinishedProductGroup.DataSource = new[] { new { FinishedProductGroupID = "MH001", FinishedProductGroup = "Hàng Hóa 1", FinishedProductGroupRowStatus = "Sử Dụng",FinishedProductGroupDescription = "Example"},
                 new { FinishedProductGroupID = "MH002", FinishedProductGroup = "Hàng Hóa 2", FinishedProductGroupRowStatus = "Sử Dụng", FinishedProductGroupDescription = "Example"},
                  new { FinishedProductGroupID = "MH003", FinishedProductGroup = "Hàng Hóa 3", FinishedProductGroupRowStatus = "Sử Dụng", FinishedProductGroupDescription = "Example"},
                   new { FinishedProductGroupID = "MH004", FinishedProductGroup = "Hàng Hóa 4", FinishedProductGroupRowStatus = "Sử Dụng", FinishedProductGroupDescription = "Example"}
            };
            grdataFinishedProductGroup.DataBind();

            grdataFinishedProductUnit.DataSource = new[] { new { FinishedProductUnitID = "MH001", FinishedProductUnit = "Hàng Hóa 1", FinishedProductUnitRowStatus = "Sử Dụng",FinishedProductUnitDescription = "Example"},
                 new { FinishedProductUnitID = "MH002", FinishedProductUnit = "Hàng Hóa 2", FinishedProductUnitRowStatus = "Sử Dụng", FinishedProductUnitDescription = "Example"},
                  new { FinishedProductUnitID = "MH003", FinishedProductUnit = "Hàng Hóa 3", FinishedProductUnitRowStatus = "Sử Dụng", FinishedProductUnitDescription = "Example"},
                   new { FinishedProductUnitID = "MH004", FinishedProductUnit = "Hàng Hóa 4", FinishedProductUnitRowStatus = "Sử Dụng", FinishedProductUnitDescription = "Example"}
            };
            grdataFinishedProductUnit.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Bind_data();
        }
        //FinishedProduct
        protected void grdataFinishedProduct_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            
            grdataFinishedProduct.CancelEdit();
            grdataFinishedProduct.JSProperties.Add("cpEditFinishedProduct", "new");
        }

        protected void grdataFinishedProduct_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdataFinishedProduct.CancelEdit();
            grdataFinishedProduct.JSProperties.Add("cpEditFinishedProduct", "edit");
        }


        //FinishedProductGroup
        protected void grdataFinishedProductGroup_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
         
            grdataFinishedProductGroup.CancelEdit();
            grdataFinishedProductGroup.JSProperties.Add("cpEditFinishedProductGroup", "new");
        }

        protected void grdataFinishedProductGroup_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdataFinishedProductGroup.CancelEdit();
            grdataFinishedProductGroup.JSProperties.Add("cpEditFinishedProductGroup", "edit");
        }

        //FinishedProductGroup

        //FinisherProductUnit
        protected void grdataFinishedProductUnit_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            grdataFinishedProductUnit.CancelEdit();
            grdataFinishedProductUnit.JSProperties.Add("cpEditFinishedProductUnit", "new");
        }

        protected void grdataFinishedProductUnit_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdataFinishedProductUnit.CancelEdit();
            grdataFinishedProductUnit.JSProperties.Add("cpEditFinishedProductUnit", "edit");
        }

        //FinisherProductUnit
       
    }
}