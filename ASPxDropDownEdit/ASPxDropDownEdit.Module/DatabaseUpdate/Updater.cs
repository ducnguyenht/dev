using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using ASPxDropDownEdit.Module.BusinessObjects;

namespace ASPxDropDownEdit.Module.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            CreateDetail();
        }


        private void CreateDetail()
        {
           if (ObjectSpace.FindObject<Master>(null) == null)
           {
               Master master = ObjectSpace.CreateObject<Master>();
               master.MasterName = "Master";
               master.Save();

                for (int i = 0; i < 1000; i++)
                {
                    Detail line = ObjectSpace.CreateObject<Detail>();
                    line.DetailCode = i.ToString();
                }
           }
        }
    }
}
