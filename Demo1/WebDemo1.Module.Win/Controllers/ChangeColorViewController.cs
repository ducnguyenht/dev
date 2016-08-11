using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Drawing;

namespace WebDemo1.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ChangeColorViewController : ViewController
    {
        public ChangeColorViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            // Target required Views (via the TargetXXX properties) and create their Actions.
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

        private void ChangeColorViewController_ViewControlsCreated(object sender, EventArgs e)
        {
            GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
            if (listEditor != null)
            {
                GridView gridView = listEditor.GridView;
                gridView.OptionsView.EnableAppearanceEvenRow = true;
                gridView.Appearance.EvenRow.BackColor = Color.Gray;
                //gridView.OptionsView.EnableAppearanceOddRow = true;
                //gridView.Appearance.OddRow.BackColor = Color.Red;//.FromArgb(244, 244, 244);
            }
        }

        private void ChangeColorViewController_Activated(object sender, EventArgs e)
        {
            FilterController standardFilterController = Frame.GetController<FilterController>();
            if (standardFilterController != null)
            {
                standardFilterController.CustomGetFullTextSearchProperties +=
                    new EventHandler<CustomGetFullTextSearchPropertiesEventArgs>(
                    standardFilterController_CustomGetFullTextSearchProperties);
            }
        }

        public ColumnFilterMode FilterMode { get; set; }

        private void standardFilterController_CustomGetFullTextSearchProperties(
        object sender, CustomGetFullTextSearchPropertiesEventArgs e)
        {
            GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
        }
    }
}