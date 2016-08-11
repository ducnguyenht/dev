using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
//using BLL.PurchasingBLO;

namespace WebModule.Purchasing
{
    public partial class CombineMaterial : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_PRODUCT_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //MaterialBLO materialBLO = new MaterialBLO();
            //grdDataMaterial.DataSource = materialBLO.getMaterialList();
            //grdDataMaterial.DataBind();

            //MaterialCategoryBLO categoryBLO = new MaterialCategoryBLO();
            //grdDataMaterialCategory.DataSource = categoryBLO.getMaterialCategoryList();
            //grdDataMaterialCategory.DataBind();

            //MaterialUnitBLO unitBLO = new MaterialUnitBLO();
            //grdDataMaterialUnit.DataSource = unitBLO.getMaterialUnitList();
            //grdDataMaterialUnit.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

    }
}