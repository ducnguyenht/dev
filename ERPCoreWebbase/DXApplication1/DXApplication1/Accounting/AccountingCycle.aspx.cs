using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using NAS.DAL;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Accounting;
using NAS.DAL.Accounting.Journal;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.Period;
using DevExpress.Data.Filtering;

namespace ERPCore.Accounting
{
    public partial class AccountingCycle : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        #region *
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
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
        #endregion

        AccountingPeriodTypeBO bo = new AccountingPeriodTypeBO();
        UnitOfWork uow = XpoHelper.GetNewUnitOfWork();

        protected void Page_Init(object sender, EventArgs e)
        {
            DBPeriodType.Session = uow;
            DBPeriod.Session = uow;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected object GetMasterRowKeyValue(ASPxTreeList treeList)
        {
            GridViewBaseRowTemplateContainer container = null;
            Control control = treeList;
            while (control.Parent != null)
            {
                container = control.Parent as GridViewBaseRowTemplateContainer;
                if (container != null) break;
                control = control.Parent;
            }
            return container.KeyValue;

        }

        #region GridACCPeriodType
        protected void GridACCPeriodType_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                if (bo.checkAccountingPeriodType_Name(uow, e.NewValues["Name"].ToString().Trim()))
                {
                    e.Cancel = true;
                    throw new Exception(String.Format("Lỗi trùng Tên Chu Kỳ Thuế"));
                }
                else
                {
                    AccountingPeriodType AccPT = new AccountingPeriodType(uow);

                    #region Add IsDefault
                    if (e.NewValues["IsDefault"] == null)
                    {
                        e.NewValues["IsDefault"] = false;
                    }
                    bool isDefault = bool.Parse(e.NewValues["IsDefault"].ToString());

                    if (isDefault)
                    {
                        if (bo.changeIsDefaultAccountingPeriodType(uow))
                        {
                            e.NewValues["IsDefault"] = true;
                        }
                    }
                    #endregion
                    AccPT.Name = e.NewValues["Name"].ToString();
                    AccPT.IsDefault = bool.Parse(e.NewValues["IsDefault"].ToString());
                    AccPT.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    if (e.NewValues["Description"] == null)
                    {
                        e.NewValues["Description"] = "";
                    }
                    AccPT.Description = e.NewValues["Description"].ToString();
                    uow.FlushChanges();
                    GridACCPeriodType.DataBind();
                    GridACCPeriodType.CancelEdit();
                }

            }
            catch (Exception)
            {
                e.Cancel = true;
                throw;
            }
        }

        protected void GridACCPeriodType_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                //check Name
                if (!e.OldValues["Name"].Equals(e.NewValues["Name"]))
                {
                    if (bo.checkAccountingPeriodType_Name(uow, e.NewValues["Name"].ToString().Trim()))
                    {
                        e.Cancel = true;
                        throw new Exception(String.Format("Lỗi trùng Tên Chu Kỳ Thuế"));
                    }
                }
                //end check name

                AccountingPeriodType AccPt = uow.GetObjectByKey<AccountingPeriodType>(Guid.Parse(e.Keys[0].ToString()));

                #region update IsDefautl
                if (e.NewValues["IsDefault"] == null)
                {
                    e.NewValues["IsDefault"] = false;
                }
                bool IsDefault = bool.Parse(e.NewValues["IsDefault"].ToString());

                if (IsDefault)
                {
                    if (bo.changeIsDefaultAccountingPeriodType(uow))
                    {
                        e.NewValues["IsDefault"] = true;
                    }
                }
                #endregion

                AccPt.Name = e.NewValues["Name"].ToString();
                AccPt.IsDefault = bool.Parse(e.NewValues["IsDefault"].ToString());
                if (e.NewValues["Description"] == null)
                {
                    e.NewValues["Description"] = "";
                }
                AccPt.Description = e.NewValues["Description"].ToString();
                uow.FlushChanges();
                GridACCPeriodType.DataBind();
                GridACCPeriodType.CancelEdit();
            }
            catch (Exception)
            {
                e.Cancel = true;
                throw;
            }
        }

        protected void GridACCPeriodType_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                if (bo.checkIsAccountingPeriodTypeIdInCurrency(uow, e.Values["AccountingPeriodTypeId"].ToString().Trim()))
                {
                    e.Cancel = true;
                    throw new Exception(String.Format("Lỗi không thể xóa vì có chứa Chu Kỳ "));
                }
                else
                {
                    e.Cancel = true;
                    uow.BeginTransaction();
                    Guid a = Guid.Parse(e.Keys[0].ToString());
                    AccountingPeriodType accpT = uow.GetObjectByKey<AccountingPeriodType>(a);
                    accpT.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    accpT.Save();

                    uow.CommitTransaction();
                }
            }
            catch (Exception)
            {
                uow.RollbackTransaction();
                e.Cancel = true;
                throw;
            }
        }

        protected void GridACCPeriodType_Init(object sender, EventArgs e)
        {
            //chi tro toi 1 row MasterPage
            GridACCPeriodType.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
            if (GridACCPeriodType.SettingsDetail.AllowOnlyOneMasterRowExpanded)
            {
                GridACCPeriodType.DetailRows.CollapseAllRows();
            }
        }
        #endregion

        #region treelistACCPeriod
        protected void treelistACCPeriod_OnInit(object sender, EventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            object keyValue = GetMasterRowKeyValue(treeList);
            Session["SessionAccountingPeriodTypeId"] = keyValue.ToString();
            Organization org = uow.FindObject<Organization>(new BinaryOperator("Name", "QUASAPHARCO", BinaryOperatorType.Equal));
            if (org != null)
            {
                Session["SessionOrganizationId"] = org.OrganizationId;
            }
            else
            {
                throw new Exception(String.Format("Không Có Tổ chức QUASAPHARCO"));
            }
        }

        protected void treelistACCPeriod_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                ASPxTreeList treelist = sender as ASPxTreeList;
                object keyValue = GetMasterRowKeyValue(treelist);
                AccountingPeriodType AccPTypeId = uow.GetObjectByKey<AccountingPeriodType>(Guid.Parse(keyValue.ToString()));
                if(AccPTypeId == null){
                    throw new Exception(String.Format("AccountingPeriodType is not exist in System"));
                }

                if (bo.checkAccountingPeriod_Code(uow, e.NewValues["Code"].ToString().Trim(), AccPTypeId.AccountingPeriodTypeId.ToString()))
                {
                    throw new Exception(String.Format("Lỗi Chu Kỳ Đã Có"));
                }
                else
                {

                    AccountingPeriod ap = new AccountingPeriod(uow);

                    #region add parentACCPeriodId
                    string parentKeyStr = treelist.NewNodeParentKey.ToString();
                    

                    if (parentKeyStr != null && !parentKeyStr.Equals(String.Empty))
                    {
                        NAS.DAL.Accounting.Journal.AccountingPeriod parentAccPeriod =
                            uow.GetObjectByKey<NAS.DAL.Accounting.Journal.AccountingPeriod>(Guid.Parse(parentKeyStr.ToString()));
                        if (parentAccPeriod == null)
                        {
                            throw new Exception(String.Format("AccountingPeriod is not exist in system"));
                        }
                       // ap.ParentAccountingPeriodId = parentAccPeriod;
                    }
                    #endregion

                    #region add AccountingPeriodTypeId
                    if (AccPTypeId != null)
                    {
                        ap.AccountingPeriodTypeId = AccPTypeId;
                    }
                    #endregion

                    #region add OrganizationId
                    Organization org = uow.FindObject<Organization>(new BinaryOperator("Name", "QUASAPHARCO", BinaryOperatorType.Equal));
                    if (org != null)
                    {
                        ap.OrganizationId = org;
                    }
                    #endregion

                    #region add IsActive
                    if (e.NewValues["IsActive"] != null)
                    {
                        bool IsActive = bool.Parse(e.NewValues["IsActive"].ToString());
                        if (IsActive)
                        {

                            if (bo.changeIsActiveAccountingPeriod(uow))//, Guid.Parse(treelistCurrency.FocusedNode.Key.ToString())
                            {
                                e.NewValues["IsActive"] = true;
                            }
                            if (bo.changeIsDefaultAccountingPeriodType(uow))
                            {
                                AccPTypeId.IsDefault = true;
                                AccPTypeId.Save();
                               // bo.changeIsActiveParentInParentAccP(uow, Guid.Parse(parentKeyStr.ToString()), IsActive);
                            }

                        }
                        else
                        {
                            e.NewValues["IsActive"] = false;
                        }
                        ap.IsActive = bool.Parse(e.NewValues["IsActive"].ToString());
                    }
                    #endregion
                    ap.Code = e.NewValues["Code"].ToString();
                    ap.FromDateTime = DateTime.Parse(e.NewValues["FromDateTime"].ToString());
                    ap.ToDateTime = DateTime.Parse(e.NewValues["ToDateTime"].ToString());

                    if (e.NewValues["Description"] == null)
                    {
                        e.NewValues["Description"] = "";
                    }
                    ap.Description = e.NewValues["Description"].ToString();
                    ap.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    uow.FlushChanges();
                    treelist.CancelEdit();
                    
                }
                GridACCPeriodType.DataBind();
                treelist.JSProperties.Add("cpSaved", true);
            }
            catch (Exception)
            {
                e.Cancel = true;
                throw;
            }
        }

        protected void treelistACCPeriod_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                //CHECK CODE 
                ASPxTreeList treelist = sender as ASPxTreeList;
                object keyValue = GetMasterRowKeyValue(treelist);
                AccountingPeriodType AccPTId = uow.GetObjectByKey<AccountingPeriodType>(Guid.Parse(keyValue.ToString()));

                if (AccPTId == null)
                {
                    throw new Exception(String.Format("AccountingPeriodType is not exist in system"));
                }
                if (!e.OldValues["Code"].Equals(e.NewValues["Code"]))
                {
                    if (bo.checkAccountingPeriod_Code(uow, e.NewValues["Code"].ToString().Trim(), AccPTId.AccountingPeriodTypeId.ToString()))
                    {
                        e.Cancel = true;
                        throw new Exception(String.Format("Lỗi Chu Kỳ đã có"));
                    }
                }
                //END CHECK CODE
                AccountingPeriod ap = uow.GetObjectByKey<AccountingPeriod>(Guid.Parse(e.Keys[0].ToString()));
                if (ap != null)
                {
                    if (e.NewValues["IsActive"] != null)
                    {
                        bool IsActive = bool.Parse(e.NewValues["IsActive"].ToString());
                        if (IsActive)
                        {
                            if (bo.changeIsActiveAccountingPeriod(uow))//, Guid.Parse(treelistCurrency.FocusedNode.Key.ToString())
                            {
                                ap.IsActive = true;
                            }
                            if (bo.changeIsDefaultAccountingPeriodType(uow))
                            {
                                AccPTId.IsDefault = true;
                                AccPTId.Save();
                                //if (ap.ParentAccountingPeriodId != null)
                                //{
                                //    Guid parentID = Guid.Parse(ap.ParentAccountingPeriodId.AccountingPeriodId.ToString());
                                //    bo.changeIsActiveParentInParentAccP(uow, parentID, IsActive);
                                //}
                            }
                            
                        }
                        else
                        {
                            ap.IsActive = false;
                        }
                    }
                    if (e.NewValues["Description"] == null)
                    {
                        e.NewValues["Description"] = "";
                    }
                    ap.Description = e.NewValues["Description"].ToString();
                    ap.Code = e.NewValues["Code"].ToString();
                    ap.FromDateTime = DateTime.Parse(e.NewValues["FromDateTime"].ToString());
                    ap.ToDateTime = DateTime.Parse(e.NewValues["ToDateTime"].ToString());
                    uow.FlushChanges();
                }
                treelist.CancelEdit();
                GridACCPeriodType.DataBind();
                treelist.JSProperties.Add("cpSaved", true);
            }
            catch (Exception)
            {

                e.Cancel = true;
                throw;
            }
        }

        protected void treelistACCPeriod_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                uow.BeginTransaction();
                Guid a = Guid.Parse(e.Keys[0].ToString());
                AccountingPeriod accp = uow.GetObjectByKey<AccountingPeriod>(a);
                accp.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                accp.Save();

                uow.CommitTransaction();
            }
            catch (Exception)
            {
                uow.RollbackTransaction();
                e.Cancel = true;
                throw;
            }
        }

        protected void treelistACCPeriod_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            //Validate period
            if (e.NewValues["FromDateTime"] != null && e.NewValues["ToDateTime"] != null)
            {
                DateTime fromDate = (DateTime)e.NewValues["FromDateTime"];
                DateTime toDate = (DateTime)e.NewValues["ToDateTime"];
                if (fromDate > toDate)
                {
                    throw new Exception(String.Format("Từ ngày phải nhỏ hơn hoặc bằng Đến ngày"));
                }
            }
            else
            {
                throw new Exception(String.Format("Từ ngày và Đến ngày không được để trống"));
            }

        }
        #endregion



    }
}