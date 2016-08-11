using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.DAL.System.Resource;

namespace WebModule.Authorization.Application
{

    public partial class Application : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_APPLICATION_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            ApplicationXDS.Session = session;
            ResourceApplicationXDS.Session = session;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState.Clear();
            grdApplication.DataBind();
            
        }


        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void tlSitemap_Load(object sender, EventArgs e)
        {
           
        }

        void detailView_Load(object sender, EventArgs e)
        {
           
        }

        protected void grdApplication_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            App ae = new App(session);
            ae.RowStatus = Constant.ROWSTATUS_DELETED;
            ae.Save();
            
            e.Cancel = true;
        }

        protected void grdApplication_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            App ae = new App(session);

            ae.AppId = Guid.NewGuid();            
            ae.Description = e.NewValues["Description"].ToString();
            ae.Name = e.NewValues["Name"].ToString();
            ae.RowStatus = Constant.ROWSTATUS_ACTIVE;

            ae.Save();

            e.Cancel = true;
            grdApplication.CancelEdit();
        }

        protected void grdApplication_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            App ae = session.GetObjectByKey<App>(Guid.Parse(grdApplication.GetRowValues(grdApplication.FocusedRowIndex, "AppId").ToString()));
            if (ae == null)
            {
                throw new Exception("Ứng dụng này đã bị xóa hoặc không tồn tại !");
            }
            
            ae.Description = e.NewValues["Description"].ToString();
            ae.Name = e.NewValues["Name"].ToString();

            if (e.NewValues["RowStatus"].ToString().Equals("Sử dụng") || e.NewValues["RowStatus"].ToString().Equals("1"))
            {
                ae.RowStatus = Constant.ROWSTATUS_ACTIVE;
            }
            else
            {
                ae.RowStatus = Constant.ROWSTATUS_INACTIVE;
            }

            ae.Save();

            e.Cancel = true;
            grdApplication.CancelEdit();

            grdApplication.JSProperties.Add("cpRefresh", "refresh");
        }

        protected void grdApplication_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void tlSitemap_NodeCollapsing(object sender, TreeListNodeCancelEventArgs e)
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

        protected void tlSitemap_Init(object sender, EventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            object keyValue = GetMasterRowKeyValue(treeList);
            treeList.RootValue = keyValue;
        }

        protected void grdApplication_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["Name"] == null)
            {
                e.Errors[grdApplication.Columns["Name"]] = "Chưa nhập tên ứng dụng !";
                return;
            }

            bool checkName = false;
            if (e.IsNewRow)
            {
                checkName = true;
            }
            else
            {
                if (e.NewValues["Name"].ToString() != e.OldValues["Name"].ToString())
                {
                    checkName = true;
                }
            }

            if (checkName)
            {
                CriteriaOperator filter = CriteriaOperator.And(new BinaryOperator("Name", e.NewValues["Name"].ToString(), BinaryOperatorType.Equal),
                                                            new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual));
                XPCollection<App> ac = new XPCollection<App>(session, filter);

                if (ac.Count >= 1)
                {
                    e.Errors[grdApplication.Columns["Name"]] = "Tên ứng dụng đã tồn tại !";
                    return;
                }
            }
        }

        protected void grdApplication_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "RowStatus")
            {
                if (e.Editor.Value == null)
                {
                    e.Editor.Value = Constant.ROWSTATUS_ACTIVE;
                }
            }
        }
    }
}