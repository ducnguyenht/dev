using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxFileManager;
using System.IO;

//using DAL.Purchasing;
//using BLL.BO.Purchasing;
using DevExpress.Web.ASPxFormLayout;
using DevExpress.Web.ASPxEditors;
//using BLL.PurchasingBLO;
using DevExpress.Web.ASPxUploadControl;
using GDI = System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxGridView;
using System.Data;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using Utility;
using NAS.BO.Nomenclature.Supplier;
using System.Web.Script.Serialization;
using NAS.DAL.CMS.ObjectDocument;

namespace ERPCore.Purchasing.UserControl
{
    public partial class uSupplierEdit : System.Web.UI.UserControl
    {
        //----Duc.Vo 03/11/2013 INS -START
        private List<Guid> selectedObjectTypeId;
        private SupplierBO supplierBO = new SupplierBO();

        private bool IsSelectedLbType()
        {
            // Load listbox phân loại
            foreach (ListEditItem l in lbSupplierType.Items)
            {
                if (l.Selected)
                {
                    return true;
                }
            }
            return false;
        }
        //----Duc.Vo 03/11/2013 INS -END

        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.SupplierOrgId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["ERPCore_Purchasing_UserControl_uSupplierEdit"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["ERPCore_Purchasing_UserControl_uSupplierEdit"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("ERPCore_Purchasing_UserControl_uSupplierEdit");
            }

            /////Declares all session properties here
            public Guid SupplierOrgId { get; set; }

        }

        private Session session;

        //----Duc.Vo 03/11/2013 INS -START
        private void loadLbSupplierType()
        {
            foreach (ListEditItem l in lbSupplierType.Items)
            {
                l.Selected = false;
            }
            // Load listbox phân loại
            foreach (ListEditItem l in lbSupplierType.Items)
            {
                Guid key = Guid.Parse(l.GetValue("TradingCategoryId").ToString());
                foreach (OrganizationCategory o in CurrentSupplierOrg.OrganizationCategories)
                {
                    if (o.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE && key == o.TradingCategoryId.TradingCategoryId)
                    {
                        l.Selected = true;
                        break;
                    }
                }
            }
        }

        private void initLbSupplierTypeForAdding()
        {
            foreach (ListEditItem l in lbSupplierType.Items)
            {
                l.Selected = true;
            }
        }
        //----Duc.Vo 03/11/2013 INS -END

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsSupplier.Session = session;
            ObjectSupplierTypeLbXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagSupplier.ActiveTabIndex = 0;
            }
            frmSupplier.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmSupplier);
            pagSupplier.ActiveTabIndex = 0;
            cbRowStatus.SelectedIndex = 0;
            txtCode.IsValid = true;
            txtName.IsValid = true;
        }

        private Organization CurrentSupplierOrg
        {
            get
            {
                if (PrivateSession.Instance.SupplierOrgId == Guid.Empty)
                {
                    return null;
                }
                return
                    session.GetObjectByKey<Organization>(PrivateSession.Instance.SupplierOrgId);
            }
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String supplierCode = e.Value.ToString().Trim();
            //New mode
            if (PrivateSession.Instance.SupplierOrgId == Guid.Empty)
            {
                bool isExist = Util.isExistXpoObject<SupplierOrg>("Code", supplierCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExist)
                {
                    e.IsValid = false;
                    e.ErrorText =
                        String.Format("Mã nhà cung cấp '{0}' đã được sử dụng", supplierCode);
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
                if (!supplierCode.Equals(CurrentSupplierOrg.Code))
                {
                    bool isExist = Util.isExistXpoObject<SupplierOrg>("Code", supplierCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                    if (isExist)
                    {
                        e.IsValid = false;
                        e.ErrorText = String.Format("Mã nhà cung cấp '{0}' đã được sử dụng", supplierCode);
                    }
                    else
                    {
                        e.IsValid = true;
                        e.ErrorText = String.Empty;
                    }
                }
            }
        }

        protected void popSupplierEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":
                    SupplierOrg tempSupplierOrg = SupplierOrg.InitNewRow(session);
                    PrivateSession.Instance.SupplierOrgId = tempSupplierOrg.OrganizationId;
                    dsSupplier.CriteriaParameters["SupplierOrgId"].DefaultValue = PrivateSession.Instance.SupplierOrgId.ToString();
                    ClearForm();
                    initLbSupplierTypeForAdding();

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
                            OrganizationId = tempSupplierOrg
                        };
                        organizatoinObject_cus.Save();

                        // OrganizationCustomType
                        OrganizationCustomType organizationCustomType_cus = new OrganizationCustomType(session)
                        {
                            ObjectTypeId = objectType_cus,
                            OrganizationId = tempSupplierOrg
                        };
                        organizationCustomType_cus.Save();
                        session.CommitTransaction();
                    }
                    catch (Exception)
                    {
                        session.RollbackTransaction();
                        throw;
                    }

                    

                    if (lbSupplierType.Items[0].Selected == true)
                    {
                        xCallbackPanel_customer.Visible = true;

                        //grid_Customer.CMSObjectId = cmsObject_cus.ObjectId;
                        //grid_Customer.DataBind();
                        ObjectType objectType_cus1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);
                        OrganizationObject organizationObject_sup = tempSupplierOrg.OrganizationObjects
                            .Where(r => r.ObjectId.ObjectTypeId == objectType_cus1).FirstOrDefault();
                        grid_Customer.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                        grid_Customer.DataBind();
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
                            OrganizationId = tempSupplierOrg
                        };
                        organizatoinObject_sup.Save();

                        // OrganizationCustomType
                        OrganizationCustomType organizationCustomType_sup = new OrganizationCustomType(session)
                        {
                            ObjectTypeId = objectType_sup,
                            OrganizationId = tempSupplierOrg
                        };
                        organizationCustomType_sup.Save();
                        session.CommitTransaction();
                    }
                    catch (Exception)
                    {
                        session.RollbackTransaction();
                        throw;
                    }
                    

                    if (lbSupplierType.Items[1].Selected == true)
                    {
                        xCallbackPanel_supplier.Visible = true;

                        ObjectType objectType_sup1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);
                        OrganizationObject organizationObject_sup = tempSupplierOrg.OrganizationObjects
                            .Where(r => r.ObjectId.ObjectTypeId == objectType_sup1).FirstOrDefault();
                        grid_Supplier.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                        grid_Supplier.DataBind();
                        //grid_Supplier.CMSObjectId = cmsObject_sup.ObjectId;
                        //grid_Supplier.DataBind();
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
                        PrivateSession.Instance.SupplierOrgId = Guid.Parse(args[1]);
                        dsSupplier.CriteriaParameters["SupplierOrgId"].DefaultValue = PrivateSession.Instance.SupplierOrgId.ToString();
                        txtCode.Text = CurrentSupplierOrg.Code;
                        loadLbSupplierType();

                        // if haven't relations
                        if (CurrentSupplierOrg.OrganizationObjects.FirstOrDefault() == null)
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
                                    OrganizationId = CurrentSupplierOrg
                                };
                                organizatoinObject_cus1.Save();

                                // OrganizationCustomType
                                OrganizationCustomType organizationCustomType_cus1 = new OrganizationCustomType(session)
                                {
                                    ObjectTypeId = objectType_cus1,
                                    OrganizationId = CurrentSupplierOrg
                                };
                                organizationCustomType_cus1.Save();
                                session.CommitTransaction();
                            }
                            catch (Exception)
                            {
                                session.RollbackTransaction();
                                throw;
                            }
                            

                            if (lbSupplierType.Items[0].Selected == true)
                            {
                                xCallbackPanel_customer.Visible = true;

                                ObjectType objectType_cus1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);
                                OrganizationObject organizationObject_sup = CurrentSupplierOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_cus1).FirstOrDefault();
                                grid_Customer.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_Customer.DataBind();
                                //grid_Customer.CMSObjectId = cmsObject_cus1.ObjectId;
                                //grid_Customer.DataBind();
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
                                    OrganizationId = CurrentSupplierOrg
                                };
                                organizatoinObject_sup1.Save();

                                // OrganizationCustomType
                                OrganizationCustomType organizationCustomType_sup1 = new OrganizationCustomType(session)
                                {
                                    ObjectTypeId = objectType_sup1,
                                    OrganizationId = CurrentSupplierOrg
                                };
                                organizationCustomType_sup1.Save();
                                session.CommitTransaction();
                            }
                            catch (Exception)
                            {
                                session.RollbackTransaction();
                                throw;
                            }

                            

                            if (lbSupplierType.Items[1].Selected == true)
                            {
                                xCallbackPanel_supplier.Visible = true;

                                ObjectType objectType_sup1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);
                                OrganizationObject organizationObject_sup = CurrentSupplierOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_sup1).FirstOrDefault();
                                grid_Supplier.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_Supplier.DataBind();
                                //grid_Supplier.CMSObjectId = cmsObject_sup1.ObjectId;
                                //grid_Supplier.DataBind();
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

                            if (lbSupplierType.Items[0].Selected == true)
                            {
                                xCallbackPanel_customer.Visible = true;
                                ObjectType objectType_cus1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.CUSTOMER);
                                OrganizationObject organizationObject_sup = CurrentSupplierOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_cus1).FirstOrDefault();
                                grid_Customer.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_Customer.DataBind();
                            }
                            else
                            {
                                xCallbackPanel_customer.Visible = false;
                            }
                            #endregion

                            #region load supplier data
                            if (lbSupplierType.Items[1].Selected == true)
                            {
                                xCallbackPanel_supplier.Visible = true;
                                ObjectType objectType_sup1 =
                                    ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.SUPPLIER);
                                OrganizationObject organizationObject_sup = CurrentSupplierOrg.OrganizationObjects
                                    .Where(r => r.ObjectId.ObjectTypeId == objectType_sup1).FirstOrDefault();
                                grid_Supplier.CMSObjectId = organizationObject_sup.ObjectId.ObjectId;
                                grid_Supplier.DataBind();
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
                            throw new Exception(String.Format("Phải chọn ít nhất một phân loại cho nhà cung cấp"));

                        if (!ASPxEdit.AreEditorsValid(pagSupplier, true))
                        {
                            popSupplierEdit.JSProperties.Add("cpInvalid", true);
                            pagSupplier.ActiveTabIndex = 0;
                            return;
                        }
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            //Update general information
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            Organization editSupplierOrg =
                                session.GetObjectByKey<Organization>(PrivateSession.Instance.SupplierOrgId);
                            editSupplierOrg.Code = txtCode.Text;
                            editSupplierOrg.Name = txtName.Text;
                            editSupplierOrg.TaxNumber = txtTaxNumber.Text;
                            //----Duc.Vo 10/09/2013 INS -START
                            if (editSupplierOrg is SupplierOrg)
                            {
                                (editSupplierOrg as SupplierOrg).BankName = txtBankName.Text;
                                (editSupplierOrg as SupplierOrg).AccountNumber = txtAccountNumber.Text;
                            }
                            editSupplierOrg.Address = txtAddress.Text;
                            //----Duc.Vo 10/09/2013 INS -END
                            editSupplierOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());

                            //----Duc.Vo 03/11/2013 INS -START
                            selectedObjectTypeId = new List<Guid>();
                            foreach (ListEditItem l in lbSupplierType.Items)
                            {
                                if (l.Selected)
                                    selectedObjectTypeId.Add(Guid.Parse(l.Value.ToString()));
                            }
                            if (editSupplierOrg is SupplierOrg)
                                supplierBO.updateTradingCategoriesForObject<SupplierOrg>(session, recordId, selectedObjectTypeId);
                            else
                                supplierBO.updateTradingCategoriesForObject<CustomerOrg>(session, recordId, selectedObjectTypeId);
                            //----Duc.Vo 03/11/2013 INS -END
                            editSupplierOrg.Save();
                            session.CommitTransaction();
                        }
                        else
                        {
                            //Insert mode
                            SupplierOrg newSupplierOrg =
                                session.GetObjectByKey<SupplierOrg>(PrivateSession.Instance.SupplierOrgId);
                            newSupplierOrg.Code = txtCode.Text;
                            newSupplierOrg.Name = txtName.Text;
                            newSupplierOrg.TaxNumber = txtTaxNumber.Text;
                            //----Duc.Vo 10/09/2013 INS -START
                            newSupplierOrg.BankName = txtBankName.Text;
                            newSupplierOrg.AccountNumber = txtAccountNumber.Text;
                            newSupplierOrg.Address = txtAddress.Text;
                            //----Duc.Vo 10/09/2013 INS -END
                            newSupplierOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            //----Duc.Vo 03/11/2013 INS -START
                            selectedObjectTypeId = new List<Guid>();
                            foreach (ListEditItem l in lbSupplierType.Items)
                            {
                                if (l.Selected)
                                    selectedObjectTypeId.Add(Guid.Parse(l.Value.ToString()));
                            }
                            supplierBO.updateTradingCategoriesForObject<SupplierOrg>(session, newSupplierOrg.OrganizationId, selectedObjectTypeId);
                            //----Duc.Vo 03/11/2013 INS -END
                            newSupplierOrg.Save();
                            session.CommitTransaction();
                        }
                    }
                    catch (Exception ex)
                    {
                        session.RollbackTransaction();
                        isSuccess = false;
                    }
                    finally
                    {
                        popSupplierEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }

    }
}