using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.NASID;
using DevExpress.Xpo;
//using DAL.NASID;
using Utility;
using NAS.DAL;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Nomenclature.Organization;
using DevExpress.Web.ASPxGridView;
using System.Text.RegularExpressions;

namespace WebModule.NAANAdmin.Authorization
{
    public partial class UserManagement : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        XPCollection<Person> LoginPersonLST
        {
            get
            {
                return Session["LoginPersonLST_UserManagement"] as XPCollection<Person>;
            }

            set
            {
                Session["LoginPersonLST_UserManagement"] = value;
            }
        }

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
                return Constant.ACCESSOBJECT_AUTHORIZATION_USERID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            Department.Session = session;
            Department.CriteriaParameters["OrganizationId"].DefaultValue = Utility.CurrentSession.Instance.AccessingOrganizationId.ToString();
            trlDepartmentMenu.DataBind();
            if (!IsPostBack)
            {
                if (trlDepartmentMenu.Nodes != null && trlDepartmentMenu.Nodes.Count > 0)
                    loadListUsers(trlDepartmentMenu.Nodes[0]);
                //LoginPersonLST = null;
                //LoginPersonLST = new XPCollection<Person>(session);
                //LoginPersonLST.Criteria = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
            }
            grd_ListUser.DataSource = LoginPersonLST;
            grd_ListUser.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxPageControl1.ActiveTabIndex = 0;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void ASPxGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventArgs e)
        {
        }

        protected void grd_ListUser_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] para = e.Parameters.Split(',');
            if ((para[0].Equals("Refresh")))
            {
                loadListUsers(trlDepartmentMenu.FocusedNode);
            }
            if (para[0].Equals("Delete"))
            {
                string id = para.Count<string>() == 2 ? para[1] : String.Empty;
                if (id != "")
                {
                    DepartmentBO checkID = new DepartmentBO();
                    bool checkBill = checkID.checkExistPersonInBill(session, Guid.Parse(id));
                    bool checkVouchesActor = checkID.checkExistPersonInVouchesActor(session, Guid.Parse(id));
                    bool checkStock = checkID.checkExistPersonInStockCartActor(session, Guid.Parse(id));
                    bool checkSpecialPrivilege = checkID.checkExistPersonInSpecialPrivilege(session, Guid.Parse(id));
                    bool checkBillActor = checkID.checkExistPersonInBillActor(session, Guid.Parse(id));
                    //bool checkDepartmentPerson = checkID.checkExistPersonInDepartmentPerson(session, Guid.Parse(id));
                    //bool checkLoginAccount = checkID.checkExistPersonInLoginAccount(session, Guid.Parse(id));
                    if (checkBill || checkVouchesActor || checkStock || checkSpecialPrivilege
                        || checkBillActor)
                    {
                        throw new Exception("Người dùng này không xóa được!");
                    }
                    else
                    {
                        Person p = session.GetObjectByKey<Person>(Guid.Parse(id));
                        if (p != null)
                        {
                            CriteriaOperator criteria = new BinaryOperator("PersonId", p.PersonId, BinaryOperatorType.Equal);
                            XPCollection<DepartmentPerson> dpPersonCL = new XPCollection<DepartmentPerson>(session, criteria);
                            foreach (DepartmentPerson dpPerson in dpPersonCL)
                            {
                                if (dpPerson != null)
                                {
                                    dpPerson.RowStatus = -1;
                                    dpPerson.Save();
                                }
                            }
                            XPCollection<LoginAccount> lgAccountCL = new XPCollection<LoginAccount>(session, criteria);
                            foreach (LoginAccount lgAccount in lgAccountCL)
                            {
                                if (lgAccount != null)
                                {
                                    lgAccount.RowStatus = -1;
                                    lgAccount.Save();
                                }
                            }
                            p.RowStatus = -1;
                            p.Save();
                            loadListUsers(trlDepartmentMenu.FocusedNode);
                        }
                    }
                }
            }


        }

        public void loadListUsers(TreeListNode node)
        {
            if (node != null)
            {
                NAS.DAL.Nomenclature.Organization.Department selectedDP = (NAS.DAL.Nomenclature.Organization.Department)node.DataItem;
                DepartmentBO bo = new DepartmentBO();
                List<Person> LoginPersonLST = bo.getAllPeopleInDepartments(session, selectedDP);
                grd_ListUser.DataSource = LoginPersonLST;
                grd_ListUser.DataBind();
            }
        }      
    }
}