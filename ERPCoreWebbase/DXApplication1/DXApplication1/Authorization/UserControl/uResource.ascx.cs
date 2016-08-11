using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.System.Resource;
using Utility;
using DevExpress.Data.Filtering;

namespace WebModule.Authorization.UserControl
{
    public partial class uResource : System.Web.UI.UserControl
    {
        Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ResourceXDS.Criteria = "AppId = ?  And RowStatus >= ?";
            ResourceXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hdId.Contains("id"))
            {
                ResourceXDS.Criteria = "AppId = ?  And RowStatus >= ?";
                ResourceXDS.CriteriaParameters.Add("AppId", hdId.Get("id").ToString());
                ResourceXDS.CriteriaParameters.Add("RowStatus", Constant.ROWSTATUS_ACTIVE.ToString());                                
            }

            grdResource.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdResource_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
        {
            if (e.Column.FieldName == "RowStatus")
            {
                if (e.Editor.Value == null)
                {
                    e.Editor.Value = Constant.ROWSTATUS_ACTIVE;
                }
            }
        }

        protected void grdResource_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            AppComponent re = session.GetObjectByKey<AppComponent>(Guid.Parse(grdResource.FocusedNode[grdResource.KeyFieldName].ToString()));
            re.RowStatus = Constant.ROWSTATUS_DELETED;
            re.Save();

            e.Cancel = true;
            grdResource.DataBind();    
        }

        protected void grdResource_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            AppComponent re = new AppComponent(session);

            re.Code = e.NewValues["Code"].ToString();
            re.Name = e.NewValues["Name"].ToString();
            re.Description = e.NewValues["Description"] as string;
            re.RowCreationTimeStamp = DateTime.Now;
            re.RowStatus = Constant.ROWSTATUS_ACTIVE;
            
            App aa = session.GetObjectByKey<App>(Guid.Parse(hdId.Get("id").ToString()));
            if (aa == null)
            {
                throw new Exception("Ứng dụng đã bị xóa hoặc không tồn tại !");
            }            
            re.AppId = aa;

            if (e.NewValues[grdResource.ParentFieldName] != null)
            {
                re.ParentAppComponentId = session.GetObjectByKey<AppComponent>(Guid.Parse(e.NewValues[grdResource.ParentFieldName].ToString()));
            }

            re.Save();

            e.Cancel = true;
            grdResource.CancelEdit();
        }

        protected void grdResource_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            AppComponent re = session.GetObjectByKey<AppComponent>(Guid.Parse(grdResource.FocusedNode[grdResource.KeyFieldName].ToString()));

            //AppComponent re = session.GetObjectByKey<AppComponent>(Guid.Parse(e.OldValues["AppComponentId"].ToString()));

            re.Code = e.NewValues["Code"].ToString();
            re.Name = e.NewValues["Name"].ToString();
            re.Description = e.NewValues["Description"] as string;

            if (e.NewValues["RowStatus"].ToString().Equals("Sử dụng") || e.NewValues["RowStatus"].ToString().Equals("1"))
            {
                re.RowStatus = Constant.ROWSTATUS_ACTIVE;
            }
            else
            {
                re.RowStatus = Constant.ROWSTATUS_INACTIVE;
            }

            re.Save();

            e.Cancel = true;
            grdResource.CancelEdit();

            grdResource.JSProperties.Add("cpRefresh", "true");
        }

        protected void grdResource_NodeValidating(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventArgs e)
        {
            bool checkCode = false;

            if (e.NewValues["Code"] == null)
            {
                e.Errors["Code"] = "Chưa nhập mã tài nguyên !";
                return;
            }

            if (e.IsNewNode)
            {
                checkCode = true;
            }
            else
            {
                if (e.NewValues["Code"].ToString() != e.OldValues["Code"].ToString())
                {
                    checkCode = true;
                }
            }

            if (checkCode)
            {
                CriteriaOperator filter = CriteriaOperator.And(new BinaryOperator("Code", e.NewValues["Code"].ToString(), BinaryOperatorType.Equal),
                                                            new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual));
                XPCollection<AppComponent> ac = new XPCollection<AppComponent>(session, filter);

                if (ac.Count >= 1)
                {
                    e.Errors["Code"] = "Mã tài nguyên đã tồn tại !";
                    return;
                }
            }

            if (e.NewValues["Name"] == null)
            {
                e.Errors["Name"] = "Chưa nhập tên tài nguyên !";
                return;
            }

            if (e.NewValues["RowStatus"] == null)
            {
                e.Errors["RowStatus"] = "Chưa chọn trạng thái tài nguyên !";
                return;
            }
        }

        protected void cpResourceLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }


    }
}