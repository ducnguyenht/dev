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
using WebDemo1.Module.BusinessObjects.WebDemoORMDataModelCode;
//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.Security.Strategy;
//using WebDemo1.Module.BusinessObjects;

namespace WebDemo1.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        //public override void UpdateDatabaseAfterUpdateSchema()
        //{
        //    base.UpdateDatabaseAfterUpdateSchema();
        //    //string name = "MyName";
        //    //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
        //    //if(theObject == null) {
        //    //    theObject = ObjectSpace.CreateObject<DomainObject1>();
        //    //    theObject.Name = name;
        //    //}
        //}
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            if (ObjectSpace.CanInstantiate(typeof(Project)))
            {
                Project project = ObjectSpace.FindObject<Project>(
                    new BinaryOperator("Name", "DevExpress XAF Features Overview"));
                if (project == null)
                {
                    project = ObjectSpace.CreateObject<Project>();
                    project.Name = "DevExpress XAF Features Overview";
                }
                ObjectSpace.CommitChanges();
            }
            if (ObjectSpace.CanInstantiate(typeof(Customer)))
            {
                Customer customer = ObjectSpace.FindObject<Customer>(
                    CriteriaOperator.Parse("FirstName == 'Robert' && LastName == 'Anderson'"));
                if (customer == null)
                {
                    customer = ObjectSpace.CreateObject<Customer>();
                    customer.FirstName = "Robert";
                    customer.LastName = "Anderson";
                    customer.Company = "Coprocess, ZeroSharp";
                }
                ObjectSpace.CommitChanges();
            }
        }
    }
}
