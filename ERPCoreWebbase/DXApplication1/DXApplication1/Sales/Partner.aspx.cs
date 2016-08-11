using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;

//using DAL.Sale;
//using BLL.SalesBLO;

namespace WebModule.GUI.Sales
{
    public partial class Partner : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_PARTNER_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }
       

        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();

            //PartnerCategoryXDS.Session = session;
            //PartnerCategoryXDS.Criteria = "[RowStatus] <> 'D'";

            //PartnerXDS.Session = session;
            //PartnerXDS.Criteria = "[RowStatus] <> 'D'";

        }

        /// //////////////////////////////// PartnerCategory
        
   
        protected void grdPartnerCategory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {            
            grdPartnerCategory.CancelEdit();
            grdPartnerCategory.JSProperties.Add("cpPartnerCategoryEdit", "new");
        }

        protected void grdPartnerCategory_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdPartnerCategory.CancelEdit();
            grdPartnerCategory.JSProperties.Add("cpPartnerCategoryEdit", "edit");
        }
        

        protected void grdPartnerCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //PartnerCategoryBLO.deletePartnerCategory(Guid.Parse(e.Values["PartnerCategoryId"].ToString()), false);
            e.Cancel = true;
            grdPartnerCategory.JSProperties.Add("cpRefresh", "refresh");
        }

        /// //////////////////////////////// Partner

        protected void grdPartner_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdPartner.CancelEdit();
            grdPartner.JSProperties.Add("cpPartnerEdit", "new");
        }

        protected void grdPartner_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdPartner.CancelEdit();
            grdPartner.JSProperties.Add("cpPartnerEdit", "edit");
        }

        protected void grdPartner_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //PartnerBLO.deletePartner(Guid.Parse(e.Values["PartnerId"].ToString()), false);
            e.Cancel = true;
            grdPartner.JSProperties.Add("cpRefresh", "refresh");
        }

     


      
    }
}