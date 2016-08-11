using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using SercurityModule.BusinessObjects.Security;
using DevExpress.ExpressApp.Security.Strategy;
//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.Security.Strategy;
//using SercurityModule.Module.BusinessObjects;

namespace SercurityModule.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
           

            base.UpdateDatabaseAfterUpdateSchema();
            CreateLogin();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        private void CreateLogin()
        {
            #region  CreateDefaultAdminRole
            ExtendedSecurityRole adminRole = ObjectSpace.FindObject<ExtendedSecurityRole>(
                new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<ExtendedSecurityRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.IsAdministrative = true;
            }
            #endregion  CreateDefaultAdminRole

            #region CreateDefaultUserRole
            ExtendedSecurityRole userRole = ObjectSpace.FindObject<ExtendedSecurityRole>(
                new BinaryOperator("Name", "User"));
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<ExtendedSecurityRole>();
                userRole.Name = "User";
                SecuritySystemTypePermissionObject userTypePermission =
                    ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();

                userTypePermission.TargetType = typeof(SecuritySystemUser);
                SecuritySystemObjectPermissionsObject currentUserObjectPermission =
                    ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                currentUserObjectPermission.Criteria = "[Oid] = CurrentUserId()";
                currentUserObjectPermission.AllowNavigate = true;
                currentUserObjectPermission.AllowRead = true;

                userTypePermission.ObjectPermissions.Add(currentUserObjectPermission);
                userRole.TypePermissions.Add(userTypePermission);
            }
            #endregion CreateDefaultUserRole

            #region CreateDefaultAdminAccount
            SecurityApplicationUser defaultEmployeeUserAccount = ObjectSpace.FindObject<SecurityApplicationUser>(new BinaryOperator("UserName", "admin"));
            if (defaultEmployeeUserAccount == null)
            {
                defaultEmployeeUserAccount = ObjectSpace.CreateObject<SecurityApplicationUser>();
                defaultEmployeeUserAccount.UserName = "admin";
                defaultEmployeeUserAccount.ExtendedSecurityRoles.Add(adminRole);
                defaultEmployeeUserAccount.SetPassword("@123456");
            }
            #endregion CreateDefaultAdminAccount
        }
    }
}
