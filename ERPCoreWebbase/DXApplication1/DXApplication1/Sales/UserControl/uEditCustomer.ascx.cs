using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
//using DAL.Sale;
//using BLL.BO.Sale;
using DevExpress.Web.ASPxHtmlEditor;
//using BLL.SalesBLO;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Utility;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Nomenclature.Supplier;
using NAS.DAL.CMS.ObjectDocument;

namespace WebModule.GUI.usercontrol
{
    public partial class uEditCustomer : System.Web.UI.UserControl
    {

        //----Duc.Vo 03/11/2013 INS -START
        private List<Guid> selectedObjectTypeId;
        private SupplierBO supplierBO = new SupplierBO();

        private bool IsSelectedLbType()
        {
            // Load listbox phân loại
            foreach (ListEditItem l in lbCustomerType.Items)
            {
                if (l.Selected)
                {
                    return true;
                }
            }
            return false;
        }

        private void loadLbCustomerType()
        {
            foreach (ListEditItem l in lbCustomerType.Items)
            {
                l.Selected = false;
            }
            // Load listbox phân loại
            foreach (ListEditItem l in lbCustomerType.Items)
            {
                Guid key = Guid.Parse(l.GetValue("TradingCategoryId").ToString());
                foreach (OrganizationCategory o in CurrentCustomerOrg.OrganizationCategories)
                {
                    if (o.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE && key == o.TradingCategoryId.TradingCategoryId)
                    {
                        l.Selected = true;
                        break;
                    }
                }
            }
        }

        private void initLbCustomerTypeForAdding()
        {
            foreach (ListEditItem l in lbCustomerType.Items)
            {
                l.Selected = true;
            }
        }
        //----Duc.Vo 03/11/2013 INS -END

        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.CustomerOrgId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["ERPCore_Sale_UserControl_uEditCustomer"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["ERPCore_Sale_UserControl_uEditCustomer"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("ERPCore_Sale_UserControl_uEditCustomer");
            }

            /////Declares all session properties here
            public Guid CustomerOrgId { get; set; }

        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsCustomer.Session = session;
            ObjectCustomerTypeLbXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                pagCustomer.ActiveTabIndex = 0;
            }
            frmCustomer.DataBind();

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmCustomer);
            pagCustomer.ActiveTabIndex = 0;
            cbRowStatus.SelectedIndex = 0;
            txtCode.IsValid = true;
            txtName.IsValid = true;
        }

        private Organization CurrentCustomerOrg
        {
            get
            {
                if (PrivateSession.Instance.CustomerOrgId == Guid.Empty)
                {
                    return null;
                }
                return
                    session.GetObjectByKey<Organization>(PrivateSession.Instance.CustomerOrgId);
            }
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String supplierCode = e.Value.ToString().Trim();
            //New mode
            if (PrivateSession.Instance.CustomerOrgId == Guid.Empty)
            {
                bool isExist = Util.isExistXpoObject<CustomerOrg>("Code", supplierCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExist)
                {
                    e.IsValid = false;
                    e.ErrorText =
                        String.Format("Mã khách hàng '{0}' đã được sử dụng", supplierCode);
                }
                else
                {
                    e.IsValid = true;
                    e.ErrorText = String.Empty;
                }
            }
            //Edit mode  
            else
            {
                //Validate if new code not equal old code
                if (!supplierCode.Equals(CurrentCustomerOrg.Code))
                {
                    bool isExist = Util.isExistXpoObject<CustomerOrg>("Code", supplierCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                    if (isExist)
                    {
                        e.IsValid = false;
                        e.ErrorText = String.Format("Mã khách hàng '{0}' đã được sử dụng", supplierCode);
                    }
                    else
                    {
                        e.IsValid = true;
                        e.ErrorText = String.Empty;
                    }
                }
            }
        }

        protected void popCustomerEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    CustomerOrg tempCustomerOrg = CustomerOrg.InitNewRow(session);
                    PrivateSession.Instance.CustomerOrgId = tempCustomerOrg.OrganizationId;
                    dsCustomer.CriteriaParameters["CustomerOrgId"].DefaultValue = PrivateSession.Instance.CustomerOrgId.ToString();
                    ClearForm();
                    initLbCustomerTypeForAdding();

                    #region customer

                    session.BeginTransaction();
                    try
                    {
                        ObjectType objectType_cus =
                                   ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);

                        NAS.BO.CMS.ObjectDocument.ObjectBO objectBO_cus = new NAS.BO.CMS.ObjectDocument.ObjectBO();
                        NAS.DAL.CMS.ObjectDocument.Object cmsObject_cus =
                            objectBO_cus.CreateCMSObject(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);

                        // OrganizationObject
                        OrganizationObject organizatoinObject_cus = new OrganizationObject(session)
                        {
                            ObjectId = cmsObject_cus,
                            OrganizationId = tempCustomerOrg
                        };
                        organizatoinObject_cus.Save();

                        // OrganizationCustomType
                        OrganizationCustomType organizationCustomType_cus = new OrganizationCustomType(session)
                        {
                            ObjectTypeId = objectType_cus,
                            OrganizationId = tempCustomerOrg
                        };
                        organizationCustomType_cus.Save();
                        session.CommitTransaction();
                    }
                    catch (Exception)
                    {
                        session.RollbackTransaction();
                        throw;
                    }

                    
                    if (lbCustomerType.Items[0].Selected == true)
                    {
                        xCallbackPanel_customer.Visible = true;

                        ObjectType objectType_cus1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);
                        OrganizationObject organizationObject_sup = tempCustomerOrg.OrganizationObjects
                            .Where(r => r.ObjectId.ObjectTypeId == objectType_cus1).FirstOrDefault();
                        grid_customer.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                        grid_customer.DataBind();
                        //grid_customer.CMSObjectId = cmsObject_cus.ObjectId;
                        //grid_customer.DataBind();
                    }
                    else
                    {
                        xCallbackPanel_customer.Visible = false;
                    }

                    #endregion

                    #region supplier

                    session.BeginTransaction();
                    try
                    {
                        ObjectType objectType_sup =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);

                        NAS.BO.CMS.ObjectDocument.ObjectBO objectBO_sup = new NAS.BO.CMS.ObjectDocument.ObjectBO();
                        NAS.DAL.CMS.ObjectDocument.Object cmsObject_sup =
                            objectBO_sup.CreateCMSObject(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);

                        // OrganizationObject
                        OrganizationObject organizatoinObject_sup = new OrganizationObject(session)
                        {
                            ObjectId = cmsObject_sup,
                            OrganizationId = tempCustomerOrg
                        };
                        organizatoinObject_sup.Save();

                        // OrganizationCustomType
                        OrganizationCustomType organizationCustomType_sup = new OrganizationCustomType(session)
                        {
                            ObjectTypeId = objectType_sup,
                            OrganizationId = tempCustomerOrg
                        };
                        organizationCustomType_sup.Save();
                        session.CommitTransaction();
                    }
                    catch (Exception)
                    {
                        session.RollbackTransaction();
                        throw;
                    }
                    

                    if (lbCustomerType.Items[1].Selected == true)
                    {
                        xCallbackPanel_supplier.Visible = true;

                        ObjectType objectType_sup1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);
                        OrganizationObject organizationObject_sup = tempCustomerOrg.OrganizationObjects
                            .Where(r => r.ObjectId.ObjectTypeId == objectType_sup1).FirstOrDefault();
                        grid_supplier.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                        grid_supplier.DataBind();
                        //grid_supplier.CMSObjectId = cmsObject_sup.ObjectId;
                        //grid_supplier.DataBind();
                    }
                    else
                    {
                        xCallbackPanel_supplier.Visible = false;
                    }
                    #endregion

                    break;
                case "edit":
                    ClearForm();
                    if (args.Length > 1)
                    {
                        PrivateSession.Instance.CustomerOrgId = Guid.Parse(args[1]);
                        dsCustomer.CriteriaParameters["CustomerOrgId"].DefaultValue = PrivateSession.Instance.CustomerOrgId.ToString();
                        txtCode.Text = CurrentCustomerOrg.Code;
                        loadLbCustomerType();

                        if (CurrentCustomerOrg.OrganizationObjects.FirstOrDefault() == null)
                        {
                            #region customer
                            session.BeginTransaction();
                            try
                            {
                                ObjectType objectType_cus1 =
                                   ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);

                                NAS.BO.CMS.ObjectDocument.ObjectBO objectBO_cus1 = new NAS.BO.CMS.ObjectDocument.ObjectBO();
                                NAS.DAL.CMS.ObjectDocument.Object cmsObject_cus1 =
                                    objectBO_cus1.CreateCMSObject(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);

                                // OrganizationObject
                                OrganizationObject organizatoinObject_cus1 = new OrganizationObject(session)
                                {
                                    ObjectId = cmsObject_cus1,
                                    OrganizationId = CurrentCustomerOrg
                                };
                                organizatoinObject_cus1.Save();

                                // OrganizationCustomType
                                OrganizationCustomType organizationCustomType_cus1 = new OrganizationCustomType(session)
                                {
                                    ObjectTypeId = objectType_cus1,
                                    OrganizationId = CurrentCustomerOrg
                                };
                                organizationCustomType_cus1.Save();
                                session.CommitTransaction();
                            }
                            catch (Exception)
                            {
                                session.RollbackTransaction();
                                throw;
                            }
                            

                            if (lbCustomerType.Items[0].Selected == true)
                            {
                                xCallbackPanel_customer.Visible = true;

                                ObjectType objectType_cus1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);
                                OrganizationObject organizationObject_sup = CurrentCustomerOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_cus1).FirstOrDefault();
                                grid_customer.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_customer.DataBind();
                                //grid_customer.CMSObjectId = cmsObject_cus1.ObjectId;
                                //grid_customer.DataBind();
                            }
                            else
                            {
                                xCallbackPanel_customer.Visible = false;
                            }
                            #endregion

                            #region supplier

                            session.BeginTransaction();
                            try
                            {
                                ObjectType objectType_sup1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);

                                NAS.BO.CMS.ObjectDocument.ObjectBO objectBO_sup1 = new NAS.BO.CMS.ObjectDocument.ObjectBO();
                                NAS.DAL.CMS.ObjectDocument.Object cmsObject_sup1 =
                                    objectBO_sup1.CreateCMSObject(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);

                                // OrganizationObject
                                OrganizationObject organizatoinObject_sup1 = new OrganizationObject(session)
                                {
                                    ObjectId = cmsObject_sup1,
                                    OrganizationId = CurrentCustomerOrg
                                };
                                organizatoinObject_sup1.Save();

                                // OrganizationCustomType
                                OrganizationCustomType organizationCustomType_sup1 = new OrganizationCustomType(session)
                                {
                                    ObjectTypeId = objectType_sup1,
                                    OrganizationId = CurrentCustomerOrg
                                };
                                organizationCustomType_sup1.Save();
                                session.CommitTransaction();
                            }
                            catch (Exception)
                            {
                                session.RollbackTransaction();
                                throw;
                            }
                            

                            if (lbCustomerType.Items[1].Selected == true)
                            {
                                xCallbackPanel_supplier.Visible = true;

                                ObjectType objectType_sup1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);
                                OrganizationObject organizationObject_sup = CurrentCustomerOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_sup1).FirstOrDefault();
                                grid_supplier.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_supplier.DataBind();
                                //grid_supplier.CMSObjectId = cmsObject_sup1.ObjectId;
                                //grid_supplier.DataBind();
                            }
                            else
                            {
                                xCallbackPanel_supplier.Visible = false;
                            }
                            #endregion
                        }

                        else
                        {
                            #region load customer data
                            if (lbCustomerType.Items[0].Selected == true)
                            {
                                xCallbackPanel_customer.Visible = true;
                                ObjectType objectType_cus1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);
                                OrganizationObject organizationObject_sup = CurrentCustomerOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_cus1).FirstOrDefault();
                                grid_customer.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_customer.DataBind();
                            }
                            else
                            {
                                xCallbackPanel_customer.Visible = false;
                            }
                            #endregion

                            #region load supplier data
                            if (lbCustomerType.Items[1].Selected == true)
                            {
                                xCallbackPanel_supplier.Visible = true;
                                ObjectType objectType_sup1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);
                                OrganizationObject organizationObject_sup = CurrentCustomerOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_sup1).FirstOrDefault();
                                grid_supplier.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_supplier.DataBind();
                            }
                            else
                            {
                                xCallbackPanel_supplier.Visible = false;
                            }
                            #endregion
                        }
                    }
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        session.BeginTransaction();
                        //Check validation
                        if (!IsSelectedLbType())
                            throw new Exception(String.Format("Phải chọn ít nhất một phân loại cho khách hàng"));

                        if (!ASPxEdit.AreEditorsValid(pagCustomer, true))
                        {
                            popCustomerEdit.JSProperties.Add("cpInvalid", true);
                            pagCustomer.ActiveTabIndex = 0;
                            return;
                        }
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            //Update general information
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            Organization editCustomerOrg =
                                session.GetObjectByKey<Organization>(PrivateSession.Instance.CustomerOrgId);
                            editCustomerOrg.Code = txtCode.Text;
                            editCustomerOrg.Name = txtName.Text;
                            editCustomerOrg.TaxNumber = txtTaxNumber.Text;
                            // Duc.Vo 10/09/2013 INS-START 
                            editCustomerOrg.Address = txtAddress.Text;
                            // Duc.Vo 10/09/2013 INS-START 
                            editCustomerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //----Duc.Vo 03/11/2013 INS -START
                            selectedObjectTypeId = new List<Guid>();
                            foreach (ListEditItem l in lbCustomerType.Items)
                            {
                                if (l.Selected)
                                    selectedObjectTypeId.Add(Guid.Parse(l.Value.ToString()));
                            }

                            if (editCustomerOrg is SupplierOrg)
                                supplierBO.updateTradingCategoriesForObject<SupplierOrg>(session, recordId, selectedObjectTypeId);
                            else
                                supplierBO.updateTradingCategoriesForObject<CustomerOrg>(session, recordId, selectedObjectTypeId);
                            //----Duc.Vo 03/11/2013 INS -END

                            editCustomerOrg.Save();
                            session.CommitTransaction();
                        }
                        else
                        {
                            //Insert mode
                            CustomerOrg newCustomerOrg =
                                session.GetObjectByKey<CustomerOrg>(PrivateSession.Instance.CustomerOrgId);
                            newCustomerOrg.Code = txtCode.Text;
                            newCustomerOrg.Name = txtName.Text;
                            newCustomerOrg.TaxNumber = txtTaxNumber.Text;
                            // Duc.Vo 10/09/2013 INS-START 
                            newCustomerOrg.Address = txtAddress.Text;
                            // Duc.Vo 10/09/2013 INS-START 
                            newCustomerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            newCustomerOrg.Save();
                            //----Duc.Vo 03/11/2013 INS -START
                            selectedObjectTypeId = new List<Guid>();
                            foreach (ListEditItem l in lbCustomerType.Items)
                            {
                                if (l.Selected)
                                    selectedObjectTypeId.Add(Guid.Parse(l.Value.ToString()));
                            }
                            supplierBO.updateTradingCategoriesForObject<CustomerOrg>(session, newCustomerOrg.OrganizationId, selectedObjectTypeId);
                            //----Duc.Vo 03/11/2013 INS -END
                            session.CommitTransaction();
                        }
                    }
                    catch (Exception ex)
                    {
                        session.RollbackTransaction();
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        popCustomerEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}