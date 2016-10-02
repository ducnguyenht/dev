using System;
using System.Drawing;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;

namespace WinWebSolution.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            // If a user named 'Sam' doesn't exist in the database, create this user
            Employee user1 = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", "Sam"));
            if (user1 == null) {
                user1 = ObjectSpace.CreateObject<Employee>();
                user1.Color = Color.Red;
                user1.UserName = "Sam";
                user1.FirstName = "Sam";
                user1.Caption = user1.UserName;
                // Set a password if the standard authentication type is used
                user1.SetPassword(null);
            }
            // If a user named 'John' doesn't exist in the database, create this user
            Employee user2 = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", "John"));
            if (user2 == null) {
                user2 = ObjectSpace.CreateObject<Employee>();
                user2.Color = Color.Green;
                user2.UserName = "John";
                user2.FirstName = "John";
                user2.Caption = user2.UserName;
                // Set a password if the standard authentication type is used
                user2.SetPassword(null);
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            Group adminRole = ObjectSpace.FindObject<Group>(new BinaryOperator("Name", Group.DefaultAdministratorsGroupName));
            if (adminRole == null) {
                adminRole = ObjectSpace.CreateObject<Group>();
                adminRole.Name = Group.DefaultAdministratorsGroupName;
            }
            // If a role with the Users name doesn't exist in the database, create this role
            Group userRole = ObjectSpace.FindObject<Group>(new BinaryOperator("Name", Group.DefaultUsersGroupName));
            if (userRole == null) {
                userRole = ObjectSpace.CreateObject<Group>();
                userRole.Name = Group.DefaultUsersGroupName;
            }
            // Delete all permissions assigned to the Administrators and Users roles
            while (adminRole.PersistentPermissions.Count > 0) {
                ObjectSpace.Delete(adminRole.PersistentPermissions[0]);
            }
            while (userRole.PersistentPermissions.Count > 0) {
                ObjectSpace.Delete(userRole.PersistentPermissions[0]);
            }
            // Allow full access to all objects to the Administrators role
            adminRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
            // Allow editing the application model to the Administrators role
            adminRole.AddPermission(new EditModelPermission(ModelAccessModifier.Allow));
            // Save the Administrators role to the database
            adminRole.Save();
            // Allow full access to all objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
            // Deny full access to the User type objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(Employee),
               ObjectAccess.ChangeAccess, ObjectAccessModifier.Deny));
            userRole.AddPermission(new ObjectAccessPermission(typeof(Group),
               ObjectAccess.AllAccess, ObjectAccessModifier.Deny));
            // Deny editing the application model to the Users role
            userRole.AddPermission(new EditModelPermission(ModelAccessModifier.Deny));
            // Save the Users role to the database
            userRole.Save();
            // Add the Administrators role to the user1
            user1.Groups.Add(adminRole);
            // Add the Users role to the user2
            user2.Groups.Add(userRole);
            // Save the users to the database
            user1.Save();
            user2.Save();
        }
    }
}
