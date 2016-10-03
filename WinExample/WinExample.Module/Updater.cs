using DevExpress.ExpressApp;
using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;

namespace WinExample.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null) {
                adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                adminRole.Name = "Administrators";
                adminRole.IsAdministrative = true;
            }
            SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));
            if (userRole == null) {
                userRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                userRole.Name = "Users";
                userRole.SetTypePermissionsRecursively<object>(SecurityOperations.FullAccess, SecuritySystemModifier.Allow);
            }
            MyUser user1 = ObjectSpace.FindObject<MyUser>(new BinaryOperator("UserName", "Admin"));
            if (user1 == null) {
                user1 = ObjectSpace.CreateObject<MyUser>();
                user1.UserName = "Admin";
                user1.SetPassword("");
                user1.Roles.Add(adminRole);
            }
            MyUser user2 = ObjectSpace.FindObject<MyUser>(new BinaryOperator("UserName", "User"));
            if (user2 == null) {
                user2 = ObjectSpace.CreateObject<MyUser>();
                user2.UserName = "User";
                user2.SetPassword("");
                user2.Roles.Add(userRole);
            }
        }
    }
}
