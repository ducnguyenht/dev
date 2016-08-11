using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using NAS.BO.Accounting;
using DevExpress.Data.Filtering;

namespace WebModule.Accounting
{
    public partial class TaxTypeSetting : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            TaxTypeSettingXDS.Session = session;
            TaxSettingXDS.Session = session;
            grdTaxTypeSetting.DetailRows.CollapseAllRows();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string AccessObjectId
        {
            get
            {
                return Utility.Constant.ACCESSOBJECT_ACCOUNT_TAXTYPESETTING_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void grdTaxTypeSetting_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            string a = grid.GetMasterRowKeyValue().ToString();
            Session["TaxTypeId_Tax"] = a.ToString();
        }


        ///////
        protected void grdTaxSetting_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            TaxBO bo = new TaxBO();
            if (bo.checkIsDupplicateTaxCode(session, e.NewValues["Code"].ToString()))
                throw new Exception(string.Format("Mã '{0}' đã tồn tại. Xin vui lòng nhập mã khác", e.NewValues["Code"].ToString()));

            ASPxGridView grid = sender as ASPxGridView;
            e.NewValues["TaxTypeId!Key"] = grid.GetMasterRowKeyValue();
        }

        protected void grdTaxSetting_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            TaxBO bo = new TaxBO();
            if (!e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
                if (bo.checkIsDupplicateTaxCode(session, e.NewValues["Code"].ToString()))
                    throw new Exception(string.Format("Mã '{0}' đã tồn tại. Xin vui lòng nhập mã khác", e.NewValues["Code"].ToString()));
        }

        protected void grdTaxSetting_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            TaxBO bo = new TaxBO();
            string itemCode = string.Empty;
            if (bo.checkTaxIsExistInItemTax(session, e.Values["Code"].ToString(), out itemCode))
                throw new Exception(string.Format("Mã '{0}' đã được sử dụng trong cấu hình thuế của mã hàng hóa '{1}'", e.Values["Code"].ToString(), itemCode));
            e.Cancel = true;
            NAS.DAL.Accounting.Tax.Tax tax = session.FindObject<NAS.DAL.Accounting.Tax.Tax>(new BinaryOperator("Code", e.Values["Code"].ToString().Trim(), BinaryOperatorType.Equal));
            if (tax != null)
            {
                tax.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                tax.Save();
            }
        }
        
        ///////
        protected void grdTaxTypeSetting_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            TaxBO bo = new TaxBO();
            if (bo.checkIsDupplicateTaxTypeCode(session, e.NewValues["Code"].ToString()))
                throw new Exception(string.Format("Mã '{0}' đã tồn tại. Xin vui lòng nhập mã khác", e.NewValues["Code"].ToString()));
        }

        protected void grdTaxTypeSetting_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            TaxBO bo = new TaxBO();
            if (!e.NewValues["Code"].ToString().Equals(e.OldValues["Code"].ToString()))
            {
                if (bo.checkIsDupplicateTaxTypeCode(session, e.NewValues["Code"].ToString()))
                    throw new Exception(string.Format("Mã '{0}' đã tồn tại. Xin vui lòng nhập mã khác", e.NewValues["Code"].ToString()));

                string pricePolicy = string.Empty;
                if (bo.checkTaxTypeIsExistInPricePolicy(session, e.OldValues["Code"].ToString(), out pricePolicy))
                    throw new Exception(string.Format("Mã '{0}' đã được sử dụng trong cấu hình mã chính sách giá '{1}' nên không thể sửa", 
                        e.OldValues["Code"].ToString(), pricePolicy));
            }
        }

        protected void grdTaxTypeSetting_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            
        }

        protected void grdTaxTypeSetting_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            TaxBO bo = new TaxBO();
            string pricePolicy = string.Empty;
            if (bo.checkTaxTypeIsExistInPricePolicy(session, e.Values["Code"].ToString(), out pricePolicy))
                throw new Exception(string.Format("Mã '{0}' đã được sử dụng trong cấu hình mã chính sách giá '{1}' nên không thể xóa",
                    e.Values["Code"].ToString(), pricePolicy));
            e.Cancel = true;
            NAS.DAL.Invoice.TaxType tt = session.FindObject<NAS.DAL.Invoice.TaxType>(new BinaryOperator("Code", e.Values["Code"].ToString().Trim(), BinaryOperatorType.Equal));
            if (tt != null)
            {
                tt.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                tt.Save();
            }
        }

        protected void grdTaxTypeSetting_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit
                || e.ButtonType == ColumnCommandButtonType.Delete)
            {
                bool isInternal = (bool)grdTaxTypeSetting.GetRowValues(e.VisibleIndex, "IsInternal");
                if (isInternal)
                {
                    e.Visible = false;
                }
            }
        }
    }
}