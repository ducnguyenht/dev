using System;
using System.Linq;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security;
using System.Collections.ObjectModel;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base.Security;
using DevExpress.Data.Filtering;


namespace SercurityModule.BusinessObjects.Security
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty("UserName")]
    public class SecurityApplicationUser : BaseObject,
        ISecurityUser,
        IAuthenticationStandardUser,
        IOperationPermissionProvider,
        ISecurityUserWithRoles
    {
        public SecurityApplicationUser(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //ExtendedSecurityRoles.AddRange(new XPCollection<ExtendedSecurityRole>(this.Session,
            //    CriteriaOperator.And(new BinaryOperator("Name", "User"))));
        }



        private string _UserName;
        private bool _IsActive = true;




        #region Associations

        [RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save,
            TargetCriteria = "IsActive",
            CustomMessageTemplate = "An active account must have at least one role assigned")]
        [Association("SecurityApplicationUser-ExtendedSecurityRoles")]
        public XPCollection<ExtendedSecurityRole> ExtendedSecurityRoles
        {
            get
            {
                return GetCollection<ExtendedSecurityRole>("ExtendedSecurityRoles");
            }
        }
        #endregion

        #region ISecurityUser
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetPropertyValue("IsActive", ref _IsActive, value); }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                SetPropertyValue("UserName", ref _UserName, value);
            }
        }
        #endregion

        #region IAuthenticationStandardUser
        private bool changePasswordOnFirstLogon;
        public bool ChangePasswordOnFirstLogon
        {
            get { return changePasswordOnFirstLogon; }
            set
            {
                SetPropertyValue("ChangePasswordOnFirstLogon", ref changePasswordOnFirstLogon, value);
            }
        }

        private string storedPassword;
        [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
        protected string StoredPassword
        {
            get { return storedPassword; }
            set { storedPassword = value; }
        }


        public bool ComparePassword(string password)
        {
            return SecurityUserBase.ComparePassword(this.storedPassword, password);
        }

        public void SetPassword(string password)
        {
            this.storedPassword = new PasswordCryptographer().GenerateSaltedPassword(password);
            OnChanged("StoredPassword");
        }
        #endregion

        #region IOperationPermissionProvider
        protected virtual IEnumerable<ISecurityRole> GetSecurityRoles()
        {
            IList<ISecurityRole> result = new List<ISecurityRole>();
            foreach (ExtendedSecurityRole role in ExtendedSecurityRoles)
            {
                result.Add(role);
            }
            return new ReadOnlyCollection<ISecurityRole>(result);
        }

        protected virtual IEnumerable<IOperationPermission> GetPermissions()
        {
            return new IOperationPermission[0];
        }
        protected virtual IEnumerable<IOperationPermissionProvider> GetChildPermissionProviders()
        {
            return new EnumerableConverter<IOperationPermissionProvider, ExtendedSecurityRole>(ExtendedSecurityRoles);
        }

        IEnumerable<IOperationPermission> IOperationPermissionProvider.GetPermissions()
        {
            return GetPermissions();
        }
        IEnumerable<IOperationPermissionProvider> IOperationPermissionProvider.GetChildren()
        {
            return GetChildPermissionProviders();
        }
        #endregion

        #region ISecurityUserWithRoles
        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (ExtendedSecurityRole role in GetSecurityRoles())
                {
                    result.Add(role);
                }
                return new ReadOnlyCollection<ISecurityRole>(result);
            }
        }
        #endregion



        private bool _IsExtractData;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsExtractData
        {
            get
            {
                return _IsExtractData;
            }
            set
            {
                SetPropertyValue("IsExtractData", ref _IsExtractData, value);
            }
        }



        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public bool IsAdmin
        {
            get
            {
                try
                {
                    if (ExtendedSecurityRoles.Where(w => w.IsAdministrative == true).Count() != 0)
                        return true;
                    return false;
                }
                catch { return false; }
            }
        }


    }
}
