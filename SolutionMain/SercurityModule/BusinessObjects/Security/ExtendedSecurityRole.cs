using System;
using System.Linq;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace SercurityModule.BusinessObjects.Security
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty("Roles")]
    public class ExtendedSecurityRole : SecuritySystemRoleBase
    {
        public ExtendedSecurityRole(Session session)
            : base(session)
        {
        }




        private string _HiddenNavigationItems;

        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false)]
        public string HiddenNavigationItems
        {
            get { return _HiddenNavigationItems; }
            set { SetPropertyValue("HiddenNavigationItems", ref _HiddenNavigationItems, value); }
        }


        private string _HiddenActions;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false)]
        public string HiddenActions
        {
            get { return _HiddenActions; }
            set { SetPropertyValue("HiddenActions", ref _HiddenActions, value); }
        }


        protected override IEnumerable<IOperationPermission> GetPermissionsCore()
        {
            List<IOperationPermission> result = new List<IOperationPermission>();
            result.AddRange(base.GetPermissionsCore());
            if (!String.IsNullOrEmpty(HiddenNavigationItems))
            {
                foreach (string hiddenNavigationItem in HiddenNavigationItems.Split(';', ','))
                {
                    //result.Add(new NavigationItemPermission(hiddenNavigationItem.Trim()));
                }
            }
            if (!String.IsNullOrEmpty(HiddenActions))
            {
                foreach (string hiddenAction in HiddenActions.Split(';', ','))
                {
                    //result.Add(new HideActionByIDPermission(hiddenAction.Trim()));
                }
            }
            return result;
        }

        protected override IEnumerable<IOperationPermissionProvider> GetChildrenCore()
        {
            List<IOperationPermissionProvider> result = new List<IOperationPermissionProvider>();
            result.AddRange(base.GetChildrenCore());
            result.AddRange(new EnumerableConverter<IOperationPermissionProvider, ExtendedSecurityRole>(ChildRoles));
            return result;
        }

        [Association("ParentRoles-ChildRoles")]
        public XPCollection<ExtendedSecurityRole> ChildRoles
        {
            get { return GetCollection<ExtendedSecurityRole>("ChildRoles"); }
        }

        [Association("ParentRoles-ChildRoles")]
        public XPCollection<ExtendedSecurityRole> ParentRoles
        {
            get { return GetCollection<ExtendedSecurityRole>("ParentRoles"); }
        }

        [Association("SecurityApplicationUser-ExtendedSecurityRoles")]
        public XPCollection<SecurityApplicationUser> EmployeeUserAccounts
        {
            get
            {
                return GetCollection<SecurityApplicationUser>("EmployeeUserAccounts");
            }
        }
    }
}
