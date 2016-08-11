using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
//using DAL.Sale;
using DevExpress.Web.ASPxGridView;
using NAS.DAL;
using WebModule.Sales;
using NAS.BO.Nomenclature.Organization;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
//using DAL.SaleCode;
//using BLL.SalesBLO;

namespace WebModule.GUI.Sales
{
    public partial class Customer : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_CUSTOMER_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }

        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsCustomer.Session = session;
        }

        //2013-11-03 ERP-872 Khoa.Truong INS START
        private void SetCriteriaForOrganization()
        {
            //Get CUSTOMER trading type
            TradingCategory customerTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "CUSTOMER").FirstOrDefault();

            CriteriaOperator criteria = CriteriaOperator.And(
                new ContainsOperator("OrganizationCategories",
                    CriteriaOperator.And(
                        new BinaryOperator("TradingCategoryId.TradingCategoryId", customerTradingCategory.TradingCategoryId),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                    )
                ),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );

            dsCustomer.Criteria = criteria.ToString();
            grdCustomer.DataBind();

        }
        //2013-11-03 ERP-872 Khoa.Truong INS END

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagCustomer.ActiveTabIndex = 0;
            }
            SetCriteriaForOrganization();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdCustomer_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            switch (args[0])
            {
                case "delete":
                    try
                    {
                        if (args.Length > 1)
                        {
                            CustomerOrgBO bo = new CustomerOrgBO();
                            Guid recordId = Guid.Parse(args[1]);
                            if (bo.CheckIsExistedCustomerInBill(session, recordId))
                                throw new Exception("Khách hàng này đã có dữ liệu trong phiếu bán nên không thể xóa");

                            if (bo.CheckIsExistedCustomerInVouche(session, recordId))
                                throw new Exception("Khách hàng này đã có dữ liệu trong phiếu thu nên không thể xóa");

                            CustomerOrgBO.DeleteLogical(recordId);
                            grdCustomer.JSProperties.Add("cpEvent", "deleted");
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

        protected void grdCustomer_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
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

        

    }
}