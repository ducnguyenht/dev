using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win.Editors;
using System.Drawing.Printing;
using WebDemo1.Module.BusinessObjects;

namespace WebDemo1.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ViewController1 : ViewController
    {
        public ViewController1()
        {
            SimpleAction simpleAction1 = new SimpleAction(this, "ExportMaterial", DevExpress.Persistent.Base.PredefinedCategory.View);
            simpleAction1.ImageName = "text_imports";
            simpleAction1.Caption = "In";
            simpleAction1.Execute += simpleAction1_Execute;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            XtraReport11 r = new XtraReport11();
            GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
            //InTrucTiepGridControl.XemVaIn(listEditor.Grid, r, PaperKind.A4, false);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}