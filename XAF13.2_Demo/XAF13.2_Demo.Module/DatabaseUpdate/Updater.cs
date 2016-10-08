//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.Security.Strategy;
//using XAF13._2_Demo.Module.BusinessObjects;
using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using XAF13_2_Demo.Module.BusinessObjects;

namespace XAF13_2_Demo.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater
    {
        private readonly XafApplication _Application;

        public Updater(XafApplication application, IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
            _Application = application;
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            CreateCalculationProxy(SumMode.PureClientSide);
            CreateCalculationProxy(SumMode.ClientSide);
            CreateCalculationProxy(SumMode.ServerSide);

            const int objectCount = 1000;

            var os = _Application.ObjectSpaceProvider.CreateUpdatingObjectSpace(false);

            var overAllCount = os.GetObjectsCount(typeof(LargeBusinessObject), null);

            if (overAllCount == objectCount)
            {
                os.Dispose();
                return;
            }
            
            for (int i = 1; i <= objectCount; i++)
            {
                var name = "Item" + i;

                var count = os.GetObjectsCount(typeof(LargeBusinessObject), CriteriaOperator.Parse("Name = ?", name));

                var title = "Object " + i + " of " + objectCount;

                this.UpdateStatus("DBUpdate", title, "Searching the " + name + " object");

                if (count <= 0)
                {
                    this.UpdateStatus("DBUpdate", title, "Creating the " + name + " object");
                    var obj = os.CreateObject<LargeBusinessObject>();
                    obj.Name = name;
                    obj.IntPropertyToCalculate = i;

                    obj.Property1 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property2 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property3 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property4 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property5 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property6 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property7 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property8 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property9 = NLipsum.Core.LipsumGenerator.Generate(100);
                    obj.Property10 = NLipsum.Core.LipsumGenerator.Generate(100);
                }

                if (i % 100 == 0)
                {
                    if (os.IsModified)
                    {
                        this.UpdateStatus("DBUpdate", title, "Committing changes");    

                        os.CommitChanges();

                        if (os != null)
                            os.Dispose();

                        os = _Application.ObjectSpaceProvider.CreateUpdatingObjectSpace(false);    
                    }
                }
            }

            if (!os.IsDisposed)
            {
                os.CommitChanges();
                os.Dispose();    
            }
        }

        private void CreateCalculationProxy(SumMode mode)
        {
            UpdateStatus("DBUpdate", "Proxy", "Creating and searching proxy in mode {0}", mode);

            var criteria = CriteriaOperator.Parse("SumMode == ?", mode);

            var proxy =
                ObjectSpace.FindObject<XPViewCalculationProxy>(criteria);

            if (proxy == null)
            {
                proxy = ObjectSpace.CreateObject<XPViewCalculationProxy>();
                proxy.SumMode = mode;
                ObjectSpace.CommitChanges();
            }
        }
    }
}
