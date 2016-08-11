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
//using DAL.Authorization;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
using DevExpress.Xpo.Metadata;
using NAS.DAL;
using NAS.BO.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule.Purchasing
{
    public partial class CombineSupplier : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase 
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

        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsSupplier.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        //2013-11-03 ERP-872 Khoa.Truong INS START
        private void SetCriteriaForOrganization()
        {

            //Get SUPPLIER trading type
            TradingCategory supplierTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "SUPPLIER").FirstOrDefault();

            CriteriaOperator criteria = CriteriaOperator.And(
                new ContainsOperator("OrganizationCategories",
                    CriteriaOperator.And(
                        new BinaryOperator("TradingCategoryId.TradingCategoryId", supplierTradingCategory.TradingCategoryId),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                    )
                ),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );

            dsSupplier.Criteria = criteria.ToString();
            grdSupplier.DataBind();

        }
        //2013-11-03 ERP-872 Khoa.Truong INS END

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCriteriaForOrganization();
            if (!IsPostBack)
            {
                pagSupplier.ActiveTabIndex = 0;
            }
        }
        
        protected void grdSupplier_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            Guid recordId = Guid.Parse(args[1]);
                            SupplierOrgBO.DeleteLogical(recordId);
                            grdSupplier.JSProperties.Add("cpEvent", "deleted");
                        }
                        else
                        {
                            throw new Exception("Must be pass id of the record");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                default:
                    break;
            }
        }

        protected void grdSupplier_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("RowStatus"))
            {
                if (e.Value.Equals(Constant.ROWSTATUS_ACTIVE))
                {
                    e.DisplayText = "Hoạt động";
                }
                else if (e.Value.Equals(Constant.ROWSTATUS_INACTIVE))
                {
                    e.DisplayText = "Ngừng hoạt động";
                }
            }
        }
        ////SupplierGroup
    }
}