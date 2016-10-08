using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Updating;

namespace XAF13_2_Demo.Module.DatabaseUpdate
{
    public class NotificationUpdater : ModuleUpdater
    {
        public NotificationUpdater(IObjectSpace objectSpace, Version currentDBVersion)
            : base(objectSpace, currentDBVersion)
        {
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            UpdateStatus("DBUpdater", "UpdateDatabaseBeforeUpdateSchema", "Before updating the schema");
            base.UpdateDatabaseBeforeUpdateSchema();
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            UpdateStatus("DBUpdater", "UpdateDatabaseAfterUpdateSchema", "After updating the schema");
            base.UpdateDatabaseAfterUpdateSchema();

            //var role = ObjectSpace.FindObject<SecuritySystemRole>(CriteriaOperator.Parse("Name == ?", "Administrators"));
            
            //if (role == null)
            //{
            //    role = ObjectSpace.CreateObject<SecuritySystemRole>();
            //    role.Name = "Administrators";
            //    role.IsAdministrative = true;

            //    ObjectSpace.CommitChanges();
            //}

            //var user = ObjectSpace.FindObject<SecuritySystemUser>(CriteriaOperator.Parse("UserName == ?", "Administrator"));
            //if (user == null)
            //{
            //    user = ObjectSpace.CreateObject<SecuritySystemUser>();
            //    user.UserName = "Administrator";

            //    user.Roles.Add(role);
            //    ObjectSpace.CommitChanges();
            //}
        }
    }
}
