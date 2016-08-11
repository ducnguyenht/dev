using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using Newtonsoft.Json.Linq;
using ModuleReuse1.BusinessObjects;

namespace PhucSinhFMCG.Office.Module.Controllers.Import.ImportSupplier
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ImportSupplierVC : ViewController
    {
        public ImportSupplierVC()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            SimpleAction simpleAction2 = new SimpleAction(this, "ImportSupplier", DevExpress.Persistent.Base.PredefinedCategory.View);
            simpleAction2.ImageName = "text_Import";
            simpleAction2.Caption = "Import";
            simpleAction2.Execute += ShowDialogImport_Execute;
            TargetObjectType = typeof(Class1);
            //TargetViewId = "ExternalBusinessEntity_ListView_Supplier";
        }
        private void ShowDialogImport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = this.ObjectSpace.CreateNestedObjectSpace();
            ImportDataEXCEL obj = objectSpace.CreateObject<ImportDataEXCEL>();

            DetailView detailView = Application.CreateDetailView(objectSpace, obj);
            detailView.ViewEditMode = ViewEditMode.Edit;
            e.ShowViewParameters.CreatedView = detailView;
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

            DialogController dialogController = Application.CreateController<DialogController>();
            dialogController.AcceptAction.Caption = "Import";
            dialogController.CancelAction.Caption = "Close";
            dialogController.AcceptAction.Executed += ImportJson_Executed;
            //dialogController.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dialogController_Accepting);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Controllers.Add(dialogController);
        }
        private void ImportJson_Executed(object sender, ActionBaseEventArgs e)
        {
            ImportDataEXCEL importData = e.Action.Controller.Frame.View.CurrentObject as ImportDataEXCEL;
            byte[] data = importData.ImportFile.Content;
            string result = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
            dynamic jslstAccountOrder = JArray.Parse(result);
            foreach (var item in jslstAccountOrder)
            {
                if (item!=null)
                {
                    Class1 ebe = ObjectSpace.CreateObject<Class1>();
                    ebe.Code1 = item.STT;
                    ebe.Name2 = item.TENKHACHHANG;
                }
            }
            ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
            View.Refresh();
            //foreach (var joAccountOrder in lstAccountOrder)
            //{
            //    if (joAccountOrder != null)
            //    {
            //        AccountOrder AccountOrder = ObjectSpace.CreateObject<AccountOrder>();
            //        AccountOrder.Name = joAccountOrder.Name;
            //        AccountOrder.Title = " ";
            //        AccountOrder.AllowPosting = joAccountOrder.AllowPosting;
            //        if (joAccountOrder.lstAccountLevel1 != null)
            //        {
            //            foreach (var joAccountLevel1 in joAccountOrder.lstAccountLevel1)
            //            {
            //                Account AccountLevel1 = ObjectSpace.CreateObject<Account>();
            //                AccountLevel1.Name = joAccountLevel1.Name;
            //                AccountLevel1.Title = joAccountLevel1.Description;
            //                AccountLevel1.Level = 1;
            //                AccountLevel1.AllowPosting = joAccountLevel1.AllowPosting;
            //                AccountLevel1.AccountType = (AccountType)Enum.Parse(typeof(AccountType), joAccountLevel1.AccountType.ToString());
            //                if (joAccountLevel1.lstAccountLevel2 != null)
            //                {
            //                    foreach (var joAccountLevel2 in joAccountLevel1.lstAccountLevel2)
            //                    {
            //                        Account AccountLevel2 = ObjectSpace.CreateObject<Account>();
            //                        AccountLevel2.Name = joAccountLevel2.Name;
            //                        AccountLevel2.Title = joAccountLevel2.Description;
            //                        AccountLevel2.Level = 2;
            //                        AccountLevel2.AllowPosting = joAccountLevel2.AllowPosting;
            //                        AccountLevel2.ParentAccount = AccountLevel1;
            //                        AccountLevel2.AccountType = (AccountType)Enum.Parse(typeof(AccountType), joAccountLevel2.AccountType.ToString());
            //                        if (joAccountLevel2.lstCustomAccount != null)
            //                        {
            //                            foreach (var joCustomAccount in joAccountLevel2.lstCustomAccount)
            //                            {
            //                                Account CustomAccount = ObjectSpace.CreateObject<Account>();
            //                                CustomAccount.Name = joCustomAccount.Name;
            //                                CustomAccount.Title = joCustomAccount.Description;
            //                                CustomAccount.Level = 3;
            //                                CustomAccount.AllowPosting = joCustomAccount.AllowPosting;
            //                                CustomAccount.AccountType = (AccountType)Enum.Parse(typeof(AccountType), joCustomAccount.AccountType.ToString());
            //                                CustomAccount.ParentAccount = AccountLevel2;
            //                            }
            //                        }
            //                    }
            //                }
            //                AccountLevel1.AccountOrder = AccountOrder;
            //            }
            //        }
            //        ObjectSpace.CommitChanges();
            //    }
            //}
           
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
