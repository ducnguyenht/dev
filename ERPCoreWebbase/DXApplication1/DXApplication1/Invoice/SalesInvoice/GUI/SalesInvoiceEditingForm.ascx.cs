using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Invoice;
using NAS.GUI.Pattern;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using NAS.BO.Invoice;
using WebModule.Invoice.SalesInvoice.State;
using NAS.BO.System.ArtifactCode;
using Utility;

namespace WebModule.Invoice.SalesInvoice.GUI
{
    public partial class SalesInvoiceEditingForm : System.Web.UI.UserControl
    {
        public string ClientInstanceName { get; set; }

        public string _ClientInstanceName
        {
            get
            {
                if (ClientInstanceName == null || ClientInstanceName.Trim().Length == 0)
                    return ClientID;
                return ClientInstanceName;
            }
        }

        public string Closing { get; set; }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        //    if (hReportBillId.Contains("id"))
        //    {
        //        XPQuery<BillItem> billItem = new XPQuery<BillItem>(session);
        //        Guid billId = BillId;

        //        var list = billItem.Where(b => b.BillId.BillId == billId).Select(b => new CReportPurchaseInvoice()
        //        {
        //            IssueDate = b.BillId.IssuedDate,
        //            Code = b.ItemUnitId.ItemId.Code,
        //            Name = b.ItemUnitId.ItemId.Name,
        //            Unit = b.ItemUnitId.UnitId.Name,
        //            ManufacturerName = b.ItemUnitId.ItemId.ManufacturerOrgId.Name,
        //            Quantity = b.Quantity
        //        });

        //        List<CReportPurchaseInvoice> lst = new List<CReportPurchaseInvoice>();

        //        try
        //        {
        //            lst = list.ToList();
        //        }
        //        catch
        //        {
        //        }

        //        ReportPurchaseInvoice report = new ReportPurchaseInvoice();

        //        report.DataSource = lst;
        //        report.DataMember = "";

        //        rptReportViewer.Report = report;

        //        cpReportViewer.JSProperties.Add("cpShowReport", "report");
        //    }

            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
                GUIContext = new Context();
                BillId = Guid.Empty;
            }
            if (BillId != null)
            {
                SetControlsProperties();
            }
        }

        private void SetControlsProperties()
        {
            billTotalSummary.BillId = BillId;
            billTotalSummary.UpdateTotalSummary();

            billDetails.BillId = BillId;
            billDetails.BillType = BillType;

            if (!billTotalSummary.IsInCallback)
            {
                billDetails.LoadDataBaseOnCurrentState();
            }

        }

        private Guid BillId
        {
            get { return (Guid)Session["BillId_" + ViewStateControlId]; }
            set { Session["BillId_" + ViewStateControlId] = value; }
        }

        public BillTypeEnum BillType
        {
            get;
            set;
        }

        #region State Pattern
        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get
            {
                return (string)ViewState["ViewStateControlId"];
            }
            set
            {
                ViewState["ViewStateControlId"] = value;
            }
        }

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ViewStateControlId]; }
            set { Session["GUIContext_" + ViewStateControlId] = value; }
        }

        #region UpdateGUI
        public bool SalesInvoiceCanceling_UpdateGUI()
        {
            popupInvoiceEditingForm.ShowOnPageLoad = false;
            return true;
        }
        public bool SalesInvoiceCreating_UpdateGUI()
        {
            popupInvoiceEditingForm.ShowOnPageLoad = true;
            popupInvoiceEditingForm.HeaderText = "Thông tin phiếu bán hàng - Thêm mới";
            ButtonShowBookingEntries.Enabled = false;
            ButtonDeclareTax.Enabled = false;
            ButtonPrint.Enabled = false;
            ButtonCreateInventoryCommand.Enabled = false;
            ButtonCreateVoucher.Enabled = false;
            return true;
        }
        public bool SalesInvoiceEditing_UpdateGUI()
        {
            popupInvoiceEditingForm.ShowOnPageLoad = true;

            NAS.BO.Invoice.SalesInvoiceBO salesInvoiceBO = new NAS.BO.Invoice.SalesInvoiceBO();
            NAS.DAL.Invoice.SalesInvoice bill =
                (NAS.DAL.Invoice.SalesInvoice)salesInvoiceBO.GetBillById(session, BillId);
            popupInvoiceEditingForm.HeaderText = String.Format("Thông tin phiếu bán hàng - {0}", bill.Code);

            ButtonShowBookingEntries.Enabled = true;
            ButtonDeclareTax.Enabled = true;
            ButtonPrint.Enabled = true;
            ButtonCreateInventoryCommand.Enabled = true;
            ButtonCreateVoucher.Enabled = true;
            return true;
        }
        public bool SalesInvoiceLocked_UpdateGUI()
        {
            popupInvoiceEditingForm.ShowOnPageLoad = true;

            NAS.BO.Invoice.SalesInvoiceBO salesInvoiceBO = new NAS.BO.Invoice.SalesInvoiceBO();
            NAS.DAL.Invoice.SalesInvoice bill =
                (NAS.DAL.Invoice.SalesInvoice)salesInvoiceBO.GetBillById(session, BillId);
            popupInvoiceEditingForm.HeaderText = String.Format("Thông tin phiếu bán hàng - {0}", bill.Code);

            //ButtonDeclareTax.Visible = false;
            ButtonSave.Visible = false;

            txtCode.Enabled = false;
            txtIssuedDate.Enabled = false;
            comboOrganization.Enabled = false;
            return true;
        }
        #endregion

        #region CRUD
        public bool SalesInvoiceCanceling_CRUD()
        {
            return true;
        }
        public bool SalesInvoiceCreating_CRUD()
        {
            //Create new temporary bill
            NAS.BO.Invoice.SalesInvoiceBO salesInvoiceBO = new NAS.BO.Invoice.SalesInvoiceBO();
            Bill bill = salesInvoiceBO.InitTemporary(session, BillType);
            BillId = bill.BillId;
            hfBillId["BillId"] = BillId.ToString();
            formlayoutInvoiceEditingForm_LoadData();

            //Get default code
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVOICE_SALE);

            SetControlsProperties();
            return true;
        }
        public bool SalesInvoiceEditing_CRUD()
        {
            //Read bill data
            formlayoutInvoiceEditingForm_LoadData();

            SetControlsProperties();
            return true;
        }
        public bool SalesInvoiceLocked_CRUD()
        {
            //Read bill data
            formlayoutInvoiceEditingForm_LoadData();

            SetControlsProperties();
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool SalesInvoiceCanceling_PreTransitionCRUD(string transition)
        {
            //Delete temporary bill when cancel for Creating state
            NAS.BO.Invoice.SalesInvoiceBO salesInvoiceBO = new NAS.BO.Invoice.SalesInvoiceBO();
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    salesInvoiceBO.Delete(session, BillId);
                    uow.CommitChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool SalesInvoiceCreating_PreTransitionCRUD(string transition)
        {
            //Save data when transition is SAVE
            if (transition.ToUpper().Equals("SAVE"))
            {
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    try
                    {
                        //Get user input data
                        string code = txtCode.Text;
                        DateTime issuedDate = txtIssuedDate.Date;
                        Organization sourceOrganization = null;
                        Person targetOrganization = null;
                        if (comboOrganization.Value != null)
                        {
                            sourceOrganization = uow.GetObjectByKey<Organization>(comboOrganization.Value);
                        }
                        NAS.BO.Invoice.SalesInvoiceBO salesInvoiceBO = new NAS.BO.Invoice.SalesInvoiceBO();
                        salesInvoiceBO.Save(uow, BillId, code, issuedDate, sourceOrganization, targetOrganization);

                        //Update bill actor
                        Guid creatorId = Guid.Empty;
                        Guid salesId = Guid.Empty;
                        Guid chiefAccountantId = Guid.Empty;
                        Guid directorId = Guid.Empty;

                        if (comboCreator.Value != null)
                        {
                            creatorId = (Guid)comboCreator.Value;
                        }
                        if (comboSales.Value != null)
                        {
                            salesId = (Guid)comboSales.Value;
                        }
                        if (comboChiefAccountant.Value != null)
                        {
                            chiefAccountantId = (Guid)comboChiefAccountant.Value;
                        }
                        if (comboDirector.Value != null)
                        {
                            directorId = (Guid)comboDirector.Value;
                        }

                        salesInvoiceBO.UpdateBillActor(uow, BillId, creatorId, salesId, chiefAccountantId, directorId);

                        uow.CommitChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool SalesInvoiceEditing_PreTransitionCRUD(string transition)
        {
            //Save data when transition is SAVE
            if (transition.ToUpper().Equals("SAVE"))
            {
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    try
                    {
                        //Get user input data
                        string code = txtCode.Text;
                        DateTime issuedDate = txtIssuedDate.Date;
                        Organization sourceOrganization = null;
                        Person targetOrganization = null;
                        if (comboOrganization.Value != null)
                        {
                            sourceOrganization = uow.GetObjectByKey<Organization>(comboOrganization.Value);
                        }
                        NAS.BO.Invoice.SalesInvoiceBO salesInvoiceBO = new NAS.BO.Invoice.SalesInvoiceBO();
                        salesInvoiceBO.Save(uow, BillId, code, issuedDate, sourceOrganization, targetOrganization);

                        //Update bill actor
                        Guid creatorId = Guid.Empty;
                        Guid salesId = Guid.Empty;
                        Guid chiefAccountantId = Guid.Empty;
                        Guid directorId = Guid.Empty;

                        if (comboCreator.Value != null)
                        {
                            creatorId = (Guid)comboCreator.Value;
                        }
                        if (comboSales.Value != null)
                        {
                            salesId = (Guid)comboSales.Value;
                        }
                        if (comboChiefAccountant.Value != null)
                        {
                            chiefAccountantId = (Guid)comboChiefAccountant.Value;
                        }
                        if (comboDirector.Value != null)
                        {
                            directorId = (Guid)comboDirector.Value;
                        }

                        salesInvoiceBO.UpdateBillActor(uow, BillId, creatorId, salesId, chiefAccountantId, directorId);

                        uow.CommitChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool SalesInvoiceLocked_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

        private void formlayoutInvoiceEditingForm_LoadData()
        {
            NAS.BO.Invoice.SalesInvoiceBO salesInvoiceBO = new NAS.BO.Invoice.SalesInvoiceBO();
            Bill bill = salesInvoiceBO.GetBillById(session, BillId);
            txtCode.Text = bill.Code;
            txtIssuedDate.Date = bill.IssuedDate;
            if (bill.SourceOrganizationId != null)
            {
                comboOrganization.Value = bill.SourceOrganizationId.OrganizationId;
                comboOrganization.DataBindItems();
            }
            else
            {
                comboOrganization.SelectedIndex = -1;
            }

            int countExistType;
            BillActorType billActorType;
            //Update index of creator combobox
            billActorType = BillActorType.GetDefault(session, BillActorTypeEnum.CREATOR);
            countExistType = bill.BillActors.Count(r => r.BillActorTypeId == billActorType);
            if (countExistType == 0)
            {
                comboCreator.SelectedIndex = -1;
            }
            else
            {
                Person person = bill.BillActors
                    .FirstOrDefault(r => r.BillActorTypeId == billActorType).PersonId;
                if (person == null)
                {
                    comboCreator.SelectedIndex = -1;
                }
                else
                {
                    comboCreator.Value = person.PersonId;
                }
                comboCreator.DataBindItems();
            }

            //Update index of buyer combobox
            billActorType = BillActorType.GetDefault(session, BillActorTypeEnum.SALES);
            countExistType = bill.BillActors.Count(r => r.BillActorTypeId == billActorType);
            if (countExistType == 0)
            {
                comboSales.SelectedIndex = -1;
            }
            else
            {
                Person person = bill.BillActors
                    .FirstOrDefault(r => r.BillActorTypeId == billActorType).PersonId;
                if (person == null)
                {
                    comboSales.SelectedIndex = -1;
                }
                else
                {
                    comboSales.Value = person.PersonId;
                }
                comboSales.DataBindItems();
            }

            //Update index of chief accountant combobox
            billActorType = BillActorType.GetDefault(session, BillActorTypeEnum.CHIEFACCOUNTANT);
            countExistType = bill.BillActors.Count(r => r.BillActorTypeId == billActorType);
            if (countExistType == 0)
            {
                comboChiefAccountant.SelectedIndex = -1;
            }
            else
            {
                Person person = bill.BillActors
                    .FirstOrDefault(r => r.BillActorTypeId == billActorType).PersonId;
                if (person == null)
                {
                    comboChiefAccountant.SelectedIndex = -1;
                }
                else
                {
                    comboChiefAccountant.Value = person.PersonId;
                }
                comboChiefAccountant.DataBindItems();
            }

            //Update index of creator combobox
            billActorType = BillActorType.GetDefault(session, BillActorTypeEnum.DIRECTOR);
            countExistType = bill.BillActors.Count(r => r.BillActorTypeId == billActorType);
            if (countExistType == 0)
            {
                comboDirector.SelectedIndex = -1;
            }
            else
            {
                Person person = bill.BillActors
                    .FirstOrDefault(r => r.BillActorTypeId == billActorType).PersonId;
                if (person == null)
                {
                    comboDirector.SelectedIndex = -1;
                }
                else
                {
                    comboDirector.Value = person.PersonId;
                }
                comboDirector.DataBindItems();
            }
        }

        private ASPxButton ButtonShowBookingEntries
        {
            get
            {
                return popupInvoiceEditingForm.FindControl("btnShowBookingEntries") as ASPxButton;
            }
        }

        private ASPxButton ButtonDeclareTax
        {
            get
            {
                return popupInvoiceEditingForm.FindControl("btnDeclareTax") as ASPxButton;
            }
        }

        private ASPxButton ButtonPrint
        {
            get
            {
                return popupInvoiceEditingForm.FindControl("btnPrint") as ASPxButton;
            }
        }

        private ASPxButton ButtonSave
        {
            get
            {
                return popupInvoiceEditingForm.FindControl("btnSave") as ASPxButton;
            }
        }

        private ASPxButton ButtonCreateInventoryCommand
        {
            get
            {
                return popupInvoiceEditingForm.FindControl("btnCreateInventoryCommand") as ASPxButton;
            }
        }

        private ASPxButton ButtonCreateVoucher
        {
            get
            {
                return popupInvoiceEditingForm.FindControl("btnCreateVoucher") as ASPxButton;
            }
        }

        protected void comboOrganization_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<Organization> collection = new XPCollection<Organization>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            //Get CUSTOMER trading type
            TradingCategory customerTradingCategory =
                session.FindObject<TradingCategory>(new BinaryOperator("Code", "CUSTOMER"));

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                CriteriaOperator.Or(
                //find code contains the filter
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                //find name contains the filter
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                ),
                //find customer and supplier
                new ContainsOperator("OrganizationCategories",
                    CriteriaOperator.And(
                        new BinaryOperator("TradingCategoryId", customerTradingCategory),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                    )
                )
            );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();
        }

        protected void comboOrganization_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            Organization obj = session.GetObjectByKey<Organization>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new Organization[] { obj };
                combo.DataBindItems();
            }
        }

        protected void panelInvoiceEditingForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            SalesInvoiceBO salesInvoiceBO = new SalesInvoiceBO();
            Bill bill = null;
            switch (command)
            {
                case "Create":
                    GUIContext.State = new SalesInvoiceCreating(this);

                    billDetails.InitState();
                    break;
                case "Edit":
                    if (args.Length < 2)
                    {
                        throw new Exception("Invalid parameters");
                    }
                    BillId = Guid.Parse(args[1]);
                    //Determine bill status
                    hfBillId["BillId"] = BillId.ToString();
                    bill = salesInvoiceBO.GetBillById(session, BillId);
                    if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                    {
                        GUIContext.State = new SalesInvoiceLocked(this);
                    }
                    else
                    {
                        GUIContext.State = new SalesInvoiceEditing(this);
                    }

                    billDetails.InitState();
                    break;
                case "Save":
                    GUIContext.Request(command, this);

                    bill = salesInvoiceBO.GetBillById(session, BillId);
                    if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                    {
                        GUIContext.State = new SalesInvoiceLocked(this);
                    }
                    break;
                case "Cancel":
                    GUIContext.Request(command, this);
                    panelInvoiceEditingForm.JSProperties["cpEvent"] = "Closing";
                    break;
                case "Refresh":
                    bill = salesInvoiceBO.GetBillById(session, BillId);
                    if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                    {
                        GUIContext.State = new SalesInvoiceLocked(this);
                    }
                    else
                    {
                        GUIContext.State = new SalesInvoiceEditing(this);
                    }
                    break;
                default:
                    break;
            }

            uEdittingOutputInventoryCommand1.SettingInit<NAS.DAL.Invoice.SalesInvoice>(BillId, ButtonCreateInventoryCommand);
        }

        protected void cpReportViewer_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        private void txtCode_SetValidation(ValidationEventArgs e, bool isValid, string errorText)
        {
            e.IsValid = isValid;
            e.ErrorText = errorText;
        }

        private void txtCode_SetExistValidation(ValidationEventArgs e, bool isCodeExist)
        {
            if (isCodeExist)
            {
                txtCode_SetValidation(e, false, String.Format("Mã hóa đơn '{0}' đã được sử dụng", e.Value));
            }
            else
            {
                txtCode_SetValidation(e, true, String.Empty);
            }
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            string code = e.Value.ToString().Trim();
            //New mode
            if (GUIContext.State is SalesInvoiceCreating)
            {
                bool isExist = Util.isExistXpoObject<Bill>("Code", code,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, 
                            Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_BOOKED_ENTRY);
                txtCode_SetExistValidation(e, isExist);
            }
            //Edit mode  
            else
            {
                SalesInvoiceBO salesInvoiceBO = new SalesInvoiceBO();
                //Validate if new code not equal old code
                if (!code.Equals(salesInvoiceBO.GetBillById(session, BillId).Code))
                {
                    bool isExist = Util.isExistXpoObject<Bill>("Code", code,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT,
                            Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_BOOKED_ENTRY);
                    txtCode_SetExistValidation(e, isExist);
                }
            }
        }

        protected void comboPerson_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            Person obj = session.GetObjectByKey<Person>(e.Value);
            if (obj != null)
            {
                combo.DataSource = new Person[] { obj };
                combo.DataBindItems();
            }
        }

        protected void comboPerson_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<Person> collection = new XPCollection<Person>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                CriteriaOperator.Or(
                //find code contains the filter
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                //find name contains the filter
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                )
            );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();
        }
    }
}