using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridLookup;
using NAS.DAL.Accounting.Period;
using DevExpress.Data.Filtering;
using NAS.BO.Accounting.Journal;
using DevExpress.Web.ASPxCallbackPanel;
using Utility;

namespace WebModule.Accounting.UserControl
{
    public partial class ucAccountingPeriod : System.Web.UI.UserControl
    {
        Session session;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            AccountingPeriod.Session = session;
            XPOAccountingPeriodType.Session = session;
            XPOAccountingPeriodLookup.Session = session;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //AccountingPeriodBO bo = new AccountingPeriodBO();
            //AccountingPeriod test = AccountingPeriodBO.GetAccountingPeriod(session, DateTime.Parse("2013/10/10"));
            //AccountingPeriodType type = session.GetObjectByKey<AccountingPeriodType>(Guid.Parse("7ad4ca8c-898d-4850-89ce-86b4d70e50b8"));
            //if(type!=null)type.Delete();
        }

        protected void ASPxGridView1_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.VisibleRowIndex >= 0)
            {
                AccountingPeriod CurrentDataRow = ASPxGridView1.GetRow(e.VisibleRowIndex) as AccountingPeriod;
                if (e.Column.FieldName == "AccountingPeriodTypeId!Key")
                {                    
                    if (CurrentDataRow.AccountingPeriodTypeId == null) return;
                    e.DisplayText = CurrentDataRow.AccountingPeriodTypeId.Name;
                }
                if (e.Column.Caption == "Chu kì trực thuộc")
                {
                    string displayText = "";
                    CriteriaOperator criteria_0 = new BinaryOperator("AccountingPeriodId", CurrentDataRow, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                    XPCollection<AccountingPeriodComposite> collection = new XPCollection<AccountingPeriodComposite>(session, criteria);
                    foreach (AccountingPeriodComposite cp in collection)
                    {
                        displayText = displayText + cp.ChildrenAccountingPeriodId.Code+"; ";
                    }
                    e.DisplayText = displayText;
                }
            }
        }

        protected void ASPxGridView1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex >= 0)
            {
                AccountingPeriod CurrentDataRow = ASPxGridView1.GetRow(e.VisibleIndex) as AccountingPeriod;
                if (e.ButtonID == "btUnder")
                {
                    if (CurrentDataRow.AccountingPeriodTypeId == null) return;
                    if (CurrentDataRow.AccountingPeriodTypeId.IsDefault == true)
                    {
                        e.Enabled = false;
                    }
                }
            }
        }

        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                AccountingPeriod currentAP = session.GetObjectByKey<AccountingPeriod>((Guid)e.Keys[0]);
                if (Util.isExistXpoObject<AccountingPeriod>("Code", e.NewValues["Code"].ToString()))
                {
                    CriteriaOperator criteria0 = new BinaryOperator("Code", e.NewValues["Code"].ToString(), BinaryOperatorType.Equal);                    
                    CriteriaOperator criteria1 = new BinaryOperator("AccountingPeriodId", e.Keys[0], BinaryOperatorType.NotEqual);
                    CriteriaOperator criteria2 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator cr = new GroupOperator(GroupOperatorType.And,criteria0,criteria1,criteria2);
                    AccountingPeriod ap = session.FindObject<AccountingPeriod>(cr);
                    if (ap!=null)
                    {
                        e.Cancel = true;
                        throw (new Exception("Trùng mã chu kì"));
                        return;
                    }
                }
                ASPxGridLookup grid = ASPxGridView1.FindEditRowCellTemplateControl(ASPxGridView1.Columns[5] as GridViewDataColumn, "cp_Grid1").FindControl("GridUnderPeriod") as ASPxGridLookup;                
                CriteriaOperator criteria_0 = new BinaryOperator("AccountingPeriodId", currentAP, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                XPCollection<AccountingPeriodComposite> collection = new XPCollection<AccountingPeriodComposite>(session, criteria);
                foreach (AccountingPeriodComposite cp in collection)
                {
                    cp.RowStatus = Constant.ROWSTATUS_DELETED;
                    cp.Save();
                }
                List<object> selectedRows = grid.GridView.GetSelectedFieldValues("AccountingPeriodId");
                if (!currentAP.AccountingPeriodTypeId.IsDefault)
                {
                    foreach (var o in selectedRows)
                    {
                        AccountingPeriodBO.CreatAccountingPeriodComposite(session, currentAP.AccountingPeriodId, Guid.Parse(o.ToString()));
                    }
                }   
            }
            catch(Exception)
            {
                throw (new Exception("Trùng mã chu kì"));
            }
            
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["IsActive"] == null)
            {
                e.NewValues["IsActive"] = false;
            }            
            if (Util.isExistXpoObject<AccountingPeriod>("Code", (string)e.NewValues["Code"]))
            {
                e.Cancel = true;
                throw (new Exception("Trùng mã chu kì"));
                return;
            }
            if (e.NewValues["AccountingPeriodTypeId!Key"] == null)
            {
                e.Cancel = true;
                throw (new Exception("Chưa chọn thể loại"));
                return;
            }
            AccountingPeriod accountingPeriod = new AccountingPeriod(session);
            accountingPeriod.Description = (string)e.NewValues["Description"];
            accountingPeriod.Code = (string)e.NewValues["Code"];
            accountingPeriod.AccountingPeriodTypeId = session.GetObjectByKey<AccountingPeriodType>(Guid.Parse(e.NewValues["AccountingPeriodTypeId!Key"].ToString()));
            accountingPeriod.FromDateTime =(DateTime)e.NewValues["FromDateTime"];
            accountingPeriod.ToDateTime = (DateTime)e.NewValues["ToDateTime"];
            accountingPeriod.RowStatus = Constant.ROWSTATUS_ACTIVE;
            accountingPeriod.IsActive = (bool)e.NewValues["IsActive"];
            if (accountingPeriod.Code != null)
            {
                accountingPeriod.Save();
            }
            ASPxGridLookup grid = ASPxGridView1.FindEditRowCellTemplateControl(ASPxGridView1.Columns[5] as GridViewDataColumn, "cp_Grid1").FindControl("GridUnderPeriod") as ASPxGridLookup;
            List<object> selectedRows = grid.GridView.GetSelectedFieldValues("AccountingPeriodId");
            if (!accountingPeriod.AccountingPeriodTypeId.IsDefault)
            {
                foreach (var o in selectedRows)
                {
                    AccountingPeriodBO.CreatAccountingPeriodComposite(session, accountingPeriod.AccountingPeriodId, Guid.Parse(o.ToString()));
                }
            }
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }

        protected void ASPxGridView1_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {            
        }

        protected void ASPxGridView1_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
        }

        protected void ASPxGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
        }

        protected void ASPxGridView1_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            AccountingPeriod currentEdit = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(e.EditingKeyValue.ToString()));
            Guid accountingPeriodId = Guid.Parse(e.EditingKeyValue.ToString());
            
            if (AccountingPeriodBO.IsChildOfAnother(session, accountingPeriodId))
            {
                e.Cancel = true;
                (sender as ASPxGridView).CancelEdit();
                throw new Exception("Chu kì kế toán này đang là chu kì trực thuộc của một chu kì khác! Không thể chỉnh sửa");                
            }

            Session["AccountingPeriodId_cr"] = currentEdit.AccountingPeriodId;
            ASPxGridLookup grid = ASPxGridView1.FindEditRowCellTemplateControl(ASPxGridView1.Columns[5] as GridViewDataColumn, "cp_Grid1").FindControl("GridUnderPeriod") as ASPxGridLookup;
            if (currentEdit.AccountingPeriodTypeId.IsDefault == true)
            {
                XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [IsActive] <> True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";                
                grid.Enabled = false;
            }
            else
            {
                XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";
                grid.Enabled = true;
            }
        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            AccountingPeriodType type = session.GetObjectByKey<AccountingPeriodType>(Guid.Parse(e.Parameter.ToString()));
            if (type.IsDefault == true)
            {
                XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [IsActive] <> True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";
            }
            else
            {
                XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";
            }
        }

        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            //if (e.Column.Caption == "Thể loại")
            //{
            //    var a = e.Value;
            //    AccountingPeriodType type = session.GetObjectByKey<AccountingPeriodType>(Guid.Parse(e.Value.ToString()));
            //    if (type.IsDefault == true)
            //    {
            //        XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [IsActive] <> True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";                    
            //    }
            //    else
            //    {
            //        XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";                    
            //    }
            //}
        }

        protected void cp_Grid1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {            
            try
            {
                AccountingPeriodType type = session.GetObjectByKey<AccountingPeriodType>(Guid.Parse(e.Parameter.ToString()));
                ASPxCallbackPanel cp_Grid = sender as ASPxCallbackPanel;
                ASPxGridLookup grid = cp_Grid.FindControl("GridUnderPeriod") as ASPxGridLookup;

                if (type.IsDefault == true)
                {
                    XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [IsActive] <> True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";
                    grid.GridView.DataBind();
                }
                else
                {
                    XPOAccountingPeriodLookup.Criteria = "[RowStatus] > 0s And [IsActive] = True And [Code] <> 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True";
                    grid.GridView.DataBind();
                }
            }
            catch (Exception)
            {                
            }            
        }

        protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["RowStatus"] = Constant.ROWSTATUS_ACTIVE;
            if (e.NewValues["IsDefault"] == null) e.NewValues["IsDefault"] = false;
            if (Util.isExistXpoObject<AccountingPeriodType>("Name", (string)e.NewValues["Name"]))
            {
                e.Cancel = true;
                throw(new Exception("Trùng tên thể loại chu kì"));
            }
            if ((bool)e.NewValues["IsDefault"] == true)
            {
                CriteriaOperator criteria = new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal);
                AccountingPeriodType type = session.FindObject<AccountingPeriodType>(criteria);
                e.NewValues["RowStatus"] = Constant.ROWSTATUS_ACTIVE;
                if (type != null)
                {
                    if (AccountingPeriodTypeBO.IsUsedAccoutingPeriodType(session, type.AccountingPeriodTypeId))
                    {
                        ASPxGridView2.CancelEdit();
                        throw new Exception("Loại chu kì nhỏ nhất " + type.Name + " đang được sử dụng nên không thể thay đổi");
                    }
                    type.IsDefault = false;
                    type.Save();
                }
            }
        }

        protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (e.Values["IsDefault"].ToString() == "True")
            {
                e.Cancel = true;
                throw (new Exception("Không thể xóa thể loại mặc định"));
            }
        }

        protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (Util.isExistXpoObject<AccountingPeriodType>("Name", (string)e.NewValues["Name"]))
            {
                CriteriaOperator criteria_0 = new BinaryOperator("Name", (string)e.NewValues["Name"], BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_2 = new BinaryOperator("AccountingPeriodTypeId", e.Keys[0], BinaryOperatorType.NotEqual);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
                AccountingPeriodType type = session.FindObject<AccountingPeriodType>(criteria);
                if (type != null)
                {
                    e.Cancel = true;
                    throw (new Exception("Trùng tên thể loại chu kì"));
                }
            }
            if (e.NewValues["IsDefault"] == null) e.NewValues["IsDefault"] = false;
            if ((bool)e.NewValues["IsDefault"] == true)
            {
                CriteriaOperator criteria = new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal);
                AccountingPeriodType type = session.FindObject<AccountingPeriodType>(criteria);
                if (type != null)
                {
                    if (AccountingPeriodTypeBO.IsUsedAccoutingPeriodType(session, type.AccountingPeriodTypeId))
                    {
                        ASPxGridView2.CancelEdit();
                        throw new Exception("Loại chu kì nhỏ nhất " + type.Name + " đang được sử dụng nên không thể thay đổi");
                    }
                    type.IsDefault = false;
                    type.Save();
                }
            }
            else
            {
                if (e.OldValues["IsDefault"].ToString() == "True")
                {
                    e.Cancel = true;
                    ASPxGridView2.CancelEdit();
                    throw (new Exception("Phải luôn có 1 thể loại chu kì nhỏ nhất"));
                }
            }
        }

        protected void ASPxGridView2_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            (sender as ASPxGridView).DataBind();
        }

        protected void ASPxGridView2_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            ASPxGridView2.DataBind();
        }

        protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            CriteriaOperator criteria_0 = new BinaryOperator("RowStatus",Constant.ROWSTATUS_ACTIVE,BinaryOperatorType.GreaterOrEqual);
            CriteriaOperator criteria_1 = new BinaryOperator("IsDefault",true,BinaryOperatorType.Equal);
            CriteriaOperator criteria  = new GroupOperator(GroupOperatorType.And,criteria_0,criteria_1);
            AccountingPeriodType type = session.FindObject<AccountingPeriodType>(criteria);
            if(type != null){
                e.NewValues["AccountingPeriodTypeId!Key"] = type.AccountingPeriodTypeId;
            }
            e.NewValues["IsDefault"] = false;
        }

        protected void ASPxGridView2_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            Guid accountingPeriodTypeId = (Guid)e.EditingKeyValue;
            if (AccountingPeriodTypeBO.IsUsedAccoutingPeriodType(session, accountingPeriodTypeId))
            {
                ((ASPxGridView)sender).CancelEdit();
                throw new Exception("Loại chu kì này đang được sử dụng, không cho phép chỉnh sửa");
            }
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid accountingPeriodId = (Guid)e.Keys[0];
            if (AccountingPeriodBO.IsChildOfAnother(session, accountingPeriodId) == true)
            {
                e.Cancel = true;
                throw new Exception("Chu kì kế toán này đang là chu kì trực thuộc của một chu kì khác! Không thể xóa");
            }
        }
    }
}