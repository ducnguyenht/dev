using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using Utility;
using NAS.DAL;
using DevExpress.Xpo;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Nomenclature.Organization;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;

namespace WebModule.NAANAdmin.Authorization
{
    public partial class Department : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
        Session session;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            Department_XPO.Session = session;
            Department_XPO.CriteriaParameters["OrganizationId"].DefaultValue = Utility.CurrentSession.Instance.AccessingOrganizationId.ToString();
            DepartmentType.Session = session;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void LoadDepartmentTreeList()
        {
        }

        private void LoadRolesOfOrganizationGridView()
        {
        }

        protected void grdPermissions_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }

        void grid_Load(object sender, EventArgs e)
        {

        }

        protected void trlDepartment_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["RowCreationTimeStamp"] = DateTime.Now;
            e.NewValues["RowStatus"] = Constant.ROWSTATUS_ACTIVE;
            NAS.DAL.Nomenclature.Organization.DepartmentType departmenttype =
                Util.getXPCollection<NAS.DAL.Nomenclature.Organization.DepartmentType>(session, "Name", NAS.DAL.Nomenclature.Organization.DepartmentTypeConstant.NAAN_DEFAULT.Value).FirstOrDefault();
            e.NewValues["DepartmentTypeId"] = departmenttype;
            e.NewValues["OrganizationId!Key"] = Utility.CurrentSession.Instance.AccessingOrganizationId.ToString();
            string parentKey = trlDepartment.NewNodeParentKey;
            if (parentKey.Equals(String.Empty))
            {
                e.NewValues["ParentDepartmentId!Key"] = trlDepartment.RootValue;
            }
        }

        protected void trlDepartment_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventArgs e)
        {
            if (e.Column.Name == "DpId")
            {
                NAS.DAL.Nomenclature.Organization.Department department = (NAS.DAL.Nomenclature.Organization.Department)trlDepartment.FindNodeByKeyValue(e.NodeKey).DataItem;
                string dptype = department.DepartmentTypeId.Name.ToString();
                e.Cell.Text = dptype;
            }
        }

        protected void trlDepartment_NodeValidating(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventArgs e)
        {
            ASPxTreeList treeDepart = (ASPxTreeList)sender;
            string departCode = e.NewValues["Code"].ToString().Trim();
            if (treeDepart.IsNewNodeEditing)
            {
                bool isExist = Util.isExistXpoObject<NAS.DAL.Nomenclature.Organization.Department>("Code", departCode,
                    Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExist)
                {
                    CriteriaOperator criteria = new BinaryOperator("Code", departCode, BinaryOperatorType.Equal);
                    NAS.DAL.Nomenclature.Organization.Department depart = session.FindObject<NAS.DAL.Nomenclature.Organization.Department>(criteria);
                    short rowstatus = depart.RowStatus;
                    if (rowstatus > 0)
                    {
                        Utility.Helpers.AddErrorToTreeListNode(e.Errors, "Code", "Mã phòng ban đã tồn tại.");
                        return;
                    }                    
                }
            }
            else
            {
                if (treeDepart.IsEditing)
                {
                    string newCode = e.NewValues["Code"].ToString().Trim();
                    string oldCode = e.OldValues["Code"].ToString().Trim();
                    if (!newCode.Equals(oldCode))
                    {
                        bool isExist = Util.isExistXpoObject<NAS.DAL.Nomenclature.Organization.Department>("Code", departCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                        if (isExist)
                        {
                            CriteriaOperator criteria = new BinaryOperator("Code", departCode, BinaryOperatorType.Equal);
                            NAS.DAL.Nomenclature.Organization.Department depart = session.FindObject<NAS.DAL.Nomenclature.Organization.Department>(criteria);
                            short rowstatus = depart.RowStatus;
                            if (rowstatus > 0)
                            {
                                Utility.Helpers.AddErrorToTreeListNode(e.Errors, "Code", "Mã phòng ban đã tồn tại.");
                                return;
                            }
                        }
                    }
                }
            }
        }

        protected void trlDepartment_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            DepartmentBO checkID = new DepartmentBO();
            Guid departmentId = (Guid)e.Keys["DepartmentId"];
            //Guid departmentId = Guid.Parse(trlDepartment.FocusedNode["DepartmentId"].ToString());        
            //TreeListNode node = trlDepartment.FindNodeByFieldValue("DepartmentId", departmentId);
            //node.Focus();

            //CriteriaOperator criteriaParent = new BinaryOperator("ParentDepartmentId", departmentId);
            //XPCollection<NAS.DAL.Nomenclature.Organization.Department> parentCL = new XPCollection<NAS.DAL.Nomenclature.Organization.Department>(session, criteriaParent);
            //if (parentCL.Count() > 0)
            //{
            //    throw new Exception("Không được xóa phòng ban này vì có phòng ban con.");
            //}
            //else
            //{
            bool checkdepartPerson = checkID.checkExistDepartmentPersonInDepartment(session, departmentId);
            bool checkPrivilege = checkID.checkExistPrivilegeDepartmentInDepartment(session, departmentId);
            bool checkVouchesActor = checkID.checkExistVouchesActorInDepartment(session, departmentId);
            //trlDepartment.JSProperties.Add("cpQuestion", false);
            if (checkdepartPerson)
            {
                throw new Exception("Không được xóa phòng ban này.");
            }
            else if (checkPrivilege || checkVouchesActor)
            {
                throw new Exception("Không được xóa phòng ban này.");
            }
            else
            {
                //e.Values["RowStatus"] = Constant.ROWSTATUS_DELETED;                    
                trlDepartment.JSProperties.Add("cpQuestion", departmentId.ToString());
                //trlDepartment.JSProperties["cpQuestion"] = true;
            }
            
            //}
        }

        protected void trlDepartment_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                e.Editor.Focus();
            }
        }

        protected void trlDepartment_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
        {
            string[] p = e.Argument.Split('|');
            if (p[0] == "Delete")
            {
                Guid departmentId = Guid.Parse(p[1]);
                NAS.DAL.Nomenclature.Organization.Department department = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Department>(departmentId);
                department.RowStatus = Constant.ROWSTATUS_DELETED;
                department.Save();
                trlDepartment.JSProperties.Add("cpRefresh", "RefreshOnly");
            }
            trlDepartment.DataBind();
        }
    }
}



