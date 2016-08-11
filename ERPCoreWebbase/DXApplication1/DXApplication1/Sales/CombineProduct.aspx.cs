using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;

using DevExpress.Xpo;

//using DAL.Purchasing;
//using BLL.PurchasingBLO;
//using BLL.SalesBLO;

namespace WebModule.Sales
{
    public partial class CombineProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_SALES_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_SALES_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
            
            //ProductUnitXDS.Session = session;
            //ProductUnitXDS.Criteria = "[RowStatus] <> 'D'";

            //ProductCategoryXDS.Session = session;
            //ProductCategoryXDS.Criteria = "[RowStatus] <> 'D'";

            //ProductXDS.Session = session;
            //ProductXDS.Criteria = "[RowStatus] <> 'D'";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {                        
        }


       // ProductUnit

        protected void grdDataUnit_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdUnit.CancelEdit();
            grdUnit.JSProperties.Add("cpUnitEdit", "new");
        }

        protected void grdDataUnit_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //ProductUnitBLO.deleteProductUnit(Guid.Parse(e.Values["ProductUnitId"].ToString()));
            e.Cancel = true;

            grdUnit.JSProperties.Add("cpRefresh", "refresh");
        }

        protected void grdDataUnit_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdUnit.CancelEdit();

            grdUnit.JSProperties.Add("cpUnitEdit", "edit");
        }


        // ProductCategory

        protected void grdProductCategory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
          
            grdProductCategory.CancelEdit();
            grdProductCategory.JSProperties.Add("cpEditProductGroup", "new");
        }

        protected void grdProductCategory_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;

            grdProductCategory.CancelEdit();
            grdProductCategory.JSProperties.Add("cpEditProductGroup", "edit");
        }

        protected void grdProductCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //SalingProductCategoryBLO.deleteSalingProductCategory(Guid.Parse(e.Values["SalingProductCategoryId"].ToString()), false);           
            e.Cancel = true;
            
            grdProductCategory.JSProperties.Add("cpRefresh", "refresh");

        }

        // Product

        protected void grdProduct_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdProduct.CancelEdit();

            grdProduct.JSProperties.Add("cpProductEdit", "edit");
        }

        protected void grdProduct_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdProduct.CancelEdit();
            grdProduct.JSProperties.Add("cpProductEdit", "new");
        }

        protected void grdProduct_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //BLL.SalesBLO.ProductBLO.deleteProduct(Guid.Parse(e.Values["ProductId"].ToString()));

            //e.Cancel = true;
            //grdProduct.JSProperties.Add("cpRefresh", "refresh");
        }

       
      

      

       
    }
}