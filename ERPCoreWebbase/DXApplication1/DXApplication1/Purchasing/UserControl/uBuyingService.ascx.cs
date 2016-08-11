using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxEditors;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxHtmlEditor;
//using BLL.PurchasingBLO;
using DevExpress.Web.ASPxGridView;
using Utility;
//using DAL.Purchasing;

namespace DXApplication1.Purchasing.UserControl
{
    public partial class uBuyingService : System.Web.UI.UserControl
    {

        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.BuyingServiceId = Guid.Empty;
                //this.BuyingServiceBuyingServiceCategoryEntities =
                //                               new List<BuyingServiceBuyingServiceCategoryEntity>();

                //this.BuyingServiceEquivalenceEntities =
                //                                new List<BuyingServiceEquivalenceEntity>();

                //this.BuyingServiceSupplierEntities =
                //                                new List<BuyingServiceSupplierEntity>();
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["DXApplication1_Purchasing_UserControl_uBuyingService"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["DXApplication1_Purchasing_UserControl_uBuyingService"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("DXApplication1_Purchasing_UserControl_uBuyingService");
            }

            /////Declares all session properties here
            public Guid BuyingServiceId { get; set; }

            //public List<BuyingServiceBuyingServiceCategoryEntity>
            //                BuyingServiceBuyingServiceCategoryEntities { get; set; }

            //public List<BuyingServiceSupplierEntity>
            //                BuyingServiceSupplierEntities { get; set; }

            //public List<BuyingServiceEquivalenceEntity>
            //                BuyingServiceEquivalenceEntities { get; set; }

        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelpers.GetNewSession();
            //dsBuyingServiceProperty.Session = session;
            //dsBuyingServiceCategoryProperty.Session = session;
            //dsBuyingServiceEquivalence.Session = session;
            //dsSupplierProperty.Session = session;
        }

        //private BuyingServiceBLO buyingServiceBLO;

        protected void Page_Load(object sender, EventArgs e)
        {

            //buyingServiceBLO = new BuyingServiceBLO();

            if (!IsPostBack)
            {
                pagBuyingService.ActiveTabIndex = 0;

                //PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities =
                //                                new List<BuyingServiceBuyingServiceCategoryEntity>();

                //PrivateSession.Instance.BuyingServiceEquivalenceEntities =
                //                                new List<BuyingServiceEquivalenceEntity>();

                //PrivateSession.Instance.BuyingServiceSupplierEntities =
                //                                new List<BuyingServiceSupplierEntity>();

            }

            frmBuyingService.DataBind();

            //Set criteria default value for combobox column BuyingServiceCategoryId of grdSeviceCategory gridview
            dsBuyingServiceCategoryProperty.CriteriaParameters["Language"].DefaultValue =
                                                                    Utility.CurrentSession.Instance.Lang;

            //Set criteria default value for combobox column EquivalentBuyingServicetId of grdServiceEquivalence gridview
            dsBuyingServiceEquivalence.CriteriaParameters["Language"].DefaultValue =
                                                                    Utility.CurrentSession.Instance.Lang;
            dsBuyingServiceEquivalence.CriteriaParameters["ForBuyingServiceId"].DefaultValue =
                                                                    PrivateSession.Instance.BuyingServiceId.ToString();

            //Set criteria default value for combobox column SupplierId of grdSeviceSupplier gridview
            dsSupplierProperty.CriteriaParameters["Language"].DefaultValue =
                                                                    Utility.CurrentSession.Instance.Lang;

            //Bind data to grdSeviceCategory gridview
            //grdSeviceCategory.DataSource =
            //    PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities.Where(r => !r.DeletedFlag).ToList();
            //grdSeviceCategory.DataBind();

            //Bind data to grdSeviceSupplier gridview
            //grdSeviceSupplier.DataSource =
            //    PrivateSession.Instance.BuyingServiceSupplierEntities.Where(r => !r.DeletedFlag).ToList();
            //grdSeviceSupplier.DataBind();

            //Bind data to grdServiceEquivalence gridview
            //grdServiceEquivalence.DataSource =
            //    PrivateSession.Instance.BuyingServiceEquivalenceEntities.Where(r => !r.DeletedFlag).ToList();
            //grdServiceEquivalence.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected ASPxHtmlEditor HtmlEditorDescription
        {
            get
            {
                return (ASPxHtmlEditor)navbarDetailInfo.Groups.FindByName("Description").FindControl("htmleditorDescription");
            }
        }

        //protected BuyingServiceEntity BuyingServiceEntity
        //{
        //    get
        //    {
        //        Guid recordId = Guid.Empty;
        //        try
        //        {
        //            recordId = PrivateSession.Instance.BuyingServiceId;
        //            if (recordId == Guid.Empty) return null;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //        return buyingServiceBLO.getBuyingServiceEntity(recordId, Utility.CurrentSession.Instance.Lang);
        //    }
        //}

        private void LoadBuyingServiceBuyingServiceCategoryEntities(Guid buyingServiceId)
        {
            if (buyingServiceId == Guid.Empty)
            {
                //PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities =
                //                                new List<BuyingServiceBuyingServiceCategoryEntity>();
            }
            else
            {
                //PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities =
                //    buyingServiceBLO.getBuyingServiceBuyingServiceCategoryEntities(buyingServiceId,
                                                                                    //Utility.CurrentSession.Instance.Lang);
            }
            //grdSeviceCategory.DataSource = PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities;
            grdSeviceCategory.DataBind();            
        }

        private void LoadBuyingServiceSupplierEntities(Guid buyingServiceId)
        {
            if (buyingServiceId == Guid.Empty)
            {
                //PrivateSession.Instance.BuyingServiceSupplierEntities =
                //                                    new List<BuyingServiceSupplierEntity>();
            }
            else
            {
                //PrivateSession.Instance.BuyingServiceSupplierEntities =
                //    buyingServiceBLO.getBuyingServiceSupplierEntities(buyingServiceId,
                //                                                                    Utility.CurrentSession.Instance.Lang);
            }
            //grdSeviceSupplier.DataSource = PrivateSession.Instance.BuyingServiceSupplierEntities;
            grdSeviceSupplier.DataBind();
        }

        private void LoadBuyingServiceEquivalenceEntities(Guid buyingServiceId)
        {
            if (buyingServiceId == Guid.Empty)
            {
                //PrivateSession.Instance.BuyingServiceEquivalenceEntities =
                //                                        new List<BuyingServiceEquivalenceEntity>();
            }
            else
            {
                //PrivateSession.Instance.BuyingServiceEquivalenceEntities =
                //    buyingServiceBLO.getBuyingServiceEquivalenceEntities(buyingServiceId,
                //                                                                    Utility.CurrentSession.Instance.Lang);
            }
            //grdServiceEquivalence.DataSource = PrivateSession.Instance.BuyingServiceEquivalenceEntities;
            grdServiceEquivalence.DataBind();
        }

        protected void popBuyingService_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    PrivateSession.Instance.BuyingServiceId = Guid.Empty;
                    this.LoadBuyingServiceBuyingServiceCategoryEntities(Guid.Empty);
                    this.LoadBuyingServiceSupplierEntities(Guid.Empty);
                    this.LoadBuyingServiceEquivalenceEntities(Guid.Empty);
                    frmBuyingService.DataSourceID = null;
                    ClearForm();
                    break;
                case "edit":
                    ClearForm();
                    frmBuyingService.DataSourceID = dsBuyingServiceProperty.ID;
                    if (args.Length > 1)
                    {
                        Guid buyingServiceId = Guid.Parse(args[1]);
                        PrivateSession.Instance.BuyingServiceId = buyingServiceId;

                        this.LoadBuyingServiceBuyingServiceCategoryEntities(buyingServiceId);
                        this.LoadBuyingServiceEquivalenceEntities(buyingServiceId);
                        this.LoadBuyingServiceSupplierEntities(buyingServiceId);

                        dsBuyingServiceProperty.CriteriaParameters["BuyingServiceId"].DefaultValue = buyingServiceId.ToString();
                        dsBuyingServiceProperty.CriteriaParameters["Language"].DefaultValue = Utility.CurrentSession.Instance.Lang;
                        
                        //HtmlEditorDescription.Html = BuyingServiceEntity.Description;
                        //txtCode.Text = BuyingServiceEntity.Code;
                    }
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagBuyingService, true))
                        {
                            popBuyingService.JSProperties.Add("cpInvalid", true);
                            pagBuyingService.ActiveTabIndex = 0;
                            return;
                        }
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            //BuyingServiceEntity entity = new BuyingServiceEntity();
                            //entity.BuyingServiceId = recordId;
                            //entity.Code = txtCode.Text;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;

                            //entity.BuyingServiceBuyingServiceCategoryEntities =
                            //    PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities;

                            //entity.BuyingServiceSupplierEntities =
                            //    PrivateSession.Instance.BuyingServiceSupplierEntities;

                            //entity.BuyingServiceEquivalenceEntities =
                            //    PrivateSession.Instance.BuyingServiceEquivalenceEntities;

                            //buyingServiceBLO.Update(entity);
                        }
                        else
                        {
                            //Insert mode
                            //BuyingServiceEntity entity = new BuyingServiceEntity();
                            //entity.Code = txtCode.Text;
                            //entity.Language = Utility.CurrentSession.Instance.Lang;
                            //entity.RowStatus = char.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //entity.Name = txtName.Text;
                            //entity.Description = HtmlEditorDescription.Html;

                            //entity.BuyingServiceBuyingServiceCategoryEntities =
                            //    PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities;

                            //entity.BuyingServiceSupplierEntities =
                            //    PrivateSession.Instance.BuyingServiceSupplierEntities;

                            //entity.BuyingServiceEquivalenceEntities =
                            //    PrivateSession.Instance.BuyingServiceEquivalenceEntities;

                            //buyingServiceBLO.Insert(entity);
                        }
                    }
                    catch (Exception)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        popBuyingService.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));

                        PrivateSession.ClearInstance();
                    }
                    break;
                default:
                    break;
            }
        }

        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmBuyingService);
            pagBuyingService.ActiveTabIndex = 0;
            cbRowStatus.SelectedIndex = 0;
            HtmlEditorDescription.Html = String.Empty;
            txtCode.IsValid = true;
            txtName.IsValid = true;
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String buyingServiceCode = e.Value.ToString().Trim();
            //New mode
            if (PrivateSession.Instance.BuyingServiceId == Guid.Empty)
            {
                //if (buyingServiceBLO.isCodeExist(buyingServiceCode))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = String.Format("Mã nhà sản xuất '{0}' đã được sử dụng", buyingServiceCode);
                //}
                //else
                //{
                //    e.IsValid = true;
                //    e.ErrorText = String.Empty;
                //}
            }
            //Edit mode
            else
            {
                //Validate if new code not equal old code
                //if (!buyingServiceCode.Equals(this.BuyingServiceEntity.Code))
                //{
                //    if (buyingServiceBLO.isCodeExist(buyingServiceCode))
                //    {
                //        e.IsValid = false;
                //        e.ErrorText = String.Format("Mã nhà sản xuất '{0}' đã được sử dụng", buyingServiceCode);
                //    }
                //    else
                //    {
                //        e.IsValid = true;
                //        e.ErrorText = String.Empty;
                //    }
                //}
            }
        }


        #region grdSeviceCategory Gridview

        protected void grdSeviceCategory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                Guid buyingServiceCategoryId = (Guid)e.Keys["BuyingServiceCategoryId"];
                //BuyingServiceBuyingServiceCategoryEntity deletedObject =
                //    PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities.
                //        FirstOrDefault(r => r.BuyingServiceCategoryId == buyingServiceCategoryId
                //                            && !r.DeletedFlag);
                //if (deletedObject == null)
                //{
                //    throw new Exception("Invalid key");
                //}
                //deletedObject.DeletedFlag = true;

                //Bind data to grdSeviceCategory gridview
                //grdSeviceCategory.DataSource =
                //    PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities.Where(r => !r.DeletedFlag).ToList();
                grdSeviceCategory.DataBind();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void grdSeviceCategory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                //BuyingServiceBuyingServiceCategoryEntity newObject = new BuyingServiceBuyingServiceCategoryEntity();
                //newObject.DeletedFlag = false;
                //newObject.BuyingServiceBuyingServiceCategoryId = 0;
                //newObject.BuyingServiceCategoryId = (Guid)e.NewValues["BuyingServiceCategoryId"];
                //newObject.BuyingServiceId = PrivateSession.Instance.BuyingServiceId;
                //newObject.Code = (String)e.NewValues["Code"];
                //newObject.Name = (String)e.NewValues["Name"];
                //PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities.Add(newObject);

                //Bind data to grdSeviceCategory gridview
                //grdSeviceCategory.DataSource =
                //    PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities.Where(r => !r.DeletedFlag).ToList();
                //grdSeviceCategory.DataBind();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                grdSeviceCategory.CancelEdit();
            }
        }

        protected void grdSeviceCategory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            //New mode
            if (grid.IsNewRowEditing)
            {
                //Validate BuyingServiceCategoryId 
                if (e.NewValues["BuyingServiceCategoryId"] == null
                    || e.NewValues["BuyingServiceCategoryId"].ToString().Trim().Length == 0)
                {
                    Helpers.AddErrorToGridViewColumn(e.Errors,
                                    grid.Columns["BuyingServiceCategoryId"],
                                    "Chưa chọn nhóm dịch vụ");
                }
                else
                {

                    Guid buyingServiceCategoryId = (Guid)e.NewValues["BuyingServiceCategoryId"];
                    //int itemCount = PrivateSession.Instance.BuyingServiceBuyingServiceCategoryEntities
                    //        .Where(r => r.BuyingServiceCategoryId == buyingServiceCategoryId && !r.DeletedFlag).Count();
                    //if (itemCount > 0)
                    //{
                    //    Helpers.AddErrorToGridViewColumn(e.Errors,
                    //                grid.Columns["BuyingServiceCategoryId"],
                    //                "Nhóm dịch vụ này đã được chọn");
                    //}
                }
            }
        }

        #endregion

        #region grdSeviceSupplier Gridview

        protected void grdSeviceSupplier_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                Guid supplierId = (Guid)e.Keys["SupplierId"];
                //BuyingServiceSupplierEntity deletedObject =
                    //PrivateSession.Instance.BuyingServiceSupplierEntities.
                    //    FirstOrDefault(r => r.SupplierId == supplierId
                    //                        && !r.DeletedFlag);
                //if (deletedObject == null)
                //{
                //    throw new Exception("Invalid key");
                //}
                //deletedObject.DeletedFlag = true;

                //Bind data to grdSeviceSupplier gridview
                //grdSeviceSupplier.DataSource =
                //    PrivateSession.Instance.BuyingServiceSupplierEntities.Where(r => !r.DeletedFlag).ToList();
                //grdSeviceSupplier.DataBind();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void grdSeviceSupplier_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                //BuyingServiceSupplierEntity newObject = new BuyingServiceSupplierEntity();
                //newObject.DeletedFlag = false;
                //newObject.BuyingServiceSupplierId = 0;
                //newObject.SupplierId = (Guid)e.NewValues["SupplierId"];
                //newObject.BuyingServiceId = PrivateSession.Instance.BuyingServiceId;
                //newObject.Code = (String)e.NewValues["Code"];
                //newObject.Name = (String)e.NewValues["Name"];
                //PrivateSession.Instance.BuyingServiceSupplierEntities.Add(newObject);

                //Bind data to grdSeviceSupplier gridview
                //grdSeviceSupplier.DataSource =
                //    PrivateSession.Instance.BuyingServiceSupplierEntities.Where(r => !r.DeletedFlag).ToList();
                //grdSeviceSupplier.DataBind();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                grdSeviceSupplier.CancelEdit();
            }
        }

        protected void grdSeviceSupplier_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            //Gridview new mode
            if (grid.IsNewRowEditing)
            {
                //Validate ManufacturerCategoryId 
                if (e.NewValues["SupplierId"] == null
                    || e.NewValues["SupplierId"].ToString().Trim().Length == 0)
                {
                    Helpers.AddErrorToGridViewColumn(e.Errors,
                                    grid.Columns["SupplierId"],
                                    "Chưa chọn nhà cung cấp");
                }
                else
                {

                    Guid supplierId = (Guid)e.NewValues["SupplierId"];
                    //int itemCount = PrivateSession.Instance.BuyingServiceSupplierEntities
                    //        .Where(r => r.SupplierId == supplierId && !r.DeletedFlag).Count();
                    //if (itemCount > 0)
                    //{
                    //    Helpers.AddErrorToGridViewColumn(e.Errors,
                    //                grid.Columns["SupplierId"],
                    //                "Nhà cung cấp này đã được chọn");
                    //}
                }
            }
        }

        #endregion

        #region grdServiceEquivalence Gridview

        protected void grdServiceEquivalence_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                Guid equivalentBuyingServicetId = (Guid)e.Keys["EquivalentBuyingServicetId"];
                //BuyingServiceEquivalenceEntity deletedObject =
                //    PrivateSession.Instance.BuyingServiceEquivalenceEntities.
                //        FirstOrDefault(r => r.EquivalentBuyingServicetId == equivalentBuyingServicetId
                //                            && !r.DeletedFlag);
                //if (deletedObject == null)
                //{
                //    throw new Exception("Invalid key");
                //}
                //deletedObject.DeletedFlag = true;

                ////Bind data to grdServiceEquivalence gridview
                //grdServiceEquivalence.DataSource =
                //    PrivateSession.Instance.BuyingServiceEquivalenceEntities.Where(r => !r.DeletedFlag).ToList();
                //grdServiceEquivalence.DataBind();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void grdServiceEquivalence_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                //BuyingServiceEquivalenceEntity newObject = new BuyingServiceEquivalenceEntity();
                //newObject.DeletedFlag = false;
                //newObject.BuyingServiceEquivalenceId = Guid.Empty;
                //newObject.EquivalentBuyingServicetId = (Guid)e.NewValues["EquivalentBuyingServicetId"];
                //newObject.BuyingServiceId = PrivateSession.Instance.BuyingServiceId;
                //newObject.BuyingServiceName = (String)e.NewValues["BuyingServiceName"];
                //newObject.Description = (String)e.NewValues["Description"];
                //newObject.Language = Utility.CurrentSession.Instance.Lang;
                //PrivateSession.Instance.BuyingServiceEquivalenceEntities.Add(newObject);

                ////Bind data to grdServiceEquivalence gridview
                //grdServiceEquivalence.DataSource =
                //    PrivateSession.Instance.BuyingServiceEquivalenceEntities.Where(r => !r.DeletedFlag).ToList();
                //grdServiceEquivalence.DataBind();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                grdServiceEquivalence.CancelEdit();
            }
        }

        protected void grdServiceEquivalence_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                Guid equivalentBuyingServicetId = (Guid)e.OldValues["EquivalentBuyingServicetId"];
                //BuyingServiceEquivalenceEntity editObject =
                //   PrivateSession.Instance.BuyingServiceEquivalenceEntities.
                //      FirstOrDefault(r => r.EquivalentBuyingServicetId == equivalentBuyingServicetId && !r.DeletedFlag);
                //editObject.EquivalentBuyingServicetId = (Guid)e.NewValues["EquivalentBuyingServicetId"];
                //editObject.BuyingServiceName = (String)e.NewValues["BuyingServiceName"];
                //editObject.Description = (String)e.NewValues["Description"];

                //Bind data to grdServiceEquivalence gridview
                //grdServiceEquivalence.DataSource =
                //    PrivateSession.Instance.BuyingServiceEquivalenceEntities.Where(r => !r.DeletedFlag).ToList();
                grdServiceEquivalence.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                grdServiceEquivalence.CancelEdit();
            }
            
        }

        protected void grdServiceEquivalence_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            //Gridview new mode
            if (grid.IsNewRowEditing)
            {
                //Validate EquivalentBuyingServicetId 
                Guid equivalentBuyingServicetId = (Guid)e.NewValues["EquivalentBuyingServicetId"];
                //int itemCount = PrivateSession.Instance.BuyingServiceEquivalenceEntities
                //        .Where(r => r.EquivalentBuyingServicetId == equivalentBuyingServicetId && !r.DeletedFlag).Count();
                //if (itemCount > 0)
                //{
                //    Helpers.AddErrorToGridViewColumn(e.Errors,
                //                grid.Columns["EquivalentBuyingServicetId"], "Dịch vụ này đã được chọn");
                //}
            }
            //Gridview edit mode
            else
            {
                Guid newEquivalentBuyingServicetId = (Guid)e.NewValues["EquivalentBuyingServicetId"];
                Guid oldEquivalentBuyingServicetId = (Guid)e.NewValues["EquivalentBuyingServicetId"];
                if (newEquivalentBuyingServicetId != oldEquivalentBuyingServicetId)
                {
                    //int itemCount = PrivateSession.Instance.BuyingServiceEquivalenceEntities
                    //    .Where(r => r.EquivalentBuyingServicetId == newEquivalentBuyingServicetId && !r.DeletedFlag).Count();
                    //if (itemCount > 0)
                    //{
                    //    Helpers.AddErrorToGridViewColumn(e.Errors,
                    //                grid.Columns["EquivalentBuyingServicetId"], "Dịch vụ này đã được chọn");
                    //}
                }
            }
        }

        #endregion

    }
}