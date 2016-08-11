using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.XtraReports.Web;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrintingLinks;
using DevExpress.Xpo.DB;
using DevExpress.Web.ASPxGridView;
using System.Data;
using System.Globalization;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Inventory;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Item;


namespace WebModule.Accounting.Report
{
    public partial class GeneralJournal : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;
        string sql;
        SelectedData seletectedData;
        string codeBegin;
        GridViewBandColumn bandColumn;
        int maxMonth;
        DataRow line;
        double total;
        int widthEndCol;
        string space;

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_DIARYVOUCHER_ID;
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

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            CriteriaOperator filter = new BinaryOperator("IsActive", 1, BinaryOperatorType.Equal);
            AccountingPeriod ap = session.FindObject<AccountingPeriod>(filter);

            if (ap != null)
            {
                cboAccountPeriod.Value = ap.AccountingPeriodId;
                txtFromDate.Value = new DateTime(2014, 1, 1) ;
                txtToDate.Value = new DateTime(2014, 1, 31);
            }
            else
            {
                txtFromDate.Value = DateTime.Now;
                txtToDate.Value = DateTime.Now;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvData.DataSource = ReportMappingConstant.getDiaryVoucherDS();
            gvData.DataBind();
        }

        protected void gvData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            gvData.CancelEdit();
        }

        protected void ReportViewer1_Unload(object sender, EventArgs e)
        {
            ((ReportViewer)sender).Report = null;
        }

        protected void cboAccountPeriod_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            AccountingPeriod obj = session.GetObjectByKey<AccountingPeriod>(e.Value);
            if (obj != null)
            {
                cboAccountPeriod.DataSource = new AccountingPeriod[] { obj };
                cboAccountPeriod.DataBindItems();
            }

        }

        protected void cboAccountPeriod_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection<AccountingPeriod> collection = new XPCollection<AccountingPeriod>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            collection.Criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)});
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboAccountPeriod.DataSource = collection;
            cboAccountPeriod.DataBindItems();
        }

        protected void formReportViewer_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
        }


        protected void gridResult_Init(object sender, EventArgs e)
        {
    
        }

        protected void cboAccountPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboAccount_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection<Account> collection = new XPCollection<Account>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            collection.Criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                    new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] {
                                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like) }),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)});

            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboAccount.DataSource = collection;
            cboAccount.DataBindItems();
        }

        protected void cboAccount_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            Account obj = session.GetObjectByKey<Account>(e.Value);
            if (obj != null)
            {
                cboAccount.DataSource = new Account[] { obj };
                cboAccount.DataBindItems();
            }
        }

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] p = e.Parameter.Split('|');
            switch (p[0])
            {
                case "cboAccountingPeriodChanged":

                    AccountingPeriod ap = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(cboAccountPeriod.Value.ToString()));
                    if (ap != null)
                    {
                        txtFromDate.Value = ap.FromDateTime;
                        txtToDate.Value = ap.ToDateTime;
                    }
                    break;

                default:
                    break;
            }
        }

        protected void cboAccountPeriod_Init(object sender, EventArgs e)
        {
        }

        protected void cboItem_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            Item obj = session.GetObjectByKey<Item>(e.Value);
            if (obj != null)
            {
                cboItem.DataSource = new Item[] { obj };
                cboItem.DataBindItems();
            }
        }

        protected void cboItem_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection<Item> collection = new XPCollection<Item>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            collection.Criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                    new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] {
                                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like) }),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)});

            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboItem.DataSource = collection;
            cboItem.DataBindItems();
        }

        protected void cboInventory_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            Inventory obj = session.GetObjectByKey<Inventory>(e.Value);
            if (obj != null)
            {
                cboInventory.DataSource = new Inventory[] { obj };
                cboInventory.DataBindItems();
            }
        }

        protected void cboInventory_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection<Inventory> collection = new XPCollection<Inventory>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            collection.Criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                    new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] {
                                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like) }),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)});

            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboInventory.DataSource = collection;
            cboInventory.DataBindItems();
        }

        protected void cpReportViewer_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (hReportViewer.Contains("report"))
            {
                string[] reportKey = hReportViewer.Get("report").ToString().Split('|');            
                
                hReportViewer.Remove("report");
                cpReportViewer.JSProperties.Add("cpShowReport", reportKey[0]);
            }
        }


        protected void cbo_UnitDim_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            UnitDim obj = session.GetObjectByKey<UnitDim>(e.Value);
            if (obj != null)
            {
                cbo_UnitDim.DataSource = new UnitDim[] { obj };
                cbo_UnitDim.DataBindItems();
            }

        }

        protected void cbo_UnitDim_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection<UnitDim> collection = new XPCollection<UnitDim>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            collection.Criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)});
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cbo_UnitDim.DataSource = collection;
            cbo_UnitDim.DataBindItems();
        }
    }
}