using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Xpo;
using NAS.BO.Nomenclature.Organization;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using System.Linq;
using NAS.GUI.Pattern;
using WebModule.NAANAdmin.Authorization.Sate;
using System.Collections;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;

namespace WebModule.NAANAdmin.Authorization.UserControl
{
    public partial class uPopupOrganization : System.Web.UI.UserControl
    {
        Session session;

        public Guid OrganizationId
        {
            set { Session["uPopupOrganization_OrganizationId"] = value; }
            get
            {
                if (Session["uPopupOrganization_OrganizationId"] == null)
                {
                    Session["uPopupOrganization_OrganizationId"] = Guid.Empty;
                    return Guid.Empty;
                }
                return Guid.Parse(Session["uPopupOrganization_OrganizationId"].ToString());
            }
        }

        List<Person> LoginPersonLST
        {
            get
            {
                return Session["LoginPersonLST_UserManagement"] as List<Person>;
            }

            set
            {
                Session["LoginPersonLST_UserManagement"] = value;
            }
        }

        public Guid ParentOrganizationId
        {
            set { Session["uPopupOrganization_ParentOrganizationId"] = value; }
            get
            {
                if (Session["uPopupOrganization_ParentOrganizationId"] == null)
                {
                    Session["uPopupOrganization_ParentOrganizationId"] = Guid.Empty;
                    return Guid.Empty;
                }
                return Guid.Parse(Session["uPopupOrganization_ParentOrganizationId"].ToString());
            }
        }

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_Organization"]; }
            set { Session["GUIContext_Organization"] = value; }
        }

        #region UpdateGUI

        public bool OrganizationCreating_UpdateGUI()
        {
            clearForm();
            ASPxPageControl_OrganizationTabs.ActiveTabIndex = 0;
            ASPxPageControl_OrganizationTabs.Enabled = false;
            popup_Organization.ShowOnPageLoad = true;
            popup_Organization.HeaderText = "Tổ Chức - Thêm mới";

            return true;
        }

        public bool OrganizationEditing_UpdateGUI()
        {
            clearForm();
            ASPxPageControl_OrganizationTabs.Enabled = true;
            ASPxGridView_Person.Enabled = true;
            popup_Organization.ShowOnPageLoad = true;
            popup_Organization.HeaderText = string.Format("Tổ Chức - {0}", session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(OrganizationId).Name);
            ASPxGridView_Person.DataSource = LoginPersonLST;
            ASPxGridView_Person.DataBind();
            return true;
        }

        public bool OrganizationCanceling_UpdateGUI()
        {
            clearForm();
            popup_Organization.ShowOnPageLoad = false;
            return true;
        }

        #endregion

        #region CRUD

        public bool OrganizationCreating_CRUD()
        {
            OrganizationId = Guid.Empty;
            form_Organization.DataSourceID = null;
            form_Organization.DataBind();
            return true;
        }

        public bool OrganizationEditing_CRUD()
        {
            //Load Data vào form
            XpoOrganization.CriteriaParameters["OrganizationId"].DefaultValue = OrganizationId.ToString();
            // load data vào cây thư mục
            XpoDepartment.CriteriaParameters["OrganizationId"].DefaultValue = OrganizationId.ToString();
            ASPxTreeList_Department2.DataBind();
            loadListUsers(ASPxTreeList_Department2.FocusedNode);
            //DepartmentBO bo = new DepartmentBO();
            //LoginPersonLST = bo.getAllPeopleInOrganization(session, OrganizationId);
            return true;
        }

        #endregion

        #region PreTransitionCRUD

        public bool OrganizationCreating_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    if (!ASPxEdit.AreEditorsValid(popup_Organization))
                        return false;
                    // Insert data 
                    NAS.DAL.Nomenclature.Organization.Organization org = new NAS.DAL.Nomenclature.Organization.Organization(session)
                    {
                        OrganizationId = Guid.NewGuid(),
                        Name = txt_TenToChuc.Text,
                        Code = txt_MaToChuc.Text,
                        Address = txt_DiaChi.Text,
                        TaxNumber = txt_MaSoThue.Text,
                        Description = txt_MoTa.Text,
                        RowCreationTimeStamp = DateTime.Now,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };

                    org.OrganizationTypeId = Util.getXPCollection<OrganizationType>(session, "Name", OrganizationTypeConstant.NAAN_CUSTOMER_SUB_ORGANIZATION.Value).FirstOrDefault();
                    org.ParentOrganizationId = Util.getXPCollection<NAS.DAL.Nomenclature.Organization.Organization>(session, "OrganizationId", ParentOrganizationId).FirstOrDefault();
                    if (org.ParentOrganizationId == null)
                    { 
                        NAS.DAL.Nomenclature.Organization.Organization Rootorg = session.FindObject<NAS.DAL.Nomenclature.Organization.Organization>(
                            new BinaryOperator("Code", "QUASAPHARCO", BinaryOperatorType.Equal));
                        org.ParentOrganizationId = Rootorg;
                    }
                    session.Save(org);

                    OrganizationId = org.OrganizationId;
                    return true;
            }
            return false;
        }

        public bool OrganizationEditting_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    if (!ASPxEdit.AreEditorsValid(popup_Organization))
                        return false;
                    // Update data
                    NAS.DAL.Nomenclature.Organization.Organization org = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(OrganizationId);
                    //org.Name = txt_TenTo
                    //Chuc.Text;
                    org.Code = txt_MaToChuc.Text;
                    org.TaxNumber = txt_MaSoThue.Text;
                    org.Address = txt_DiaChi.Text;
                    org.Description = txt_MoTa.Text;
                    session.Save(org);
                    break;
            }
            return true;
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            //if (!IsPostBack)
            //{
            //    if (ASPxTreeList_Department2.Nodes.Count > 0)
            //    {
            //        ASPxTreeList_Department2.Nodes[0].Focus();
            //        loadListUsers(ASPxTreeList_Department2.Nodes[0]);
            //    }
            //}

            ASPxGridView_Person.DataSource = LoginPersonLST;
            ASPxGridView_Person.DataBind();

            XpoDepartment.Session = session;
            XpoOrganization.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GUIContext = new Context();

            }
        }

        protected void cpOrganization_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == null || e.Parameter.Equals(string.Empty))
                return;
            // BEGIN
            string[] transition = e.Parameter.Split(',');
            switch (transition[0])
            {
                case "New":
                    ParentOrganizationId = Guid.Parse(transition[1].ToString());
                    GUIContext.State = new OrganizationCreating(this);
                    break;
                case "Edit":
                    if (transition[1].ToString().Equals("DepartmentChange"))
                    {
                        //ASPxTreeList_Department2.DataBind();
                        loadListUsers(ASPxTreeList_Department2.FocusedNode);
                        ASPxPageControl_OrganizationTabs.ActiveTabIndex = 1;
                    }
                    else
                    {
                        OrganizationId = Guid.Parse(transition[1].ToString());
                        ASPxPageControl_OrganizationTabs.ActiveTabIndex = 0;
                    }
                    GUIContext.State = new OrganizationEditting(this);
                    break;
                case "NewNodeFirst":
                    ParentOrganizationId = Guid.Empty;
                    GUIContext.State = new OrganizationCreating(this);
                    break;
                    //GUIContext.State = new OrganizationCreating(this);
                    //break;
                default:
                    if (GUIContext != null)
                        GUIContext.Request(transition[0], this);
                    break;
            }
        }

        void clearForm()
        {
            //form_Organization.
            txt_TenToChuc.Text = string.Empty;
            txt_MaToChuc.Text = string.Empty;
            txt_MaSoThue.Text = string.Empty;
            txt_DiaChi.Text = string.Empty;
            txt_MoTa.Text = string.Empty;
        }

        protected void ASPxTreeList_Department_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var Organization = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(OrganizationId);

            string parentKey = ASPxTreeList_Department.NewNodeParentKey;

            if (parentKey.Equals(String.Empty))
            {
                e.NewValues["ParentDepartmentId!Key"] = parentKey = Guid.Empty.ToString();
            }

            NAS.DAL.Nomenclature.Organization.Department department = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Department>(Guid.Parse(parentKey));

            NAS.DAL.Nomenclature.Organization.Department o = new NAS.DAL.Nomenclature.Organization.Department(session)
            {
                Name = e.NewValues["Name"].ToString(),
                Code = e.NewValues["Code"].ToString(),
                Description = e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : string.Empty,
                OrganizationId = Organization,
                ParentDepartmentId = department,
                RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
            };
            session.Save(o);
            e.Cancel = true;
            ASPxTreeList_Department.CancelEdit();
        }

        protected void ASPxGridView_Person_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            Person p = new Person(session)
            {
                Code = e.NewValues["Code"].ToString(),
                Name = e.NewValues["Name"].ToString(),
                RowStatus = Convert.ToInt16(e.NewValues["RowStatus"].ToString()),
                Description = e.NewValues["Description"].ToString(),
                PersonId = Guid.NewGuid()
            };
            session.Save(p);
        }

        protected void ASPxGridView_Person_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            string id = e.Keys[ASPxGridView_Person.KeyFieldName].ToString();
            if (id.Equals("") || id != null)
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
                        loadListUsers(ASPxTreeList_Department2.FocusedNode);
                    }
                }
            }
        }

        public void loadListUsers(TreeListNode node)
        {
            LoginPersonLST = null;
            if (node != null)
            {
                NAS.DAL.Nomenclature.Organization.Department selectedDP = (NAS.DAL.Nomenclature.Organization.Department)node.DataItem;
                DepartmentBO bo = new DepartmentBO();
                LoginPersonLST = bo.getAllPeopleInDepartments(session, selectedDP);
            }
            ASPxGridView_Person.DataSource = LoginPersonLST;
            ASPxGridView_Person.DataBind();
        }

        protected void ASPxGridView_Person_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            //if (e.CellType == GridViewTableCommandCellType.Data && ASPxTreeList_Department2.Nodes.Count == 0 && e.ButtonID.Equals("button_EditPerson"))
            //{
            //    e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //}
        }

        protected void ASPxTreeList_Department_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            if (e.NewValues["Code"] == null)
            {
                e.Errors["Code"] = "Bắt buộc nhập mã phòng ban";
                return;
            }

            if (e.NewValues["Name"] == null)
            {
                e.Errors["Name"] = "Bắt buộc nhập tên phòng ban";
                return;
            }
        }

        protected void ASPxTreeList_Department2_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
        {
            ASPxTreeList_Department2.DataBind();
            if (ASPxTreeList_Department2.Nodes.Count > 0)
                ASPxTreeList_Department2.Nodes[0].Focus();
        }
    }
}