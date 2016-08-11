using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;
using Utility;
using DevExpress.Web.ASPxTreeList;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule.Accounting.UserControl
{
    public partial class ucAccount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //AccountType accountType = session.GetObjectByKey<AccountType>(cbAccountType.Value);
            //CriteriaOperator criteria;
            //criteria = new BinaryOperator("AccountTypeId", accountType.AccountTypeId);
            //AccountXPO.Criteria = criteria.ToString();            
            ASPxTreeList1.DataSourceID = "AccountXPO";
            ASPxTreeList1.KeyFieldName = "AccountId";
            ASPxTreeList1.DataBind();
            //if (cbAccountType.Value != null)
            //{
            //    if (cbAccountType.Value.ToString() != "")
            //    {
            //        Guid guid = Guid.Parse(cbAccountType.Value.ToString());
            //        AccountType accountType = session.GetObjectByKey<AccountType>(guid);
            //        CriteriaOperator criteria = new BinaryOperator("AccountTypeId!Key", accountType.AccountTypeId);
            //        AccountXPO.Criteria = criteria.ToString();
            //        ASPxTreeList1.DataBind();
            //    }
            //}
        }

        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dSAccountType.Session = session;
            dSAccountCategory.Session = session;
            AccountXPO.Session = session;            
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
        }

        protected void grdAccountType_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void ASPxTreeList1_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //if (cbAccountType.Value == null)
            //{
            //    e.Cancel = true;
            //    (sender as ASPxTreeList).CancelEdit();
            //    throw new Exception("Chọn loại tài khoản trước khi tạo mới");
            //}
            //Guid guid = Guid.Parse(cbAccountType.Value.ToString());
            //AccountType accountType = session.GetObjectByKey<AccountType>(guid);
            //e.NewValues["AccountTypeId"] = accountType;
            e.NewValues["OrganizationId!Key"] = Utility.CurrentSession.Instance.AccessingOrganizationId.ToString();            
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
        }

        protected void ASPxTreeList1_NodeInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            
        }

        protected void cbAccountType_ValueChanged(object sender, EventArgs e)
        {
            
        }

        protected void ASPxTreeList1_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {            
            //Guid guid = Guid.Parse(cbAccountType.Value.ToString());
            //AccountType accountType = session.GetObjectByKey<AccountType>(guid);
            //CriteriaOperator criteria = new BinaryOperator("AccountTypeId!Key",accountType.AccountTypeId);
            //AccountXPO.Criteria = criteria.ToString();
            //ASPxTreeList1.DataBind();
        }

        protected void ASPxTreeList1_InitNewNode(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
                
        }

        protected internal void grdAccountType_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {            
            ASPxGridView grdAccType = (ASPxGridView)sender;
            string accCode = e.NewValues["Code"].ToString();
            string accName = e.NewValues["Name"].ToString();
            // New Row 
            if (grdAccType.IsNewRowEditing)
            {
                bool isExistCode = Util.isExistXpoObject<NAS.DAL.Accounting.AccountChart.AccountType>("Code", accCode,
                    Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExistCode)
                {
                    Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grdAccType.Columns["Code"], "Mã phân loại đã tồn tại.");
                }
                bool isExistName = Util.isExistXpoObject<NAS.DAL.Accounting.AccountChart.AccountType>("Name", accName,
                    Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExistName)
                {
                    Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grdAccType.Columns["Name"], "Tên phân loại đã tồn tại.");
                }                
            }
            // Edit Row
            else
            {
                if(grdAccType.IsEditing)
                {
                    string newCode = e.NewValues["Code"].ToString().Trim();
                    string oldCode = e.OldValues["Code"].ToString().Trim();
                    if (!newCode.Equals(oldCode))
                    {
                        bool isExistCode = Util.isExistXpoObject<NAS.DAL.Accounting.AccountChart.AccountType>("Code", accCode,
                        Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                        if (isExistCode)
                        {
                            Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grdAccType.Columns["Code"], "Mã phân loại đã tồn tại.");
                            return;
                        }    
                    }
                    string newName = e.NewValues["Name"].ToString().Trim();
                    string oldName = e.OldValues["Name"].ToString().Trim();
                    if (!newName.Equals(oldName))
                    {
                        bool isExistName = Util.isExistXpoObject<NAS.DAL.Accounting.AccountChart.AccountType>("Name", accName,
                        Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                        if (isExistName)
                        {
                            Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grdAccType.Columns["Name"], "Tên phân loại đã tồn tại.");
                            return;
                        }
                    }                    
                }
            }
        }

        protected void ASPxTreeList1_NodeValidating(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventArgs e)
        { 
            ASPxTreeList tree = (ASPxTreeList)sender;
            string accCode = e.NewValues["Code"].ToString();
            if (e.NewValues["AccountTypeId!Key"] == null)
            {
                Utility.Helpers.AddErrorToTreeListNode(e.Errors, "AccountTypeId!Key", "Chưa chọn loại tài khoản");                
            }
            // New Node
            if (tree.IsNewNodeEditing)
            {
                bool isExistCode = Util.isExistXpoObject<NAS.DAL.Accounting.AccountChart.Account>("Code", accCode,
                    Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExistCode)
                {
                    Utility.Helpers.AddErrorToTreeListNode(e.Errors, "Code", "Số tài khoản đã tồn tại.");
                    return;
                }
            }

            // Edit Node
            else
            {
                if (tree.IsEditing)
                {
                    string newCode = e.NewValues["Code"].ToString().Trim();
                    string oldCode = e.OldValues["Code"].ToString().Trim();
                    if (!newCode.Equals(oldCode))
                    {
                        bool isExistCode = Util.isExistXpoObject<NAS.DAL.Accounting.AccountChart.Account>("Code", accCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                        if (isExistCode)
                        {
                            Utility.Helpers.AddErrorToTreeListNode(e.Errors, "Code", "Số tài khoản đã tồn tại.");
                            return;
                        }
                    }
                }  
            }                      
        }

        protected void ASPxTreeList1_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void ASPxTreeList1_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            if (e.Column.Caption == "Loại Tài Khoản")
            {
                try
                {
                    e.Cell.Text = session.GetObjectByKey<AccountType>(Guid.Parse(e.CellValue.ToString())).Name;
                }
                catch (Exception)
                {
                    e.Cell.Text = "";
                }
            }
        }        
    }
}