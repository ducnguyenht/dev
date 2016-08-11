using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.ReportsV2;
using WebDemo1.Module.Report;
using WebDemo1.Module.BusinessObjects.DBWebDemo;

namespace WebDemo1.Module
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppModuleBasetopic.
    public sealed partial class WebDemo1Module : ModuleBase
    {
        public WebDemo1Module()
        {
            InitializeComponent();
        }
        //b3 
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            PredefinedReportsUpdater predefinedReportsUpdater =
                new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);
           //predefinedReportsUpdater.AddPredefinedReport<ReportPhieuNhapKho>("Phieu nhap kho", typeof(Employee), true);
           //b3
            predefinedReportsUpdater.AddPredefinedReport<ReportPhieuNhapKho>("Phieu nhap kho 2", typeof(Employee), typeof(ReportPhieuNhapKho_Param), false);
            return new ModuleUpdater[] { updater, predefinedReportsUpdater };

        }
        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
    }
}
