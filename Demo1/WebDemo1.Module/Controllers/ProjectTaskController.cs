using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using System;
using WebDemo1.Module.BusinessObjects.WebDemoORMDataModelCode;

namespace WebDemo1.Module.Controllers
{
    // Add-> New Item -> Dev Express XAF
    // DeV Express 13.2 XAF View Controller
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ProjectTaskController : ViewController
    {
        public ProjectTaskController()
        {
            // Controller targets the ProjectTask business class
            //(the target type is specified via the ViewController.TargetObjectType property)
            TargetObjectType = typeof(ProjectTask);
            //This Controller will be active for both list and detail data screens
            //if the ViewController.TargetViewType property is set to Any
            TargetViewType = ViewType.Any;
            //add simple action to this controller
            SimpleAction markCompleteAction = new SimpleAction(
                this,
                "MarkCompleted",
                PredefinedCategory.RecordEdit
            )
            {
                //action chi thuc thi khi Status != Completed
                TargetObjectsCriteria = (
                    CriteriaOperator.Parse("Status!=?", ProjectTaskStatus.Completed)
                ).ToString(),
                ConfirmationMessage = "Are u sure to mark selected task as 'Completed' ?",
                ImageName = "State_Task_Completed"
            };
            //kiem tra property dc thay doi
            //luu thay doi
            //refesh lai view hien tai
            markCompleteAction.Execute += markCompleteAction_Execute;
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        private void markCompleteAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (ProjectTask projectTask in e.SelectedObjects)
            {
                projectTask.EndDate = DateTime.Now;
                projectTask.Status = ProjectTaskStatus.Completed;
                View.ObjectSpace.SetModified(projectTask);
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
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