using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using System.Text.RegularExpressions;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Xpo;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Nomenclature.Organization;
using NAS.GUI.Pattern;
using WebModule.NAANAdmin.Authorization.Sate;
using Utility;

namespace WebModule.NAANAdmin.Authorization.UserControl
{
    public partial class uPopupUserCreating : System.Web.UI.UserControl
    {
        Session session = null;
        Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_PopupUserCreating"]; }
            set { Session["GUIContext_PopupUserCreating"] = value; }
        }
        const string SESSION_NAME_PERSONID = "uPopupUserCreating_PersonId";
        /// <summary>
        /// Key: PersonId được lưu vào session
        /// </summary>
        public Guid PersonId
        {
            set { Session[SESSION_NAME_PERSONID] = value; }
            get
            {
                if (Session[SESSION_NAME_PERSONID] == null)
                {
                    Session[SESSION_NAME_PERSONID] = Guid.Empty;
                    return Guid.Empty;
                }
                return Guid.Parse(Session[SESSION_NAME_PERSONID].ToString());
            }
        }

        const string SESSION_NAME_TMPLoginAccount = "uPopupUserCreating_TMPLoginAccount";
        List<TMPLoginAccount> Temp_LoginAccount
        {
            get
            {
                if (Session[SESSION_NAME_TMPLoginAccount] == null)
                    Session[SESSION_NAME_TMPLoginAccount] = new List<TMPLoginAccount>();
                return (Session[SESSION_NAME_TMPLoginAccount] as List<TMPLoginAccount>);
            }
            set
            {
                Session[SESSION_NAME_TMPLoginAccount] = value;
            }
        }

        public List<TreeListNode> DepartmentNodes
        {
            get
            {
                List<TreeListNode> nodes = new List<TreeListNode>();
                TreeListNodeIterator iterator = new TreeListNodeIterator(ASPxTreeList_OfDepartment.RootNode);
                while (iterator.Current != null)
                {
                    if (iterator.Current != ASPxTreeList_OfDepartment.RootNode)
                    {
                        nodes.Add(iterator.Current);
                    }
                    iterator.GetNext();
                }

                return nodes;
            }
        }

        #region STATE MACHINE

        #region Person Creating
        public bool PersonCreating_CRUD()
        {
            Temp_LoginAccount = null;
            ASPxGridView_LoginAccount.DataSource = Temp_LoginAccount;
            ASPxGridView_LoginAccount.DataBind();
            return true;
        }

        public bool PersonCreating_UpdateGUI()
        {
            ClearForm();
            txt_Code.Focus();
            popup_PersonCreate.ShowOnPageLoad = true;
            popup_PersonCreate.HeaderText = "Thông Tin Người Dùng";
            ASPxGridView_LoginAccount.DataSource = Temp_LoginAccount;
            ASPxGridView_LoginAccount.DataBind();
            ASPxTreeList_OfDepartment.DataBind();
            ASPxGridView_LoginAccount.AddNewRow();

            return true;
        }

        public bool PersonCreating_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    //session.BeginTransaction();
                    if (!ASPxEdit.AreEditorsValid(popup_PersonCreate))
                        return false;
                    Person person = new Person(session)
                    {
                        PersonId = Guid.NewGuid(),
                        Code = txt_Code.Text,
                        Name = txt_Name.Text,
                        RowCreationTimeStamp = DateTime.Now,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };
                    person.Save();

                    foreach (TMPLoginAccount tmpAccount in Temp_LoginAccount)
                    {
                        LoginAccount account = new LoginAccount(session);
                        //account.LoginAccountId = Guid.NewGuid();
                        account.Email = tmpAccount.Email;
                        account.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        account.RowCreationTimeStamp = DateTime.Now;
                        account.PersonId = person;
                        account.Save();
                    }

                    foreach (TreeListNode node in ASPxTreeList_OfDepartment.GetSelectedNodes())
                    {
                        DepartmentPerson dp = new DepartmentPerson(session)
                        {
                            DepartmentId = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Department>(Guid.Parse(node.Key)),
                            DepartmentPersonId = Guid.NewGuid(),
                            RowCreationTimeStamp = DateTime.Now,
                            PersonId = person,
                            RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                        };
                        dp.Save();
                    }

                    PersonId = person.PersonId;
                    //session.CommitTransaction();
                    return true;
            }
            return false;
        }
        #endregion

        #region Person Editing
        public bool PersonEditing_CRUD()
        {
            Temp_LoginAccount.Clear();
            return true;
        }

        public bool PersonEditing_UpdateGUI()
        {
            ClearForm();
            // Load Person
            Person person = session.GetObjectByKey<Person>(PersonId);
            frmPersonEdit.DataSource = person;
            frmPersonEdit.DataBind();

            // load TMP_LOGINACCOUNT
            var selectedLA = from la in person.LoginAccounts where la.RowStatus > 0 select la;
            foreach (LoginAccount la in selectedLA)
            {
                Temp_LoginAccount.Add(new TMPLoginAccount()
                {
                    LoginAccountId = la.LoginAccountId,
                    Email = la.Email
                });
            }
            ASPxGridView_LoginAccount.DataSource = Temp_LoginAccount;
            ASPxGridView_LoginAccount.DataBind();

            // Bind selected Node
            var selectedDP = from dp in person.DepartmentPersons where dp.RowStatus > 0 select dp;
            foreach (DepartmentPerson dp in selectedDP)
                foreach (TreeListNode node in DepartmentNodes)
                {
                    if (Guid.Parse(node.Key).Equals(dp.DepartmentId.DepartmentId))
                        node.Selected = true;
                }
            ASPxTreeList_OfDepartment.DataBind();

            // UPDATE GUI
            popup_PersonCreate.ShowOnPageLoad = true;
            popup_PersonCreate.HeaderText = string.Format("Thông Tin Người Dùng: {0}", person.Name);
            return true;
        }

        public bool PersonEditing_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    if (!ASPxEdit.AreEditorsValid(popup_PersonCreate))
                        return false;
                    Person person = session.GetObjectByKey<Person>(PersonId);
                    {
                        person.Code = txt_Code.Text;
                        person.Name = txt_Name.Text;
                        person.RowStatus = Convert.ToInt16(Combo_RowStatus.SelectedItem.Value);
                    };
                    person.Save();

                    var statementLoginAccounts = from la in person.LoginAccounts
                                                 where la.RowStatus > 0
                                                 && la.PersonId == person
                                                 select la.LoginAccountId;

                    foreach (TMPLoginAccount tmpAccount in Temp_LoginAccount)
                    {
                        LoginAccount account;
                        if (statementLoginAccounts.Contains(tmpAccount.LoginAccountId))
                        {
                            account = session.GetObjectByKey<LoginAccount>(tmpAccount.LoginAccountId);
                            account.Email = tmpAccount.Email;
                            account.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            account.Save();
                        }
                        else
                        {
                            account = new LoginAccount(session);
                            account.LoginAccountId = Guid.NewGuid();
                            account.Email = tmpAccount.Email;
                            account.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                            account.RowCreationTimeStamp = DateTime.Now;
                            account.PersonId = person;
                            account.Save();
                        }
                    }

                    // update Department
                    List<TreeListNode> nodes = ASPxTreeList_OfDepartment.GetSelectedNodes();
                    List<NAS.DAL.Nomenclature.Organization.Department> departmentList = new List<NAS.DAL.Nomenclature.Organization.Department>();
                    foreach (TreeListNode n in nodes)
                    {
                        NAS.DAL.Nomenclature.Organization.Department d = (NAS.DAL.Nomenclature.Organization.Department)n.DataItem;
                        departmentList.Add(d);
                    }
                    DepartmentBO bo = new DepartmentBO();
                    bo.updatePerson(session, departmentList, PersonId, person.Code, person.Name, person.RowStatus);
                    return true;
            }
            return false;
        }
        #endregion

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            XpoDepartment.Session = session;

            // Load General Person Info
            //XpoPerson.Session = session;
            //XpoPerson.CriteriaParameters["PersonId"].DefaultValue = PersonId.ToString();

            //Load Login Account Infomation
            //XpoLoginAccount.Session = session;
            //XpoLoginAccount.CriteriaParameters["PersonId"].DefaultValue = Guid.Empty.ToString();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GUIContext = new Context();
            }
            //session = XpoHelper.GetNewSession();
            //PersonEdittingXDS.Session = session;
            //XpoLoginAccount.Session = session;
            //XpoDepartment.Session = session;
            //PersonEdittingXDS.CriteriaParameters["PersonId"].DefaultValue = Guid.Empty.ToString();
            //XpoLoginAccount.CriteriaParameters["PersonId"].DefaultValue = Guid.Empty.ToString();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void ASPxCallbackPanel_PopupPerson_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (e.Parameter == null || e.Parameter.Equals(string.Empty))
                return;
            string[] transition = e.Parameter.Split(',');

            switch (transition[0])
            {
                case "Create":
                    GUIContext.State = new PersonCreating(this);
                    break;
                case "Edit":
                    PersonId = Guid.Parse(transition[1].ToString());
                    GUIContext.State = new PersonEditing(this);
                    //ASPxCallbackPanel_PopupPerson.JSProperties.Add("{{\"EndCallback:\"{0}\"\"}}", PersonId);
                    break;
                default:
                    if (GUIContext != null)
                        GUIContext.Request(transition[0], this);
                    break;
            }

            if (transition[0].Equals("Save"))
            {
                ASPxCallbackPanel_PopupPerson.JSProperties.Add("cpIsSaved", true);
            }
        }

        //

        void ClearForm()
        {
            txt_Name.Text = string.Empty;
            txt_Code.Text = string.Empty;
            ASPxTreeList_OfDepartment.ClearNodes();
        }


        protected void ASPxGridView_LoginAccount_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).ForceDataRowType(typeof(TMPLoginAccount));
        }

        protected void ASPxGridView_LoginAccount_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            Temp_LoginAccount.Add(new TMPLoginAccount()
                {
                    LoginAccountId = Guid.NewGuid(),
                    Email = e.NewValues["Email"].ToString()
                });

            ASPxGridView_LoginAccount.DataSource = Temp_LoginAccount;
            ASPxGridView_LoginAccount.DataBind();
            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();
        }

        protected void ASPxGridView_LoginAccount_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            Temp_LoginAccount.Where(x => x.LoginAccountId == Guid.Parse(e.Keys[0].ToString())).FirstOrDefault().Email = e.NewValues["Email"].ToString();
            ASPxGridView_LoginAccount.DataSource = Temp_LoginAccount;
            ASPxGridView_LoginAccount.DataBind();
            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();
        }

        protected void ASPxGridView_LoginAccount_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid key = Guid.Parse(e.Keys[0].ToString());
            var loginAccounts = (from la in session.GetObjectByKey<Person>(PersonId).LoginAccounts
                                 where la.LoginAccountId == key
                                 select la).FirstOrDefault();
            if (loginAccounts != null)
            {
                loginAccounts.RowStatus = Constant.ROWSTATUS_DELETED;
                loginAccounts.Save();
            }
            Temp_LoginAccount.Remove(Temp_LoginAccount.Where(x => x.LoginAccountId == key).FirstOrDefault());
            ASPxGridView_LoginAccount.DataSource = Temp_LoginAccount;
            ASPxGridView_LoginAccount.DataBind();
            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();
        }

        private class TMPLoginAccount
        {
            Guid fLoginAccountId;
            [Key]
            public Guid LoginAccountId
            {
                get { return fLoginAccountId; }
                set { fLoginAccountId = value; }
            }

            string fEmail;
            public string Email
            {
                get { return fEmail; }
                set { fEmail = value; }
            }
        }

        protected void txt_Code_TextChanged(object sender, EventArgs e)
        {
            throw new Exception(txt_Code.Text);
        }

        #region Validation

        protected void txt_Code_Validation(object sender, ValidationEventArgs e)
        {
            string code = txt_Code.Text.Trim();
            if (!code.Equals(string.Empty) && isDupplicateCode(code))
            {
                txt_Code.ErrorText = "Mã người dùng đã được sử dụng";
                txt_Code.IsValid = false;
                txt_Code.Focus();
            }
            else
            {
                txt_Code.ErrorText = string.Empty;
                txt_Code.IsValid = true;
            }
        }

        public bool isDupplicateCode(string code)
        {
            if (GUIContext.GetType() == typeof(PersonEditing))
            {
                Person p = session.GetObjectByKey<Person>(PersonId);
                if (p == null)
                    throw new Exception(String.Format("Không tồn tại PersonId {0}", PersonId));
                string oldcode = p.Code;
                if (oldcode.Equals(code))
                    return false;
            }

            return Util.isExistXpoObject<Person>("Code", code,
                Utility.Constant.ROWSTATUS_INACTIVE,
                Utility.Constant.ROWSTATUS_ACTIVE,
                Utility.Constant.ROWSTATUS_DEFAULT);
        }

        #endregion

        protected void ASPxGridView_LoginAccount_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.Name.Equals("txt_Email"))
            {
                e.Editor.Focus();
            }
        }

    }
}