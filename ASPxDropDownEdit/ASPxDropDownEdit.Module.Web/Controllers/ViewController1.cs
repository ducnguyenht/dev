//using System;
//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.ExpressApp.SystemModule;
//using DevExpress.ExpressApp.Web.Editors.ASPx;
//using DevExpress.Web.ASPxGridView;

//namespace ASPxDropDownEdit.Module.Web.Controllers 
//{
//    public class ViewController1 : ViewController 
//    {
        
//        public ViewController1() {
//            TargetViewId = "Detail_LookupListView";
//        }
    
//        //protected override void OnActivated() {
//        //    base.OnActivated();
//        //    Frame.GetController<FilterController>().FullTextFilterAction.Executed += FullTextFilterAction_Executed;
//        //    Frame.GetController<FilterController>().FullTextFilterAction.ValueChanged += FullTextFilterAction_ValueChanged;
//        //}

//        //void FullTextFilterAction_ValueChanged(object sender, EventArgs e)
//        //{
//        //    var action = sender as ParametrizedAction;
//        //    if (action.Value != null && action.Value.ToString() != action.Value.ToString().ToUpper())
//        //    {
//        //        action.ValueChanged -= FullTextFilterAction_ValueChanged;
//        //        action.Value = action.Value.ToString().ToUpper();
//        //        action.ValueChanged += FullTextFilterAction_ValueChanged;
//        //    }
//        //}

//        //protected override void OnDeactivated()
//        //{
//        //    Frame.GetController<FilterController>().FullTextFilterAction.Executed -= FullTextFilterAction_Executed;
//        //    Frame.GetController<FilterController>().FullTextFilterAction.ValueChanged -= FullTextFilterAction_ValueChanged;
//        //    base.OnDeactivated();
//        //}

//        ASPxGridView grid;
//        //void FullTextFilterAction_Executed(object sender, ActionBaseEventArgs e) 
//        //{
//        //    if (grid != null)
//        //    {
//        //        // focus grid after search is completed
//        //        grid.Focus();
//        //    }
//        //}
        
//        protected override void OnViewControlsCreated() 
//        {
//            base.OnViewControlsCreated();
//            ASPxGridListEditor gridEditor = ((ListView)View).Editor as ASPxGridListEditor;
//            if (gridEditor != null) {
//                grid = gridEditor.Grid;
//                grid.KeyboardSupport = true;
//                grid.SelectionChanged += new EventHandler(grid_SelectionChanged);
//            }
//        }

//        void grid_SelectionChanged(object sender, EventArgs e) 
//        {
//            DialogController dc = Frame.GetController<DialogController>();
//            if (dc.AcceptAction.Active && dc.AcceptAction.Enabled) {
//                Frame.GetController<DialogController>().AcceptAction.DoExecute();
//            }
//        }
//    }
//}