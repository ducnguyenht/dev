using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebModule.Interfaces;
using Utility;
using DevExpress.Xpo;
//using BLL.BO.Authorization;
//using BLL.Authorization;
//using DAL.Authorization;
using DevExpress.Data.Filtering;

namespace WebModule.Authorization
{
    public partial class DepartmentManagement : System.Web.UI.Page, IERPCoreWebModuleBase
    {

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_AUTHORIZATION_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_AUTHORIZATION_ORGANIZATIONID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private Session session;
        //private DepartmentBLO departmentBLO;


        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelper.GetNewSession();
            dsDepartmentTree.Session = session;
            //trlDepartment.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //departmentBLO = new DepartmentBLO();
            CurrentSession.Instance.AccessingOrganizationId = Guid.Parse("35D24072-CB53-4EE6-B76B-2C509A4E3897");
            dsDepartmentTree.CriteriaParameters["AccessingOrganizationId"].DefaultValue = CurrentSession.Instance.AccessingOrganizationId.ToString();
            dsDepartmentTree.CriteriaParameters["Language"].DefaultValue = CurrentSession.Instance.Lang;
            //CriteriaOperator dsDepartmentTreeCriteria =
            //        departmentBLO.getCriteriaOfDepartmentTreeView(Utility.CurrentSession.Instance.AccessingOrganizationId, CurrentSession.Instance.Lang);
            //dsDepartmentTree.Criteria = dsDepartmentTreeCriteria.ToString();
            trlDepartment.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void trlDepartment_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                //DepartmentEntity entity = new DepartmentEntity();
                //entity.DepartmentId = Guid.NewGuid();
                //entity.Code = (string)e.NewValues["Code"];
                //entity.Description = (string)e.NewValues["Description"];
                //entity.Email = (string)e.NewValues["Email"];
                //entity.Language = CurrentSession.Instance.Lang;
                //entity.Name = (string)e.NewValues["Name"];
                //entity.PhoneNumber = (string)e.NewValues["PhoneNumber"];
                //entity.OrganizationId = CurrentSession.Instance.AccessingOrganizationId;
                //string newNodeParentKey = trlDepartment.NewNodeParentKey;
                //if (newNodeParentKey.Equals(String.Empty))
                //{
                //    entity.ParentDepartmentId = Guid.Empty;
                //}
                //else
                //{
                //    entity.ParentDepartmentId = Guid.Parse(newNodeParentKey);
                //}
                //departmentBLO.Insert(entity);
            }
            finally
            {
                e.Cancel = true;
                trlDepartment.CancelEdit();
            }
        }

        private const string CPACTION_FORCEREFRESH = "ForceRefresh";

        protected void trlDepartment_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                //DepartmentEntity entity = new DepartmentEntity();
                //entity.DepartmentId = (Guid)e.Keys["DepartmentId"];
                //entity.Code = (string)e.NewValues["Code"];
                //entity.Description = (string)e.NewValues["Description"];
                //entity.Email = (string)e.NewValues["Email"];
                //entity.Language = CurrentSession.Instance.Lang;
                //entity.Name = (string)e.NewValues["Name"];
                //entity.PhoneNumber = (string)e.NewValues["PhoneNumber"];
                //departmentBLO.Update(entity);

                trlDepartment.JSProperties.Add("cpAction", CPACTION_FORCEREFRESH);

            }
            finally
            {
                e.Cancel = true;
                trlDepartment.CancelEdit();
            }
        }

        protected void trlDepartment_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                Guid departmentId = (Guid)e.Keys["DepartmentId"];
                //departmentBLO.DeleteLogical(departmentId);
            }
            finally
            {
                //e.Cancel = true;
            }
        }

        protected void trlDepartment_NodeDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {
            e.ExceptionHandled = true;
        }

    }
}