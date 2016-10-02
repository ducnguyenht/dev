using System;
using DevExpress.Xpo;
using System.Drawing;
using System.Security;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Base.Security;

namespace WinWebSolution.Module {
    [NavigationItem]
    [ImageName("BO_User")]
    [DefaultProperty("UserName")]
    public class Employee : BasePersistentObject, IPerson, IUser, IUserWithRoles, IAuthenticationActiveDirectoryUser, IAuthenticationStandardUser, IResource {
        private string _FirstName;
        private string _Email;
        private string _LastName;
        private string _MiddleName;
        private DateTime _Birthday;
        private bool _IsActive = true;
        private bool _ChangePasswordOnFirstLogon;
        private List<IPermission> _Permissions;
        [Persistent("Color")]
        private int _Color;
        private string _Caption;
        private string _UserName;
        private string _StoredPassword;
        public Employee(Session session)
            : base(session) {
            _Permissions = new List<IPermission>();
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            _Color = Color.White.ToArgb();
        }
        [NonPersistent]
        public Color Color {
            get { return Color.FromArgb(_Color); }
            set { SetPropertyValue("Color", ref _Color, value.ToArgb()); }
        }
        [Association("Employee-Group", UseAssociationNameAsIntermediateTableName = true)]
        [RuleRequiredField(null, DefaultContexts.Save, TargetCriteria = "IsActive", CustomMessageTemplate = "You cannot save an Active employee without Groups")]
        public XPCollection<Group> Groups {
            get { return GetCollection<Group>("Groups"); }
        }
        [Association("Activity-Employees", UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Activity> Activities {
            get { return GetCollection<Activity>("Activities"); }
        }
        [MemberDesignTimeVisibility(false)]
        public string StoredPassword {
            get { return _StoredPassword; }
            set { SetPropertyValue("StoredPassword", ref _StoredPassword, value); }
        }
        #region IUserWithRoles Members
        IList<IRole> IUserWithRoles.Roles {
            get { return new ListConverter<IRole, Group>(Groups); }
        }
        #endregion
        #region IUser Members
        public bool IsActive {
            get { return _IsActive; }
            set { SetPropertyValue("IsActive", ref _IsActive, value); }
        }
        public IList<IPermission> Permissions {
            get {
                _Permissions.Clear();
                foreach (IRole role in Groups)
                    _Permissions.AddRange(role.Permissions);
                return _Permissions.AsReadOnly();
            }
        }
        public void ReloadPermissions() {
            Groups.Reload();
            foreach (Group group in Groups)
                group.PersistentPermissions.Reload();
        }
        string IUser.UserName {
            get { return UserName; }
        }
        #endregion
        #region IAuthenticationActiveDirectoryUser Members
        string IAuthenticationActiveDirectoryUser.UserName {
            get { return UserName; }
            set { UserName = value; }
        }
        #endregion
        #region IAuthenticationStandardUser Members
        [RuleRequiredField(null, "Save", "The user name must not be empty")]
        [RuleUniqueValue(null, "Save", "The login with the entered UserName was already registered within the system")]
        public string UserName {
            get { return _UserName; }
            set { SetPropertyValue("UserName", ref _UserName, value); }
        }
        public bool ChangePasswordOnFirstLogon {
            get { return _ChangePasswordOnFirstLogon; }
            set { SetPropertyValue("ChangePasswordOnFirstLogon", ref _ChangePasswordOnFirstLogon, value); }
        }
        public bool ComparePassword(string password) {
            return new PasswordCryptographer().AreEqual(StoredPassword, password);
        }
        public void SetPassword(string password) {
            StoredPassword = new PasswordCryptographer().GenerateSaltedPassword(password);
        }
        #endregion
        #region IResource Members
        public string Caption {
            get { return _Caption; }
            set { SetPropertyValue("Caption", ref _Caption, value); }
        }
        [Browsable(false)]
        public object Id { get { return Oid; } }
        [Browsable(false)]
        public int OleColor {
            get { return ColorTranslator.ToOle(Color.FromArgb(_Color)); }
        }
        #endregion
        #region IPerson Members
        public string FirstName {
            get { return _FirstName; }
            set { SetPropertyValue("FirstName", ref _FirstName, value); }
        }
        public string LastName {
            get { return _LastName; }
            set { SetPropertyValue("LastName", ref _LastName, value); }
        }
        public string MiddleName {
            get { return _MiddleName; }
            set { SetPropertyValue("MiddleName", ref _MiddleName, value); }
        }
        public DateTime Birthday {
            get { return _Birthday; }
            set { SetPropertyValue("Birthday", ref _Birthday, value); }
        }
        [SizeAttribute(255)]
        public string Email {
            get { return _Email; }
            set { SetPropertyValue("Email", ref _Email, value); }
        }
        [PersistentAlias("concat(FirstName, MiddleName, LastName)")]
        [SearchMemberOptions(SearchMemberMode.Include)]
        public string FullName {
            get { return Convert.ToString(EvaluateAlias("FullName")); }
        }
        #endregion
    }
}